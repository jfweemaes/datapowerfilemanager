using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using DataPowerFileManager.WebReference;
using DataPowerFileManager.com.prolifics.dpowerxi50;

namespace DataPowerFileManager
{
    public partial class frmLogin : Form
    {
        float fltOpacity = 0.01F;
        /// <summary>
        /// Keeps track of the number of open forms
        /// </summary>
        int _formCount = 0;

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
            if (txtUsername.Text.Length <= 0)
            {
                MessageBox.Show("Please Enter A Username");
                return;
            }

            if (txtPassword.Text.Length <= 0)
            {
                MessageBox.Show("Please Enter A Password");
                return;
            }

            if (txtDataPowerIP.Text.Length <= 0)
            {
                MessageBox.Show("Please Enter a DataPower IP");                
                return;
            }
            GlobalDataStore pt = new GlobalDataStore();
            
            //Show other forms in seperate thread
            Thread t = new Thread(new ThreadStart(ShowForms));
            t.IsBackground = false;
            t.Start();
            GlobalDataStore.GetInstance().strDataPowerHost = "dpowerxi50.prolifics.com";
            GlobalDataStore.GetInstance().strDataPowerPort = "8080";
            //Close the main form - this will exit the main
            //UI thread message pump, but we will have another one
            //this.Hide();
            this.Close();            
           
        }

        private void ShowForms()
        {
            frmMain frm = new frmMain();
            
            //Add event handler to decrement count on close
            frm.FormClosed += new FormClosedEventHandler(ChildFormClosed);
            

            //store how mant forms there are open
            _formCount = 2;

            //show the form
            frm.Show();
            
            //Create a new message pump outside of the main
            //UI thread message pump
            Application.Run();
        }

        void ChildFormClosed(object sender, FormClosedEventArgs e)
        {
            _formCount--;

            //if no open forms - kill message pump
            if (_formCount == 0)
            {
                //Tell the current thread to exit it message pump
                Application.ExitThread();
            }
            
        }

        private void tOpacity_Tick(object sender, EventArgs e)
        {
            fltOpacity = fltOpacity + .1F;
            this.Opacity = fltOpacity;
        }

       

        
    }
}
