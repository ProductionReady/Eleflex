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
            string[] packages = Directory.GetDirectories(txtFolder.Text);

            foreach(string package in packages)
            {
                string[] packageDirs = Directory.GetDirectories(package);
                string projectName = package.Substring(package.LastIndexOf(@"\") + 1);
                string oldPackageVersion = Path.Combine(package, txtPreviousVersion.Text);
                if(packageDirs.Contains(oldPackageVersion))
                { 
                    CopyFolder(oldPackageVersion, Path.Combine(package, txtNewVersion.Text));
                    DeleteFile(Path.Combine(package, txtNewVersion.Text, projectName + "." + txtPreviousVersion.Text + ".nupkg"));                                            
                }
            }

            MessageBox.Show("Done!");
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
            catch { }
        }

    }
}
