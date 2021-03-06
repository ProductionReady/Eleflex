﻿namespace EleflexNuGetRelease
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtFolder = new System.Windows.Forms.TextBox();
            this.btnRelease = new System.Windows.Forms.Button();
            this.txtPreviousVersion = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNewVersion = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ckbPackage = new System.Windows.Forms.CheckBox();
            this.ckbPublish = new System.Windows.Forms.CheckBox();
            this.ckbCopyPackage = new System.Windows.Forms.CheckBox();
            this.ckbCopyDlls = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "NuGet Folder:";
            // 
            // txtFolder
            // 
            this.txtFolder.Location = new System.Drawing.Point(107, 12);
            this.txtFolder.Name = "txtFolder";
            this.txtFolder.Size = new System.Drawing.Size(376, 20);
            this.txtFolder.TabIndex = 0;
            this.txtFolder.Text = "C:\\Work\\TFS\\Source\\EleflexV3\\SourceCode\\Managed\\Release\\NuGet";
            // 
            // btnRelease
            // 
            this.btnRelease.Location = new System.Drawing.Point(408, 90);
            this.btnRelease.Name = "btnRelease";
            this.btnRelease.Size = new System.Drawing.Size(75, 23);
            this.btnRelease.TabIndex = 5;
            this.btnRelease.Text = "Release";
            this.btnRelease.UseVisualStyleBackColor = true;
            this.btnRelease.Click += new System.EventHandler(this.btnRelease_Click);
            // 
            // txtPreviousVersion
            // 
            this.txtPreviousVersion.Location = new System.Drawing.Point(107, 38);
            this.txtPreviousVersion.Name = "txtPreviousVersion";
            this.txtPreviousVersion.Size = new System.Drawing.Size(376, 20);
            this.txtPreviousVersion.TabIndex = 1;
            this.txtPreviousVersion.Text = "3.2.0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Previous Version:";
            // 
            // txtNewVersion
            // 
            this.txtNewVersion.Location = new System.Drawing.Point(107, 64);
            this.txtNewVersion.Name = "txtNewVersion";
            this.txtNewVersion.Size = new System.Drawing.Size(376, 20);
            this.txtNewVersion.TabIndex = 2;
            this.txtNewVersion.Text = "3.2.1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "New Version";
            // 
            // ckbPackage
            // 
            this.ckbPackage.AutoSize = true;
            this.ckbPackage.Location = new System.Drawing.Point(164, 117);
            this.ckbPackage.Name = "ckbPackage";
            this.ckbPackage.Size = new System.Drawing.Size(75, 17);
            this.ckbPackage.TabIndex = 3;
            this.ckbPackage.Text = "Package?";
            this.ckbPackage.UseVisualStyleBackColor = true;
            // 
            // ckbPublish
            // 
            this.ckbPublish.AutoSize = true;
            this.ckbPublish.Location = new System.Drawing.Point(262, 117);
            this.ckbPublish.Name = "ckbPublish";
            this.ckbPublish.Size = new System.Drawing.Size(66, 17);
            this.ckbPublish.TabIndex = 4;
            this.ckbPublish.Text = "Publish?";
            this.ckbPublish.UseVisualStyleBackColor = true;
            this.ckbPublish.CheckedChanged += new System.EventHandler(this.ckbPublish_CheckedChanged);
            // 
            // ckbCopyPackage
            // 
            this.ckbCopyPackage.AutoSize = true;
            this.ckbCopyPackage.Location = new System.Drawing.Point(12, 94);
            this.ckbCopyPackage.Name = "ckbCopyPackage";
            this.ckbCopyPackage.Size = new System.Drawing.Size(257, 17);
            this.ckbCopyPackage.TabIndex = 7;
            this.ckbCopyPackage.Text = "Copy Old Package to New and Update Nuspec?";
            this.ckbCopyPackage.UseVisualStyleBackColor = true;
            // 
            // ckbCopyDlls
            // 
            this.ckbCopyDlls.AutoSize = true;
            this.ckbCopyDlls.Location = new System.Drawing.Point(12, 117);
            this.ckbCopyDlls.Name = "ckbCopyDlls";
            this.ckbCopyDlls.Size = new System.Drawing.Size(109, 17);
            this.ckbCopyDlls.TabIndex = 8;
            this.ckbCopyDlls.Text = "Copy New DLLs?";
            this.ckbCopyDlls.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(501, 141);
            this.Controls.Add(this.ckbCopyDlls);
            this.Controls.Add(this.ckbCopyPackage);
            this.Controls.Add(this.ckbPublish);
            this.Controls.Add(this.ckbPackage);
            this.Controls.Add(this.txtNewVersion);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtPreviousVersion);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnRelease);
            this.Controls.Add(this.txtFolder);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ELEFLEX NuGet Package Release";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFolder;
        private System.Windows.Forms.Button btnRelease;
        private System.Windows.Forms.TextBox txtPreviousVersion;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNewVersion;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox ckbPackage;
        private System.Windows.Forms.CheckBox ckbPublish;
        private System.Windows.Forms.CheckBox ckbCopyPackage;
        private System.Windows.Forms.CheckBox ckbCopyDlls;
    }
}

