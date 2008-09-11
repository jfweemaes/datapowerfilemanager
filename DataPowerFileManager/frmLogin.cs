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
        public frmLogin()
        {            
            InitializeComponent();           
        }                

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {            
            if (txtUsername.Text.Length <= 0)
            {
                //MessageBox.Show("Please Enter A Username");
                //return;
            }

            if (txtPassword.Text.Length <= 0)
            {
                //MessageBox.Show("Please Enter A Password");
                //return;
            }

            if (txtDataPowerIP.Text.Length <= 0)
            {
                //MessageBox.Show("Please Enter a DataPower IP");                
                //return;
            }          
            
            GlobalDataStore.GetInstance().strDataPowerHost = "dpowerxi50.prolifics.com";
            GlobalDataStore.GetInstance().strDataPowerPort = "8080";
            this.Close();
        }
    }
}
