namespace BIA.ProjectCreatorWizard.UI
{
    partial class CompanyAndDesignOptionForm
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
            this.CompanyLabel = new System.Windows.Forms.Label();
            this.CompanyNameTextbox = new System.Windows.Forms.TextBox();
            this.ValidateButton = new System.Windows.Forms.Button();
            this.CompanyFilesPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.BrowseCompanyFilesPath = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CompanyLabel
            // 
            this.CompanyLabel.AutoSize = true;
            this.CompanyLabel.Location = new System.Drawing.Point(12, 12);
            this.CompanyLabel.Name = "CompanyLabel";
            this.CompanyLabel.Size = new System.Drawing.Size(88, 13);
            this.CompanyLabel.TabIndex = 0;
            this.CompanyLabel.Text = "Company Name :";
            // 
            // CompanyNameTextbox
            // 
            this.CompanyNameTextbox.Location = new System.Drawing.Point(179, 9);
            this.CompanyNameTextbox.Name = "CompanyNameTextbox";
            this.CompanyNameTextbox.Size = new System.Drawing.Size(180, 20);
            this.CompanyNameTextbox.TabIndex = 1;
            // 
            // ValidateButton
            // 
            this.ValidateButton.Location = new System.Drawing.Point(150, 84);
            this.ValidateButton.Name = "ValidateButton";
            this.ValidateButton.Size = new System.Drawing.Size(75, 23);
            this.ValidateButton.TabIndex = 7;
            this.ValidateButton.Text = "Valider";
            this.ValidateButton.UseVisualStyleBackColor = true;
            this.ValidateButton.Click += new System.EventHandler(this.ValidateButton_Click);
            // 
            // CompanyFilesPath
            // 
            this.CompanyFilesPath.Location = new System.Drawing.Point(179, 32);
            this.CompanyFilesPath.Name = "CompanyFilesPath";
            this.CompanyFilesPath.Size = new System.Drawing.Size(180, 20);
            this.CompanyFilesPath.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Path for company files";
            // 
            // BrowseCompanyFilesPath
            // 
            this.BrowseCompanyFilesPath.Location = new System.Drawing.Point(366, 32);
            this.BrowseCompanyFilesPath.Name = "BrowseCompanyFilesPath";
            this.BrowseCompanyFilesPath.Size = new System.Drawing.Size(34, 23);
            this.BrowseCompanyFilesPath.TabIndex = 11;
            this.BrowseCompanyFilesPath.Text = "...";
            this.BrowseCompanyFilesPath.UseVisualStyleBackColor = true;
            this.BrowseCompanyFilesPath.Click += new System.EventHandler(this.BrowseCompanyFilesPath_Click);
            // 
            // CompanyAndDesignOptionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(412, 124);
            this.Controls.Add(this.BrowseCompanyFilesPath);
            this.Controls.Add(this.CompanyFilesPath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ValidateButton);
            this.Controls.Add(this.CompanyNameTextbox);
            this.Controls.Add(this.CompanyLabel);
            this.Name = "CompanyAndDesignOptionForm";
            this.Text = "CompanyAndDesignOptionForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label CompanyLabel;
        private System.Windows.Forms.TextBox CompanyNameTextbox;
        private System.Windows.Forms.Button ValidateButton;
        private System.Windows.Forms.TextBox CompanyFilesPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BrowseCompanyFilesPath;
    }
}