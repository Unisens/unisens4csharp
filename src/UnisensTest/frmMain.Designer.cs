namespace UnisensTest
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
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.lblFileName = new System.Windows.Forms.Label();
            this.btnOpen = new System.Windows.Forms.Button();
            this.lblMsg = new System.Windows.Forms.Label();
            this.folderDialogue = new System.Windows.Forms.FolderBrowserDialog();
            this.btnSbin = new System.Windows.Forms.Button();
            this.btnSxml = new System.Windows.Forms.Button();
            this.btnExml = new System.Windows.Forms.Button();
            this.btnVxml = new System.Windows.Forms.Button();
            this.btnVcsv = new System.Windows.Forms.Button();
            this.btnEcsv = new System.Windows.Forms.Button();
            this.btnScsv = new System.Windows.Forms.Button();
            this.btnVbin = new System.Windows.Forms.Button();
            this.btnEbin = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(252, 44);
            this.txtPath.Name = "txtPath";
            this.txtPath.ReadOnly = true;
            this.txtPath.Size = new System.Drawing.Size(186, 21);
            this.txtPath.TabIndex = 0;
            // 
            // lblFileName
            // 
            this.lblFileName.AutoSize = true;
            this.lblFileName.Location = new System.Drawing.Point(49, 47);
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(197, 12);
            this.lblFileName.TabIndex = 1;
            this.lblFileName.Text = "Please select the path of files:";
            // 
            // btnOpen
            // 
            this.btnOpen.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOpen.Location = new System.Drawing.Point(444, 42);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(34, 23);
            this.btnOpen.TabIndex = 2;
            this.btnOpen.Text = "...";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // lblMsg
            // 
            this.lblMsg.AutoSize = true;
            this.lblMsg.Location = new System.Drawing.Point(167, 287);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(0, 12);
            this.lblMsg.TabIndex = 4;
            // 
            // btnSbin
            // 
            this.btnSbin.Location = new System.Drawing.Point(51, 111);
            this.btnSbin.Name = "btnSbin";
            this.btnSbin.Size = new System.Drawing.Size(75, 23);
            this.btnSbin.TabIndex = 5;
            this.btnSbin.Text = "Signal Bin";
            this.btnSbin.UseVisualStyleBackColor = true;
            this.btnSbin.Click += new System.EventHandler(this.btnSbin_Click);
            // 
            // btnSxml
            // 
            this.btnSxml.Location = new System.Drawing.Point(415, 111);
            this.btnSxml.Name = "btnSxml";
            this.btnSxml.Size = new System.Drawing.Size(75, 23);
            this.btnSxml.TabIndex = 6;
            this.btnSxml.Text = "Signal XML";
            this.btnSxml.UseVisualStyleBackColor = true;
            this.btnSxml.Click += new System.EventHandler(this.btnSxml_Click);
            // 
            // btnExml
            // 
            this.btnExml.Location = new System.Drawing.Point(415, 171);
            this.btnExml.Name = "btnExml";
            this.btnExml.Size = new System.Drawing.Size(75, 23);
            this.btnExml.TabIndex = 7;
            this.btnExml.Text = "Event XML";
            this.btnExml.UseVisualStyleBackColor = true;
            this.btnExml.Click += new System.EventHandler(this.btnExml_Click);
            // 
            // btnVxml
            // 
            this.btnVxml.Location = new System.Drawing.Point(415, 235);
            this.btnVxml.Name = "btnVxml";
            this.btnVxml.Size = new System.Drawing.Size(75, 23);
            this.btnVxml.TabIndex = 8;
            this.btnVxml.Text = "Value XML";
            this.btnVxml.UseVisualStyleBackColor = true;
            this.btnVxml.Click += new System.EventHandler(this.btnVxml_Click);
            // 
            // btnVcsv
            // 
            this.btnVcsv.Location = new System.Drawing.Point(231, 235);
            this.btnVcsv.Name = "btnVcsv";
            this.btnVcsv.Size = new System.Drawing.Size(75, 23);
            this.btnVcsv.TabIndex = 9;
            this.btnVcsv.Text = "Value Csv";
            this.btnVcsv.UseVisualStyleBackColor = true;
            this.btnVcsv.Click += new System.EventHandler(this.btnVcsv_Click);
            // 
            // btnEcsv
            // 
            this.btnEcsv.Location = new System.Drawing.Point(231, 171);
            this.btnEcsv.Name = "btnEcsv";
            this.btnEcsv.Size = new System.Drawing.Size(75, 23);
            this.btnEcsv.TabIndex = 10;
            this.btnEcsv.Text = "Event Csv";
            this.btnEcsv.UseVisualStyleBackColor = true;
            this.btnEcsv.Click += new System.EventHandler(this.btnEcsv_Click);
            // 
            // btnScsv
            // 
            this.btnScsv.Location = new System.Drawing.Point(231, 111);
            this.btnScsv.Name = "btnScsv";
            this.btnScsv.Size = new System.Drawing.Size(75, 23);
            this.btnScsv.TabIndex = 11;
            this.btnScsv.Text = "Signal Csv";
            this.btnScsv.UseVisualStyleBackColor = true;
            this.btnScsv.Click += new System.EventHandler(this.btnScsv_Click);
            // 
            // btnVbin
            // 
            this.btnVbin.Location = new System.Drawing.Point(51, 235);
            this.btnVbin.Name = "btnVbin";
            this.btnVbin.Size = new System.Drawing.Size(75, 23);
            this.btnVbin.TabIndex = 12;
            this.btnVbin.Text = "Value Bin";
            this.btnVbin.UseVisualStyleBackColor = true;
            this.btnVbin.Click += new System.EventHandler(this.btnVbin_Click);
            // 
            // btnEbin
            // 
            this.btnEbin.Location = new System.Drawing.Point(51, 171);
            this.btnEbin.Name = "btnEbin";
            this.btnEbin.Size = new System.Drawing.Size(75, 23);
            this.btnEbin.TabIndex = 13;
            this.btnEbin.Text = "Event Bin";
            this.btnEbin.UseVisualStyleBackColor = true;
            this.btnEbin.Click += new System.EventHandler(this.btnEbin_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 349);
            this.Controls.Add(this.btnEbin);
            this.Controls.Add(this.btnVbin);
            this.Controls.Add(this.btnScsv);
            this.Controls.Add(this.btnEcsv);
            this.Controls.Add(this.btnVcsv);
            this.Controls.Add(this.btnVxml);
            this.Controls.Add(this.btnExml);
            this.Controls.Add(this.btnSxml);
            this.Controls.Add(this.btnSbin);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.lblFileName);
            this.Controls.Add(this.txtPath);
            this.Name = "frmMain";
            this.Text = "Welcome to Unisens!";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Label lblFileName;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.FolderBrowserDialog folderDialogue;
        private System.Windows.Forms.Button btnSbin;
        private System.Windows.Forms.Button btnSxml;
        private System.Windows.Forms.Button btnExml;
        private System.Windows.Forms.Button btnVxml;
        private System.Windows.Forms.Button btnVcsv;
        private System.Windows.Forms.Button btnEcsv;
        private System.Windows.Forms.Button btnScsv;
        private System.Windows.Forms.Button btnVbin;
        private System.Windows.Forms.Button btnEbin;
    }
}

