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
        public int TotalRam;
        public float TotalRamf;
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
                device_name_label.Text = real;
                textBox1.Text = real;
                timer1.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
      
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                info.FileName = "adb.exe";
                info.Arguments = "shell \"cat /proc/meminfo\"";
                process.StartInfo = info;
                process.Start();
                string str1_totalram = process.StandardOutput.ReadToEnd();
                string str2_totalram = str1_totalram.Substring(0, str1_totalram.Length - 1049);
                string str3_totalram = str2_totalram.Substring(17);
                float int_totalram = float.Parse(str3_totalram);
                float GB_totalram = int_totalram / (1024 * 1024);
                TotalRamf = GB_totalram;
                TotalRam = (int)GB_totalram;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            try
            {
                info.FileName = "adb.exe";
                info.Arguments = "shell \"cat /proc/meminfo\"";
                process.StartInfo = info;
                process.Start();
                string str1_freeram = process.StandardOutput.ReadToEnd();
                string str2_freeram = str1_freeram.Substring(0, str1_freeram.Length - 992);
                string str3_freeram = str2_freeram.Substring(75);
                float float_freeram = float.Parse(str3_freeram);
                float GB_freeram = float_freeram / (1024 * 1024);
                float abc = TotalRamf - GB_freeram;
                freeram_label.Text = abc.ToString("0.00") + "GB" + "" + "/" + "" + TotalRamf.ToString("0.00") + "GB";
                freeram_progressbar.Maximum = TotalRam;
                freeram_progressbar.Minimum = 0;
                freeram_progressbar.Value = TotalRam - (int)GB_freeram;

                textBox1.Text = str1_freeram;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
