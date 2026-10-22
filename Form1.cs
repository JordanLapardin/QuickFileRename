using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuickFileRename
{
    public partial class Form1 : Form
    {
        private List<FileInfo> files = new List<FileInfo>();
        private string selectedFolderName = string.Empty;
        private List<string> allowedExtensions = new List<string>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Quick File Rename";
            this.Size = new Size(820, 480);
            this.StartPosition = FormStartPosition.CenterScreen;
            txtPattern.Text = "[FileName] Episode {0}";

            // Initialize default allowed extensions (video files only)
            InitializeDefaultFileTypes();

            // Enable drag and drop for the ListView
            lvFiles.AllowDrop = true;
            lvFiles.DragOver += LvFiles_DragOver;
            lvFiles.DragDrop += LvFiles_DragDrop;
            lvFiles.MouseDown += LvFiles_MouseDown;
        }

        private void InitializeDefaultFileTypes()
        {
            // Set default to video files only
            allowedExtensions.Clear();
            allowedExtensions.AddRange(new[] { ".mp4", ".mkv", ".avi", ".mov", ".flv", ".wmv", ".webm", ".m4v", ".3gp", ".ogv", ".ts", ".mpg", ".mpeg", ".m2ts", ".mts" });
        }

        private void LvFiles_MouseDown(object sender, MouseEventArgs e)
        {
            ListViewItem item = lvFiles.GetItemAt(e.X, e.Y);
            if (item != null)
            {
                lvFiles.DoDragDrop(item, DragDropEffects.Move);
            }
        }

        private void LvFiles_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
            Point pt = lvFiles.PointToClient(new Point(e.X, e.Y));
            ListViewItem hoverItem = lvFiles.GetItemAt(pt.X, pt.Y);
            if (hoverItem != null)
            {
                hoverItem.Selected = true;
            }
        }

        private void LvFiles_DragDrop(object sender, DragEventArgs e)
        {
            Point pt = lvFiles.PointToClient(new Point(e.X, e.Y));
            ListViewItem draggedItem = (ListViewItem)e.Data.GetData(typeof(ListViewItem));
            ListViewItem dropTarget = lvFiles.GetItemAt(pt.X, pt.Y);

            if (draggedItem != null && dropTarget != null && draggedItem != dropTarget)
            {
                int draggedIndex = draggedItem.Index;
                int dropIndex = dropTarget.Index;

                // Swap in the files list
                FileInfo temp = files[draggedIndex];
                files[draggedIndex] = files[dropIndex];
                files[dropIndex] = temp;

                // Update the preview
                UpdatePreview();
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                txtFolderPath.Text = folderBrowser.SelectedPath;
                selectedFolderName = new DirectoryInfo(folderBrowser.SelectedPath).Name;
                LoadFiles();
            }
        }

        private void LoadFiles()
        {
            files.Clear();
            lvFiles.Items.Clear();

            if (string.IsNullOrWhiteSpace(txtFolderPath.Text))
            {
                lblStatus.Text = "Please enter or select a folder path.";
                return;
            }

            try
            {
                string folderPath = txtFolderPath.Text.Trim();

                // Check if the path exists
                if (!Directory.Exists(folderPath))
                {
                    lblStatus.Text = "Folder path does not exist.";
                    MessageBox.Show($"The folder path does not exist:\n{folderPath}", "Invalid Path", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DirectoryInfo dir = new DirectoryInfo(folderPath);
                selectedFolderName = dir.Name;
                FileInfo[] fileArray = dir.GetFiles();

                // Filter files based on allowed extensions
                files = fileArray
                    .Where(f => allowedExtensions.Contains(f.Extension.ToLower()))
                    .OrderBy(f => f.Name)
                    .ToList();

                UpdatePreview();
                lblStatus.Text = $"Loaded {files.Count} files.";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading files: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblStatus.Text = "Error loading files.";
            }
        }

        private void UpdatePreview()
        {
            lvFiles.Items.Clear();

            if (files.Count == 0)
            {
                return;
            }

            try
            {
                string pattern = txtPattern.Text;
                int startNumber = (int)numStartNumber.Value;

                for (int i = 0; i < files.Count; i++)
                {
                    FileInfo file = files[i];
                    string extension = file.Extension;

                    // Build new name by replacing [FileName] with folder name and {0} with episode number
                    int episodeNumber = startNumber + i;
                    string newBaseName = pattern
                        .Replace("[FileName]", selectedFolderName)
                        .Replace("{0}", episodeNumber.ToString());
                    string newName = newBaseName + extension;

                    // Add to list view
                    ListViewItem item = new ListViewItem(file.Name);
                    item.SubItems.Add(newName);
                    item.Tag = file;
                    lvFiles.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = $"Error updating preview: {ex.Message}";
            }
        }

        private void txtPattern_TextChanged(object sender, EventArgs e)
        {
            UpdatePreview();
        }

        private void numStartNumber_ValueChanged(object sender, EventArgs e)
        {
            UpdatePreview();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadFiles();
        }

        private void btnRename_Click(object sender, EventArgs e)
        {
            if (files.Count == 0)
            {
                MessageBox.Show("No files to rename.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult result = MessageBox.Show(
                $"Are you sure you want to rename {files.Count} file(s)?",
                "Confirm Rename",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
            {
                return;
            }

            // Create and show progress form
            ProgressForm progressForm = new ProgressForm();

            // Perform rename in background thread
            BackgroundWorker renameWorker = new BackgroundWorker();
            renameWorker.DoWork += (sender1, e1) =>
            {
                string pattern = txtPattern.Text;
                int startNumber = (int)numStartNumber.Value;
                int successCount = 0;
                int errorCount = 0;

                for (int i = 0; i < files.Count; i++)
                {
                    FileInfo file = files[i];
                    string extension = file.Extension;

                    // Build new name by replacing [FileName] with folder name and {0} with episode number
                    int episodeNumber = startNumber + i;
                    string newBaseName = pattern
                        .Replace("[FileName]", selectedFolderName)
                        .Replace("{0}", episodeNumber.ToString());
                    string newName = newBaseName + extension;
                    string newPath = Path.Combine(file.DirectoryName, newName);

                    try
                    {
                        // Update progress
                        progressForm.Invoke(new Action(() =>
                        {
                            progressForm.UpdateProgress(i + 1, files.Count, file.Name);
                        }));

                        // Check if new name already exists
                        if (File.Exists(newPath) && !newPath.Equals(file.FullName, StringComparison.OrdinalIgnoreCase))
                        {
                            File.Delete(newPath);
                        }

                        file.MoveTo(newPath);
                        successCount++;
                    }
                    catch
                    {
                        errorCount++;
                    }
                }

                e1.Result = new { successCount, errorCount };
            };

            renameWorker.RunWorkerCompleted += (sender1, e1) =>
            {
                progressForm.SetComplete();
                System.Threading.Thread.Sleep(500); // Brief pause to show completion

                dynamic renameResult = e1.Result;
                int successCount = renameResult.successCount;
                int errorCount = renameResult.errorCount;

                progressForm.Close();

                string message = $"Rename operation completed.\nSuccessfully renamed: {successCount}\nFailed: {errorCount}";
                MessageBox.Show(message, "Rename Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadFiles();
                lblStatus.Text = $"Renamed {successCount} file(s).";
            };

            progressForm.Show();
            renameWorker.RunWorkerAsync();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOptions_Click(object sender, EventArgs e)
        {
            OptionsForm optionsForm = new OptionsForm();
            optionsForm.SelectedExtensions = new List<string>(allowedExtensions);

            if (optionsForm.ShowDialog(this) == DialogResult.OK)
            {
                allowedExtensions = optionsForm.SelectedExtensions;
                LoadFiles();
            }
        }
    }
}
