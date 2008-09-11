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
        float fltOpacity = 0.01F;        

        public frmSplash()
        {
            InitializeComponent();
        }

        private void tSplash_Tick(object sender, EventArgs e)
        {
            fltOpacity = fltOpacity + .1F;
            Thread.Sleep(100);
            //this.Opacity = fltOpacity;
            if (fltOpacity >= 1)
            {
                Thread.Sleep(1000);
                //this.Opacity = 0;
                frmMain frm = new frmMain();
                //tSplash.Stop();
                //frm.ShowDialog();
                //this.Close();
            }
        }
        
    }
}
