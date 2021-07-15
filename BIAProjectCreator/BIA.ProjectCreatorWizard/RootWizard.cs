// <copyright file="RootWizard.cs" company="BIA">
//     Copyright (c) BIA. All rights reserved.
// </copyright>
namespace BIA.ProjectCreatorWizard
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Windows.Forms;
    using BIA.ProjectCreatorWizard.UI;
    using EnvDTE;
    using Microsoft.VisualStudio.TemplateWizard;

    /// <summary>
    /// Root Wizard.
    /// </summary>
    /// <seealso cref="Microsoft.VisualStudio.TemplateWizard.IWizard" />
    public class RootWizard : IWizard
    {
        /// <summary>
        /// The saferootprojectname.
        /// </summary>
        public const string SAFEROOTPROJECTNAME = "$saferootprojectname$";

        /// <summary>
        /// The safeprojectname.
        /// </summary>
        public const string SAFEPROJECTNAME = "$safeprojectname$";

        /// <summary>
        /// The $safecompanyName$.
        /// </summary>
        public const string SAFECOMPANYNAME = "$safecompanyName$";

        /// <summary>
        /// The global dictionary.
        /// </summary>
        public static readonly Dictionary<string, string> GlobalDictionary = new Dictionary<string, string>();

        /// <summary>
        /// View model use for UI.
        /// </summary>
        private static readonly CompanyAndDesignOptionViewModel ViewModel = new CompanyAndDesignOptionViewModel();

        /// <summary>
        /// Destinaion Folder for solution.
        /// </summary>
        private DirectoryInfo destFolder;

        /// <summary>
        /// The solution name.
        /// </summary>
        private string solutionName;

        /// <inheritdoc/>
        public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
        {
            this.destFolder = Directory.GetParent(replacementsDictionary["$destinationdirectory$"]);

            CompanyAndDesignOptionForm window = new CompanyAndDesignOptionForm(ViewModel);
            DialogResult showDialog = window.ShowDialog();

            while (showDialog == DialogResult.OK && (string.IsNullOrEmpty(ViewModel.CompanyFilesPath) || !File.Exists(ViewModel.CompanyFilesPath + "/.biaCompanyFiles")))
            {
                MessageBox.Show("The company file folder is not correct : it does not contain the file .biaCompanyFiles");
                showDialog = window.ShowDialog();
            }

            if (showDialog != DialogResult.OK)
            {
                throw new WizardBackoutException();
            }

            this.solutionName = replacementsDictionary[SAFEPROJECTNAME];
            string companyName = ViewModel.CompanyName;
            string combineName = string.Join(".", companyName, this.solutionName);

            GlobalDictionary[SAFEPROJECTNAME] = combineName;
            GlobalDictionary[SAFEROOTPROJECTNAME] = this.solutionName;
            GlobalDictionary[SAFECOMPANYNAME] = companyName;

            replacementsDictionary[SAFEROOTPROJECTNAME] = this.solutionName;
            replacementsDictionary[SAFEPROJECTNAME] = combineName;
        }

        /// <inheritdoc/>
        public bool ShouldAddProjectItem(string filePath)
        {
            return true;
        }

        /// <inheritdoc/>
        public void RunFinished()
        {
            this.PlaceAdditionnalFilesInSolutionFolder();
            this.CopyCompanyFiles();
        }

        /// <inheritdoc/>
        public void BeforeOpeningFile(ProjectItem projectItem)
        {
            // Method intentionally left empty.
        }

        /// <inheritdoc/>
        public void ProjectItemFinishedGenerating(ProjectItem projectItem)
        {
            // Method intentionally left empty.
        }

        /// <inheritdoc/>
        public void ProjectFinishedGenerating(Project project)
        {
            int i = 0;
            // Method intentionally left empty.
        }

        /// <summary>
        /// Copy files for VSIX AdditionnalFiles folder to the root solution folder.
        /// </summary>
        /// <param name="fileName">file to copy.</param>
        private void PlaceAdditionnalFilesInSolutionFolder()
        {
            var destPath = Path.Combine(this.destFolder.FullName, this.solutionName);

            string path_codebase = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;

            string path_dir = Path.GetDirectoryName(path_codebase).Replace("file:\\", string.Empty) + "\\AdditionalFiles\\";

            this.Copy(path_dir, destPath);
        }

        private void CopyCompanyFiles()
        {
            if (!string.IsNullOrEmpty(ViewModel.CompanyFilesPath))
            {
                var srcPath = ViewModel.CompanyFilesPath;
                if (File.Exists(srcPath + "\\.biaCompanyFiles"))
                {
                    var destPath = Path.Combine(this.destFolder.FullName, this.solutionName);
                    var tmpPath = destPath + "\\tmp";
                    this.Copy(srcPath + "\\DotNet", tmpPath);

                    this.ReplaceInFileAndFileName(tmpPath, "TheBIADevCompany", ViewModel.CompanyName);
                    this.ReplaceInFileAndFileName(tmpPath, "BIATemplate", this.solutionName);

                    this.Copy(tmpPath, destPath);
                }
                else
                {
                    MessageBox.Show("The company file folder is not correct : it does not contain the file .biaCompanyFiles");
                }
            }
        }

        /// <summary>
        /// Copy files for VSIX AdditionnalFiles folder to the root solution folder.
        /// </summary>
        /// <param name="sourceDir">source folder.</param>
        /// <param name="targetDir">target folder.</param>
        private void Copy(string sourceDir, string targetDir)
        {
            Directory.CreateDirectory(targetDir);
            foreach (var dir in Directory.GetDirectories(sourceDir))
            {
                string targetRelDir = Path.Combine(targetDir, this.GetRelativePath(sourceDir + "\\", dir));
                if (!Directory.Exists(targetRelDir))
                {
                    Directory.CreateDirectory(targetRelDir);
                }
            }

            foreach (var file in Directory.GetFiles(sourceDir))
            {
                string targetFile = Path.Combine(targetDir, this.GetRelativePath(sourceDir + "\\", file));
                File.Copy(file, targetFile, true);
                this.ReplaceInFile(targetFile, "BIATemplate", this.solutionName);
            }

            foreach (var directory in Directory.GetDirectories(sourceDir))
            {
                this.Copy(directory, Path.Combine(targetDir, Path.GetFileName(directory)));
            }
        }

        private void ReplaceInFile(string filePath, string oldValue, string newValue)
        {
            string text = File.ReadAllText(filePath);
            text = text.Replace(oldValue, newValue);
            File.WriteAllText(filePath, text);
        }

        /// <summary>
        /// Copy files for VSIX AdditionnalFiles folder to the root solution folder.
        /// </summary>
        /// <param name="sourceDir">source folder.</param>
        /// <param name="oldString">old string.</param>
        /// <param name="newString">new string.</param>
        private void ReplaceInFileAndFileName(string sourceDir, string oldString, string newString)
        {
            foreach (var dir in Directory.GetDirectories(sourceDir))
            {
                var name = Path.GetFileName(dir);
                if (name.Contains(oldString))
                {
                    string newName = Path.Combine(sourceDir, name.Replace(oldString, newString));
                    Directory.Move(dir, newName);
                }
            }

            foreach (var file in Directory.GetFiles(sourceDir))
            {
                var name = Path.GetFileName(file);
                if (name.Contains(oldString))
                {
                    string newName = Path.Combine(sourceDir, name.Replace(oldString, newString));
                    File.Move(file, newName);
                }
                this.ReplaceInFile(file, oldString, newString);
            }

            foreach (var directory in Directory.GetDirectories(sourceDir))
            {
                this.ReplaceInFileAndFileName(directory, oldString, newString);
            }
        }

        /// <summary>
        /// Creates a relative path from one file or folder to another.
        /// </summary>
        /// <param name="fromPath">Contains the directory that defines the start of the relative path.</param>
        /// <param name="toPath">Contains the path that defines the endpoint of the relative path.</param>
        /// <returns>The relative path from the start directory to the end path or <c>toPath</c> if the paths are not related.</returns>
        private string GetRelativePath(string fromPath, string toPath)
        {
            Uri fromUri = new Uri(fromPath);
            Uri toUri = new Uri(toPath);

            if (fromUri.Scheme != toUri.Scheme) { return toPath; } // path can't be made relative.

            Uri relativeUri = fromUri.MakeRelativeUri(toUri);
            string relativePath = Uri.UnescapeDataString(relativeUri.ToString());

            if (toUri.Scheme.Equals("file", StringComparison.InvariantCultureIgnoreCase))
            {
                relativePath = relativePath.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
            }

            return relativePath;
        }
    }
}