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
using System.Xml.XPath;
using System.Xml;
using System.IO;
using System.Xml.Xsl;
using System.Reflection;
using System.Threading;

namespace DataPowerFileManager
{
    public partial class frmMain : Form
    {        
        String[] combobox = new string[1000];
        String[] tmp = new string[1];
        
        String domain;
        char[] delimiterChars = {'/'};
        string[] words;
        ImageList myImageList = new ImageList();
        
        private string tempDrive;
        
        public frmMain()
        {
            InitializeComponent();
            tempDrive = this.driveListBox.Drive;
            this.dirListBox.Path = Environment.GetFolderPath(Environment .SpecialFolder.MyDocuments);
            combobox[0] = "default";
            cmbDataPowerDomains.DataSource = combobox;
            cmbDataPowerDomains.SelectedIndex = 0;
                        

            //myImageList.Images.Add(Image.FromFile("pics/120px-Hdd_icon.svg.png"));
            //myImageList.Images.Add(Image.FromFile("pics/Hardware-CD-ROM-256x256.png"));
            //myImageList.Images.Add(Image.FromFile("pics/My_Computer.png"));
            //myImageList.Images.Add(Image.FromFile("pics/Open_Folder.png"));
            //treeView1.ImageList = myImageList;
            //treeView1.ImageIndex = 2;           
        }

        public void GetDataPowerDomains()
        {            
            GetDomainListRequest gdr = new GetDomainListRequest();
            Amp.GetAppInstance().GetDomainList(gdr).Items.CopyTo(combobox, 0);
            cmbDataPowerDomains.DataSource = tmp;
            cmbDataPowerDomains.DataSource = combobox; 
            //int tmp2 = combobox.Length;
            //treeView1.Nodes.AddRange(combobox.);
            //for(int index = 0; index < combobox.Length ; index++)
            //            {
            //                  string data = combobox[index].ToString();
            //                  data = data.PadRight(20,' ');
            //                  if(listView1.Items.Count > index)
            //                  {
            //                        data = listView1.Items[index].ToString() + data;
            //                        listView1.Items.RemoveAt(index);
            //                        listView1.Items.Insert(index, data) ;
            //                  }
            //                  else
            //                        listView1.Items.Add(data);
            //            }
                  }
           
        

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            domain = combobox[cmbDataPowerDomains.SelectedIndex];
            //textBox1.Text = domain;
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

            // create a writer and open the file
            
            TextWriter tw = new StreamWriter("datapower.xml");
            string test = xmlres1.Any.Length.ToString();
            int test2 = Convert.ToInt32(test);
            tw.WriteLine("<foo>");
            for (int i = 0; i <=test2  - 1; i++)
            {
                // write a line of text to the file
                tw.WriteLine("<foo"+i+">");
                tw.WriteLine(xmlres1.Any[i].InnerXml.ToString());
                //tw.WriteLine(xmlres.Item.);
                tw.WriteLine("</foo"+i+">");
            }
            tw.WriteLine("</foo>");
            // close the stream
            tw.Close();
            




            XmlDocument xmldoc = new XmlDocument();
            //xmldoc.Load(DataPowerFileManager
            xmldoc.Load("datapower.xml");



            XslTransform myXslTrans = new XslTransform() ;
            //myXslTrans.Load("datapower.xslt");
            XmlTextReader xsltReader = new XmlTextReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("DataPowerFileManager.DataPower FileSystem Transformations.xslt"));
            XmlTextWriter myWriter = new XmlTextWriter("result.xml", null);
            myXslTrans.Load(xsltReader, null, null);
            myXslTrans.Transform(xmldoc, null, myWriter);
            myWriter.Close();
            xmldoc.Load("result.xml");






            //string size = null;
            //string modified = null;
            //XmlNodeList list = null;
            //XmlNodeReader reader = null;

