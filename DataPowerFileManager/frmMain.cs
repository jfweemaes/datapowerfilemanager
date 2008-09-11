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

namespace DataPowerFileManager
{
    public partial class frmMain : Form
    {        
        String[] combobox = new string[9];
        String[] tmp = new string[1];
        String[] list = new string[100];
        String domain;
        char[] delimiterChars = {'/'};
        string[] words;
        ImageList myImageList = new ImageList();
    


        private int nIndex = 3;
        public string dirPath = "";
        public string dirName = "";
        public TreeNodeMouseClickEventArgs ev;

        int cntdrv = 0;
        


        public frmMain()
        {
            InitializeComponent();
            combobox[0] = "default";
            cmbDataPowerDomains.DataSource = combobox;
            cmbDataPowerDomains.SelectedIndex = 0;
            //listLocalDrive();

            myImageList.Images.Add(Image.FromFile("pics/120px-Hdd_icon.svg.png"));
            myImageList.Images.Add(Image.FromFile("pics/Hardware-CD-ROM-256x256.png"));
            myImageList.Images.Add(Image.FromFile("pics/My_Computer.png"));
            myImageList.Images.Add(Image.FromFile("pics/Open_Folder.png"));
            treeView1.ImageList = myImageList;
            treeView1.ImageIndex = 2;
            
            
        }

        

        private void populate_drive()
        {
            DriveInfo[] drvlist = DriveInfo.GetDrives();
            int cnt = drvlist.Length;
            cntdrv = 0;
            foreach (DriveInfo lst in drvlist)
            {
                if (lst.DriveType == DriveType.CDRom)
                {
                    cntdrv++;
                }
                else
                {
                    DirectoryInfo di = new DirectoryInfo(lst.ToString());
                    DirectoryInfo[] di_list = di.GetDirectories();
                    foreach (DirectoryInfo lstdir in di_list)
                    {
                        string n = lstdir.Name.ToString();
                        treeView1.Nodes[0].Nodes[cntdrv].Nodes.Add(n, n, 3, 3);
                        treeView1.Update();
                        populate_folders_with_files(lstdir.FullName.ToString(), n);
                    }
                    cntdrv++;
                }
            }
        }

        //private void populate_folders_with_folders(string dir, string test)
        //{
        //    string strFolder = dir;

        //    DirectoryInfo di = new DirectoryInfo(strFolder);
            
        //    DirectoryInfo[] di_list = di.GetDirectories();
        //    foreach (DirectoryInfo lstdir in di_list)
        //    {
        //        string n = file.Name.ToString();
        //        treeView1.Nodes[0].Nodes[cntdrv].Nodes[test].Nodes.Add(n, n, 3, 3);
        //        treeView1.Update();
        //    }
        //}

        private void populate_folders_with_files(string dir,string test)
        {
            string strFolder = dir;
            
            DirectoryInfo di = new DirectoryInfo(strFolder);
            //Access is Denied if we Adventure into this folder... Just skipping it...
            if (strFolder == @"C:\System Volume Information")
            {
                return;
            }
            FileInfo[] files = di.GetFiles();


            foreach (FileInfo file in files)
            {
                string n = file.Name.ToString();
                treeView1.Nodes[0].Nodes[cntdrv].Nodes[test].Nodes.Add(n, n, 1, 1);
                treeView1.Update();
            }
        }
            
        

        private void Fill(TreeNode dirNode)
        {

            DirectoryInfo dir = new DirectoryInfo(dirNode.FullPath);
            try
            {
                foreach (DirectoryInfo dirItem in dir.GetDirectories())
                {
                    // Add node for the directory.
                    TreeNode newNode = new TreeNode(dirItem.Name);
                    dirNode.Nodes.Add(newNode);
                    newNode.Nodes.Add("*");
                }
            }
            catch
            {
                return;
            }
        }

        private void GetDataPowerDomains()
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
            GetDataPowerDomains();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            domain = combobox[cmbDataPowerDomains.SelectedIndex];
            //textBox1.Text = domain;
        }

        //public void listLocalDrive()
        //{
        //    string strFolder = @"\";
        //    DirectoryInfo di = new DirectoryInfo(strFolder);
        //    DirectoryInfo[] files2 = di.GetDirectories();
        //    FileInfo[] files = di.GetFiles();

        //    foreach (DirectoryInfo file in files2)
        //    {
        //        //lvLocalDrive.Items.Add(file.Name);
        //        treeView1.Nodes.Add(file.Name);
        //        //treeView1.Nodes.Add("Second Node");
        //        //treeView1.Nodes.Add("Third Node");
        //    }
        //}
        

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
            
            // write a line of text to the file
            tw.WriteLine("<foo>");
            tw.WriteLine(xmlres1.Any[12].InnerXml.ToString());
            //tw.WriteLine(xmlres.Item.);
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
                    lvDataPower.Items.Add(words[0]);

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

        private void frmMain_Load_1(object sender, EventArgs e)
        {
            treeView1.Nodes.Add(System.Environment.MachineName, System.Environment.MachineName, 2,2);

            string[] strdrives = Directory.GetLogicalDrives();
            foreach (string str in strdrives)
            {
                TreeNode tndrive = new TreeNode(str);
                treeView1.Nodes[0].Nodes.Add(str, str, 0,0);
                //Fill(tndrive);

                if (str == "C:\\")
                    treeView1.SelectedNode = tndrive;

            }
            populate_drive();
            treeView1.Nodes[0].Expand();
        }

        

        
    }
}