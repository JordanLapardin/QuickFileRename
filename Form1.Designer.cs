namespace QuickFileRename
{
    partial class Form1
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
            this.lblFolder = new System.Windows.Forms.Label();
            this.txtFolderPath = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.lblPattern = new System.Windows.Forms.Label();
            this.txtPattern = new System.Windows.Forms.TextBox();
            this.lblStartNumber = new System.Windows.Forms.Label();
            this.numStartNumber = new System.Windows.Forms.NumericUpDown();
            this.lblPreview = new System.Windows.Forms.Label();
            this.lvFiles = new System.Windows.Forms.ListView();
            this.colFileName = new System.Windows.Forms.ColumnHeader();
            this.colNewName = new System.Windows.Forms.ColumnHeader();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnRename = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOptions = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.folderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            ((System.ComponentModel.ISupportInitialize)(this.numStartNumber)).BeginInit();
            this.SuspendLayout();
            // 
            // lblFolder
            // 
            this.lblFolder.AutoSize = true;
            this.lblFolder.Location = new System.Drawing.Point(12, 15);
            this.lblFolder.Name = "lblFolder";
            this.lblFolder.Size = new System.Drawing.Size(39, 13);
            this.lblFolder.TabIndex = 0;
            this.lblFolder.Text = "Folder:";
            // 
            // txtFolderPath
            // 
            this.txtFolderPath.Location = new System.Drawing.Point(60, 12);
            this.txtFolderPath.Name = "txtFolderPath";
            this.txtFolderPath.ReadOnly = false;
            this.txtFolderPath.Size = new System.Drawing.Size(650, 20);
            this.txtFolderPath.TabIndex = 1;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(716, 11);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(72, 23);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // lblPattern
            // 
            this.lblPattern.AutoSize = true;
            this.lblPattern.Location = new System.Drawing.Point(12, 42);
            this.lblPattern.Name = "lblPattern";
            this.lblPattern.Size = new System.Drawing.Size(41, 13);
            this.lblPattern.TabIndex = 3;
            this.lblPattern.Text = "Pattern";
            // 
            // txtPattern
            // 
            this.txtPattern.Location = new System.Drawing.Point(60, 39);
            this.txtPattern.Name = "txtPattern";
            this.txtPattern.Size = new System.Drawing.Size(328, 20);
            this.txtPattern.TabIndex = 4;
            this.txtPattern.Text = "[FileName] Episode {0}";
            this.txtPattern.TextChanged += new System.EventHandler(this.txtPattern_TextChanged);
            // 
            // lblStartNumber
            // 
            this.lblStartNumber.AutoSize = true;
            this.lblStartNumber.Location = new System.Drawing.Point(408, 42);
            this.lblStartNumber.Name = "lblStartNumber";
            this.lblStartNumber.Size = new System.Drawing.Size(72, 13);
            this.lblStartNumber.TabIndex = 5;
            this.lblStartNumber.Text = "Start Number:";
            // 
            // numStartNumber
            // 
            this.numStartNumber.Location = new System.Drawing.Point(486, 40);
            this.numStartNumber.Name = "numStartNumber";
            this.numStartNumber.Size = new System.Drawing.Size(60, 20);
            this.numStartNumber.TabIndex = 6;
            this.numStartNumber.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numStartNumber.ValueChanged += new System.EventHandler(this.numStartNumber_ValueChanged);
            // 
            // lblPreview
            // 
            this.lblPreview.AutoSize = true;
            this.lblPreview.Location = new System.Drawing.Point(12, 70);
            this.lblPreview.Name = "lblPreview";
            this.lblPreview.Size = new System.Drawing.Size(138, 13);
            this.lblPreview.TabIndex = 7;
            this.lblPreview.Text = "Preview (Original - New Name)";
            // 
            // lvFiles
            // 
            this.lvFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colFileName,
            this.colNewName});
            this.lvFiles.FullRowSelect = true;
            this.lvFiles.GridLines = true;
            this.lvFiles.Location = new System.Drawing.Point(12, 86);
            this.lvFiles.Name = "lvFiles";
            this.lvFiles.Size = new System.Drawing.Size(776, 297);
            this.lvFiles.TabIndex = 8;
            this.lvFiles.UseCompatibleStateImageBehavior = false;
            this.lvFiles.View = System.Windows.Forms.View.Details;
            // 
            // colFileName
            // 
            this.colFileName.Text = "Original Name";
            this.colFileName.Width = 380;
            // 
            // colNewName
            // 
            this.colNewName.Text = "New Name";
            this.colNewName.Width = 380;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(552, 39);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(70, 23);
            this.btnRefresh.TabIndex = 9;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnRename
            // 
            this.btnRename.Location = new System.Drawing.Point(628, 389);
            this.btnRename.Name = "btnRename";
            this.btnRename.Size = new System.Drawing.Size(75, 23);
            this.btnRename.TabIndex = 10;
            this.btnRename.Text = "Rename";
            this.btnRename.UseVisualStyleBackColor = true;
            this.btnRename.Click += new System.EventHandler(this.btnRename_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(709, 389);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Exit";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOptions
            // 
            this.btnOptions.Location = new System.Drawing.Point(628, 39);
            this.btnOptions.Name = "btnOptions";
            this.btnOptions.Size = new System.Drawing.Size(70, 23);
            this.btnOptions.TabIndex = 12;
            this.btnOptions.Text = "Options";
            this.btnOptions.UseVisualStyleBackColor = true;
            this.btnOptions.Click += new System.EventHandler(this.btnOptions_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(12, 394);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(38, 13);
            this.lblStatus.TabIndex = 13;
            this.lblStatus.Text = "Ready.";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 424);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnRename);
            this.Controls.Add(this.btnOptions);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.lvFiles);
            this.Controls.Add(this.lblPreview);
            this.Controls.Add(this.numStartNumber);
            this.Controls.Add(this.lblStartNumber);
            this.Controls.Add(this.txtPattern);
            this.Controls.Add(this.lblPattern);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtFolderPath);
            this.Controls.Add(this.lblFolder);
            this.Name = "Form1";
            this.Text = "Quick File Rename";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numStartNumber)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblFolder;
        private System.Windows.Forms.TextBox txtFolderPath;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Label lblPattern;
        private System.Windows.Forms.TextBox txtPattern;
        private System.Windows.Forms.Label lblStartNumber;
        private System.Windows.Forms.NumericUpDown numStartNumber;
        private System.Windows.Forms.Label lblPreview;
        private System.Windows.Forms.ListView lvFiles;
        private System.Windows.Forms.ColumnHeader colFileName;
        private System.Windows.Forms.ColumnHeader colNewName;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnRename;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOptions;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.FolderBrowserDialog folderBrowser;
    }
}

