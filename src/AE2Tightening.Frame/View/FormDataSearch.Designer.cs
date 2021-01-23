namespace AE2Tightening.Frame.View
{
    partial class FormDataSearch
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimeStart = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.dateTimeEnd = new System.Windows.Forms.DateTimePicker();
            this.btSearch = new System.Windows.Forms.Button();
            this.btReset = new System.Windows.Forms.Button();
            this.btUp = new System.Windows.Forms.Button();
            this.btDown = new System.Windows.Forms.Button();
            this.lblPage = new System.Windows.Forms.Label();
            this.lblPageInfo = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btReset);
            this.panel1.Controls.Add(this.btSearch);
            this.panel1.Controls.Add(this.dateTimeEnd);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.dateTimeStart);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.textBoxCode);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1247, 120);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblPageInfo);
            this.panel2.Controls.Add(this.lblPage);
            this.panel2.Controls.Add(this.btDown);
            this.panel2.Controls.Add(this.btUp);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 711);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1247, 52);
            this.panel2.TabIndex = 1;
            // 
            // dataGridView
            // 
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.Location = new System.Drawing.Point(0, 120);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowTemplate.Height = 23;
            this.dataGridView.Size = new System.Drawing.Size(1247, 591);
            this.dataGridView.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "发动机号：";
            // 
            // textBoxCode
            // 
            this.textBoxCode.Location = new System.Drawing.Point(148, 12);
            this.textBoxCode.Name = "textBoxCode";
            this.textBoxCode.Size = new System.Drawing.Size(194, 32);
            this.textBoxCode.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(386, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "起始：";
            // 
            // dateTimeStart
            // 
            this.dateTimeStart.CustomFormat = "yyyy/MM/dd HH:mm";
            this.dateTimeStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeStart.Location = new System.Drawing.Point(459, 12);
            this.dateTimeStart.Name = "dateTimeStart";
            this.dateTimeStart.Size = new System.Drawing.Size(208, 32);
            this.dateTimeStart.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(713, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 25);
            this.label3.TabIndex = 2;
            this.label3.Text = "截止：";
            // 
            // dateTimeEnd
            // 
            this.dateTimeEnd.CustomFormat = "yyyy/MM/dd HH:mm";
            this.dateTimeEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeEnd.Location = new System.Drawing.Point(786, 12);
            this.dateTimeEnd.Name = "dateTimeEnd";
            this.dateTimeEnd.Size = new System.Drawing.Size(208, 32);
            this.dateTimeEnd.TabIndex = 3;
            // 
            // btSearch
            // 
            this.btSearch.BackColor = System.Drawing.Color.RoyalBlue;
            this.btSearch.FlatAppearance.BorderSize = 0;
            this.btSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btSearch.ForeColor = System.Drawing.Color.White;
            this.btSearch.Location = new System.Drawing.Point(47, 72);
            this.btSearch.Name = "btSearch";
            this.btSearch.Size = new System.Drawing.Size(102, 40);
            this.btSearch.TabIndex = 4;
            this.btSearch.Text = "查询";
            this.btSearch.UseVisualStyleBackColor = false;
            this.btSearch.Click += new System.EventHandler(this.btSearch_Click);
            // 
            // btReset
            // 
            this.btReset.BackColor = System.Drawing.Color.RoyalBlue;
            this.btReset.FlatAppearance.BorderSize = 0;
            this.btReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btReset.ForeColor = System.Drawing.Color.White;
            this.btReset.Location = new System.Drawing.Point(187, 72);
            this.btReset.Name = "btReset";
            this.btReset.Size = new System.Drawing.Size(102, 40);
            this.btReset.TabIndex = 4;
            this.btReset.Text = "重置";
            this.btReset.UseVisualStyleBackColor = false;
            this.btReset.Click += new System.EventHandler(this.btReset_Click);
            // 
            // btUp
            // 
            this.btUp.BackColor = System.Drawing.Color.RoyalBlue;
            this.btUp.Enabled = false;
            this.btUp.FlatAppearance.BorderSize = 0;
            this.btUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btUp.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btUp.ForeColor = System.Drawing.Color.White;
            this.btUp.Location = new System.Drawing.Point(559, 5);
            this.btUp.Name = "btUp";
            this.btUp.Size = new System.Drawing.Size(44, 40);
            this.btUp.TabIndex = 0;
            this.btUp.Text = "<";
            this.btUp.UseVisualStyleBackColor = false;
            this.btUp.Click += new System.EventHandler(this.btUp_Click);
            // 
            // btDown
            // 
            this.btDown.BackColor = System.Drawing.Color.RoyalBlue;
            this.btDown.Enabled = false;
            this.btDown.FlatAppearance.BorderSize = 0;
            this.btDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btDown.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btDown.ForeColor = System.Drawing.Color.White;
            this.btDown.Location = new System.Drawing.Point(689, 5);
            this.btDown.Name = "btDown";
            this.btDown.Size = new System.Drawing.Size(44, 40);
            this.btDown.TabIndex = 0;
            this.btDown.Text = ">";
            this.btDown.UseVisualStyleBackColor = false;
            this.btDown.Click += new System.EventHandler(this.btDown_Click);
            // 
            // lblPage
            // 
            this.lblPage.Location = new System.Drawing.Point(609, 7);
            this.lblPage.Name = "lblPage";
            this.lblPage.Size = new System.Drawing.Size(74, 37);
            this.lblPage.TabIndex = 1;
            this.lblPage.Text = "1";
            this.lblPage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPageInfo
            // 
            this.lblPageInfo.AutoSize = true;
            this.lblPageInfo.Location = new System.Drawing.Point(739, 13);
            this.lblPageInfo.Name = "lblPageInfo";
            this.lblPageInfo.Size = new System.Drawing.Size(118, 25);
            this.lblPageInfo.TabIndex = 2;
            this.lblPageInfo.Text = "总共0条数据";
            // 
            // FormDataSearch
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1247, 763);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "FormDataSearch";
            this.Text = "FormDataSearch";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DateTimePicker dateTimeEnd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dateTimeStart;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Button btReset;
        private System.Windows.Forms.Button btSearch;
        private System.Windows.Forms.Button btUp;
        private System.Windows.Forms.Button btDown;
        private System.Windows.Forms.Label lblPage;
        private System.Windows.Forms.Label lblPageInfo;
    }
}