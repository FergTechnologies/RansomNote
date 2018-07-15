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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFolderPath = new System.Windows.Forms.TextBox();
            this.btnFolder = new System.Windows.Forms.Button();
            this.txtImagePath = new System.Windows.Forms.TextBox();
            this.btnLoad = new System.Windows.Forms.Button();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.shellView1 = new GongSolutions.Shell.ShellView();
            this.lblConsoleOut = new System.Windows.Forms.Label();
            this.btnExport = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.btnExport);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.txtFolderPath);
            this.splitContainer1.Panel1.Controls.Add(this.btnFolder);
            this.splitContainer1.Panel1.Controls.Add(this.txtImagePath);
            this.splitContainer1.Panel1.Controls.Add(this.btnLoad);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.txtOutput);
            this.splitContainer1.Panel2.Controls.Add(this.shellView1);
            this.splitContainer1.Panel2.Controls.Add(this.lblConsoleOut);
            this.splitContainer1.Size = new System.Drawing.Size(698, 794);
            this.splitContainer1.SplitterDistance = 209;
            this.splitContainer1.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Folder to Save Results";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Image To Extract From";
            // 
            // txtFolderPath
            // 
            this.txtFolderPath.Location = new System.Drawing.Point(15, 104);
            this.txtFolderPath.Name = "txtFolderPath";
            this.txtFolderPath.Size = new System.Drawing.Size(671, 20);
            this.txtFolderPath.TabIndex = 8;
            // 
            // btnFolder
            // 
            this.btnFolder.Location = new System.Drawing.Point(606, 130);
            this.btnFolder.Name = "btnFolder";
            this.btnFolder.Size = new System.Drawing.Size(80, 23);
            this.btnFolder.TabIndex = 7;
            this.btnFolder.Text = "Select Folder";
            this.btnFolder.UseVisualStyleBackColor = true;
            this.btnFolder.Click += new System.EventHandler(this.btnFolder_Click);
            // 
            // txtImagePath
            // 
            this.txtImagePath.Location = new System.Drawing.Point(15, 28);
            this.txtImagePath.Name = "txtImagePath";
            this.txtImagePath.Size = new System.Drawing.Size(671, 20);
            this.txtImagePath.TabIndex = 6;
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(606, 54);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(80, 23);
            this.btnLoad.TabIndex = 4;
            this.btnLoad.Text = "Load Image";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // txtOutput
            // 
            this.txtOutput.BackColor = System.Drawing.SystemColors.ControlLight;
            this.txtOutput.ForeColor = System.Drawing.Color.DimGray;
            this.txtOutput.Location = new System.Drawing.Point(12, 28);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtOutput.Size = new System.Drawing.Size(674, 541);
            this.txtOutput.TabIndex = 5;
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
            // lblConsoleOut
            // 
            this.lblConsoleOut.AutoSize = true;
            this.lblConsoleOut.Location = new System.Drawing.Point(12, 12);
            this.lblConsoleOut.Name = "lblConsoleOut";
            this.lblConsoleOut.Size = new System.Drawing.Size(45, 13);
            this.lblConsoleOut.TabIndex = 3;
            this.lblConsoleOut.Text = "Console";
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(502, 171);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(184, 23);
            this.btnExport.TabIndex = 11;
            this.btnExport.Text = "Export Character Images";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(698, 794);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMain";
            this.Text = "Ransom Note";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label lblConsoleOut;
        private GongSolutions.Shell.ShellView shellView1;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFolderPath;
        private System.Windows.Forms.Button btnFolder;
        private System.Windows.Forms.TextBox txtImagePath;
        private System.Windows.Forms.Button btnExport;
    }
}

