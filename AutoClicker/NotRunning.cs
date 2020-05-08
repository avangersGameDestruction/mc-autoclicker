using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace AutoClicker
{
    public partial class NotRunning : Form
    {
        public NotRunning()
        {
            InitializeComponent();
        }

        public string ProcessTitle { get; private set; }

        private void NotRunning_Load(object sender, EventArgs e)
        {
            string[] processes = SearchMinecraftProcesses();
            cmbProcess.Items.AddRange(processes);
            if (cmbProcess.Items.Count == 1)
            {
                cmbProcess.SelectedItem = cmbProcess.Items[0];
            }
        }

        private static string[] SearchMinecraftProcesses()
        {
            return Process.GetProcesses().Where(b => b.ProcessName.StartsWith("java")).OrderBy(b => b.MainWindowTitle).Select(b => b.MainWindowTitle).ToArray();
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            if (cmbProcess.SelectedItem != null)
            {
                ProcessTitle = cmbProcess.SelectedItem.ToString();
            }
            Close();
        }
    }
}
