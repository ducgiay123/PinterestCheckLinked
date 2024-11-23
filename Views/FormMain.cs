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
using PinterestCheckLinked.Config;
using PinterestCheckLinked.Models;
using PinterestCheckLinked.Services;
using PinterestCheckLinked.Singleton;
using PinterestCheckLinked.Views;
namespace PinterestCheckLinked
{
    public partial class FormMain : Form
    {
        private ProxyType _ProxyType;

        private ConcurrentQueue<string> Dataqueue = new ConcurrentQueue<string>();

        private ViewGridDataService _ViewGridDataService = null;

        private CancellationTokenSource cancellationTokenSource = null;

        private int LinkedC;

        private int DieC;

        private int NotLinkedC;

        private int SuccessC;

        private int FailureC;

        private int RetriesC;

        private int RequestedC = 0 ;

        private string SavePath;

        private List<string> ProxyPool = new List<string>();
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
        }
        private void start_BtnClick(object sender, EventArgs e)
        {
            cancellationTokenSource = new CancellationTokenSource();
            ResetCounter();
            dataGridView.Rows.Clear();
            _ViewGridDataService.datas.Clear();
            tmupdatecount.Start();
            Isruning(true);
            string datetimnow = DateTime.Now.ToString("dd-MM-yyyy HH.mm.ss");
            SavePath = Application.StartupPath + "\\result\\result_" + datetimnow;
            Directory.CreateDirectory(SavePath);
            int Maxthreads = (int)numThreads.Value;
            new Thread(() =>
            {
                ThreadPool.SetMinThreads(Maxthreads, Maxthreads);
                Thread[] array = new Thread[Maxthreads];
                for (int i = 0; i < Maxthreads; i++)
                {
                    if (cancellationTokenSource.IsCancellationRequested)
                    {
                        break;
                    }
                    array[i] = new Thread((ThreadStart)delegate
                    {
                        while (!cancellationTokenSource.IsCancellationRequested && !Dataqueue.IsEmpty)
                        {
                            Start();
                        }
                    });
                    array[i].Start();
                }
                for (int j = 0; j < Maxthreads; j++)
                {
                    if (array[j] != null)
                    {
                        array[j].Join();
                    }
                }
                Task.Delay(TimeSpan.FromSeconds(3)).Wait();
                tmupdatecount.Stop();
                SaveRemainData();
                Isruning(false);
                cancellationTokenSource.Cancel();
                GC.SuppressFinalize(this);
                GC.WaitForFullGCApproach();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }).Start();
        }
        private void SaveRemainData()
        {
            SaveFile(SavePath + "\\RemainData.txt", string.Join(Environment.NewLine, Dataqueue));
        }
        private void Start()
        {
            string data = string.Empty;
            string proxy = string.Empty;
            try
            {
                Data fMainDatagrid = null;
                while (!cancellationTokenSource.IsCancellationRequested)
                {
                    if (Dataqueue.IsEmpty)
                    {
                        break;
                    }
                    Dataqueue.TryDequeue(out data);
                    if (string.IsNullOrEmpty(data))
                    {
                        continue;
                    }
                    if (fMainDatagrid == null)
                    {
                        fMainDatagrid = this._ViewGridDataService.AddNewRow();
                    }
                    string[] datarr = data.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                    string email = datarr[0];
                    fMainDatagrid.Email = data;
                    _ViewGridDataService.SafeSetValueCell(fMainDatagrid.Index, DataGridCellName.CellEmail, fMainDatagrid.Email);
                    proxy = RandomProxy();
                    HttpConfig httpConfig = new HttpConfig
                    {
                        UseProxy = false
                    };
                    if (!string.IsNullOrEmpty(proxy) && UIConfig.UseProxy)
                    {
                        fMainDatagrid.Proxy = proxy;
                        httpConfig.UseProxy = true;
                        httpConfig.Proxy = ProxyClient.Parse(_ProxyType, proxy);
                        httpConfig.ConnectTimeOut = 15000;
                    }
                    _ViewGridDataService.SafeSetValueCell(fMainDatagrid.Index, DataGridCellName.CellProxy, httpConfig.Proxy);
                    DoHttpApiTask(httpConfig, email, data, fMainDatagrid);
                    
                }
            }
            catch
            {

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
                : proxyType.SelectedIndex == 1 ? ProxyType.Socks4
                : ProxyType.Socks5;
        }
        private void importData_BtnClick(Object sender, EventArgs e)
        {
            //this.CheckLinkedGroup.Visible = false;
            ImportForm import = new ImportForm($"Data");
            import.ShowDialog(this);
            if (import.ImportResult == DialogResult.OK && import.ImportedData.Count != 0)
            {
                Dataqueue = new ConcurrentQueue<string>(import.ImportedData);
                dataInfo.Text = $"{import.ImportedData.Count}";
            }
        }
        private void importProxy_BtnClick(Object sender, EventArgs e)
        {
            ImportForm import = new ImportForm($"Proxies");
            import.ShowDialog(this);
            if (import.ImportResult == DialogResult.OK && import.ImportedData.Count != 0)
            {
                ProxyPool = new List<string>(import.ImportedData);
                proxiesData.Text =$"{import.ImportedData.Count}"; 
            }
        }
        private void rdCheckLinked_CheckedChanged(object sender, EventArgs e)
        {
            UIConfig.TaskType = 1;
            LinkedOrSuccess.Text = "Linked:";
            NotLinkedOrFailed.Text = "NotLinked:";
        }

        private void rdCheckDie_CheckedChanged(object sender, EventArgs e)
        {
            UIConfig.TaskType = 2;
            LinkedOrSuccess.Text = "Success:";
            NotLinkedOrFailed.Text = "Failed:";
        }
        private string RandomProxy()
        {
            return ProxyPool.RandomItemInList();
        }
        private void ResetCounter()
        {
            LinkedC = 0;
            DieC = 0;
            NotLinkedC = 0;
            SuccessC = 0;
            FailureC = 0;
            RetriesC = 0;
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
        private void DoHttpApiTask(HttpConfig httpConfig, string email, string rawdata, Data fMainDatagrid)
        {
            APIServices pinterestAPI = new APIServices();

            PinterestAPIOptions.VerifyEmailOptions verifyEmailOptions = new PinterestAPIOptions.VerifyEmailOptions
            {
                Email = email,
                HttpConfig = httpConfig
            };
            fMainDatagrid.Status = "verifying email...";
            _ViewGridDataService.SafeSetValueCell(fMainDatagrid.Index, DataGridCellName.CellStatus, fMainDatagrid.Status);
            PinterestAPIExecuteResult.VerifyEmailResult verifyEmailResult = pinterestAPI.APICheckLinkedToPinterest(verifyEmailOptions);
            if (verifyEmailResult.StatusCode == Enums.PinterestAPIExecuteStatusCode.Error)
            {
                RetriesC++;
                RequestedC++;
                Dataqueue.Enqueue(rawdata);
                return;
            }
            fMainDatagrid.Status = $"verify done ! status : {verifyEmailResult.EmailStatusCode}";
            _ViewGridDataService.SafeSetValueCell(fMainDatagrid.Index, DataGridCellName.CellStatus, fMainDatagrid.Status);
            if (verifyEmailResult.EmailStatusCode == Enums.VerifyEmailStatusCode.Linked)
            {
                LinkedC++;
                RequestedC++;
            }
            else
            {
                NotLinkedC++;
                RequestedC++;
            }
            SaveFile(SavePath + $"\\{verifyEmailResult.EmailStatusCode}.txt", rawdata);
            //Task.Delay(TimeSpan.FromSeconds(1)).Wait();
        }
        private void tmupdatecount_Tick(object sender, EventArgs e)
        {
            try
            {
                this.Invoke(new Action(() =>
                {
                    if(UIConfig.TaskType == 1)
                    {
                        LinkedOrSuccessInfo.Text = LinkedC.ToString();
                        NotLinkOrFailedInfo.Text = NotLinkedC.ToString();
                    }
                    else
                    {
                        LinkedOrSuccessInfo.Text = SuccessC.ToString();
                        NotLinkOrFailedInfo.Text = FailureC.ToString();
                    }
                    RequestedQuantity.Text = RequestedC.ToString();
                    //lbdieC.Text = DieC.ToString();
                    //lbsuccessC.Text = SuccessC.ToString();
                    //lbfailedC.Text = FailureC.ToString();
                    retryInfo.Text = RetriesC.ToString();
                }));
            }
            catch { }
        }
        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show(this, "Are your want to exit ?", "exit confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //Application.Exit();
                cancellationTokenSource?.Cancel();
                Environment.Exit(0);
            }
            else
            {
                e.Cancel = true;
            }
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
    }
}
