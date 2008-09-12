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
        //GlobalDataStore gs = new GlobalDataStore();

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

            if (txtPortNumber.Text.Length <= 0)
            {
                //MessageBox.Show("Please Enter a DataPower IP");                
                //return;
            }

            GlobalDataStore.GetInstance().strDataPowerUserName = txtUsername.Text.ToString();
            GlobalDataStore.GetInstance().strDataPowerPassword = txtPassword.Text.ToString();
           
            GlobalDataStore.GetInstance().strDataPowerHost = txtDataPowerIP.Text.ToString();
            GlobalDataStore.GetInstance().strDataPowerPort = txtPortNumber.Text.ToString();
            this.Close();
        }

        private void btnSaveSession_Click(object sender, EventArgs e)
        {
            string target = @"sessions";

            GlobalDataStore.GetInstance().strDataPowerUserName = txtUsername.Text.ToString();
            GlobalDataStore.GetInstance().strDataPowerPassword = txtPassword.Text.ToString();
            GlobalDataStore.GetInstance().strDataPowerHost = txtDataPowerIP.Text.ToString();
            GlobalDataStore.GetInstance().strDataPowerPort = txtPortNumber.Text.ToString();

            // Before encrypting data, we will append plain text to a random
            // salt value, which will be between 4 and 8 bytes long (implicitly
            // used defaults).
            RijndaelEnhanced rijndaelKey =
                new RijndaelEnhanced(GlobalDataStore.GetInstance().strPassPhrase, GlobalDataStore.GetInstance().strInitVector);     

                       

            // Determine whether the directory exists.
            if (!Directory.Exists(target))
            {
                // Create the directory it does not exist.
                Directory.CreateDirectory(target);
            }
            
            TextWriter tw = new StreamWriter("sessions/"+GlobalDataStore.GetInstance().strDataPowerUserName+"@"+GlobalDataStore.GetInstance().strDataPowerHost+".xml");
            XmlTextWriter write = new XmlTextWriter(tw);
            write.Formatting = Formatting.Indented;
            write.WriteStartDocument(true);
            write.WriteComment("Saved Sessions for DataPower File Manager");
            write.WriteStartElement("DataPower");
            
            GlobalDataStore.GetInstance().strPlainText = GlobalDataStore.GetInstance().strDataPowerUserName;
            GlobalDataStore.GetInstance().strCipherText = rijndaelKey.Encrypt(GlobalDataStore.GetInstance().strPlainText);
            write.WriteElementString("username", GlobalDataStore.GetInstance().strCipherText);
            
            GlobalDataStore.GetInstance().strPlainText = GlobalDataStore.GetInstance().strDataPowerPassword;
            GlobalDataStore.GetInstance().strCipherText = rijndaelKey.Encrypt(GlobalDataStore.GetInstance().strPlainText);
            write.WriteElementString("password", GlobalDataStore.GetInstance().strCipherText);
            
            GlobalDataStore.GetInstance().strPlainText = GlobalDataStore.GetInstance().strDataPowerHost;
            GlobalDataStore.GetInstance().strCipherText = rijndaelKey.Encrypt(GlobalDataStore.GetInstance().strPlainText);
            write.WriteElementString("host", GlobalDataStore.GetInstance().strCipherText);
            
            GlobalDataStore.GetInstance().strPlainText = GlobalDataStore.GetInstance().strDataPowerPort;
            GlobalDataStore.GetInstance().strCipherText = rijndaelKey.Encrypt(GlobalDataStore.GetInstance().strPlainText);
            write.WriteElementString("port", GlobalDataStore.GetInstance().strCipherText);
            
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
                new RijndaelEnhanced(GlobalDataStore.GetInstance().strPassPhrase, GlobalDataStore.GetInstance().strInitVector); 

            TextReader tr = new StreamReader("sessions/"+cmbSavedSessions.SelectedItem.ToString());
            XmlReader reader = new XmlTextReader(tr);
            
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    switch (reader.LocalName)
                    {
                        case "username":
                            GlobalDataStore.GetInstance().strCipherText = reader.ReadElementString();
                            GlobalDataStore.GetInstance().strDataPowerUserName = rijndaelKey.Decrypt(GlobalDataStore.GetInstance().strCipherText);
                            txtUsername.Text = GlobalDataStore.GetInstance().strDataPowerUserName;
                            break;
                        case "password":
                            GlobalDataStore.GetInstance().strCipherText = reader.ReadElementString();
                            GlobalDataStore.GetInstance().strDataPowerPassword = rijndaelKey.Decrypt(GlobalDataStore.GetInstance().strCipherText);
                            txtPassword.Text = GlobalDataStore.GetInstance().strDataPowerPassword;
                            break;
                        case "host":
                            GlobalDataStore.GetInstance().strCipherText = reader.ReadElementString();
                            GlobalDataStore.GetInstance().strDataPowerHost = rijndaelKey.Decrypt(GlobalDataStore.GetInstance().strCipherText);
                            txtDataPowerIP.Text = GlobalDataStore.GetInstance().strDataPowerHost;
                            break;
                        case "port":
                            GlobalDataStore.GetInstance().strCipherText = reader.ReadElementString();
                            GlobalDataStore.GetInstance().strDataPowerPort = rijndaelKey.Decrypt(GlobalDataStore.GetInstance().strCipherText);
                            txtPortNumber.Text = GlobalDataStore.GetInstance().strDataPowerPort;
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
                        txtPortNumber.Text = reader.GetAttribute("port");
                        break;
                }
            }
        }
        

    }
}
