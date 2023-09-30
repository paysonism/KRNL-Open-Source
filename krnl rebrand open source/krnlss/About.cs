using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sexploit_Y
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Process.Start("https://dsc.gg/subzer0fn");
        }

        private void SourceCreatorLabel_Click(object sender, EventArgs e)
        {
            Process.Start("https://discord.com/users/1155866973243191296");
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Credits Menu already open.", "Error!");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void About_Load(object sender, EventArgs e)
        {

        }
    }
}
