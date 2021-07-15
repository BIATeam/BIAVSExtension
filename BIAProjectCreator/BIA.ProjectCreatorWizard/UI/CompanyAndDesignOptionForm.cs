// <copyright file="CompanyAndDesignOptionForm.cs" company="BIA">
//     Copyright (c) BIA. All rights reserved.
// </copyright>

namespace BIA.ProjectCreatorWizard.UI
{
    using System;
    using System.Windows.Forms;

    /// <summary>
    /// CompanyAndDesignOption Form.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class CompanyAndDesignOptionForm : Form
    {
        private CompanyAndDesignOptionViewModel viewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompanyAndDesignOptionForm"/> class.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        public CompanyAndDesignOptionForm(CompanyAndDesignOptionViewModel viewModel)
        {
            this.InitializeComponent();

            this.viewModel = viewModel;
        }


        private void ValidateButton_Click(object sender, EventArgs e)
        {
            this.viewModel.CompanyName = this.CompanyNameTextbox.Text;
            this.viewModel.CompanyFilesPath = this.CompanyFilesPath.Text;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void BrowseCompanyFilesPath_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                fbd.SelectedPath = this.CompanyFilesPath.Text;
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    this.CompanyFilesPath.Text = fbd.SelectedPath;
                }
            }
        }
    }
}
