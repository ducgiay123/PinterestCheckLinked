using System.Collections.Concurrent;
using Leaf.xNet;
using System.Windows.Forms;
//using LinkedinCheckerTools.View;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CheckDieLinkedinToolV3.Config;
using CheckDieLinkedinToolV3.Models;
using CheckDieLinkedinToolV3.Services;
using CheckDieLinkedinToolV3.Singleton;
using CheckDieLinkedinToolV3.Views;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using static CheckDieLinkedinToolV3.Services.ChromeDriverServices;
using static CheckDieLinkedinToolV3.Models.LinkedinAPIExecuteResult;
using OpenQA.Selenium.Interactions;
using System.Security.Policy;
using CheckDieLinkedinToolV3.Enums;
namespace CheckDieLinkedinToolV3
{
    public partial class FormMain : Form
    {
        private ProxyType _ProxyType;

        private ConcurrentQueue<string> Dataqueue = new ConcurrentQueue<string>();

        private ViewGridDataService _ViewGridDataService = null;

        private CancellationTokenSource cancellationTokenSource = null;
        private static readonly object _shutdownLock = new object();

        private List<ChromeService> _chromeServicePool = new List<ChromeService>();
        private static readonly Random random = new Random();

        private int DieC;

        private int LiveC;

        private int ErrorC;

        private int RequestedC = 0 ;

        private string SavePath;

        private List<string> ProxyPool = new List<string>();

        string urlCheckDie = "https://www.linkedin.com/login";

        private string apiLink = "";

        private int cntApiLink = 0;

        private int requestPerAPI = 0;

        private DateTime startTime;
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

            for (int i = 0; i <= Maxthreads; i++)
            {
                _ViewGridDataService.AddNewRow(i);
            }
            //if (_ProxyType == ProxyType.Socks4A) _ViewGridDataService.AddNewRow(Maxthreads);
            tmupdatecount.Start();
            Isruning(true);
            string datetimnow = DateTime.Now.ToString("dd-MM-yyyy HH.mm.ss");
            SavePath = Application.StartupPath + "\\result\\result_" + datetimnow;
            Directory.CreateDirectory(SavePath);
            
            new Thread(() =>
            {
                if(_ProxyType == ProxyType.Socks4A)
                {
                    int totalThreads = Maxthreads + 1; // Include the extra thread
                    Thread[] threads = new Thread[totalThreads];

                    for (int i = 0; i < totalThreads; i++)
                    {
                        int threadIndex = i;
                        if (cancellationTokenSource.IsCancellationRequested)
                        {
                            break;
                        }

                        if (threadIndex == Maxthreads) // Special task for the last thread
                        {
                            threads[threadIndex] = new Thread((ThreadStart)delegate
                            {
                                while (!cancellationTokenSource.IsCancellationRequested && !Dataqueue.IsEmpty)
                                {
                                    HandleCallApiProxy();

                                }
                            });
                        }
                        else
                        {
                            threads[threadIndex] = new Thread((ThreadStart)delegate
                            {
                                while (!cancellationTokenSource.IsCancellationRequested && !Dataqueue.IsEmpty)
                                {
                                    Start(threadIndex);
                                }
                            });
                        }
                        // Start the thread
                        threads[i].Start();
                    }

                    // Wait for all threads to complete
                    for (int j = 0; j < totalThreads; j++)
                    {
                        if (threads[j] != null)
                        {
                            threads[j].Join();
                        }
                    }

                }
                else
                {
                    Thread[] array = new Thread[Maxthreads];
                    for (int i = 0; i < Maxthreads; i++)
                    {
                        int threadIndex = i;
                        if (cancellationTokenSource.IsCancellationRequested)
                        {
                            break;
                        }
                        array[threadIndex] = new Thread((ThreadStart)delegate
                        {
                            while (!cancellationTokenSource.IsCancellationRequested && !Dataqueue.IsEmpty)
                            {
                                Start(threadIndex);
                            }
                        });
                        array[threadIndex].Start();
                    }

                    for (int j = 0; j < Maxthreads; j++)
                    {
                        if (array[j] != null)
                        {
                            array[j].Join();
                        }
                    }
                }
               

                Task.Delay(TimeSpan.FromSeconds(3)).Wait();
                tmupdatecount.Stop();
                Isruning(false);
                cancellationTokenSource.Cancel();
                GC.SuppressFinalize(this);
                GC.WaitForFullGCApproach();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }).Start();
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


                    // Exit the loop if no data was dequeued
                    if (listData.Count == 0)
                    {
                        break; // No more data to process
                    }

                    // Prepare the proxy and HTTP configuration
                    Data fMainDatagrid = new Data { Index = index };
                    proxy = RandomProxy();

                    HttpConfig httpConfig = new HttpConfig
                    {
                        UseProxy = false,
                        ConnectTimeOut = 15000
                    };

