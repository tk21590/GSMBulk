namespace GSMBulk
{
    partial class GSMBULK
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
            this.components = new System.ComponentModel.Container();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.lb_COMConnect = new System.Windows.Forms.Label();
            this.btn_Ussd = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.txt_SMS = new System.Windows.Forms.TextBox();
            this.txt_Number = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.btn_refresh = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_Ussd = new System.Windows.Forms.TextBox();
            this.btn_SMS = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.SelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chọnToànBộĐãCóSốToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chọnHếtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bỏChọnHếtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(900, 112);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(203, 417);
            this.textBox1.TabIndex = 0;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column9});
            this.dataGridView1.Location = new System.Drawing.Point(2, 112);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(901, 417);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentDoubleClick);
            this.dataGridView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dataGridView1_MouseDown);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "ALL";
            this.Column1.Name = "Column1";
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column1.Width = 40;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "#";
            this.Column2.Name = "Column2";
            this.Column2.Width = 50;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "COM";
            this.Column3.Name = "Column3";
            this.Column3.Width = 60;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Status";
            this.Column4.Name = "Column4";
            this.Column4.Width = 80;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Phone";
            this.Column5.Name = "Column5";
            this.Column5.Width = 80;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Time";
            this.Column6.Name = "Column6";
            // 
            // Column7
            // 
            this.Column7.HeaderText = "From";
            this.Column7.Name = "Column7";
            this.Column7.Width = 80;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "OTP";
            this.Column8.Name = "Column8";
            this.Column8.Width = 60;
            // 
            // Column9
            // 
            this.Column9.HeaderText = "SMS";
            this.Column9.Name = "Column9";
            this.Column9.Width = 200;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.lb_COMConnect);
            this.groupBox1.Controls.Add(this.btn_Ussd);
            this.groupBox1.Controls.Add(this.button4);
            this.groupBox1.Controls.Add(this.txt_SMS);
            this.groupBox1.Controls.Add(this.txt_Number);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.btn_refresh);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txt_Ussd);
            this.groupBox1.Controls.Add(this.btn_SMS);
            this.groupBox1.Location = new System.Drawing.Point(2, -2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1097, 108);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.TextChanged += new System.EventHandler(this.groupBox1_TextChanged);
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(589, 17);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 20;
            this.button1.Text = "USSD";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lb_COMConnect
            // 
            this.lb_COMConnect.AutoSize = true;
            this.lb_COMConnect.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lb_COMConnect.Location = new System.Drawing.Point(895, 16);
            this.lb_COMConnect.Name = "lb_COMConnect";
            this.lb_COMConnect.Size = new System.Drawing.Size(99, 13);
            this.lb_COMConnect.TabIndex = 19;
            this.lb_COMConnect.Text = "COM dang kết nối :";
            // 
            // btn_Ussd
            // 
            this.btn_Ussd.BackColor = System.Drawing.Color.Transparent;
            this.btn_Ussd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Ussd.Location = new System.Drawing.Point(407, 14);
            this.btn_Ussd.Name = "btn_Ussd";
            this.btn_Ussd.Size = new System.Drawing.Size(75, 23);
            this.btn_Ussd.TabIndex = 4;
            this.btn_Ussd.Text = "USSD";
            this.btn_Ussd.UseVisualStyleBackColor = false;
            this.btn_Ussd.Click += new System.EventHandler(this.btn_Ussd_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.Transparent;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Location = new System.Drawing.Point(166, 16);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 12;
            this.button4.Text = "Nhắn tin";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // txt_SMS
            // 
            this.txt_SMS.Location = new System.Drawing.Point(242, 41);
            this.txt_SMS.Multiline = true;
            this.txt_SMS.Name = "txt_SMS";
            this.txt_SMS.Size = new System.Drawing.Size(126, 61);
            this.txt_SMS.TabIndex = 13;
            this.txt_SMS.TextChanged += new System.EventHandler(this.txt_SMS_TextChanged);
            // 
            // txt_Number
            // 
            this.txt_Number.Location = new System.Drawing.Point(243, 17);
            this.txt_Number.Name = "txt_Number";
            this.txt_Number.Size = new System.Drawing.Size(125, 20);
            this.txt_Number.TabIndex = 18;
            this.txt_Number.TextChanged += new System.EventHandler(this.txt_Number_TextChanged);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Transparent;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(85, 46);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "Xóa SMS";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btn_refresh
            // 
            this.btn_refresh.BackColor = System.Drawing.Color.Transparent;
            this.btn_refresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_refresh.Location = new System.Drawing.Point(4, 16);
            this.btn_refresh.Name = "btn_refresh";
            this.btn_refresh.Size = new System.Drawing.Size(75, 23);
            this.btn_refresh.TabIndex = 1;
            this.btn_refresh.Text = "Refresh";
            this.btn_refresh.UseVisualStyleBackColor = false;
            this.btn_refresh.Click += new System.EventHandler(this.btn_refresh_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(439, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(19, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "00";
            // 
            // txt_Ussd
            // 
            this.txt_Ussd.Location = new System.Drawing.Point(488, 17);
            this.txt_Ussd.Name = "txt_Ussd";
            this.txt_Ussd.Size = new System.Drawing.Size(82, 20);
            this.txt_Ussd.TabIndex = 3;
            this.txt_Ussd.TextChanged += new System.EventHandler(this.txt_Ussd_TextChanged);
            // 
            // btn_SMS
            // 
            this.btn_SMS.BackColor = System.Drawing.Color.Transparent;
            this.btn_SMS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_SMS.Location = new System.Drawing.Point(85, 17);
            this.btn_SMS.Name = "btn_SMS";
            this.btn_SMS.Size = new System.Drawing.Size(75, 23);
            this.btn_SMS.TabIndex = 5;
            this.btn_SMS.Text = "SMS";
            this.btn_SMS.UseVisualStyleBackColor = false;
            this.btn_SMS.Click += new System.EventHandler(this.btn_SMS_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SelectedToolStripMenuItem,
            this.chọnToànBộĐãCóSốToolStripMenuItem,
            this.chọnHếtToolStripMenuItem,
            this.bỏChọnHếtToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(208, 92);
            this.contextMenuStrip1.Text = "Chọn toàn bộ có số";
            // 
            // SelectedToolStripMenuItem
            // 
            this.SelectedToolStripMenuItem.Name = "SelectedToolStripMenuItem";
            this.SelectedToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.SelectedToolStripMenuItem.Text = "Chọn toàn bộ chưa có số";
            this.SelectedToolStripMenuItem.Click += new System.EventHandler(this.SelectedToolStripMenuItem_Click);
            // 
            // chọnToànBộĐãCóSốToolStripMenuItem
            // 
            this.chọnToànBộĐãCóSốToolStripMenuItem.Name = "chọnToànBộĐãCóSốToolStripMenuItem";
            this.chọnToànBộĐãCóSốToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.chọnToànBộĐãCóSốToolStripMenuItem.Text = "Chọn toàn bộ đã có số";
            this.chọnToànBộĐãCóSốToolStripMenuItem.Click += new System.EventHandler(this.Selected2ToolStripMenuItem_Click);
            // 
            // chọnHếtToolStripMenuItem
            // 
            this.chọnHếtToolStripMenuItem.Name = "chọnHếtToolStripMenuItem";
            this.chọnHếtToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.chọnHếtToolStripMenuItem.Text = "Chọn hết";
            this.chọnHếtToolStripMenuItem.Click += new System.EventHandler(this.Selected3ToolStripMenuItem_Click);
            // 
            // bỏChọnHếtToolStripMenuItem
            // 
            this.bỏChọnHếtToolStripMenuItem.Name = "bỏChọnHếtToolStripMenuItem";
            this.bỏChọnHếtToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.bỏChọnHếtToolStripMenuItem.Text = "Bỏ chọn hết";
            this.bỏChọnHếtToolStripMenuItem.Click += new System.EventHandler(this.Selected4ToolStripMenuItem_Click);
            // 
            // GSMBULK
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.OrangeRed;
            this.ClientSize = new System.Drawing.Size(1103, 530);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.textBox1);
            this.Name = "GSMBULK";
            this.Text = "GSMBULK";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.Button btn_Ussd;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox txt_SMS;
        private System.Windows.Forms.TextBox txt_Number;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btn_refresh;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_Ussd;
        private System.Windows.Forms.Button btn_SMS;
        private System.Windows.Forms.Label lb_COMConnect;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem SelectedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem chọnToànBộĐãCóSốToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem chọnHếtToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bỏChọnHếtToolStripMenuItem;
        private System.Windows.Forms.Button button1;
    }
}

