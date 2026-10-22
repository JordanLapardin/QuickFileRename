using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuickFileRename
{
    public partial class OptionsForm : Form
    {
        private List<string> selectedExtensions = new List<string>();

        public List<string> SelectedExtensions
        {
            get { return selectedExtensions; }
            set { selectedExtensions = value; }
        }

        public OptionsForm()
        {
            InitializeComponent();
        }

        private void OptionsForm_Load(object sender, EventArgs e)
        {
            // Populate the checked list box with common file types
            Dictionary<string, string[]> fileTypes = new Dictionary<string, string[]>
            {
                { "Video Files", new[] { ".mp4", ".mkv", ".avi", ".mov", ".flv", ".wmv", ".webm", ".m4v", ".3gp", ".ogv", ".ts", ".mpg", ".mpeg", ".m2ts", ".mts" } },
                { "Audio Files", new[] { ".mp3", ".flac", ".aac", ".ogg", ".wma", ".wav", ".m4a", ".opus" } },
                { "Image Files", new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tiff", ".webp" } },
                { "Document Files", new[] { ".pdf", ".docx", ".doc", ".txt", ".xlsx", ".pptx" } },
                { "Archive Files", new[] { ".zip", ".rar", ".7z", ".tar", ".gz" } }
            };

            foreach (var category in fileTypes)
            {
                foreach (var ext in category.Value)
                {
                    int index = clbFileTypes.Items.Add(ext);
                    if (selectedExtensions.Contains(ext))
                    {
                        clbFileTypes.SetItemChecked(index, true);
                    }
                }
            }
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < clbFileTypes.Items.Count; i++)
            {
                clbFileTypes.SetItemChecked(i, true);
            }
        }

        private void btnDeselectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < clbFileTypes.Items.Count; i++)
            {
                clbFileTypes.SetItemChecked(i, false);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            selectedExtensions.Clear();
            foreach (var item in clbFileTypes.CheckedItems)
            {
                selectedExtensions.Add(item.ToString().ToLower());
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
