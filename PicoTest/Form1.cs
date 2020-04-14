using PicoFile_Direct_Link;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PicoTest
{
    public partial class Form1 : Form
    {
        PicoFile pic = new PicoFile();

        public Form1()
        {
            InitializeComponent();
        }

        private void chkPass_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPass.Checked)
                txtPass.Enabled = true;
            else
                txtPass.Enabled = false;
        }

        private async void btnTest_Click(object sender, EventArgs e)
        {
            try
            {
                pic.URL = txtUrl.Text;
                if (chkPass.Checked)
                    txtView.Text = await pic.DirectLink(txtPass.Text);
                else
                    txtView.Text = await pic.DirectLink();
            }
            catch (PicoFileException pfx)
            {
                txtView.Text = pfx.Message;
            }
        }
    }
}
