using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DataPowerFileManager
{
    public partial class frmLogin : Form
    {
        float fltOpacity = 0.01F;

        public frmLogin()
        {            
            InitializeComponent();           
        }

        private void Startup()
        {
            tOpacity.Start();            
        }

        

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            frmMain frm = new frmMain();
            frm.Visible = true;
            this.Visible = false;
        }

        private void tOpacity_Tick(object sender, EventArgs e)
        {
            fltOpacity = fltOpacity + .1F;
            this.Opacity = fltOpacity;
        }
    }
}
