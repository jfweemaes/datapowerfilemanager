using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DataPowerFileManager.WebReference;
using DataPowerFileManager.com.prolifics.dpowerxi50;

namespace DataPowerFileManager
{
    public partial class frmMain : Form
    {        
        
        String[] combobox = new string[100];
        String[] tmp = new string[1];
        String[] list = new string[100];
        String domain;

        public frmMain()
        {
            InitializeComponent();
            combobox[0] = "default";
            comboBox1.DataSource = combobox;
            comboBox1.SelectedIndex = 0;          
        }

        private void GetDataPowerDomains()
        {
            appmgmtprotocol foo = new appmgmtprotocol();
            
            //String[] fooarray = new string[100];
            GetDomainListRequest gdr = new GetDomainListRequest();
            foo.GetDomainList(gdr).Items.CopyTo(combobox, 1);
            comboBox1.DataSource = tmp;
            comboBox1.DataSource = combobox;           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetDataPowerDomains();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            domain = combobox[comboBox1.SelectedIndex];
            textBox1.Text = domain;
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            xmlmgmt bar = new xmlmgmt();
            requestGetfilestore rgf = new requestGetfilestore();
            rgf.location = filestorelocation.local;
            rgf.annotated = true;
            rgf.layoutonly = false;
            request xmlr = new request();
            xmlr.domain = domain;
            xmlr.Item = rgf;
            response xmlres;
            xmlres = bar.operation(xmlr);
            responseFilestore xmlres1;
            xmlres1 = (responseFilestore) xmlres.Item;



            textBox1.Text = xmlres1.ToString();
            textBox1.Text = xmlres1.Any[0].InnerXml.ToString();
            //treeView1.da
             
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            
            this.Close();
            
        }

        private void returnToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLogin frm = new frmLogin();
            frm.ShowDialog();          
            
        }

        

        
    }
}