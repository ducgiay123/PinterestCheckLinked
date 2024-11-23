using System.Drawing;
using System.Windows.Forms;

namespace PinterestCheckLinked
{
    partial class FormMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.clstt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clEmail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clProxy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numThreads = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Feature = new System.Windows.Forms.GroupBox();
            this.btnImportData = new System.Windows.Forms.Button();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnsaving = new System.Windows.Forms.Button();
            this.btnstop = new System.Windows.Forms.Button();
            this.btnstart = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnImportProxy = new System.Windows.Forms.Button();
            this.proxyType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbproxy = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dataInfo = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.proxiesData = new System.Windows.Forms.Label();
            this.DataShow = new System.Windows.Forms.GroupBox();
            this.NotLinkedOrFailed = new System.Windows.Forms.Label();
            this.retryInfo = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.NotLinkOrFailedInfo = new System.Windows.Forms.Label();
            this.LinkedOrSuccessInfo = new System.Windows.Forms.Label();
            this.LinkedOrSuccess = new System.Windows.Forms.Label();
            this.tmupdatecount = new System.Windows.Forms.Timer(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.RequestedQuantity = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numThreads)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.Feature.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.DataShow.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clstt,
            this.clEmail,
            this.clProxy,
            this.clStatus});
            this.dataGridView.Location = new System.Drawing.Point(4, 9);
            this.dataGridView.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.RowHeadersWidth = 62;
            this.dataGridView.RowTemplate.Height = 33;
            this.dataGridView.Size = new System.Drawing.Size(556, 308);
            this.dataGridView.TabIndex = 0;
            // 
            // clstt
            // 
            this.clstt.FillWeight = 50F;
            this.clstt.HeaderText = "Stt";
            this.clstt.MinimumWidth = 8;
            this.clstt.Name = "clstt";
            // 
            // clEmail
            // 
            this.clEmail.HeaderText = "Email";
            this.clEmail.MinimumWidth = 8;
            this.clEmail.Name = "clEmail";
            // 
            // clProxy
            // 
            this.clProxy.FillWeight = 50F;
            this.clProxy.HeaderText = "Proxy";
            this.clProxy.MinimumWidth = 8;
            this.clProxy.Name = "clProxy";
            // 
            // clStatus
            // 
            this.clStatus.HeaderText = "Status";
            this.clStatus.MinimumWidth = 8;
            this.clStatus.Name = "clStatus";
            this.clStatus.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // numThreads
            // 
            this.numThreads.Location = new System.Drawing.Point(81, 35);
            this.numThreads.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numThreads.Name = "numThreads";
            this.numThreads.Size = new System.Drawing.Size(74, 26);
            this.numThreads.TabIndex = 1;
            this.numThreads.ValueChanged += new System.EventHandler(this.numthreads_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Thread";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Feature);
            this.groupBox1.Controls.Add(this.dateTimePicker1);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Location = new System.Drawing.Point(4, 466);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(556, 223);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Settings";
            // 
            // Feature
            // 
            this.Feature.Controls.Add(this.numThreads);
            this.Feature.Controls.Add(this.label1);
            this.Feature.Controls.Add(this.btnImportData);
            this.Feature.Location = new System.Drawing.Point(202, 27);
            this.Feature.Name = "Feature";
            this.Feature.Size = new System.Drawing.Size(170, 151);
            this.Feature.TabIndex = 12;
            this.Feature.TabStop = false;
            this.Feature.Text = "Features";
            // 
            // btnImportData
            // 
            this.btnImportData.Location = new System.Drawing.Point(10, 79);
            this.btnImportData.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnImportData.Name = "btnImportData";
            this.btnImportData.Size = new System.Drawing.Size(145, 55);
            this.btnImportData.TabIndex = 7;
            this.btnImportData.Text = "Data Import";
            this.btnImportData.UseVisualStyleBackColor = true;
            this.btnImportData.Click += new System.EventHandler(this.importData_BtnClick);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(534, 208);
            this.dateTimePicker1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(16, 26);
            this.dateTimePicker1.TabIndex = 8;
            this.dateTimePicker1.Visible = false;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.tmupdatecount_Tick);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnsaving);
            this.groupBox3.Controls.Add(this.btnstop);
            this.groupBox3.Controls.Add(this.btnstart);
            this.groupBox3.Location = new System.Drawing.Point(8, 27);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox3.Size = new System.Drawing.Size(172, 188);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Run";
            // 
            // btnsaving
            // 
            this.btnsaving.Font = new System.Drawing.Font("Cambria", 9F);
            this.btnsaving.Location = new System.Drawing.Point(22, 130);
            this.btnsaving.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnsaving.Name = "btnsaving";
            this.btnsaving.Size = new System.Drawing.Size(128, 44);
            this.btnsaving.TabIndex = 6;
            this.btnsaving.Text = "Rs Folder";
            this.btnsaving.UseVisualStyleBackColor = true;
            this.btnsaving.Click += new System.EventHandler(this.saving_BtnClick);
            // 
            // btnstop
            // 
            this.btnstop.Font = new System.Drawing.Font("Cambria", 9F);
            this.btnstop.Location = new System.Drawing.Point(22, 79);
            this.btnstop.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnstop.Name = "btnstop";
            this.btnstop.Size = new System.Drawing.Size(128, 44);
            this.btnstop.TabIndex = 5;
            this.btnstop.Text = "Stop";
            this.btnstop.UseVisualStyleBackColor = true;
            this.btnstop.Click += new System.EventHandler(this.stop_BtnClick);
            // 
            // btnstart
            // 
            this.btnstart.Font = new System.Drawing.Font("Cambria", 9F);
            this.btnstart.Location = new System.Drawing.Point(22, 25);
            this.btnstart.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnstart.Name = "btnstart";
            this.btnstart.Size = new System.Drawing.Size(128, 50);
            this.btnstart.TabIndex = 4;
            this.btnstart.Text = "Start";
            this.btnstart.UseVisualStyleBackColor = true;
            this.btnstart.Click += new System.EventHandler(this.start_BtnClick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnImportProxy);
            this.groupBox2.Controls.Add(this.proxyType);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.cbproxy);
            this.groupBox2.Location = new System.Drawing.Point(378, 27);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Size = new System.Drawing.Size(172, 151);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Proxy Settings";
            // 
            // btnImportProxy
            // 
            this.btnImportProxy.Location = new System.Drawing.Point(6, 89);
            this.btnImportProxy.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnImportProxy.Name = "btnImportProxy";
            this.btnImportProxy.Size = new System.Drawing.Size(162, 48);
            this.btnImportProxy.TabIndex = 8;
            this.btnImportProxy.Text = "Import";
            this.btnImportProxy.UseVisualStyleBackColor = true;
            this.btnImportProxy.Click += new System.EventHandler(this.importProxy_BtnClick);
            // 
            // proxyType
            // 
            this.proxyType.FormattingEnabled = true;
            this.proxyType.Items.AddRange(new object[] {
            "Http",
            "Socks4",
            "Socks5"});
            this.proxyType.Location = new System.Drawing.Point(54, 52);
            this.proxyType.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.proxyType.Name = "proxyType";
            this.proxyType.Size = new System.Drawing.Size(74, 28);
            this.proxyType.TabIndex = 2;
            this.proxyType.SelectedIndexChanged += new System.EventHandler(this.proxyType_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Type";
            // 
            // cbproxy
            // 
            this.cbproxy.AutoSize = true;
            this.cbproxy.Location = new System.Drawing.Point(4, 25);
            this.cbproxy.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbproxy.Name = "cbproxy";
            this.cbproxy.Size = new System.Drawing.Size(73, 24);
            this.cbproxy.TabIndex = 0;
            this.cbproxy.Text = "Proxy";
            this.cbproxy.UseVisualStyleBackColor = true;
            this.cbproxy.CheckedChanged += new System.EventHandler(this.proxyChecked_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "Data:";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // dataInfo
            // 
            this.dataInfo.AutoSize = true;
            this.dataInfo.Location = new System.Drawing.Point(84, 31);
            this.dataInfo.Name = "dataInfo";
            this.dataInfo.Size = new System.Drawing.Size(18, 20);
            this.dataInfo.TabIndex = 5;
            this.dataInfo.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 97);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "Proxies:";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // proxiesData
            // 
            this.proxiesData.AutoSize = true;
            this.proxiesData.Location = new System.Drawing.Point(100, 97);
            this.proxiesData.Name = "proxiesData";
            this.proxiesData.Size = new System.Drawing.Size(18, 20);
            this.proxiesData.TabIndex = 7;
            this.proxiesData.Text = "0";
            this.proxiesData.Click += new System.EventHandler(this.labelData_Click);
            // 
            // DataShow
            // 
            this.DataShow.Controls.Add(this.RequestedQuantity);
            this.DataShow.Controls.Add(this.label5);
            this.DataShow.Controls.Add(this.NotLinkedOrFailed);
            this.DataShow.Controls.Add(this.retryInfo);
            this.DataShow.Controls.Add(this.label9);
            this.DataShow.Controls.Add(this.NotLinkOrFailedInfo);
            this.DataShow.Controls.Add(this.LinkedOrSuccessInfo);
            this.DataShow.Controls.Add(this.LinkedOrSuccess);
            this.DataShow.Controls.Add(this.proxiesData);
            this.DataShow.Controls.Add(this.label4);
            this.DataShow.Controls.Add(this.label3);
            this.DataShow.Controls.Add(this.dataInfo);
            this.DataShow.Location = new System.Drawing.Point(4, 325);
            this.DataShow.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.DataShow.Name = "DataShow";
            this.DataShow.Padding = new System.Windows.Forms.Padding(0);
            this.DataShow.Size = new System.Drawing.Size(556, 135);
            this.DataShow.TabIndex = 9;
            this.DataShow.TabStop = false;
            this.DataShow.Text = "Information";
            this.DataShow.Enter += new System.EventHandler(this.groupBox4_Enter);
            // 
            // NotLinkedOrFailed
            // 
            this.NotLinkedOrFailed.AutoSize = true;
            this.NotLinkedOrFailed.ForeColor = System.Drawing.Color.Red;
            this.NotLinkedOrFailed.Location = new System.Drawing.Point(296, 63);
            this.NotLinkedOrFailed.Name = "NotLinkedOrFailed";
            this.NotLinkedOrFailed.Size = new System.Drawing.Size(85, 20);
            this.NotLinkedOrFailed.TabIndex = 15;
            this.NotLinkedOrFailed.Text = "NotLinked:";
            // 
            // retryInfo
            // 
            this.retryInfo.AutoSize = true;
            this.retryInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.retryInfo.Location = new System.Drawing.Point(93, 64);
            this.retryInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.retryInfo.Name = "retryInfo";
            this.retryInfo.Size = new System.Drawing.Size(18, 20);
            this.retryInfo.TabIndex = 14;
            this.retryInfo.Text = "0";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.label9.Location = new System.Drawing.Point(30, 64);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(55, 20);
            this.label9.TabIndex = 13;
            this.label9.Text = "Retry: ";
            // 
            // NotLinkOrFailedInfo
            // 
            this.NotLinkOrFailedInfo.AutoSize = true;
            this.NotLinkOrFailedInfo.ForeColor = System.Drawing.Color.Red;
            this.NotLinkOrFailedInfo.Location = new System.Drawing.Point(388, 63);
            this.NotLinkOrFailedInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.NotLinkOrFailedInfo.Name = "NotLinkOrFailedInfo";
            this.NotLinkOrFailedInfo.Size = new System.Drawing.Size(18, 20);
            this.NotLinkOrFailedInfo.TabIndex = 12;
            this.NotLinkOrFailedInfo.Text = "0";
            this.NotLinkOrFailedInfo.Click += new System.EventHandler(this.label8_Click);
            // 
            // LinkedOrSuccessInfo
            // 
            this.LinkedOrSuccessInfo.AutoSize = true;
            this.LinkedOrSuccessInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.LinkedOrSuccessInfo.Location = new System.Drawing.Point(368, 31);
            this.LinkedOrSuccessInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LinkedOrSuccessInfo.Name = "LinkedOrSuccessInfo";
            this.LinkedOrSuccessInfo.Size = new System.Drawing.Size(18, 20);
            this.LinkedOrSuccessInfo.TabIndex = 10;
            this.LinkedOrSuccessInfo.Text = "0";
            this.LinkedOrSuccessInfo.Click += new System.EventHandler(this.label6_Click);
            // 
            // LinkedOrSuccess
            // 
            this.LinkedOrSuccess.AutoSize = true;
            this.LinkedOrSuccess.ForeColor = System.Drawing.Color.LimeGreen;
            this.LinkedOrSuccess.Location = new System.Drawing.Point(296, 31);
            this.LinkedOrSuccess.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LinkedOrSuccess.Name = "LinkedOrSuccess";
            this.LinkedOrSuccess.Size = new System.Drawing.Size(60, 20);
            this.LinkedOrSuccess.TabIndex = 8;
            this.LinkedOrSuccess.Text = "Linked:";
            // 
            // tmupdatecount
            // 
            this.tmupdatecount.Tick += new System.EventHandler(this.tmupdatecount_Tick);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Blue;
            this.label5.Location = new System.Drawing.Point(296, 95);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(92, 20);
            this.label5.TabIndex = 16;
            this.label5.Text = "Requested:";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // RequestedQuantity
            // 
            this.RequestedQuantity.AutoSize = true;
            this.RequestedQuantity.ForeColor = System.Drawing.Color.Blue;
            this.RequestedQuantity.Location = new System.Drawing.Point(394, 95);
            this.RequestedQuantity.Name = "RequestedQuantity";
            this.RequestedQuantity.Size = new System.Drawing.Size(18, 20);
            this.RequestedQuantity.TabIndex = 17;
            this.RequestedQuantity.Text = "0";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(566, 691);
            this.Controls.Add(this.DataShow);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FormMain";
            this.Text = "Pinterest Check";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numThreads)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.Feature.ResumeLayout(false);
            this.Feature.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.DataShow.ResumeLayout(false);
            this.DataShow.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DataGridView dataGridView;
        private NumericUpDown numThreads;
        private Label label1;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private CheckBox cbproxy;
        private Label label2;
        private Button btnstart;
        private Button btnsaving;
        private Button btnstop;
        private Button btnImportProxy;
        private Button btnImportData;
        private GroupBox groupBox3;
        private ComboBox proxyType;
        private Label label3;
        private Label dataInfo;
        private Label label4;
        private Label proxiesData;
        private DateTimePicker dateTimePicker1;
        private DataGridViewTextBoxColumn clstt;
        private DataGridViewTextBoxColumn clEmail;
        private DataGridViewTextBoxColumn clProxy;
        private DataGridViewTextBoxColumn clStatus;
        private GroupBox DataShow;
        private Label label9;
        private Label NotLinkOrFailedInfo;
        private Label LinkedOrSuccessInfo;
        private Label LinkedOrSuccess;
        private Label retryInfo;
        private Label NotLinkedOrFailed;
        private GroupBox Feature;
        private Timer tmupdatecount;
        private Label RequestedQuantity;
        private Label label5;
    }
}
