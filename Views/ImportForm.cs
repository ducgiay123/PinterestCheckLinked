using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChangeViaFBTool.Views
{
    public partial class ImportForm : Form
    {
        public List<string> ImportedData
        {
            get
            {
                return _importdata;
            }
            set
            {
                _importdata = value;
                lbimported.Text = value.Count.ToString();
            }
        }
        private List<string> _importdata;
        private string Title { get; set; }
        public string FileName { get; set; }
        public DialogResult ImportResult { get; set; }
        private bool Isloading
        {
            get
            {
                return _loading;
            }
            set
            {
                _loading = value;
                progressBar1.Visible = value;
                btnclipboard.Enabled = !value;
                btnok.Enabled = !value;
                btncacel.Enabled = !value;
                btnfile.Enabled = !value;
            }
        }
        bool _loading;
        public ImportForm(string title)
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            Title = title;
        }
        private string GetClipboardDataThread()
        {
            try
            {
                string clipboardData = null;
                Exception threadEx = null;
                Thread staThread = new Thread(
                    delegate ()
                    {
                        try
                        {
                            clipboardData = Clipboard.GetText(TextDataFormat.Text);
                        }

                        catch (Exception ex)
                        {
                            threadEx = ex;
                        }
                    });
                staThread.SetApartmentState(ApartmentState.STA);
                staThread.Start();
                staThread.Join();
                return clipboardData;
            }
            catch (Exception exception)
            {
                return string.Empty;
            }
        }

        private void btnclipboard_Click(object sender, EventArgs e)
        {
            Isloading = true;
            ImportedData = new List<string>(GetClipboardDataThread().Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries)).Distinct().ToList();
            Isloading = false;
        }

        private void btnfile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Title = Title,
                Filter = "Text file |*.txt"
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Isloading = true;
                new Thread(() =>
                {
                    ImportedData = new List<string>(File.ReadAllLines(openFileDialog.FileName).Distinct().ToList());
                    FileName = openFileDialog.FileName;
                    Isloading = false;
                }).Start();
            }
        }

        private void FrmImport_Load(object sender, EventArgs e)
        {
            ImportedData = new List<string>();
            this.Text += $" [ {Title} ]";
            Isloading = false;
        }

        private void btncacel_Click(object sender, EventArgs e)
        {
            ImportResult = DialogResult.Cancel;
            Close();
        }

        private void btnok_Click(object sender, EventArgs e)
        {
            ImportResult = DialogResult.OK;
            Close();
        }
    }
}
