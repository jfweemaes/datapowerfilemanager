using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace DataPowerFileManager
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {           
            InitializeComponent();
            //this.label1.Location = new System.Drawing.Point(135, 121);//start
            //this.label2.Location = new System.Drawing.Point(135, 147);//26
            //this.label3.Location = new System.Drawing.Point(135, 173);//26
            //this.label5.Location = new System.Drawing.Point(135, 199);//26
            
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
