using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Xml;
using System.IO;

namespace DataPowerFileManager
{
    public partial class frmLogin : Form
    {
        GlobalDataStore gs = new GlobalDataStore();

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

            gs.strDataPowerUserName = txtUsername.Text.ToString();
            gs.strDataPowerPassword = txtPassword.Text.ToString();
           
            gs.strDataPowerHost = "dpowerxi50.prolifics.com";
            gs.strDataPowerPort = "8080";
            this.Close();
        }

        private void btnSaveSession_Click(object sender, EventArgs e)
        {
            string target = @"sessions";

            gs.strDataPowerUserName = txtUsername.Text.ToString();
            gs.strDataPowerPassword = txtPassword.Text.ToString();
            gs.strDataPowerHost = "dpowerxi50.prolifics.com";
            gs.strDataPowerPort = "8080";

            // Determine whether the directory exists.
            if (!Directory.Exists(target))
            {
                // Create the directory it does not exist.
                Directory.CreateDirectory(target);
            }


            TextWriter tw = new StreamWriter("sessions/"+gs.strDataPowerUserName+"@"+gs.strDataPowerHost+".xml");
            XmlTextWriter write = new XmlTextWriter(tw);
            write.Formatting = Formatting.Indented;
            write.WriteStartDocument(true);
            write.WriteComment("Saved Sessions for DataPower File Manager");
            write.WriteStartElement("DataPower");
            write.WriteElementString("username", gs.strDataPowerUserName);
            write.WriteElementString("password", gs.strDataPowerPassword);
            write.WriteElementString("host", gs.strDataPowerHost);
            write.WriteElementString("port", gs.strDataPowerPort);
            write.WriteEndElement();
            write.WriteEndDocument();
            write.Flush();
            write.Close();
        }

        
        

        

    }
}
