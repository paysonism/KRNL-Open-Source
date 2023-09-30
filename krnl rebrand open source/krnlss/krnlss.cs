using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using sxlib;
using sxlib.Specialized;
using FluxAPI;
using FastColoredTextBoxNS;

namespace Sexploit_Y
{
    public partial class krnlss : Form
    {
        public bool Attached;
        public bool Loaded;
        private protected readonly Flux module = new Flux();
        public krnlss()
        {
            InitializeComponent();

            module.InitializeAPI("SubZero");
            module.DownloadDLLs();
        }

        private async void SynLoadEventAsync(SxLibBase.SynLoadEvents Event, object Param)
        {

        }

        private void SynAttachEvent(SxLibBase.SynAttachEvents Event, object Param)
        {
            switch (Event)
            {
                case SxLibBase.SynAttachEvents.INJECTING:
                    return;
                case SxLibBase.SynAttachEvents.READY:
                    return;
            }
        }

        Timer t1 = new Timer();

        private void krnlss_Load(object sender, EventArgs e)
        {
            Opacity = 0;      //first the opacity is 0

            t1.Interval = 5;  //we'll increase the opacity every 10ms
            t1.Tick += new EventHandler(fadeIn);  //this calls the function that changes opacity 
            t1.Start();
        }

        void fadeIn(object sender, EventArgs e)
        {
            if (Opacity >= 1)
                t1.Stop();   //this stops the timer if the form is completely displayed
            else
                Opacity += 0.05;
        }

        private void krnlss_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;    //cancel the event so the form won't be closed

            t1.Tick += new EventHandler(fadeOut);  //this calls the fade out function
            t1.Start();

            if (Opacity == 0)  //if the form is completly transparent
                e.Cancel = false;   //resume the event - the program can be closed

        }

        void fadeOut(object sender, EventArgs e)
        {
            if (Opacity <= 0)     //check if opacity is 0
            {
                t1.Stop();    //if it is, we stop the timer
                Close();   //and we try to close the form
            }
            else
                Opacity -= 0.05;
        }
        private void menuStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void Injectbtn_Click(object sender, EventArgs e)
        {
            module.Inject();
        }

        private void Savebtn_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (Stream s = File.Open(saveFileDialog1.FileName, FileMode.CreateNew))
                using (StreamWriter sw = new StreamWriter(s))
                {
                    sw.Write(ExecuteBox.Text);
                }
            }
        }

        private void Openbtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                openFileDialog1.Title = "Open";
                ExecuteBox.Text = File.ReadAllText(openFileDialog1.FileName);
            }
        }

        private void Clearbtn_Click(object sender, EventArgs e)
        {
            ExecuteBox.Text = "";
            ExecuteBox.Focus();
        }

        private void Executebtn_Click(object sender, EventArgs e)
        {
            string script = this.ExecuteBox.Text;
            module.Execute(script);
        }

        private void listbox1_SelectedIndexChanged(object sender, TreeViewEventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void killRobloxToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Process[] runningProcesses = Process.GetProcesses();
            foreach (Process process in runningProcesses)
            {
                // now check the modules of the process
                foreach (ProcessModule module in process.Modules)
                {
                    if (module.FileName.Equals("RobloxPlayerLauncher.exe"))
                    {
                        process.Kill();
                    }
                    else
                    {
                        MessageBox.Show("Cannot Find a Running Roblox Process.", "Error!");
                    }
                }
            }
        }

        private void injectToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            module.Inject();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            About aboutUI = new About();
            aboutUI.ShowDialog();
        }

        private void creditsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ScriptHub scriptHub = new ScriptHub();
            scriptHub.ShowDialog();

        }

        private void creditsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About aboutUI = new About();
            aboutUI.ShowDialog();
        }

        private void OptionsBtn_Click(object sender, EventArgs e)
        {

        }

        private void joinDiscordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://dsc.gg/subzer0fn");
        }

        private void gitHubToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/paysonism/");
        }

        private void websiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://subzerofn.xyz/"); // payson was here
        }
    }
}
