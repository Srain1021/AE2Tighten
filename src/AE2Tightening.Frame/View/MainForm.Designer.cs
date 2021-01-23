namespace AE2Tightening.Frame
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        #region Windows 窗体设计器生成的代码
        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.Label lbTorpueUnit;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblAppName = new System.Windows.Forms.Label();
            this.labelPrjTitle = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panelFooter = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.btClose = new System.Windows.Forms.Button();
            this.lblTime = new System.Windows.Forms.Label();
            this.lblEngineCode = new System.Windows.Forms.Label();
            this.lblEngintType = new System.Windows.Forms.Label();
            this.panelEngine = new System.Windows.Forms.Panel();
            this.lblResult = new AE2Tightening.Frame.UserLabel();
            this.label5 = new System.Windows.Forms.Label();
            this.lblMTO = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panelPart = new System.Windows.Forms.Panel();
            this.lblBindStatus = new System.Windows.Forms.Label();
            this.lblBindCode = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.panelTighten = new System.Windows.Forms.Panel();
            this.lblCurAngle = new System.Windows.Forms.Label();
            this.lblCurTorque = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.monitorStopLine = new AE2Tightening.Frame.MonitorItem();
            this.monitorShield = new AE2Tightening.Frame.MonitorItem();
            this.btSyncConfig = new System.Windows.Forms.Button();
            this.btEnableTool = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.monitorPLCNet = new AE2Tightening.Frame.MonitorItem();
            this.monitorScanNet = new AE2Tightening.Frame.MonitorItem();
            this.monitorTD2Net = new AE2Tightening.Frame.MonitorItem();
            this.monitorTD1Net = new AE2Tightening.Frame.MonitorItem();
            this.monitorRfidNet = new AE2Tightening.Frame.MonitorItem();
            this.monitorAdamNet = new AE2Tightening.Frame.MonitorItem();
            this.listViewLog = new System.Windows.Forms.ListView();
            this.columnlog = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblTorp = new System.Windows.Forms.Label();
            this.dataViewTd = new System.Windows.Forms.DataGridView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            lbTorpueUnit = new System.Windows.Forms.Label();
            this.panelHeader.SuspendLayout();
            this.panelFooter.SuspendLayout();
            this.panelEngine.SuspendLayout();
            this.panelPart.SuspendLayout();
            this.panelTighten.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataViewTd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panelLeft.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbTorpueUnit
            // 
            lbTorpueUnit.AutoSize = true;
            lbTorpueUnit.Font = new System.Drawing.Font("微软雅黑", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            lbTorpueUnit.Location = new System.Drawing.Point(551, 180);
            lbTorpueUnit.Name = "lbTorpueUnit";
            lbTorpueUnit.Size = new System.Drawing.Size(101, 48);
            lbTorpueUnit.TabIndex = 11;
            lbTorpueUnit.Text = "N•m";
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.Transparent;
            this.panelHeader.Controls.Add(this.lblAppName);
            this.panelHeader.Controls.Add(this.labelPrjTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(15, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1890, 208);
            this.panelHeader.TabIndex = 0;
            // 
            // lblAppName
            // 
            this.lblAppName.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblAppName.Font = new System.Drawing.Font("宋体", 32F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblAppName.ForeColor = System.Drawing.SystemColors.Window;
            this.lblAppName.Location = new System.Drawing.Point(0, 145);
            this.lblAppName.Name = "lblAppName";
            this.lblAppName.Size = new System.Drawing.Size(1890, 63);
            this.lblAppName.TabIndex = 1;
            this.lblAppName.Text = "压缩机";
            this.lblAppName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelPrjTitle
            // 
            this.labelPrjTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelPrjTitle.Font = new System.Drawing.Font("宋体", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelPrjTitle.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.labelPrjTitle.Location = new System.Drawing.Point(0, 0);
            this.labelPrjTitle.Name = "labelPrjTitle";
            this.labelPrjTitle.Size = new System.Drawing.Size(1890, 76);
            this.labelPrjTitle.TabIndex = 0;
            this.labelPrjTitle.Text = "发动机车间生产管理系统";
            this.labelPrjTitle.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(26, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 24);
            this.label1.TabIndex = 4;
            this.label1.Text = "发动机号：";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(74, 157);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 24);
            this.label3.TabIndex = 5;
            this.label3.Text = "机型：";
            // 
            // panelFooter
            // 
            this.panelFooter.BackColor = System.Drawing.Color.Transparent;
            this.panelFooter.Controls.Add(this.label4);
            this.panelFooter.Controls.Add(this.btClose);
            this.panelFooter.Controls.Add(this.lblTime);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.Location = new System.Drawing.Point(15, 1033);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Size = new System.Drawing.Size(1890, 47);
            this.panelFooter.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.DimGray;
            this.label4.Location = new System.Drawing.Point(866, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(189, 20);
            this.label4.TabIndex = 7;
            this.label4.Text = "武汉恒信网络技术有限公司";
            // 
            // btClose
            // 
            this.btClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btClose.BackColor = System.Drawing.Color.Red;
            this.btClose.FlatAppearance.BorderSize = 0;
            this.btClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btClose.Font = new System.Drawing.Font("微软雅黑", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btClose.ForeColor = System.Drawing.Color.White;
            this.btClose.Image = ((System.Drawing.Image)(resources.GetObject("btClose.Image")));
            this.btClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btClose.Location = new System.Drawing.Point(1760, 1);
            this.btClose.Name = "btClose";
            this.btClose.Padding = new System.Windows.Forms.Padding(7, 0, 0, 0);
            this.btClose.Size = new System.Drawing.Size(128, 45);
            this.btClose.TabIndex = 1;
            this.btClose.Text = "  关闭";
            this.btClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btClose.UseVisualStyleBackColor = false;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Font = new System.Drawing.Font("宋体", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTime.ForeColor = System.Drawing.Color.Black;
            this.lblTime.Location = new System.Drawing.Point(13, 7);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(357, 34);
            this.lblTime.TabIndex = 0;
            this.lblTime.Text = "2020-01-01 00:00:00";
            // 
            // lblEngineCode
            // 
            this.lblEngineCode.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblEngineCode.AutoSize = true;
            this.lblEngineCode.BackColor = System.Drawing.Color.Transparent;
            this.lblEngineCode.Font = new System.Drawing.Font("Century Gothic", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblEngineCode.ForeColor = System.Drawing.Color.Blue;
            this.lblEngineCode.Location = new System.Drawing.Point(182, 1);
            this.lblEngineCode.Name = "lblEngineCode";
            this.lblEngineCode.Size = new System.Drawing.Size(391, 64);
            this.lblEngineCode.TabIndex = 7;
            this.lblEngineCode.Text = "XXXXXXXXXXX";
            // 
            // lblEngintType
            // 
            this.lblEngintType.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblEngintType.AutoSize = true;
            this.lblEngintType.BackColor = System.Drawing.Color.Transparent;
            this.lblEngintType.Font = new System.Drawing.Font("Century Gothic", 34F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblEngintType.Location = new System.Drawing.Point(185, 144);
            this.lblEngintType.Name = "lblEngintType";
            this.lblEngintType.Size = new System.Drawing.Size(260, 46);
            this.lblEngintType.TabIndex = 8;
            this.lblEngintType.Text = "CIVIC 1.5T";
            // 
            // panelEngine
            // 
            this.panelEngine.Controls.Add(this.lblResult);
            this.panelEngine.Controls.Add(this.label5);
            this.panelEngine.Controls.Add(this.lblEngineCode);
            this.panelEngine.Controls.Add(this.lblMTO);
            this.panelEngine.Controls.Add(this.lblEngintType);
            this.panelEngine.Controls.Add(this.label1);
            this.panelEngine.Controls.Add(this.label2);
            this.panelEngine.Controls.Add(this.label3);
            this.panelEngine.Controls.Add(this.panelPart);
            this.panelEngine.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEngine.Location = new System.Drawing.Point(0, 0);
            this.panelEngine.Name = "panelEngine";
            this.panelEngine.Size = new System.Drawing.Size(1221, 289);
            this.panelEngine.TabIndex = 0;
            // 
            // lblResult
            // 
            this.lblResult.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblResult.BackColor = System.Drawing.Color.Transparent;
            this.lblResult.Font = new System.Drawing.Font("Microsoft YaHei UI", 62F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblResult.ForeColor = System.Drawing.Color.White;
            this.lblResult.Location = new System.Drawing.Point(887, 14);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(190, 190);
            this.lblResult.TabIndex = 2;
            this.lblResult.Text = "OK";
            this.lblResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(751, 180);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(130, 24);
            this.label5.TabIndex = 4;
            this.label5.Text = "拧紧结果：";
            // 
            // lblMTO
            // 
            this.lblMTO.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblMTO.AutoSize = true;
            this.lblMTO.BackColor = System.Drawing.Color.Transparent;
            this.lblMTO.Font = new System.Drawing.Font("Century Gothic", 34F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMTO.Location = new System.Drawing.Point(185, 77);
            this.lblMTO.Name = "lblMTO";
            this.lblMTO.Size = new System.Drawing.Size(116, 46);
            this.lblMTO.TabIndex = 8;
            this.lblMTO.Text = "0000";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(74, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 24);
            this.label2.TabIndex = 5;
            this.label2.Text = "派生：";
            // 
            // panelPart
            // 
            this.panelPart.Controls.Add(this.lblBindStatus);
            this.panelPart.Controls.Add(this.lblBindCode);
            this.panelPart.Controls.Add(this.label12);
            this.panelPart.Controls.Add(this.label11);
            this.panelPart.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelPart.Location = new System.Drawing.Point(0, 211);
            this.panelPart.Name = "panelPart";
            this.panelPart.Size = new System.Drawing.Size(1221, 78);
            this.panelPart.TabIndex = 9;
            this.panelPart.Visible = false;
            // 
            // lblBindStatus
            // 
            this.lblBindStatus.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblBindStatus.AutoSize = true;
            this.lblBindStatus.BackColor = System.Drawing.Color.DimGray;
            this.lblBindStatus.Font = new System.Drawing.Font("宋体", 32F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblBindStatus.ForeColor = System.Drawing.Color.White;
            this.lblBindStatus.Location = new System.Drawing.Point(914, 9);
            this.lblBindStatus.Name = "lblBindStatus";
            this.lblBindStatus.Padding = new System.Windows.Forms.Padding(6);
            this.lblBindStatus.Size = new System.Drawing.Size(163, 55);
            this.lblBindStatus.TabIndex = 2;
            this.lblBindStatus.Text = "已绑定";
            // 
            // lblBindCode
            // 
            this.lblBindCode.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblBindCode.AutoSize = true;
            this.lblBindCode.Font = new System.Drawing.Font("Century Gothic", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblBindCode.ForeColor = System.Drawing.Color.Blue;
            this.lblBindCode.Location = new System.Drawing.Point(182, 9);
            this.lblBindCode.Name = "lblBindCode";
            this.lblBindCode.Size = new System.Drawing.Size(523, 64);
            this.lblBindCode.TabIndex = 1;
            this.lblBindCode.Text = "51610TET H021M1";
            // 
            // label12
            // 
            this.label12.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.Location = new System.Drawing.Point(751, 25);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(130, 24);
            this.label12.TabIndex = 2;
            this.label12.Text = "绑定状态：";
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.Location = new System.Drawing.Point(52, 25);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(106, 24);
            this.label11.TabIndex = 0;
            this.label11.Text = "零件码：";
            // 
            // panelTighten
            // 
            this.panelTighten.Controls.Add(this.lblCurAngle);
            this.panelTighten.Controls.Add(this.lblCurTorque);
            this.panelTighten.Controls.Add(this.groupBox3);
            this.panelTighten.Controls.Add(this.btSyncConfig);
            this.panelTighten.Controls.Add(this.btEnableTool);
            this.panelTighten.Controls.Add(this.groupBox1);
            this.panelTighten.Controls.Add(this.listViewLog);
            this.panelTighten.Controls.Add(lbTorpueUnit);
            this.panelTighten.Controls.Add(this.lblTorp);
            this.panelTighten.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTighten.Location = new System.Drawing.Point(0, 0);
            this.panelTighten.Name = "panelTighten";
            this.panelTighten.Size = new System.Drawing.Size(665, 825);
            this.panelTighten.TabIndex = 2;
            // 
            // lblCurAngle
            // 
            this.lblCurAngle.AutoSize = true;
            this.lblCurAngle.Font = new System.Drawing.Font("微软雅黑", 28F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCurAngle.Location = new System.Drawing.Point(562, 14);
            this.lblCurAngle.Name = "lblCurAngle";
            this.lblCurAngle.Size = new System.Drawing.Size(44, 50);
            this.lblCurAngle.TabIndex = 15;
            this.lblCurAngle.Text = "0";
            // 
            // lblCurTorque
            // 
            this.lblCurTorque.Font = new System.Drawing.Font("微软雅黑", 96F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCurTorque.Location = new System.Drawing.Point(69, 63);
            this.lblCurTorque.Name = "lblCurTorque";
            this.lblCurTorque.Size = new System.Drawing.Size(460, 165);
            this.lblCurTorque.TabIndex = 14;
            this.lblCurTorque.Text = "0";
            this.lblCurTorque.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.monitorStopLine);
            this.groupBox3.Controls.Add(this.monitorShield);
            this.groupBox3.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox3.Location = new System.Drawing.Point(0, 277);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(665, 91);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "信号监控";
            // 
            // monitorStopLine
            // 
            this.monitorStopLine.AutoSize = true;
            this.monitorStopLine.FalseColor = System.Drawing.Color.Yellow;
            this.monitorStopLine.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.monitorStopLine.Location = new System.Drawing.Point(27, 33);
            this.monitorStopLine.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.monitorStopLine.MinimumSize = new System.Drawing.Size(135, 33);
            this.monitorStopLine.Name = "monitorStopLine";
            this.monitorStopLine.Size = new System.Drawing.Size(160, 40);
            this.monitorStopLine.Status = false;
            this.monitorStopLine.TabIndex = 14;
            this.monitorStopLine.Text = "报警停线：";
            this.monitorStopLine.TrueColor = System.Drawing.Color.Green;
            // 
            // monitorShield
            // 
            this.monitorShield.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.monitorShield.AutoSize = true;
            this.monitorShield.FalseColor = System.Drawing.Color.Green;
            this.monitorShield.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.monitorShield.Location = new System.Drawing.Point(258, 33);
            this.monitorShield.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.monitorShield.MinimumSize = new System.Drawing.Size(135, 33);
            this.monitorShield.Name = "monitorShield";
            this.monitorShield.Size = new System.Drawing.Size(160, 40);
            this.monitorShield.Status = false;
            this.monitorShield.TabIndex = 14;
            this.monitorShield.Text = "线体互锁：";
            this.monitorShield.TrueColor = System.Drawing.Color.Yellow;
            // 
            // btSyncConfig
            // 
            this.btSyncConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btSyncConfig.BackColor = System.Drawing.Color.RoyalBlue;
            this.btSyncConfig.FlatAppearance.BorderSize = 0;
            this.btSyncConfig.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btSyncConfig.Font = new System.Drawing.Font("微软雅黑", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btSyncConfig.ForeColor = System.Drawing.Color.White;
            this.btSyncConfig.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btSyncConfig.Location = new System.Drawing.Point(411, 388);
            this.btSyncConfig.Name = "btSyncConfig";
            this.btSyncConfig.Padding = new System.Windows.Forms.Padding(7, 0, 0, 0);
            this.btSyncConfig.Size = new System.Drawing.Size(162, 70);
            this.btSyncConfig.TabIndex = 1;
            this.btSyncConfig.Text = "同步参数";
            this.btSyncConfig.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btSyncConfig.UseVisualStyleBackColor = false;
            this.btSyncConfig.Click += new System.EventHandler(this.btSyncConfig_Click);
            // 
            // btEnableTool
            // 
            this.btEnableTool.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btEnableTool.BackColor = System.Drawing.Color.RoyalBlue;
            this.btEnableTool.FlatAppearance.BorderSize = 0;
            this.btEnableTool.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btEnableTool.Font = new System.Drawing.Font("微软雅黑", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btEnableTool.ForeColor = System.Drawing.Color.White;
            this.btEnableTool.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btEnableTool.Location = new System.Drawing.Point(110, 388);
            this.btEnableTool.Name = "btEnableTool";
            this.btEnableTool.Padding = new System.Windows.Forms.Padding(7, 0, 0, 0);
            this.btEnableTool.Size = new System.Drawing.Size(162, 70);
            this.btEnableTool.TabIndex = 1;
            this.btEnableTool.Text = "解锁拧紧枪";
            this.btEnableTool.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btEnableTool.UseVisualStyleBackColor = false;
            this.btEnableTool.Click += new System.EventHandler(this.btEnableTool_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.monitorPLCNet);
            this.groupBox1.Controls.Add(this.monitorScanNet);
            this.groupBox1.Controls.Add(this.monitorTD2Net);
            this.groupBox1.Controls.Add(this.monitorTD1Net);
            this.groupBox1.Controls.Add(this.monitorRfidNet);
            this.groupBox1.Controls.Add(this.monitorAdamNet);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(0, 477);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(665, 145);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "状态监控";
            // 
            // monitorPLCNet
            // 
            this.monitorPLCNet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.monitorPLCNet.AutoSize = true;
            this.monitorPLCNet.FalseColor = System.Drawing.Color.DarkGray;
            this.monitorPLCNet.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.monitorPLCNet.Location = new System.Drawing.Point(39, 93);
            this.monitorPLCNet.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.monitorPLCNet.MinimumSize = new System.Drawing.Size(125, 33);
            this.monitorPLCNet.Name = "monitorPLCNet";
            this.monitorPLCNet.Size = new System.Drawing.Size(160, 40);
            this.monitorPLCNet.Status = false;
            this.monitorPLCNet.TabIndex = 14;
            this.monitorPLCNet.Text = "PLC通讯：";
            this.monitorPLCNet.TrueColor = System.Drawing.Color.Green;
            // 
            // monitorScanNet
            // 
            this.monitorScanNet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.monitorScanNet.AutoSize = true;
            this.monitorScanNet.FalseColor = System.Drawing.Color.DarkGray;
            this.monitorScanNet.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.monitorScanNet.Location = new System.Drawing.Point(258, 33);
            this.monitorScanNet.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.monitorScanNet.MinimumSize = new System.Drawing.Size(116, 33);
            this.monitorScanNet.Name = "monitorScanNet";
            this.monitorScanNet.Size = new System.Drawing.Size(160, 40);
            this.monitorScanNet.Status = false;
            this.monitorScanNet.TabIndex = 14;
            this.monitorScanNet.Text = "扫描枪：";
            this.monitorScanNet.TrueColor = System.Drawing.Color.Green;
            // 
            // monitorTD2Net
            // 
            this.monitorTD2Net.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.monitorTD2Net.AutoSize = true;
            this.monitorTD2Net.FalseColor = System.Drawing.Color.DarkGray;
            this.monitorTD2Net.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.monitorTD2Net.Location = new System.Drawing.Point(468, 93);
            this.monitorTD2Net.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.monitorTD2Net.MinimumSize = new System.Drawing.Size(116, 33);
            this.monitorTD2Net.Name = "monitorTD2Net";
            this.monitorTD2Net.Size = new System.Drawing.Size(160, 40);
            this.monitorTD2Net.Status = false;
            this.monitorTD2Net.TabIndex = 14;
            this.monitorTD2Net.Text = "拧紧机：";
            this.monitorTD2Net.TrueColor = System.Drawing.Color.Green;
            this.monitorTD2Net.Visible = false;
            // 
            // monitorTD1Net
            // 
            this.monitorTD1Net.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.monitorTD1Net.AutoSize = true;
            this.monitorTD1Net.FalseColor = System.Drawing.Color.DarkGray;
            this.monitorTD1Net.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.monitorTD1Net.Location = new System.Drawing.Point(258, 93);
            this.monitorTD1Net.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.monitorTD1Net.MinimumSize = new System.Drawing.Size(116, 33);
            this.monitorTD1Net.Name = "monitorTD1Net";
            this.monitorTD1Net.Size = new System.Drawing.Size(160, 40);
            this.monitorTD1Net.Status = false;
            this.monitorTD1Net.TabIndex = 14;
            this.monitorTD1Net.Text = "拧紧机：";
            this.monitorTD1Net.TrueColor = System.Drawing.Color.Green;
            this.monitorTD1Net.Visible = false;
            // 
            // monitorRfidNet
            // 
            this.monitorRfidNet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.monitorRfidNet.AutoSize = true;
            this.monitorRfidNet.FalseColor = System.Drawing.Color.DarkGray;
            this.monitorRfidNet.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.monitorRfidNet.Location = new System.Drawing.Point(39, 33);
            this.monitorRfidNet.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.monitorRfidNet.MinimumSize = new System.Drawing.Size(135, 33);
            this.monitorRfidNet.Name = "monitorRfidNet";
            this.monitorRfidNet.Size = new System.Drawing.Size(160, 40);
            this.monitorRfidNet.Status = false;
            this.monitorRfidNet.TabIndex = 14;
            this.monitorRfidNet.Text = "RFID通讯：";
            this.monitorRfidNet.TrueColor = System.Drawing.Color.Green;
            // 
            // monitorAdamNet
            // 
            this.monitorAdamNet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.monitorAdamNet.AutoSize = true;
            this.monitorAdamNet.FalseColor = System.Drawing.Color.DarkGray;
            this.monitorAdamNet.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.monitorAdamNet.Location = new System.Drawing.Point(468, 33);
            this.monitorAdamNet.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.monitorAdamNet.MinimumSize = new System.Drawing.Size(116, 33);
            this.monitorAdamNet.Name = "monitorAdamNet";
            this.monitorAdamNet.Size = new System.Drawing.Size(160, 40);
            this.monitorAdamNet.Status = false;
            this.monitorAdamNet.TabIndex = 14;
            this.monitorAdamNet.Text = "IO模块：";
            this.monitorAdamNet.TrueColor = System.Drawing.Color.Green;
            // 
            // listViewLog
            // 
            this.listViewLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listViewLog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnlog});
            this.listViewLog.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.listViewLog.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listViewLog.FullRowSelect = true;
            this.listViewLog.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listViewLog.HideSelection = false;
            this.listViewLog.Location = new System.Drawing.Point(0, 622);
            this.listViewLog.MultiSelect = false;
            this.listViewLog.Name = "listViewLog";
            this.listViewLog.Size = new System.Drawing.Size(665, 203);
            this.listViewLog.TabIndex = 4;
            this.listViewLog.UseCompatibleStateImageBehavior = false;
            this.listViewLog.View = System.Windows.Forms.View.Details;
            // 
            // columnlog
            // 
            this.columnlog.Text = "运行日志";
            this.columnlog.Width = 950;
            // 
            // lblTorp
            // 
            this.lblTorp.AutoSize = true;
            this.lblTorp.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTorp.Location = new System.Drawing.Point(11, 16);
            this.lblTorp.Name = "lblTorp";
            this.lblTorp.Size = new System.Drawing.Size(130, 24);
            this.lblTorp.TabIndex = 9;
            this.lblTorp.Text = "实时力矩：";
            // 
            // dataViewTd
            // 
            this.dataViewTd.AllowUserToAddRows = false;
            this.dataViewTd.AllowUserToDeleteRows = false;
            this.dataViewTd.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataViewTd.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataViewTd.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataViewTd.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataViewTd.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataViewTd.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataViewTd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataViewTd.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataViewTd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataViewTd.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataViewTd.Location = new System.Drawing.Point(3, 32);
            this.dataViewTd.MultiSelect = false;
            this.dataViewTd.Name = "dataViewTd";
            this.dataViewTd.ReadOnly = true;
            this.dataViewTd.RowHeadersWidth = 51;
            this.dataViewTd.RowTemplate.Height = 32;
            this.dataViewTd.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataViewTd.ShowCellToolTips = false;
            this.dataViewTd.ShowEditingIcon = false;
            this.dataViewTd.Size = new System.Drawing.Size(1215, 498);
            this.dataViewTd.TabIndex = 3;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(15, 208);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panelLeft);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panelTighten);
            this.splitContainer1.Size = new System.Drawing.Size(1890, 825);
            this.splitContainer1.SplitterDistance = 1221;
            this.splitContainer1.TabIndex = 10;
            // 
            // panelLeft
            // 
            this.panelLeft.Controls.Add(this.groupBox2);
            this.panelLeft.Controls.Add(this.panelEngine);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(1221, 822);
            this.panelLeft.TabIndex = 4;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dataViewTd);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("微软雅黑", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.Location = new System.Drawing.Point(0, 289);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1221, 533);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "拧紧过程数据";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1920, 1080);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panelFooter);
            this.Controls.Add(this.panelHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Padding = new System.Windows.Forms.Padding(15, 0, 15, 0);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "东本";
            this.panelHeader.ResumeLayout(false);
            this.panelFooter.ResumeLayout(false);
            this.panelFooter.PerformLayout();
            this.panelEngine.ResumeLayout(false);
            this.panelEngine.PerformLayout();
            this.panelPart.ResumeLayout(false);
            this.panelPart.PerformLayout();
            this.panelTighten.ResumeLayout(false);
            this.panelTighten.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataViewTd)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panelLeft.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label labelPrjTitle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panelFooter;
        private System.Windows.Forms.Label lblEngineCode;
        private System.Windows.Forms.Label lblEngintType;
        private System.Windows.Forms.Panel panelEngine;
        private System.Windows.Forms.Panel panelTighten;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblBindCode;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblBindStatus;
        private AE2Tightening.Frame.UserLabel lblResult;
        private System.Windows.Forms.Label lblAppName;
        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.DataGridView dataViewTd;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblMTO;
        private System.Windows.Forms.Label label2;
        private MonitorItem monitorShield;
        private MonitorItem monitorPLCNet;
        private MonitorItem monitorScanNet;
        private MonitorItem monitorTD1Net;
        private MonitorItem monitorRfidNet;
        private MonitorItem monitorAdamNet;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListView listViewLog;
        private System.Windows.Forms.ColumnHeader columnlog;
        private System.Windows.Forms.Label lblTorp;
        private System.Windows.Forms.GroupBox groupBox3;
        private MonitorItem monitorStopLine;
        private System.Windows.Forms.Panel panelPart;
        private System.Windows.Forms.Button btSyncConfig;
        private System.Windows.Forms.Button btEnableTool;
        private System.Windows.Forms.Label lblCurTorque;
        private System.Windows.Forms.Label lblCurAngle;
        private MonitorItem monitorTD2Net;
    }
}

