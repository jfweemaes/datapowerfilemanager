using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace DataPowerFileManager
{
    public partial class frmSplash : Form
    {
        public frmSplash()
        {
            InitializeComponent();
        }

        private void tSplash_Tick(object sender, EventArgs e)
        {
            Thread.Sleep(2000);
            this.Hide();
            frmMain frm = new frmMain();
            tSplash.Stop();
            frm.ShowDialog();
            this.Close();
        }       
    }
}
