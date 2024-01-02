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
using System.Diagnostics;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace adb_fastboot_gui_app
{
    public partial class Form1 : Form
    {
        private Process process = new Process();
        private ProcessStartInfo info = new ProcessStartInfo();
        public Form1()
        {
            InitializeComponent();
            info.WindowStyle = ProcessWindowStyle.Hidden;
            info.CreateNoWindow = true;
            info.UseShellExecute = false;
            info.RedirectStandardOutput = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                info.FileName = "adb.exe";
                info.Arguments = "devices -l";
                process.StartInfo = info;
                process.Start();
                string output = process.StandardOutput.ReadToEnd();
                string delete = output.Substring(0, output.Length - 34);
                string raw_device_name = delete.Substring(78);
                string real = raw_device_name.Replace("_", " ");
                string realreal = real.ToUpper();
                device_name_label.Text = realreal;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
