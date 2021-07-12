namespace MouseWheel
{
	partial class MouseWheel
	{
		/// <summary>
		/// 必要なデザイナー変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows フォーム デザイナーで生成されたコード

		/// <summary>
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			this.listView1 = new System.Windows.Forms.ListView();
			this.ColumnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.ColumnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.ColumnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.btnDirMac = new System.Windows.Forms.Button();
			this.btnDirWin = new System.Windows.Forms.Button();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.tbox_verinfo = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// listView1
			// 
			this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.listView1.BackColor = System.Drawing.SystemColors.ControlLight;
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnHeader1,
            this.ColumnHeader2,
            this.ColumnHeader3});
			this.listView1.FullRowSelect = true;
			this.listView1.GridLines = true;
			this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.listView1.Location = new System.Drawing.Point(0, 0);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(568, 261);
			this.listView1.TabIndex = 2;
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
			// 
			// ColumnHeader1
			// 
			this.ColumnHeader1.Text = "Hardware ID";
			this.ColumnHeader1.Width = 360;
			// 
			// ColumnHeader2
			// 
			this.ColumnHeader2.Text = "WheelFlipFlop";
			this.ColumnHeader2.Width = 93;
			// 
			// ColumnHeader3
			// 
			this.ColumnHeader3.Text = "WaitWakeEnabled";
			this.ColumnHeader3.Width = 111;
			// 
			// btnDirMac
			// 
			this.btnDirMac.BackColor = System.Drawing.Color.Orchid;
			this.btnDirMac.Location = new System.Drawing.Point(93, 12);
			this.btnDirMac.Name = "btnDirMac";
			this.btnDirMac.Size = new System.Drawing.Size(75, 40);
			this.btnDirMac.TabIndex = 3;
			this.btnDirMac.Text = "Mac Wheel";
			this.btnDirMac.UseVisualStyleBackColor = false;
			this.btnDirMac.Click += new System.EventHandler(this.btnDirMac_Click);
			// 
			// btnDirWin
			// 
			this.btnDirWin.BackColor = System.Drawing.Color.SteelBlue;
			this.btnDirWin.Location = new System.Drawing.Point(12, 12);
			this.btnDirWin.Name = "btnDirWin";
			this.btnDirWin.Size = new System.Drawing.Size(75, 40);
			this.btnDirWin.TabIndex = 4;
			this.btnDirWin.Text = "Win Wheel";
			this.btnDirWin.UseVisualStyleBackColor = false;
			this.btnDirWin.Click += new System.EventHandler(this.btnDirWin_Click);
			// 
			// splitContainer1
			// 
			this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainer1.IsSplitterFixed = true;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.tbox_verinfo);
			this.splitContainer1.Panel1.Controls.Add(this.btnDirMac);
			this.splitContainer1.Panel1.Controls.Add(this.btnDirWin);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.listView1);
			this.splitContainer1.Size = new System.Drawing.Size(568, 323);
			this.splitContainer1.SplitterDistance = 58;
			this.splitContainer1.TabIndex = 5;
			// 
			// tbox_verinfo
			// 
			this.tbox_verinfo.BackColor = System.Drawing.SystemColors.ControlLight;
			this.tbox_verinfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tbox_verinfo.ForeColor = System.Drawing.SystemColors.WindowFrame;
			this.tbox_verinfo.Location = new System.Drawing.Point(174, 12);
			this.tbox_verinfo.Multiline = true;
			this.tbox_verinfo.Name = "tbox_verinfo";
			this.tbox_verinfo.ReadOnly = true;
			this.tbox_verinfo.Size = new System.Drawing.Size(382, 40);
			this.tbox_verinfo.TabIndex = 7;
			// 
			// MouseWheel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Window;
			this.ClientSize = new System.Drawing.Size(568, 323);
			this.Controls.Add(this.splitContainer1);
			this.HelpButton = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MouseWheel";
			this.Text = "Mouse wheel direction";
			this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.MouseWheel_HelpButtonClicked);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel1.PerformLayout();
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.Button btnDirMac;
		private System.Windows.Forms.Button btnDirWin;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.ColumnHeader ColumnHeader1;
		private System.Windows.Forms.ColumnHeader ColumnHeader2;
        private System.Windows.Forms.ColumnHeader ColumnHeader3;
		private System.Windows.Forms.TextBox tbox_verinfo;
	}
}

