namespace RansomNote.GUI
{
    partial class frmMain
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
            this.pnlExplorer = new System.Windows.Forms.Panel();
            this.fToolbar = new GongSolutions.Shell.FileDialogToolbar();
            this.shellSaveFolder = new GongSolutions.Shell.ShellView();
            this.placesToolbar1 = new GongSolutions.Shell.PlacesToolbar();
            this.label1 = new System.Windows.Forms.Label();
            this.lstConsole = new System.Windows.Forms.ListBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lblConsoleOut = new System.Windows.Forms.Label();
            this.picPreview = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.shellView1 = new GongSolutions.Shell.ShellView();
            this.btnLoad = new System.Windows.Forms.Button();
            this.pnlExplorer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlExplorer
            // 
            this.pnlExplorer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlExplorer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlExplorer.Controls.Add(this.fToolbar);
            this.pnlExplorer.Controls.Add(this.placesToolbar1);
            this.pnlExplorer.Controls.Add(this.shellSaveFolder);
            this.pnlExplorer.Location = new System.Drawing.Point(190, 103);
            this.pnlExplorer.Name = "pnlExplorer";
            this.pnlExplorer.Size = new System.Drawing.Size(718, 447);
            this.pnlExplorer.TabIndex = 0;
            // 
            // fToolbar
            // 
            this.fToolbar.AutoSize = true;
            this.fToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.fToolbar.Location = new System.Drawing.Point(0, 0);
            this.fToolbar.Name = "fToolbar";
            this.fToolbar.RootFolder = new GongSolutions.Shell.ShellItem("shell:///MyComputerFolder");
            this.fToolbar.SelectedFolder = new GongSolutions.Shell.ShellItem("shell:///MyComputerFolder");
            this.fToolbar.ShellView = this.shellSaveFolder;
            this.fToolbar.Size = new System.Drawing.Size(716, 31);
            this.fToolbar.TabIndex = 2;
            // 
            // shellSaveFolder
            // 
            this.shellSaveFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.shellSaveFolder.CurrentFolder = new GongSolutions.Shell.ShellItem("shell:///MyComputerFolder");
            this.shellSaveFolder.Location = new System.Drawing.Point(87, 37);
            this.shellSaveFolder.MultiSelect = false;
            this.shellSaveFolder.Name = "shellSaveFolder";
            this.shellSaveFolder.Size = new System.Drawing.Size(621, 409);
            this.shellSaveFolder.StatusBar = null;
            this.shellSaveFolder.TabIndex = 0;
            this.shellSaveFolder.Text = "Save Folder View";
            this.shellSaveFolder.View = GongSolutions.Shell.ShellViewStyle.Details;
            // 
            // placesToolbar1
            // 
            this.placesToolbar1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.placesToolbar1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.placesToolbar1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.placesToolbar1.Location = new System.Drawing.Point(-1, 37);
            this.placesToolbar1.Name = "placesToolbar1";
            this.placesToolbar1.ShellView = this.shellSaveFolder;
            this.placesToolbar1.Size = new System.Drawing.Size(86, 354);
            this.placesToolbar1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(197, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(308, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Navigate to the Folder You Wish to Save the Character Images.";
            // 
            // lstConsole
            // 
            this.lstConsole.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstConsole.FormattingEnabled = true;
            this.lstConsole.Location = new System.Drawing.Point(12, 28);
            this.lstConsole.Name = "lstConsole";
            this.lstConsole.Size = new System.Drawing.Size(895, 186);
            this.lstConsole.TabIndex = 2;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(10);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btnLoad);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.picPreview);
            this.splitContainer1.Panel1.Controls.Add(this.pnlExplorer);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.shellView1);
            this.splitContainer1.Panel2.Controls.Add(this.lblConsoleOut);
            this.splitContainer1.Panel2.Controls.Add(this.lstConsole);
            this.splitContainer1.Size = new System.Drawing.Size(920, 794);
            this.splitContainer1.SplitterDistance = 550;
            this.splitContainer1.TabIndex = 3;
            // 
            // lblConsoleOut
            // 
            this.lblConsoleOut.AutoSize = true;
            this.lblConsoleOut.Location = new System.Drawing.Point(12, 12);
            this.lblConsoleOut.Name = "lblConsoleOut";
            this.lblConsoleOut.Size = new System.Drawing.Size(45, 13);
            this.lblConsoleOut.TabIndex = 3;
            this.lblConsoleOut.Text = "Console";
            // 
            // picPreview
            // 
            this.picPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picPreview.Location = new System.Drawing.Point(12, 274);
            this.picPreview.Name = "picPreview";
            this.picPreview.Size = new System.Drawing.Size(173, 264);
            this.picPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picPreview.TabIndex = 2;
            this.picPreview.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 255);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Image Preview";
            // 
            // shellView1
            // 
            this.shellView1.Location = new System.Drawing.Point(795, 28);
            this.shellView1.Name = "shellView1";
            this.shellView1.Size = new System.Drawing.Size(8, 8);
            this.shellView1.StatusBar = null;
            this.shellView1.TabIndex = 4;
            this.shellView1.Text = "shellView1";
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(15, 103);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(169, 23);
            this.btnLoad.TabIndex = 4;
            this.btnLoad.Text = "Load Image";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(920, 794);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.Text = "Form1";
            this.pnlExplorer.ResumeLayout(false);
            this.pnlExplorer.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlExplorer;
        private GongSolutions.Shell.ShellView shellSaveFolder;
        private GongSolutions.Shell.PlacesToolbar placesToolbar1;
        private GongSolutions.Shell.FileDialogToolbar fToolbar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lstConsole;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label lblConsoleOut;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox picPreview;
        private GongSolutions.Shell.ShellView shellView1;
        private System.Windows.Forms.Button btnLoad;
    }
}