            //list = xmldoc.GetElementsByTagName("file");
            //foreach (XmlNode n in list)
            //{
            //    reader = new XmlNodeReader(n);
            //    reader.Read();
            //    string test = reader.GetAttribute("name");
            //    modified = n[modified].InnerText;
            //    size = n["size"].InnerText;  //do something with s
            //}



            
            
            //XmlNodeList xnode = xmldoc.SelectNodes(@" /foo/directory[1]/file[*]/@name");
            //XmlNode xnode = xmldoc.SelectNodes(@" /foo/directory[1]/file[*]/@name");
            
            //XmlNodeReader nr = new XmlNodeReader(xnode);

            DataSet ds = new DataSet();

            //ds.ReadXml(nr);

            //dataGridView1.DataSource = ds.Tables[0];
            //dataGridView1.Columns[0].Visible = false; 
            





            
            System.IO.StringReader sr = new System.IO.StringReader(xmldoc.InnerXml);
            XPathDocument doc = new XPathDocument(sr);
            XPathNavigator nav = doc.CreateNavigator();
            //XPathExpression expImgSize = nav.Compile(@" /foo/directory[*]/@name");
            XPathExpression expImgSize = nav.Compile(@" /foo/directory[1]/file[*]/@name");
            XPathNodeIterator iterImageSize = nav.Select(expImgSize);
            if (iterImageSize != null)
            {
                while (iterImageSize.MoveNext())
                {
                    string imageSize = iterImageSize.Current.Value;
                    words = imageSize.Split(delimiterChars);
                    //lvDataPower.Items.Add(words[0]);

                }
            }



            try
            {

                // SECTION 2. Initialize the TreeView control.
                treeView2.Nodes.Clear();
                treeView2.Nodes.Add(new TreeNode(xmldoc.DocumentElement.Name));
                TreeNode tNode = new TreeNode();
                tNode = treeView2.Nodes[0];

                // SECTION 3. Populate the TreeView with the DOM nodes.
                AddNode(xmldoc.DocumentElement, tNode);
                treeView2.ExpandAll();
            }
            catch (XmlException xmlEx)
            {
                MessageBox.Show(xmlEx.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
		


            //textBox1.Text = xmlres1.ToString();
            //textBox1.Text = xmlres1.Any[0].InnerXml.ToString();
            //treeView1.da
             
        }

        private void AddNode(XmlNode inXmlNode, TreeNode inTreeNode)
      {
         XmlNode xNode;
         TreeNode tNode;
         XmlNodeList nodeList;
         int i;

         // Loop through the XML nodes until the leaf is reached.
         // Add the nodes to the TreeView during the looping process.
         if (inXmlNode.HasChildNodes)
         {
            nodeList = inXmlNode.ChildNodes;
            for(i = 0; i<=nodeList.Count - 1; i++)
            {
               xNode = inXmlNode.ChildNodes[i];
               inTreeNode.Nodes.Add(new TreeNode(xNode.Name));
               tNode = inTreeNode.Nodes[i];
               AddNode(xNode, tNode);
            }
         }
         else
         {
            // Here you need to pull the data from the XmlNode based on the
            // type of node, whether attribute values are required, and so forth.
            inTreeNode.Text = (inXmlNode.OuterXml).Trim();
         }
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
        
        
        private void driveListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.dirListBox.Path = this.driveListBox.Drive;                
                tempDrive = this.driveListBox.Drive;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                driveListBox.Drive = tempDrive;
            }
        }

        private void dirListBox_Change(object sender, EventArgs e)
        {
            try
            {
                this.fileListBox.Path = this.dirListBox.Path;
                //this.lblFolder.Text = dirListBox.Path.Substring(dirListBox.Path.LastIndexOf("\\") + 1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tLogin_Tick(object sender, EventArgs e)
        {
            if (GlobalDataStore.GetInstance().strLoginReady == "1")
            {
                GetDataPowerDomains();
                cmbDataPowerDomains.Enabled = true;
                GlobalDataStore.GetInstance().strLoginReady = "0";
            }
        }        
    }
}