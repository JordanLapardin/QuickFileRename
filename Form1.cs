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

            // Enable drag and drop for the ListView
            lvFiles.AllowDrop = true;
            lvFiles.DragOver += LvFiles_DragOver;
            lvFiles.DragDrop += LvFiles_DragDrop;
            lvFiles.MouseDown += LvFiles_MouseDown;
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
                LoadFiles();
            }
        }

        private void LoadFiles()
        {
            files.Clear();
            lvFiles.Items.Clear();

            if (string.IsNullOrWhiteSpace(txtFolderPath.Text))
            {
                return;
            }

            try
            {
                DirectoryInfo dir = new DirectoryInfo(txtFolderPath.Text);
                FileInfo[] fileArray = dir.GetFiles();

                files = fileArray.OrderBy(f => f.Name).ToList();

                UpdatePreview();
                lblStatus.Text = $"Loaded {files.Count} files.";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading files: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file.Name);

                    // Build new name by replacing both [FileName] and {0}
                    int episodeNumber = startNumber + i;
                    string newBaseName = pattern
                        .Replace("[FileName]", fileNameWithoutExtension)
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

            try
            {
                string pattern = txtPattern.Text;
                int startNumber = (int)numStartNumber.Value;
                int successCount = 0;
                int errorCount = 0;

                for (int i = 0; i < files.Count; i++)
                {
                    FileInfo file = files[i];
                    string extension = file.Extension;
                    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file.Name);

                    // Build new name by replacing both [FileName] and {0}
                    int episodeNumber = startNumber + i;
                    string newBaseName = pattern
                        .Replace("[FileName]", fileNameWithoutExtension)
                        .Replace("{0}", episodeNumber.ToString());
                    string newName = newBaseName + extension;
                    string newPath = Path.Combine(file.DirectoryName, newName);

                    try
                    {
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

                string message = $"Rename operation completed.\nSuccessfully renamed: {successCount}\nFailed: {errorCount}";
                MessageBox.Show(message, "Rename Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadFiles();
                lblStatus.Text = $"Renamed {successCount} file(s).";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during rename: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
