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
            Populate_Saved_Sessions();
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

            // Before encrypting data, we will append plain text to a random
            // salt value, which will be between 4 and 8 bytes long (implicitly
            // used defaults).
            RijndaelEnhanced rijndaelKey =
                new RijndaelEnhanced(gs.strPassPhrase, gs.strInitVector);     

                       

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
            
            gs.strPlainText = gs.strDataPowerUserName;
            gs.strCipherText = rijndaelKey.Encrypt(gs.strPlainText);
            write.WriteElementString("username", gs.strCipherText);
            
            gs.strPlainText = gs.strDataPowerPassword;
            gs.strCipherText = rijndaelKey.Encrypt(gs.strPlainText);
            write.WriteElementString("password", gs.strCipherText);
            
            gs.strPlainText = gs.strDataPowerHost;
            gs.strCipherText = rijndaelKey.Encrypt(gs.strPlainText);
            write.WriteElementString("host", gs.strCipherText);
            
            gs.strPlainText = gs.strDataPowerPort;
            gs.strCipherText = rijndaelKey.Encrypt(gs.strPlainText);
            write.WriteElementString("port", gs.strCipherText);
            
            write.WriteEndElement();
            write.WriteEndDocument();
            write.Flush();
            write.Close();

            Populate_Saved_Sessions();
        }

        private void Populate_Saved_Sessions()
        {
            cmbSavedSessions.Items.Clear();
            DirectoryInfo di = new DirectoryInfo("sessions");
            FileInfo[] rgFiles = di.GetFiles("*.xml");            
            foreach (FileInfo fi in rgFiles)
            {
                cmbSavedSessions.Items.Add(fi.Name);
            }
        }

        private void cmbSavedSessions_SelectedIndexChanged(object sender, EventArgs e)
        {
            RijndaelEnhanced rijndaelKey =
                new RijndaelEnhanced(gs.strPassPhrase, gs.strInitVector); 

            TextReader tr = new StreamReader("sessions/"+cmbSavedSessions.SelectedItem.ToString());
            XmlReader reader = new XmlTextReader(tr);
            
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    switch (reader.LocalName)
                    {
                        case "username":
                            gs.strCipherText = reader.ReadElementString();
                            gs.strDataPowerUserName = rijndaelKey.Decrypt(gs.strCipherText);
                            txtUsername.Text = gs.strDataPowerUserName;
                            break;
                        case "password":
                            gs.strCipherText = reader.ReadElementString();
                            gs.strDataPowerPassword = rijndaelKey.Decrypt(gs.strCipherText);
                            txtPassword.Text = gs.strDataPowerPassword;
                            break;
                        case "host":
                            gs.strCipherText = reader.ReadElementString();
                            gs.strDataPowerHost = rijndaelKey.Decrypt(gs.strCipherText);
                            txtDataPowerIP.Text = gs.strDataPowerHost;
                            break;
                        case "port":
                            gs.strCipherText = reader.ReadElementString();
                            gs.strDataPowerPort = rijndaelKey.Decrypt(gs.strCipherText);
                            mtxtPortNumber.Text = gs.strDataPowerPort;
                            break;
                    }
                }
            }
        }

        private void Populate_Saved_Information(XmlReader reader)
        {
            txtUsername.Text = reader.GetAttribute("username");
            txtPassword.Text = reader.GetAttribute("password");
            while (reader.Read() && (reader.NodeType == XmlNodeType.Element || reader.NodeType == XmlNodeType.Whitespace))
            {
                switch (reader.LocalName)
                {
                    case "username":
                        txtUsername.Text = reader.GetAttribute("username");
                        break;
                    case "password":
                        txtPassword.Text = reader.GetAttribute("password");
                        break;
                    case "host":
                        txtDataPowerIP.Text = reader.GetAttribute("host");
                        break;
                    case "port":
                        mtxtPortNumber.Text = reader.GetAttribute("port");
                        break;
                }
            }
        }
        

    }
}
