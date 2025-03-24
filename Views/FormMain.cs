using System.Collections.Concurrent;
using Leaf.xNet;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ChangeViaFBTool.Config;
using ChangeViaFBTool.Models;
using ChangeViaFBTool.Services;
using ChangeViaFBTool.Singleton;
using ChangeViaFBTool.Views;
using OpenQA.Selenium;
using static ChangeViaFBTool.Services.ChromeDriverServices;
using static OpenQA.Selenium.BiDi.Modules.Script.RemoteValue;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Text;
using System.Text.RegularExpressions;
namespace ChangeViaFBTool
{
    public partial class FormMain : Form
    {
        private ProxyType _ProxyType;

        private ConcurrentQueue<string> Dataqueue = new ConcurrentQueue<string>();

        private Dictionary<string, string> emailPasswordDict = new Dictionary<string, string>();

        private ViewGridDataService _ViewGridDataService = null;

        private CancellationTokenSource cancellationTokenSource = null;
        private static readonly object _shutdownLock = new object();

        private Dictionary<int , ChromeService> _chromeServicePool = new Dictionary<int, ChromeService>();

        private static ConcurrentDictionary<int, ChromeState> chromeStates = new ConcurrentDictionary<int, ChromeState>();
        
        private ConcurrentDictionary<int, ManualResetEventSlim> threadWaitHandles = new ConcurrentDictionary<int, ManualResetEventSlim>();

        private static readonly Random random = new Random();

        private string PasswordFB = string.Empty;

        private int FullC;

        private int TwoFAC;

        private int MailC;

        private int ErrorC;

        private int RequestedC = 0;

        private string SavePath;

        private List<string> ProxyPool = new List<string>();

        private string apiLink = "";

        private int cntApiLink = 0;

        private int requestPerAPI = 0;

        private DateTime startTime;

        private enum ChromeState
        {
            NotRunning,
            Initialize,
            WaitingForUser,
            AddMail,
            WaitingFor2FA,
            Add2FA
        }
        public FormMain()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _ViewGridDataService = new ViewGridDataService(dataGridView);
            LoadSetting();
        }

