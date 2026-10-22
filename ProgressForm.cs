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
    public partial class ProgressForm : Form
    {
        public ProgressForm()
        {
            InitializeComponent();
        }

        public void UpdateProgress(int current, int total, string currentFile)
        {
            progressBar.Maximum = total;
            progressBar.Value = current;
            lblCurrentFile.Text = $"Renaming: {currentFile}";
            lblProgress.Text = $"{current} of {total}";
            this.Refresh();
        }

        public void SetComplete()
        {
            progressBar.Value = progressBar.Maximum;
            lblCurrentFile.Text = "Rename operation complete!";
        }
    }
}
