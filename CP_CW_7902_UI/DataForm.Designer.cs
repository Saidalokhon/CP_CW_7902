namespace CP_CW_7902_UI
{
    partial class DataForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lbl_ProgressText = new System.Windows.Forms.Label();
            this.pgb_Progress = new System.Windows.Forms.ProgressBar();
            this.btn_Start = new System.Windows.Forms.Button();
            this.dgv_Terminals = new System.Windows.Forms.DataGridView();
            this.Ip = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btn_Update = new System.Windows.Forms.Button();
            this.btn_Clear = new System.Windows.Forms.Button();
            this.dgv_Swipes = new System.Windows.Forms.DataGridView();
            this.SwipeId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Direction = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TerminalIp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bgw_MainBGWorker = new System.ComponentModel.BackgroundWorker();
            this.bgw_UpdateDatabaseBGWorker = new System.ComponentModel.BackgroundWorker();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Terminals)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Swipes)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(1, 1);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(799, 447);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.TabStop = false;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lbl_ProgressText);
            this.tabPage1.Controls.Add(this.pgb_Progress);
            this.tabPage1.Controls.Add(this.btn_Start);
            this.tabPage1.Controls.Add(this.dgv_Terminals);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(791, 414);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Terminals";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lbl_ProgressText
            // 
            this.lbl_ProgressText.AutoSize = true;
            this.lbl_ProgressText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_ProgressText.Location = new System.Drawing.Point(331, 382);
            this.lbl_ProgressText.Name = "lbl_ProgressText";
            this.lbl_ProgressText.Size = new System.Drawing.Size(0, 20);
            this.lbl_ProgressText.TabIndex = 3;
            // 
            // pgb_Progress
            // 
            this.pgb_Progress.Location = new System.Drawing.Point(3, 377);
            this.pgb_Progress.Name = "pgb_Progress";
            this.pgb_Progress.Size = new System.Drawing.Size(322, 31);
            this.pgb_Progress.TabIndex = 2;
            // 
            // btn_Start
            // 
            this.btn_Start.BackColor = System.Drawing.Color.Transparent;
            this.btn_Start.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Start.Location = new System.Drawing.Point(656, 377);
            this.btn_Start.Name = "btn_Start";
            this.btn_Start.Size = new System.Drawing.Size(129, 31);
            this.btn_Start.TabIndex = 1;
            this.btn_Start.Text = "Start";
            this.btn_Start.UseVisualStyleBackColor = false;
            this.btn_Start.Click += new System.EventHandler(this.btn_Start_Click);
            // 
            // dgv_Terminals
            // 
            this.dgv_Terminals.AllowUserToAddRows = false;
            this.dgv_Terminals.AllowUserToDeleteRows = false;
            this.dgv_Terminals.AllowUserToResizeColumns = false;
            this.dgv_Terminals.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(3);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_Terminals.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_Terminals.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Terminals.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Ip,
            this.Status});
            this.dgv_Terminals.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgv_Terminals.Location = new System.Drawing.Point(0, 0);
            this.dgv_Terminals.Name = "dgv_Terminals";
            this.dgv_Terminals.ReadOnly = true;
            this.dgv_Terminals.RowHeadersVisible = false;
            this.dgv_Terminals.ShowCellErrors = false;
            this.dgv_Terminals.ShowCellToolTips = false;
            this.dgv_Terminals.ShowEditingIcon = false;
            this.dgv_Terminals.ShowRowErrors = false;
            this.dgv_Terminals.Size = new System.Drawing.Size(791, 371);
            this.dgv_Terminals.TabIndex = 0;
            // 
            // Ip
            // 
            this.Ip.Frozen = true;
            this.Ip.HeaderText = "IP";
            this.Ip.Name = "Ip";
            this.Ip.ReadOnly = true;
            this.Ip.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Ip.Width = 395;
            // 
            // Status
            // 
            this.Status.Frozen = true;
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            this.Status.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Status.Width = 396;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btn_Update);
            this.tabPage2.Controls.Add(this.btn_Clear);
            this.tabPage2.Controls.Add(this.dgv_Swipes);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(791, 414);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Swipes";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btn_Update
            // 
            this.btn_Update.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Update.Location = new System.Drawing.Point(654, 377);
            this.btn_Update.Name = "btn_Update";
            this.btn_Update.Size = new System.Drawing.Size(129, 31);
            this.btn_Update.TabIndex = 3;
            this.btn_Update.Text = "Update";
            this.btn_Update.UseVisualStyleBackColor = true;
            this.btn_Update.Click += new System.EventHandler(this.btn_Update_Click);
            // 
            // btn_Clear
            // 
            this.btn_Clear.BackColor = System.Drawing.Color.IndianRed;
            this.btn_Clear.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Clear.Location = new System.Drawing.Point(519, 377);
            this.btn_Clear.Name = "btn_Clear";
            this.btn_Clear.Size = new System.Drawing.Size(129, 31);
            this.btn_Clear.TabIndex = 2;
            this.btn_Clear.Text = "Clear";
            this.btn_Clear.UseVisualStyleBackColor = false;
            this.btn_Clear.Click += new System.EventHandler(this.btn_Clear_Click);
            // 
            // dgv_Swipes
            // 
            this.dgv_Swipes.AllowUserToAddRows = false;
            this.dgv_Swipes.AllowUserToDeleteRows = false;
            this.dgv_Swipes.AllowUserToResizeColumns = false;
            this.dgv_Swipes.AllowUserToResizeRows = false;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(3);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_Swipes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_Swipes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Swipes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SwipeId,
            this.Time,
            this.Direction,
            this.TerminalIp});
            this.dgv_Swipes.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgv_Swipes.Location = new System.Drawing.Point(0, 0);
            this.dgv_Swipes.Name = "dgv_Swipes";
            this.dgv_Swipes.ReadOnly = true;
            this.dgv_Swipes.RowHeadersVisible = false;
            this.dgv_Swipes.ShowCellErrors = false;
            this.dgv_Swipes.ShowCellToolTips = false;
            this.dgv_Swipes.ShowEditingIcon = false;
            this.dgv_Swipes.ShowRowErrors = false;
            this.dgv_Swipes.Size = new System.Drawing.Size(791, 371);
            this.dgv_Swipes.TabIndex = 1;
            // 
            // SwipeId
            // 
            this.SwipeId.Frozen = true;
            this.SwipeId.HeaderText = "Swipe ID";
            this.SwipeId.Name = "SwipeId";
            this.SwipeId.ReadOnly = true;
            this.SwipeId.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.SwipeId.Width = 197;
            // 
            // Time
            // 
            this.Time.Frozen = true;
            this.Time.HeaderText = "Time";
            this.Time.Name = "Time";
            this.Time.ReadOnly = true;
            this.Time.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Time.Width = 197;
            // 
            // Direction
            // 
            this.Direction.Frozen = true;
            this.Direction.HeaderText = "Direction";
            this.Direction.Name = "Direction";
            this.Direction.ReadOnly = true;
            this.Direction.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Direction.Width = 197;
            // 
            // TerminalIp
            // 
            this.TerminalIp.Frozen = true;
            this.TerminalIp.HeaderText = "Terminal IP";
            this.TerminalIp.Name = "TerminalIp";
            this.TerminalIp.ReadOnly = true;
            this.TerminalIp.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.TerminalIp.Width = 200;
            // 
            // bgw_MainBGWorker
            // 
            this.bgw_MainBGWorker.WorkerReportsProgress = true;
            this.bgw_MainBGWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgw_MainBGWorker_DoWork);
            this.bgw_MainBGWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgw_MainBGWorker_ProgressChanged);
            this.bgw_MainBGWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgw_MainBGWorker_RunWorkerCompleted);
            // 
            // bgw_UpdateDatabaseBGWorker
            // 
            this.bgw_UpdateDatabaseBGWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgw_DatabaseBGWorker_DoWork);
            // 
            // DataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControl1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(816, 489);
            this.MinimumSize = new System.Drawing.Size(816, 489);
            this.Name = "DataForm";
            this.Text = "Terminals controller";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Terminals)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Swipes)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dgv_Terminals;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ip;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridView dgv_Swipes;
        private System.Windows.Forms.Button btn_Start;
        private System.ComponentModel.BackgroundWorker bgw_MainBGWorker;
        private System.Windows.Forms.ProgressBar pgb_Progress;
        private System.Windows.Forms.Label lbl_ProgressText;
        private System.Windows.Forms.Button btn_Update;
        private System.Windows.Forms.Button btn_Clear;
        private System.Windows.Forms.DataGridViewTextBoxColumn SwipeId;
        private System.Windows.Forms.DataGridViewTextBoxColumn Time;
        private System.Windows.Forms.DataGridViewTextBoxColumn Direction;
        private System.Windows.Forms.DataGridViewTextBoxColumn TerminalIp;
        private System.ComponentModel.BackgroundWorker bgw_UpdateDatabaseBGWorker;
    }
}

