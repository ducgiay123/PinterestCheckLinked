using System.Drawing;
using System.Windows.Forms;

namespace CheckDieLinkedinToolV3
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
            System.Windows.Forms.Label label7;
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
            this.dieNum = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.liveNum = new System.Windows.Forms.Label();
            this.dataInfo = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.timeLabel = new System.Windows.Forms.Label();
            this.proxiesData = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.errorInfo = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnstop = new System.Windows.Forms.Button();
            this.btnstart = new System.Windows.Forms.Button();
            this.btnImportData = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnImportProxy = new System.Windows.Forms.Button();
            this.proxyType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbproxy = new System.Windows.Forms.CheckBox();
            this.tmupdatecount = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numThreads)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.Feature.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.BackColor = System.Drawing.SystemColors.Control;
            label7.CausesValidation = false;
            label7.ForeColor = System.Drawing.Color.CornflowerBlue;
            label7.Location = new System.Drawing.Point(15, 224);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(51, 20);
            label7.TabIndex = 18;
            label7.Text = "Time: ";
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
            this.numThreads.Location = new System.Drawing.Point(83, 154);
            this.numThreads.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numThreads.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.numThreads.Name = "numThreads";
            this.numThreads.Size = new System.Drawing.Size(89, 26);
            this.numThreads.TabIndex = 1;
            this.numThreads.ValueChanged += new System.EventHandler(this.numthreads_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 154);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Thread";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Feature);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Location = new System.Drawing.Point(4, 321);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(556, 314);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Settings";
            // 
            // Feature
            // 
            this.Feature.Controls.Add(this.dieNum);
            this.Feature.Controls.Add(this.label3);
            this.Feature.Controls.Add(this.liveNum);
            this.Feature.Controls.Add(this.dataInfo);
            this.Feature.Controls.Add(this.label10);
            this.Feature.Controls.Add(this.label8);
            this.Feature.Controls.Add(this.label4);
            this.Feature.Controls.Add(this.timeLabel);
            this.Feature.Controls.Add(this.proxiesData);
            this.Feature.Controls.Add(label7);
            this.Feature.Controls.Add(this.label9);
            this.Feature.Controls.Add(this.errorInfo);
            this.Feature.Location = new System.Drawing.Point(384, 27);
            this.Feature.Name = "Feature";
            this.Feature.Size = new System.Drawing.Size(172, 263);
            this.Feature.TabIndex = 12;
            this.Feature.TabStop = false;
            this.Feature.Text = "Features";
            // 
            // dieNum
            // 
            this.dieNum.AutoSize = true;
            this.dieNum.ForeColor = System.Drawing.Color.Red;
            this.dieNum.Location = new System.Drawing.Point(69, 154);
            this.dieNum.Name = "dieNum";
            this.dieNum.Size = new System.Drawing.Size(18, 20);
            this.dieNum.TabIndex = 23;
            this.dieNum.Text = "0";
            this.dieNum.Click += new System.EventHandler(this.FailedNum_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "Data:";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // liveNum
            // 
            this.liveNum.AutoSize = true;
            this.liveNum.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.liveNum.Location = new System.Drawing.Point(69, 110);
            this.liveNum.Name = "liveNum";
            this.liveNum.Size = new System.Drawing.Size(18, 20);
            this.liveNum.TabIndex = 22;
            this.liveNum.Text = "0";
            // 
            // dataInfo
            // 
            this.dataInfo.AutoSize = true;
            this.dataInfo.Location = new System.Drawing.Point(69, 26);
            this.dataInfo.Name = "dataInfo";
            this.dataInfo.Size = new System.Drawing.Size(18, 20);
            this.dataInfo.TabIndex = 5;
            this.dataInfo.Text = "0";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.Red;
            this.label10.Location = new System.Drawing.Point(15, 154);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(37, 20);
            this.label10.TabIndex = 21;
            this.label10.Text = "Die:";
            this.label10.Click += new System.EventHandler(this.label10_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.label8.Location = new System.Drawing.Point(15, 110);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 20);
            this.label8.TabIndex = 20;
            this.label8.Text = "Live:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "Proxies:";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // timeLabel
            // 
            this.timeLabel.AutoSize = true;
            this.timeLabel.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.timeLabel.Location = new System.Drawing.Point(80, 224);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(18, 20);
            this.timeLabel.TabIndex = 19;
            this.timeLabel.Text = "0";
            this.timeLabel.Click += new System.EventHandler(this.label8_Click_1);
            // 
            // proxiesData
            // 
            this.proxiesData.AutoSize = true;
            this.proxiesData.Location = new System.Drawing.Point(85, 66);
            this.proxiesData.Name = "proxiesData";
            this.proxiesData.Size = new System.Drawing.Size(18, 20);
            this.proxiesData.TabIndex = 7;
            this.proxiesData.Text = "0";
            this.proxiesData.Click += new System.EventHandler(this.labelData_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.label9.Location = new System.Drawing.Point(15, 190);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(52, 20);
            this.label9.TabIndex = 13;
            this.label9.Text = "Erorr: ";
            this.label9.Click += new System.EventHandler(this.label9_Click);
            // 
            // errorInfo
            // 
            this.errorInfo.AutoSize = true;
            this.errorInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.errorInfo.Location = new System.Drawing.Point(78, 190);
            this.errorInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.errorInfo.Name = "errorInfo";
            this.errorInfo.Size = new System.Drawing.Size(18, 20);
            this.errorInfo.TabIndex = 14;
            this.errorInfo.Text = "0";
            this.errorInfo.Click += new System.EventHandler(this.errorInfo_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.button1);
            this.groupBox3.Controls.Add(this.btnstop);
            this.groupBox3.Controls.Add(this.btnstart);
            this.groupBox3.Controls.Add(this.btnImportData);
            this.groupBox3.Location = new System.Drawing.Point(8, 27);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox3.Size = new System.Drawing.Size(167, 263);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Run";
            // 
            // btnstop
            // 
            this.btnstop.Font = new System.Drawing.Font("Cambria", 9F);
            this.btnstop.Location = new System.Drawing.Point(22, 86);
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
            // btnImportData
            // 
            this.btnImportData.Location = new System.Drawing.Point(22, 142);
            this.btnImportData.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnImportData.Name = "btnImportData";
            this.btnImportData.Size = new System.Drawing.Size(128, 44);
            this.btnImportData.TabIndex = 7;
            this.btnImportData.Text = "Data Import";
            this.btnImportData.UseVisualStyleBackColor = true;
            this.btnImportData.Click += new System.EventHandler(this.importData_BtnClick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.numThreads);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.btnImportProxy);
            this.groupBox2.Controls.Add(this.proxyType);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.cbproxy);
            this.groupBox2.Location = new System.Drawing.Point(191, 27);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Size = new System.Drawing.Size(187, 263);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Proxy and Thread";
            // 
            // btnImportProxy
            // 
            this.btnImportProxy.Location = new System.Drawing.Point(23, 200);
            this.btnImportProxy.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnImportProxy.Name = "btnImportProxy";
            this.btnImportProxy.Size = new System.Drawing.Size(149, 44);
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
            "Socks5",
            "APIlink"});
            this.proxyType.Location = new System.Drawing.Point(69, 96);
            this.proxyType.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.proxyType.Name = "proxyType";
            this.proxyType.Size = new System.Drawing.Size(112, 28);
            this.proxyType.TabIndex = 2;
            this.proxyType.SelectedIndexChanged += new System.EventHandler(this.proxyType_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Type";
            // 
            // cbproxy
            // 
            this.cbproxy.AutoSize = true;
            this.cbproxy.Location = new System.Drawing.Point(22, 40);
            this.cbproxy.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbproxy.Name = "cbproxy";
            this.cbproxy.Size = new System.Drawing.Size(73, 24);
            this.cbproxy.TabIndex = 0;
            this.cbproxy.Text = "Proxy";
            this.cbproxy.UseVisualStyleBackColor = true;
            this.cbproxy.CheckedChanged += new System.EventHandler(this.proxyChecked_ValueChanged);
            // 
            // tmupdatecount
            // 
            this.tmupdatecount.Tick += new System.EventHandler(this.tmupdatecount_Tick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(22, 200);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(128, 44);
            this.button1.TabIndex = 8;
            this.button1.Text = "Result Folder";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.saving_BtnClick);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(566, 646);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FormMain";
            this.Text = "Check Die Linkedin";
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
        private Button btnstop;
        private Button btnImportProxy;
        private Button btnImportData;
        private GroupBox groupBox3;
        private ComboBox proxyType;
        private Label label3;
        private Label dataInfo;
        private Label label4;
        private Label proxiesData;
        private DataGridViewTextBoxColumn clstt;
        private DataGridViewTextBoxColumn clEmail;
        private DataGridViewTextBoxColumn clProxy;
        private DataGridViewTextBoxColumn clStatus;
        private Label label9;
        private Label errorInfo;
        private GroupBox Feature;
        private Timer tmupdatecount;
        private Label dieNum;
        private Label liveNum;
        private Label label10;
        private Label label8;
        private Label timeLabel;
        private Button button1;
    }
}