        private void LoadSetting()
        {
            numThreads.Value = UIConfig.Threads;
            cbproxy.Checked = UIConfig.UseProxy;
            proxyType.SelectedIndex = UIConfig.ProxyType;
            fbPassword.Text = UIConfig.FBPassword;
            PasswordFB = UIConfig.FBPassword;
            //numRequestSend.Value = UIConfig.RequestSendNum;
            //numSendWrongPass.Value = UIConfig.SendWrongPassTime;
            //numRequestPerIp.Value = UIConfig.RequestPerIP;
        }
        private void start_BtnClick(object sender, EventArgs e)
        {
            cancellationTokenSource = new CancellationTokenSource();
            ResetCounter();
            dataGridView.Rows.Clear();
            _ViewGridDataService.datas.Clear();
            startTime = DateTime.Now;
            int Maxthreads = (int)numThreads.Value;
            for (int i = 0; i < Maxthreads; i++)
            {
                _ViewGridDataService.AddNewRow(i);
                _ViewGridDataService.HandleButtonColumn(i);
            }
            //_ViewGridDataService.HandleButtonColumn();

            //if (_ProxyType == ProxyType.Socks4A) _ViewGridDataService.AddNewRow(Maxthreads);
            tmupdatecount.Start();
            Isruning(true);
            string datetimnow = DateTime.Now.ToString("dd-MM-yyyy HH.mm.ss");
            SavePath = Application.StartupPath + "\\result\\result_" + datetimnow;
            Directory.CreateDirectory(SavePath);

            new Thread(() =>
            {
                Thread[] threads = new Thread[Maxthreads];

                for (int i = 0; i < Maxthreads; i++)
                {
                    int threadIndex = i;
                    chromeStates[threadIndex] = ChromeState.NotRunning;

                    if (cancellationTokenSource.IsCancellationRequested)
                        break;

                    threads[threadIndex] = new Thread(() =>
                    {
                        string hotmail = string.Empty;
                        string cookie;
                        while (!cancellationTokenSource.IsCancellationRequested && !Dataqueue.IsEmpty)
                        {
                           
                            if (chromeStates[threadIndex] == ChromeState.Initialize)
                            {
                                string data = string.Empty;
                                if (Dataqueue.TryDequeue(out data))
                                {
                                    hotmail = data;
                                }
                                else
                                {
                                    break; // Stop if no more data
                                }

                                _ViewGridDataService.SafeSetValueCell(threadIndex, DataGridCellName.CellEmail, hotmail);
                                string status = "Đang mở trình duyệt. Đề Nghị log facebook!";
                                _ViewGridDataService.SafeSetValueCell(threadIndex, DataGridCellName.CellStatus, status);
                                InvokeUI(threadIndex, "Initializing...");
                                HandleChromeService(threadIndex);
                                chromeStates[threadIndex] = ChromeState.WaitingForUser;
                                InvokeUI(threadIndex, "Waiting for User...");

                                // 🚀 Make Sure We Reset Before Waiting!
                                threadWaitHandles[threadIndex].Reset();
                                threadWaitHandles[threadIndex].Wait();
                            }
                            else if (chromeStates[threadIndex] == ChromeState.AddMail)
                            {
                                string hotmail1 = hotmail;
                                cookie = GetCookieFromFacebook(_chromeServicePool[threadIndex]);
                                cookie = RemoveDuplicateCookies(cookie);
                                Match match = Regex.Match(cookie, @"c_user=(\d+)");
                                string UID = match.Success ? match.Groups[1].Value : "Not Found";
                                InvokeUI(threadIndex, "Adding Mail...");
                                AddMailToFacebook(_chromeServicePool[threadIndex], threadIndex, hotmail1);
                                chromeStates[threadIndex] = ChromeState.Add2FA;

                            }
                            else if (chromeStates[threadIndex] == ChromeState.Add2FA)
                            {
                                string proxy = string.Empty;
                                HttpConfig httpConfig = new HttpConfig
                                {
                                    UseProxy = false,
                                    ConnectTimeOut = 15000
                                };
                                proxy = RandomProxy();
                                if (!string.IsNullOrEmpty(proxy) && UIConfig.UseProxy)
                                {
                                    //fMainDatagrid.Proxy = proxy;
                                    httpConfig.UseProxy = true;

                                    if (_ProxyType == ProxyType.Socks4A)
                                    {
                                        httpConfig.Proxy = ProxyClient.Parse(ProxyType.HTTP, proxy);
                                    }
                                    else
                                    {
                                        httpConfig.Proxy = ProxyClient.Parse(_ProxyType, proxy);
                                    }
                                }
                                InvokeUI(threadIndex, "Adding 2FA...");
                                Add2FAToFacebook(_chromeServicePool[threadIndex], threadIndex, httpConfig);
                                chromeStates[threadIndex] = ChromeState.NotRunning;
                                InvokeUI(threadIndex, "Completed (Click to Restart)");
                            }

                            Thread.Sleep(100); // Prevent CPU Overload
                        }
                    });

                    threads[threadIndex].Start();
                }

            }).Start();

        }
        private void InvokeUI(int rowIndex, string message)
        {
            dataGridView.Invoke(new Action(() => dataGridView.Rows[rowIndex].Cells["buttonCol"].Value = message));
        }
        private void HandleCallApiProxy()
        {

            HttpConfig httpConfig = new HttpConfig
            {
                UseProxy = false
            };
            HttpRequest httpRequest = HttpFactory.NewClient(httpConfig);
            httpRequest.UserAgent = Http.RandomUserAgent();
            HttpResponse httpResponse = null;
            try
            {
                httpResponse = httpRequest.Get(apiLink);
                string payload = httpRequest.ToString();
                if (payload.Contains("Please request again in 2 seconds"))
                {
                    Task.Delay(TimeSpan.FromSeconds(2)).Wait();
                    return;
                }
                string[] proxies = httpResponse.ToString().Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                //ProxyPool.Clear();
                ProxyPool.AddRange(proxies);
                cntApiLink++;
                if (cntApiLink % 4 == 3)
                {
                    if (ProxyPool.Count > 500)
                    {
                        ProxyPool.RemoveRange(0, 500);
                    }
                }
                Task.Delay(TimeSpan.FromSeconds(1)).Wait();
            }
            catch
            {
                Task.Delay(TimeSpan.FromSeconds(2)).Wait();
            }

        }
        private void SaveRemainData()
        {
            try
            {
                // Ensure that Dataqueue is not null and contains data to save
                if (Dataqueue != null && Dataqueue.Count > 0)
                {
                    // Convert the data from the queue to a list of strings
                    List<string> remainingData = new List<string>();

                    // Iterate through the queue (using TryDequeue to safely pull items)
                    while (Dataqueue.TryDequeue(out string data))
                    {
                        remainingData.Add(data); // Add to the list
                    }

                    // Save the remaining data to a file
                    string filePath = Path.Combine(SavePath, "RemainData.txt");
                    File.WriteAllText(filePath, string.Join(Environment.NewLine, remainingData));

                    Console.WriteLine("Remaining data saved successfully.");
                }
                else
                {
                    Console.WriteLine("No data in queue to save.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to save remaining data: {ex.Message}");
            }
        }

        private void Start(int index)
        {
            string data = string.Empty;
            string proxy = string.Empty;

            try
            {
                // Continue processing as long as cancellation isn't requested
                while (!cancellationTokenSource.IsCancellationRequested && !Dataqueue.IsEmpty)
                {

                    List<string> listData = new List<string>();

                    for (int i = 0; i < 1; i++)
                    {
                        if (Dataqueue.TryDequeue(out data))
                        {
                            listData.Add(data);
                        }
                        else
                        {
                            break; // Stop if no more data to dequeue
                        }
                    }


                    if (listData.Count == 0)
                    {
                        break; // No more data to process
                    }

      

                    //_ViewGridDataService.SafeSetValueCell(index, DataGridCellName.CellProxy, httpConfig.Proxy);
                    //HandleChromeService(index, proxy, listData, httpConfig);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Thread {index} encountered an error: {ex.Message}");
            }
        }

        private void dataGridButton_Click(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView.Columns["buttonCol"].Index && e.RowIndex >= 0)
            {
                int threadIndex = e.RowIndex;

                if (!chromeStates.ContainsKey(threadIndex))
                {
                    chromeStates[threadIndex] = ChromeState.NotRunning;
                }
                if (!threadWaitHandles.ContainsKey(threadIndex))
                {
                    threadWaitHandles[threadIndex] = new ManualResetEventSlim(false);
                }

                switch (chromeStates[threadIndex])
                {
                    case ChromeState.NotRunning:
                        chromeStates[threadIndex] = ChromeState.Initialize;
                        break;

                    case ChromeState.WaitingForUser:
                        chromeStates[threadIndex] = ChromeState.AddMail;
                        threadWaitHandles[threadIndex].Set(); // 
                        threadWaitHandles[threadIndex] = new ManualResetEventSlim(false); // Reset for future waits
                        break;
                    case ChromeState.WaitingFor2FA:
                        chromeStates[threadIndex] = ChromeState.Add2FA;
                        threadWaitHandles[threadIndex].Set(); // 
                        threadWaitHandles[threadIndex] = new ManualResetEventSlim(false); // Reset for future waits
                        break;

                }
            }
        }


        private void stop_BtnClick(object sender, EventArgs e)
        {
            cancellationTokenSource?.Cancel();
        }

        private void saving_BtnClick(object sender, EventArgs e)
        {
            try
            {
                string pathopen = Directory.Exists(SavePath) ? SavePath : Environment.CurrentDirectory;

                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = pathopen,
                    UseShellExecute = true,
                    Verb = "open"
                };

                Process.Start(startInfo);
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., if the directory doesn't exist or access is denied)
                MessageBox.Show($"An error occurred while trying to open the path: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public static string RemoveDuplicateCookies(string cookieString)
        {
            // Split cookies by "; " to get individual key-value pairs
            string[] cookies = cookieString.Split(new[] { "; " }, StringSplitOptions.RemoveEmptyEntries);

            // Dictionary to store unique cookies (latest value wins)
            Dictionary<string, string> uniqueCookies = new Dictionary<string, string>();

            foreach (string cookie in cookies)
            {
                string[] parts = cookie.Split(new[] { '=' }, 2); // Split only on first '=' to keep values intact
                if (parts.Length == 2)
                {
                    uniqueCookies[parts[0]] = parts[1]; // Overwrite duplicates with the latest value
                }
            }

            // Reconstruct cleaned cookie string
            return string.Join("; ", uniqueCookies.Select(kv => $"{kv.Key}={kv.Value}"));
        }
        private void fbPassword_TextChanged(object sender, EventArgs e)
        {
            UIConfig.FBPassword = fbPassword.Text;
            PasswordFB  = fbPassword.Text;
           
        }
        private void proxyChecked_ValueChanged(object sender, EventArgs e)
        {
            UIConfig.UseProxy = cbproxy.Checked;
        }
        private void numthreads_ValueChanged(object sender, EventArgs e)
        {
            UIConfig.Threads = (int)numThreads.Value;
        }

        private void proxyType_SelectedIndexChanged(object sender, EventArgs e)
        {
            UIConfig.ProxyType = proxyType.SelectedIndex;
            _ProxyType = proxyType.SelectedIndex == 0 ? ProxyType.HTTP
                : proxyType.SelectedIndex == 1 ? ProxyType.Socks4 : proxyType.SelectedIndex == 2 ?
                 ProxyType.Socks5 : ProxyType.Socks4A;
        }

        private void importData_BtnClick(object sender, EventArgs e)
        {
            //this.CheckLinkedGroup.Visible = false;
            ImportForm import = new ImportForm($"Data");
            import.ShowDialog(this);
            if (import.ImportResult == DialogResult.OK && import.ImportedData.Count != 0)
            {
                //Dataqueue = new ConcurrentQueue<string>(import.ImportedData);
                foreach (var item in import.ImportedData)
                {
                    Dataqueue.Enqueue(item);
                }
                dataInfo.Text = $"{import.ImportedData.Count}";
            }
            //emailPasswordDict = IniFile.ConvertQueueToDictionary(Dataqueue);
        }
        private void importProxy_BtnClick(object sender, EventArgs e)
        {
            ImportForm import = new ImportForm($"Proxies");
            import.ShowDialog(this);
            if (import.ImportResult == DialogResult.OK && import.ImportedData.Count != 0)
            {
                if (_ProxyType == ProxyType.Socks4A)
                {
                    apiLink = import.ImportedData[0];
                    ProxyPool.Clear();
                    HandleCallApiProxy();
                    proxiesData.Text = $"{ProxyPool.Count}";
                }
                else
                {
                    ProxyPool = new List<string>(import.ImportedData);
                    proxiesData.Text = $"{import.ImportedData.Count}";
                }
            }
        }
        //private void rdCheckLinked_CheckedChanged(object sender, EventArgs e)
        //{
        //    UIConfig.TaskType = 1;
        //    //LinkedOrSuccess.Text = "Linked:";
        //    //NotLinkedOrFailed.Text = "NotLinked:";
        //}

        private string RandomProxy()
        {
            return ProxyPool.RandomItemInList();
        }
        private void ResetCounter()
        {
            TwoFAC = 0;
            MailC = 0;
            FullC = 0;
            ErrorC = 0;
            RequestedC = 0;
        }
        private void Isruning(bool status)
        {
            this.Invoke((MethodInvoker)delegate ()
            {
                btnstart.Enabled = !status;
                btnstop.Enabled = status;
            });
        }
        private void HandleChromeService(int index)
        {

            ChromeService chromeService = null;
            APIServices aPIServices = new APIServices();
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string cookieExtentionPath = Path.Combine(basePath, "config", "CookieEditor.crx");
            string proxyExtentionPath = Path.Combine(basePath, "config", "ProxyHelper.crx");
            APIOptions.APIEmailResult verifyEmailResult = new APIOptions.APIEmailResult();

            try
            {
                chromeService = new ChromeService();
                //chromeService.SetUpProxy(proxy);          // Set proxy
                chromeService.AddOption("--start-maximized"); // Example: Add an option to start maximized
                chromeService.AddOption("--disable-popup-blocking"); // Disable popups
                //chromeService.AddOption("--headless");  // Enable headless mode
                //chromeService.AddOption("--disable-gpu"); // Disable GPU (useful for headless mode)
                chromeService.AddOption("--no-sandbox");
                //chromeService.AddOption("--window-size=1080x960");
                chromeService.AddOption("--disable-blink-features=AutomationControlled");
                chromeService.AddOption("--primary_language=en-US");
                chromeService.AddOption("--languages=en-US,en");
                chromeService.AddOption("--accept_language=en-US,en;q=0.9");
                chromeService.AddOption("--dnt=0");
                chromeService.AddOption("--flag-switches-begin");
                chromeService.AddOption("--flag-switches-end");
                chromeService.AddOption("--enable-experimental-web-platform-features");
                //chromeOptions.AddArgument("--font-masking-mode=" + Utils.AppUtils.RandomNumber(2, 6).ToString());
                chromeService.AddOption("--use-fake-device-for-media-stream");
                chromeService.AddOption("--disable-features=ExtensionsToolbarMenu,ChromeLabs,ReadLater");
                chromeService.AddOption("--ignore-certificate-errors");
                chromeService.AddOption("--disable-notifications");
                chromeService.AddOption("--disable-save-password-bubble");
                chromeService.AddExtension(cookieExtentionPath);
                chromeService.AddExtension(proxyExtentionPath);

                chromeService.InitializeDriver();
                chromeService.SetPositionByIndex(index);
                _chromeServicePool.Add(index, chromeService);
                Task.Delay(TimeSpan.FromSeconds(2)).Wait();

            }
            catch (Exception ex) { 
            
            }

        }

        private string GetCookieFromFacebook(ChromeService chromeService)
        {
            string cookieURL = "https://www.facebook.com/";
            chromeService.OpenNewTab();
            chromeService.GoToURLFullyLoaded(cookieURL);
            Task.Delay(TimeSpan.FromSeconds(1)).Wait();
            var cookies = chromeService._driver.Manage().Cookies.AllCookies;
            StringBuilder cookieString = new StringBuilder();
            foreach (var item in cookies)
            {
                cookieString.Append($"{item.Name}={item.Value}; ");
            }
            return cookieString.ToString().TrimEnd();
        }
        private string AddMailToFacebook(ChromeService chromeService, int threadIndex, string data1)
        {
            string urlAddMail = "https://accountscenter.facebook.com/personal_info/contact_points/?contact_point_type=email&dialog_type=add_contact_point";
            chromeService.OpenNewTab();
            chromeService.GoToURLFullyLoaded(urlAddMail);
            string[] parts = data1.Split('|');
            string mail = parts[0];
            chromeService._driver.FindElement(By.XPath("/html/body/div[1]/div/div/div/div[3]/div/div/div[2]/div/div/div/div/div/div/div/div[2]/div[2]/div[3]/div[2]/div/div/div/div/div[1]/input")).SendKeys(mail);
            Task.Delay(TimeSpan.FromSeconds(1)).Wait();
            chromeService._driver.FindElement(By.Name("noform")).Click();
            Task.Delay(TimeSpan.FromMilliseconds(500)).Wait();
            chromeService._driver.FindElement(By.XPath("/html/body/div[1]/div/div/div/div[3]/div/div/div[2]/div/div/div/div/div/div/div/div[3]/div/div/div/div/div/div/div/div/div")).Click();
            Task.Delay(TimeSpan.FromMilliseconds(2)).Wait();

            // can not add mail at the moment
            if (chromeService.ElementExists(By.XPath("/html/body/div[7]/div[1]/div/div[2]/div/div/div/div/div/div/div[2]/div[2]/div[3]/div/div/div[1]/div/h2/span"))){

                _ViewGridDataService.SafeSetValueCell(threadIndex, DataGridCellName.CellStatus, "Lỗi code at the moment");
                return "add mail ko dc";
            }
            else if (chromeService.ElementExists(By.XPath("/html/body/div[6]/div[1]/div/div[2]/div/div/div/div/div/div/div[2]/div[2]/div[3]/div/div/div/div/div/div[1]/div/h2/span")))
            {
                _ViewGridDataService.SafeSetValueCell(threadIndex, DataGridCellName.CellStatus, "Lỗi code What's App");
                return "add mail ko dc";
            }
            // get code from hotmail
            else if (chromeService.ElementExists(By.XPath("/html/body/div[1]/div/div/div/div[3]/div/div/div[2]/div/div/div/div/div/div[2]/div/div[2]/div[2]/div[3]/div[1]/div/div/div/span")))
            {
                _ViewGridDataService.SafeSetValueCell(threadIndex, DataGridCellName.CellStatus, "Đợi Code từ hotmail");
                chromeService.WaitForElementExists(By.XPath("/html/body/div[1]/div/div/div/div[3]/div/div/div[2]/div/div/div/div/div/div[3]/div/div[2]/div[2]/div[3]/div/div/div[1]/div/h2/span"));
                Task.Delay(TimeSpan.FromMilliseconds(2)).Wait();
                return mail;
            }

            //chromeService._driver.FindElement(By.XPath("//*[@id=\"facebook\"]/body/div[7]/div[1]/div/div[2]/div/div/div/div/div/div/div[2]/div[2]/div[3]/div/div/div/div/div/div[2]/div/div[2]/div/div/div")).Click();
            Task.Delay(TimeSpan.FromSeconds(5)).Wait();

            return string.Empty;


        }

        private string Add2FAToFacebook(ChromeService chromeService, int threadIndex , HttpConfig httpConfig) 
        {
            APIServices aPIServices = new APIServices();

            string urlAdd2FA = "https://accountscenter.facebook.com/password_and_security/two_factor";
            chromeService.OpenNewTab();
            chromeService.GoToURLFullyLoaded(urlAdd2FA);
            Task.Delay(TimeSpan.FromSeconds(1)).Wait();
            chromeService._driver.FindElement(By.XPath("/html/body/div[1]/div/div/div/div[3]/div/div/div[2]/div/div/div/div/div/div/div/div[2]/div[2]/div[3]/div/div/div[2]/div/div[1]/div[1]")).Click();
            Task.Delay(TimeSpan.FromSeconds(2)).Wait();
            if(chromeService.ElementExists(By.XPath("/html/body/div[7]/div[1]/div/div[2]/div/div/div/div/div/div/div[2]/div[2]/div[3]/div/div/div[1]/div/h2/span")))
            {
                _ViewGridDataService.SafeSetValueCell(threadIndex, DataGridCellName.CellStatus, "Lỗi code at the moment");
                return "add 2fa ko dc";
            }
            else if (chromeService.ElementExists(By.XPath("/html/body/div[7]/div[1]/div/div[2]/div/div/div/div/div/div/div[2]/div[2]/div[3]/div/div/div/div/div/div[1]/div/h2/span")))
            {
                IWebElement checkWhatappString = chromeService._driver.FindElement(By.XPath("/html/body/div[7]/div[1]/div/div[2]/div/div/div/div/div/div/div[2]/div[2]/div[3]/div/div/div/div/div/div[1]/div/span[2]/span/div/span/span"));
                if (checkWhatappString.Text.ToString().Contains("WhatsApp"))
                {
                    _ViewGridDataService.SafeSetValueCell(threadIndex, DataGridCellName.CellStatus, "Lỗi code What's App");
                    return "add 2fa ko dc";
                }
                _ViewGridDataService.SafeSetValueCell(threadIndex, DataGridCellName.CellStatus, "Đợi lấy code hotmail để bật 2fa.");
                chromeService.WaitForElementExists(By.XPath("/html/body/div[1]/div/div/div/div[3]/div/div/div[2]/div/div/div/div/div/div[2]/div/div[2]/div[2]/div[3]/div/div/div[2]/div/div/div[2]/div/div/label[1]/div[1]"));
                _ViewGridDataService.SafeSetValueCell(threadIndex, DataGridCellName.CellStatus, "Đang lấy 2fa");
                Task.Delay(TimeSpan.FromSeconds(1)).Wait();
                chromeService._driver.FindElement(By.XPath("/html/body/div[1]/div/div/div/div[3]/div/div/div[2]/div/div/div/div/div/div[2]/div/div[3]/div/div/div/div/div/div/div/div/div")).Click();
                Task.Delay(TimeSpan.FromSeconds(3)).Wait();

                // Get Code from 2fa
                IWebElement twoFaEle = chromeService._driver.FindElement(By.XPath("/html/body/div[1]/div/div/div/div[3]/div/div/div[2]/div/div/div/div/div/div[3]/div/div[2]/div[2]/div[3]/div/div/div[4]/div/div[2]/div/div/div[1]/span"));
                string string2FA = twoFaEle.Text;
                string2FA = string2FA.Replace(" ", "");
                APIOptions.VerifyEmailOptions verifyEmailOptions = new APIOptions.VerifyEmailOptions
                {
                    twoFaCode = string2FA,
                    HttpConfig = httpConfig
                };
                APIOptions.APIEmailResult codeResult = aPIServices.GetCodeService(verifyEmailOptions);


                Task.Delay(TimeSpan.FromSeconds(1)).Wait();
                chromeService._driver.FindElement(By.XPath("/html/body/div[1]/div/div/div/div[3]/div/div/div[2]/div/div/div/div/div/div[3]/div/div[3]/div/div/div/div/div/div/div/div/div")).Click();
                Task.Delay(TimeSpan.FromSeconds(2)).Wait();
                // send 2fa code
                chromeService._driver.FindElement(By.XPath("/html/body/div[1]/div/div/div/div[3]/div/div/div[2]/div/div/div/div/div/div[4]/div/div[2]/div[2]/div[3]/div/div/div[2]/div/div/div[1]/input")).SendKeys(string2FA);
                Task.Delay(TimeSpan.FromSeconds(1)).Wait();
                chromeService._driver.FindElement(By.XPath("/html/body/div[1]/div/div/div/div[3]/div/div/div[2]/div/div/div/div/div/div[4]/div/div[3]/div/div/div/div/div/div/div/div/div")).Click();
                Task.Delay(TimeSpan.FromSeconds(2)).Wait();
                // send password
                chromeService._driver.FindElement(By.XPath("/html/body/div[8]/div[1]/div/div[2]/div/div/div/div/div/div/div[2]/div[2]/div[3]/div/div/div[3]/div/div/div/div[1]/input")).SendKeys(PasswordFB);
                Task.Delay(TimeSpan.FromSeconds(1)).Wait();
                chromeService._driver.FindElement(By.XPath("/html/body/div[8]/div[1]/div/div[2]/div/div/div/div/div/div/div[2]/div[2]/div[3]/div/div/div[4]/div/div/div/div/div")).Click();
                Task.Delay(TimeSpan.FromSeconds(1)).Wait();
                chromeService.WaitForElementExists(By.XPath("/html/body/div[1]/div/div[1]/div/div[3]/div/div/div[2]/div/div/div/div/div/div[5]/div/div[2]/div[2]/div[3]/div/div/div/div/h2/span/span"));
                Task.Delay(TimeSpan.FromSeconds(1)).Wait();
                // done
                return string2FA;
            }

            
            Task.Delay(TimeSpan.FromSeconds(10)).Wait();
            return string.Empty;
        }
        private void tmupdatecount_Tick(object sender, EventArgs e)
        {
            try
            {
                this.Invoke(new Action(() =>
                {

                    //LinkedOrSuccessInfo.Text = LinkedC.ToString();
                    //NotLinkOrFailedInfo.Text = NotLinkedC.ToString();
                    //RequestedQuantity.Text = RequestedC.ToString();
                    liveNum.Text = FullC.ToString();
                    mailNum.Text = MailC.ToString();
                    twoFANum.Text = TwoFAC.ToString();
                    TimeSpan elapsed = DateTime.Now - startTime;
                    timeLabel.Text = elapsed.ToString(@"hh\:mm\:ss");
                    errorInfo.Text = ErrorC.ToString();
                }));
            }
            catch { }
        }
        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show(this, "Are you sure you want to exit?", "Exit Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    cancellationTokenSource?.Cancel(); // Signal any background tasks to stop
                    cancellationTokenSource?.Dispose(); // Dispose of the CancellationTokenSource

                    CloseAllChrome(); // Properly close and dispose of ChromeDriver instances

                    // Use Application.Exit() for graceful shutdown
                    Environment.Exit(0);
                }
                catch (Exception ex)
                {
                    // Log the exception for debugging
                    Console.WriteLine($"Error during form closing: {ex.Message}");
                }
            }
            else
            {
                // Cancel the closing if the user chooses "No"
                e.Cancel = true;
            }
        }

        private void CloseAllChrome()
        {
            foreach (var item in _chromeServicePool)
            {
                try
                {
                    item.Value.ShutDownDriver();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error shutting down driver: {ex.Message}");
                }
            }

            _chromeServicePool.Clear();
        }

        private void SaveFile(string path, string content)
        {
            try
            {
                this.BeginInvoke(new MethodInvoker(delegate
                {
                    File.AppendAllText(path, content + Environment.NewLine);
                }));
            }
            catch { }
        }
        private void timerClock_Tick(object sender, EventArgs e)
        {
            // Update the label with the current time
            timeLabel.Text = DateTime.Now.ToString("hh:mm:ss tt");
        }
        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void labelData_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click_1(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void FailedNum_Click(object sender, EventArgs e)
        {

        }

        private void errorInfo_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


    }
}
