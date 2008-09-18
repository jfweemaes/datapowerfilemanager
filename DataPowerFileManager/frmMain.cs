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
        String[] list = new string[100];
        String domain;
        char[] delimiterChars = {'/'};
        string[] words;
        ImageList myImageList = new ImageList();
        TreeNode tn;


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
            getDrives();            

            //myImageList.Images.Add(Image.FromFile("pics/120px-Hdd_icon.svg.png"));
            //myImageList.Images.Add(Image.FromFile("pics/Hardware-CD-ROM-256x256.png"));
            //myImageList.Images.Add(Image.FromFile("pics/My_Computer.png"));
            //myImageList.Images.Add(Image.FromFile("pics/Open_Folder.png"));
            //treeView1.ImageList = myImageList;
            //treeView1.ImageIndex = 2;
            
            
        }
       
        private void getDrives()
        {
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady) //Check to see there is something in the drive
                    cmbDrives.Items.Add(drive.RootDirectory);
            }
        }

        public delegate void StringDelegate(string updategui);

        public void UpdateGUI(object o)
        {
            while (true)
            {

                tvFileListing.Invoke(new StringDelegate(GetAllFileNames),
                    new object[] { "Test" });

                // break;
                //Thread.Sleep(100);

            }
        }

        public void GetAllFileNames(string text)
        {
            //
            // Store results in the file results list.
            //
            string root = GlobalDataStore.GetInstance().strDrive.ToString();
            List<string> fileResults = new List<string>();
            
            // Data structure to hold names of subfolders to be
        // examined for files.
        Stack<string> dirs = new Stack<string>(20);

        if (!System.IO.Directory.Exists(root))
        {
            throw new ArgumentException();
        }
        dirs.Push(root);

        while (dirs.Count > 0)
        {
            
            string currentDir = dirs.Pop();
            string[] subDirs;
            try
            {
                subDirs = System.IO.Directory.GetDirectories(currentDir);
            }
            // An UnauthorizedAccessException exception will be thrown if we do not have
            // discovery permission on a folder or file. It may or may not be acceptable 
            // to ignore the exception and continue enumerating the remaining files and 
            // folders. It is also possible (but unlikely) that a DirectoryNotFound exception 
            // will be raised. This will happen if currentDir has been deleted by
            // another application or thread after our call to Directory.Exists. The 
            // choice of which exceptions to catch depends entirely on the specific task 
            // you are intending to perform and also on how much you know with certainty 
            // about the systems on which this code will run.
            catch (UnauthorizedAccessException e)
            {                    
                Console.WriteLine(e.Message);
                continue;
            }
            catch (System.IO.DirectoryNotFoundException e)
            {
                Console.WriteLine(e.Message);
                continue;
            }

            string[] files = null;
            try
            {
                string test = System.Environment.SystemDirectory.ToString();
                char[] delimiterChars = { '\\' };
                string[] words = test.Split(delimiterChars);
                string test2 = words[0].ToString()+"\\"+words[1].ToString();
                if (currentDir == test2 || currentDir == words[0].ToString() + "\\ProgramData" || currentDir == words[0].ToString() + "\\System Volume Information"
                    || currentDir == words[0].ToString() + "\\Program Files (x86)" || currentDir == words[0].ToString() + "\\Program Files" || currentDir == words[0].ToString() + "\\$Recycle.Bin")
                {
                    continue;
                }
                else
                {
                    files = System.IO.Directory.GetFiles(currentDir);
                }
            }

            catch (UnauthorizedAccessException e)
            {

                Console.WriteLine(e.Message);
                continue;
            }

            catch (System.IO.DirectoryNotFoundException e)
            {
                Console.WriteLine(e.Message);
                continue;
            }
            // Perform the required action on each file here.
            // Modify this block to perform your required task.
            foreach (string file in files)
            {
                try
                {
                    // Perform whatever action is required in your scenario.
                    System.IO.FileInfo fi = new System.IO.FileInfo(file);
                    //fileResults.Add(file);
                    Application.DoEvents();
                    addNode(file, null);
                    //Console.WriteLine("{0}: {1}, {2}", fi.Name, fi.Length, fi.CreationTime);
                }
                catch (System.IO.FileNotFoundException e)
                {
                    // If file was deleted by a separate application
                    //  or thread since the call to TraverseTree()
                    // then just continue.
                    Console.WriteLine(e.Message);
                    continue;
                }
            }

            // Push the subdirectories onto the stack for traversal.
            // This could also be done before handing the files.
            foreach (string str in subDirs)
                dirs.Push(str);
        }
       // return fileResults.ToArray();

    }


        //Recursive function to add a full path into a treeview
        private void addNode(string text, TreeNode parent)
        {
            if (text.EndsWith("\\")) text.TrimEnd(new char[] { '\\' });

            if (parent == null)
            {
                //Add/Find the first node
                if (text.IndexOf("\\") != -1)
                {
                    int parentIndex;
                    string nodeString;
                    if (text.IndexOf(":") != -1) //C:\ want to preserve the backslash
                    {
                        //Check to see if it exists first                        
                        nodeString = text.Substring(0, text.IndexOf("\\") + 1);
                        parentIndex = tvFileListing.Nodes.IndexOfKey(nodeString);

                        //Node does not exist so create it
                        if (parentIndex == -1)
                        {
                            //Important to set the key to the text so it is easier to look up the node later
                            
                            tvFileListing.Nodes.Add(nodeString, nodeString);
                            parentIndex = tvFileListing.Nodes.Count - 1;
                        }

                    }
                    else
                    {
                        nodeString = text.Substring(0, text.IndexOf("\\"));
                        parentIndex = tvFileListing.Nodes.IndexOfKey(nodeString);

                        if (parentIndex == -1)
                        {
                            tvFileListing.Nodes.Add(nodeString, nodeString);
                            
                            parentIndex = tvFileListing.Nodes.Count - 1;
                        }
                    }

                    parent = tvFileListing.Nodes[parentIndex];
                    text = text.Substring(text.IndexOf("\\") + 1);
                }
                else
                {
                    //Simply add it if it does not exist
                    if (tvFileListing.Nodes.IndexOfKey(text) == -1)
                        tvFileListing.Nodes.Add(text, text);
                        
                }
            }

            if (text.IndexOf("\\") != -1)
            {
                string nodeString = text.Substring(0, text.IndexOf("\\"));
                int parentIndex = parent.Nodes.IndexOfKey(nodeString);

                if (parentIndex == -1)
                {
                    parent.Nodes.Add(nodeString, nodeString);
                    
                    parentIndex = parent.Nodes.Count - 1;
                }

                addNode(text.Substring(text.IndexOf("\\") + 1), parent.Nodes[parentIndex]);
            }
            else
            {
                //No children nodes necessary, just add it
                if (parent.Nodes.IndexOfKey(text) == -1)
                    parent.Nodes.Add(text, text);
                    
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
            //Start filling the TreeView on a separate thread
            //backgroundWorker1.RunWorkerAsync();
        }

        //private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    GlobalDataStore.GetInstance().strDrive = "C:\\";
        //    Console.WriteLine(GlobalDataStore.GetInstance().strDrive);
        //    Thread t = new Thread(UpdateGUI);
        //    t.Start();
        //}

        private void cmbDrives_SelectedIndexChanged(object sender, EventArgs e)
        {
            GlobalDataStore.GetInstance().strDrive = cmbDrives.SelectedItem.ToString();
            Thread t = new Thread(UpdateGUI);            
            t.Start();
        }        
    }
}