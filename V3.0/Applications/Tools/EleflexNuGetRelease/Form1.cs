using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace EleflexNuGetRelease
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            try
            {
                string[] packages = Directory.GetDirectories(txtFolder.Text);

                List<String> packageNames = new List<string>();
                foreach (string package in packages)
                    packageNames.Add(package.Substring(package.LastIndexOf(@"\") + 1));

                foreach (string package in packages)
                {
                    string[] packageDirs = Directory.GetDirectories(package);
                    string projectName = package.Substring(package.LastIndexOf(@"\") + 1);
                    string oldPackageVersion = Path.Combine(package, txtPreviousVersion.Text);                    
                    if (ckbCopyPackage.Checked)
                    {
                        if (packageDirs.Contains(oldPackageVersion))
                        {
                            CopyFolder(oldPackageVersion, Path.Combine(package, txtNewVersion.Text));
                            DeleteFile(Path.Combine(package, txtNewVersion.Text, projectName + "." + txtPreviousVersion.Text + ".nupkg"));

                            string packageFile = Path.Combine(package, txtNewVersion.Text, projectName + ".nuspec");
                            string packageContexts = File.ReadAllText(packageFile);

                            //Change version numbers
                            packageContexts = packageContexts.Replace(@"<version>" + txtPreviousVersion.Text + @"</version>", @"<version>" + txtNewVersion.Text + @"</version>");
                            foreach (string pname in packageNames)
                                packageContexts = packageContexts.Replace(@"<dependency id=""" + pname + @""" version=""" + txtPreviousVersion.Text + @""" />", @"<dependency id=""" + pname + @""" version=""" + txtNewVersion.Text + @""" />");
                            File.WriteAllText(packageFile, packageContexts);
                        }
                    }

                    if (ckbCopyDlls.Checked)
                    {
                        if (packageDirs.Contains(Path.Combine(package, txtNewVersion.Text)))
                        {
                            foreach (string tempProjectDir in packageDirs)
                            {
                                DirectoryInfo dirInfo = new DirectoryInfo(tempProjectDir);
                                string nugetLibFolder = Path.Combine(package, txtNewVersion.Text, "lib", "net46");
                                if (dirInfo.Name.Contains("Eleflex"))
                                {
                                    string binDir = Path.Combine(package, tempProjectDir, "bin");
                                    string releaseDir = Path.Combine(binDir, "release");
                                    string assemblyFolder = null;
                                    if (Directory.Exists(releaseDir))
                                        assemblyFolder = releaseDir;
                                    else if (Directory.Exists(binDir))
                                        assemblyFolder = binDir;
                                    if (!string.IsNullOrEmpty(assemblyFolder))
                                    {
                                        string tempFile = dirInfo.Name + ".dll";
                                        File.Copy(Path.Combine(assemblyFolder, tempFile), Path.Combine(nugetLibFolder, tempFile), true);
                                        tempFile = dirInfo.Name + ".pdb";
                                        File.Copy(Path.Combine(assemblyFolder, tempFile), Path.Combine(nugetLibFolder, tempFile), true);
                                    }
                                }
                                //CUSTOM
                                if (dirInfo.Name == "Eleflex.Lookups.WebClient")
                                {
                                    DirectoryInfo nugetFolder = new DirectoryInfo(txtFolder.Text);
                                    string moduleFolder = Path.Combine(nugetFolder.Parent.Parent.FullName, "Modules");
                                    string binFolder = Path.Combine(moduleFolder, "Lookups", "Eleflex.Lookups.Web.Admin", "bin");
                                    File.Copy(Path.Combine(binFolder, "Eleflex.Lookups.Web.Admin.dll"), Path.Combine(nugetLibFolder, "Eleflex.Lookups.Web.Admin.dll"), true);
                                    File.Copy(Path.Combine(binFolder, "Eleflex.Lookups.Web.Admin.pdb"), Path.Combine(nugetLibFolder, "Eleflex.Lookups.Web.Admin.pdb"), true);
                                }
                                //CUSTOM
                                if (dirInfo.Name == "Eleflex.WebClient")
                                {
                                    DirectoryInfo nugetFolder = new DirectoryInfo(txtFolder.Text);
                                    string moduleFolder = Path.Combine(nugetFolder.Parent.Parent.FullName, "Modules");
                                    string binFolder = Path.Combine(moduleFolder, "Logging", "Eleflex.Logging.Web.Admin", "bin");
                                    File.Copy(Path.Combine(binFolder, "Eleflex.Logging.Web.Admin.dll"), Path.Combine(nugetLibFolder, "Eleflex.Logging.Web.Admin.dll"), true);
                                    File.Copy(Path.Combine(binFolder, "Eleflex.Logging.Web.Admin.pdb"), Path.Combine(nugetLibFolder, "Eleflex.Logging.Web.Admin.pdb"), true);

                                    binFolder = Path.Combine(moduleFolder, "Security", "Eleflex.Security.Web.Admin", "bin");
                                    File.Copy(Path.Combine(binFolder, "Eleflex.Security.Web.Admin.dll"), Path.Combine(nugetLibFolder, "Eleflex.Security.Web.Admin.dll"), true);
                                    File.Copy(Path.Combine(binFolder, "Eleflex.Security.Web.Admin.pdb"), Path.Combine(nugetLibFolder, "Eleflex.Security.Web.Admin.pdb"), true);

                                    binFolder = Path.Combine(moduleFolder, "Security", "Eleflex.Security.Web.Account", "bin");
                                    File.Copy(Path.Combine(binFolder, "Eleflex.Security.Web.Account.dll"), Path.Combine(nugetLibFolder, "Eleflex.Security.Web.Account.dll"), true);
                                    File.Copy(Path.Combine(binFolder, "Eleflex.Security.Web.Account.pdb"), Path.Combine(nugetLibFolder, "Eleflex.Security.Web.Account.pdb"), true);

                                    binFolder = Path.Combine(moduleFolder, "Versioning", "Eleflex.Versioning.Web.Admin", "bin");
                                    File.Copy(Path.Combine(binFolder, "Eleflex.Versioning.Web.Admin.dll"), Path.Combine(nugetLibFolder, "Eleflex.Versioning.Web.Admin.dll"), true);
                                    File.Copy(Path.Combine(binFolder, "Eleflex.Versioning.Web.Admin.pdb"), Path.Combine(nugetLibFolder, "Eleflex.Versioning.Web.Admin.pdb"), true);
                                }
                            }
                        }
                    }
                    
                }

                if (ckbPackage.Checked)
                {
                    foreach (string package in packages)
                    {
                        if (Directory.Exists(Path.Combine(package, txtNewVersion.Text)))
                        {
                            string projectName = package.Substring(package.LastIndexOf(@"\") + 1);
                            string nugetExec = Path.Combine(txtFolder.Text, "nuget.exe");
                            string nuspecFile = Path.Combine(package, txtNewVersion.Text, projectName + ".nuspec");
                            System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo(nugetExec, @" pack """ + nuspecFile + @"""");
                            info.WorkingDirectory = txtFolder.Text;
                            var p = System.Diagnostics.Process.Start(info);
                            p.WaitForExit();
                        }
                    }
                }


                if (ckbPublish.Checked)
                {
                    foreach (string package in packages)
                    {
                        if (Directory.Exists(Path.Combine(package, txtNewVersion.Text)))
                        {
                            string projectName = package.Substring(package.LastIndexOf(@"\") + 1);
                            string nugetExec = Path.Combine(txtFolder.Text, "nuget.exe");
                            string nupackageFile = Path.Combine(txtFolder.Text, projectName + "." + txtNewVersion.Text + ".nupkg");
                            System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo(nugetExec, @" push """ + nupackageFile + @"""");
                            info.WorkingDirectory = txtFolder.Text;
                            var p = System.Diagnostics.Process.Start(info);
                            p.WaitForExit();
                        }
                    }
                }

                MessageBox.Show("Done!");
            }
            catch(Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.ToString());
            }
            
        }


        private static void DeleteFile(String path)
        {
            try
            {
                File.Delete(path);
            }
            catch { }
        }

        private static void CopyFolder(string SourcePath, string DestinationPath)
        {
            SourcePath = SourcePath.EndsWith(@"\") ? SourcePath : SourcePath + @"\";
            DestinationPath = DestinationPath.EndsWith(@"\") ? DestinationPath : DestinationPath + @"\";

            try
            {
                if (Directory.Exists(SourcePath))
                {
                    if (Directory.Exists(DestinationPath) == false)
                    {
                        Directory.CreateDirectory(DestinationPath);
                    }

                    foreach (string files in Directory.GetFiles(SourcePath))
                    {
                        FileInfo fileInfo = new FileInfo(files);
                        fileInfo.CopyTo(string.Format(@"{0}\{1}", DestinationPath, fileInfo.Name), true);
                    }

                    foreach (string drs in Directory.GetDirectories(SourcePath))
                    {
                        DirectoryInfo directoryInfo = new DirectoryInfo(drs);
                        CopyFolder(drs, DestinationPath + directoryInfo.Name);
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error copying files: " + ex.ToString());
            }
        }

        private void ckbPublish_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
