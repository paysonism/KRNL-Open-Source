using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sexploit_Y
{
    public partial class ScriptHub : Form
    {
        internal static string data;
        public ScriptHub()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void DarkDexbtn_Click(object sender, EventArgs e)
        {
            ScriptHub.data = "Dark Dex";
            Functions.Lib.ScriptHub();
            Functions.Lib.ScriptHubMarkAsClosed();
        }

        private void UnESPbtn_Click(object sender, EventArgs e)
        {
            ScriptHub.data = "Unnamed ESP";
            Functions.Lib.ScriptHub();
            Functions.Lib.ScriptHubMarkAsClosed();
        }

        private void madcitybtn_Click(object sender, EventArgs e)
        {
            ScriptHub.data = "MadCityHaxx V2";
            Functions.Lib.ScriptHub();
            Functions.Lib.ScriptHubMarkAsClosed();
        }
        // Script Dump
        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            ScriptHub.data = "Script Dumper";
            Functions.Lib.ScriptHub();
            Functions.Lib.ScriptHubMarkAsClosed();
        }
        // Remote Spy
        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            ScriptHub.data = "Remote Spy";
            Functions.Lib.ScriptHub();
            Functions.Lib.ScriptHubMarkAsClosed();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            About aboutUI = new About();
            aboutUI.ShowDialog();
        }

        private void ScriptHub_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click_2(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
    }
}