                    if (!string.IsNullOrEmpty(proxy) && UIConfig.UseProxy)
                    {
                        fMainDatagrid.Proxy = proxy;
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

                    _ViewGridDataService.SafeSetValueCell(index, DataGridCellName.CellProxy, httpConfig.Proxy);
                    HandleChromeService(index,proxy,listData);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Thread {index} encountered an error: {ex.Message}");
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
                 ProxyType.Socks5 : ProxyType.Socks4A ;
        }

        private void importData_BtnClick(Object sender, EventArgs e)
        {
            //this.CheckLinkedGroup.Visible = false;
            ImportForm import = new ImportForm($"Data");
            import.ShowDialog(this);
            if (import.ImportResult == DialogResult.OK && import.ImportedData.Count != 0)
            {
                //Dataqueue = new ConcurrentQueue<string>(import.ImportedData);
                foreach(var item in import.ImportedData)
                {
                    Dataqueue.Enqueue(item);
                }
                dataInfo.Text = $"{import.ImportedData.Count}";
            }
        }
        private void importProxy_BtnClick(Object sender, EventArgs e)
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
            DieC = 0;
            LiveC = 0;
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
        private void HandleChromeService(int index, string proxy , List<string> rawdata)
        {
            ChromeService chromeService = null;
            _ViewGridDataService.SafeSetValueCell(index, DataGridCellName.CellEmail, rawdata[0]);
            LinkedinAPIExecuteResult.VerifyEmailResult verifyEmailResult = new VerifyEmailResult();
            try
            {
                // Initialize the ChromeService and configure options
                chromeService = new ChromeService();
                chromeService.SetUpProxy(proxy);          // Set proxy
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

                _ViewGridDataService.SafeSetValueCell(index, DataGridCellName.CellStatus, "init chrome");
                chromeService.InitializeDriver();
                chromeService.SetSize(500, 400);
                chromeService.SetPositionByIndex(index);
                string rs = rawdata[0];
                _chromeServicePool.Add(chromeService);
                _ViewGridDataService.SafeSetValueCell(index, DataGridCellName.CellStatus, "Processing");
                // Navigate to a URL and wait for it to fully load
                chromeService.GoToURLFullyLoaded(urlCheckDie);
                Task.Delay(TimeSpan.FromSeconds(2)).Wait();
                chromeService._driver.FindElement(By.Id("username")).SendKeys(rawdata[0]);
                //Task.Delay(TimeSpan.FromSeconds(1)).Wait();
                string dummyPassword = RandomSingleton.GenerateRandomString(10);
                chromeService._driver.FindElement(By.Id("password")).SendKeys(dummyPassword);
                Task.Delay(TimeSpan.FromSeconds(1)).Wait();
                chromeService.ClickAndWaitForURLToExist(By.XPath("//button[@data-litms-control-urn='login-submit']"));
                //chromeService.WaitForUrlChange(chromeService.GetURLOfDriver(), 30);

                if (chromeService._driver.Url.Contains("https://www.linkedin.com/checkpoint/challenge"))
                {
                    verifyEmailResult.EmailStatusCode = VerifyEmailStatusCode.Die;
                    DieC++;
                }
                else if(chromeService.ElementExists(By.Id("error-for-password"))) {
                    verifyEmailResult.EmailStatusCode = VerifyEmailStatusCode.Live;
                    LiveC++;
                }
                else
                {
                    verifyEmailResult.EmailStatusCode = VerifyEmailStatusCode.Error;
                    ErrorC++;
                }
                
                SaveFile(SavePath + $"\\{verifyEmailResult.EmailStatusCode}.txt", rawdata[0]);

                Task.Delay(TimeSpan.FromSeconds(1)).Wait();
            }
            catch (Exception ex)
            {
                Dataqueue.Enqueue(rawdata[0]);
                Console.WriteLine($"An error occurred: {ex.Message}");
                Task.Delay(TimeSpan.FromSeconds(2)).Wait();
            }
            finally
            {
                if (chromeService != null)
                {
                    chromeService.ShutDownDriver();  // Shutdown the driver properly
                    _chromeServicePool.Remove(chromeService);
                }
            }
        }
        private int GenerateRandomNumber(int numberOfDigits)
        {
            if (numberOfDigits <= 0)
                throw new ArgumentException("Number of digits must be greater than 0.");

            // Calculate the range for the number of digits
            int min = (int)Math.Pow(10, numberOfDigits - 1); // Smallest number with the specified digits
            int max = (int)Math.Pow(10, numberOfDigits) - 1; // Largest number with the specified digits

            // Generate a random number in the range [min, max]
            return min + (int)(random.NextDouble() * (max - min + 1));
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
                    liveNum.Text = LiveC.ToString();
                    dieNum.Text = DieC.ToString();   
                    //lbdieC.Text = DieC.ToString();
                    //lbsuccessC.Text = SuccessC.ToString();
                    //lbfailedC.Text = FailureC.ToString();
                    //proxiesData.Text = ProxyPool.Count.ToString();
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
                    item.ShutDownDriver();
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
    }
}
