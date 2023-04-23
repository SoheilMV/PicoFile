using System;
using System.Windows.Forms;
using PicoFileSharp;

namespace PicoTest
{
    public partial class Form1 : Form
    {
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
                PicoFile pic = new PicoFile(txtUrl.Text);
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
