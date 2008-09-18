namespace DataPowerFileManager
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {           
            if (disposing && (components != null))
            {                
                components.Dispose();                
            }
            base.Dispose(disposing);
        }


        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("Test");
            this.cmbDataPowerDomains = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.msMainMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.returnToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataPowerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getDomainsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lvLocalDrive = new System.Windows.Forms.ListView();
            this.lvDataPower = new System.Windows.Forms.ListView();
            this.tvFileListing = new System.Windows.Forms.TreeView();
            this.treeView2 = new System.Windows.Forms.TreeView();
            this.listView1 = new System.Windows.Forms.ListView();
            this.cmbDrives = new System.Windows.Forms.ComboBox();
            this.msMainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbDataPowerDomains
            // 
            this.cmbDataPowerDomains.FormattingEnabled = true;
            this.cmbDataPowerDomains.Location = new System.Drawing.Point(583, 126);
            this.cmbDataPowerDomains.Name = "cmbDataPowerDomains";
            this.cmbDataPowerDomains.Size = new System.Drawing.Size(270, 21);
            this.cmbDataPowerDomains.TabIndex = 2;
            this.cmbDataPowerDomains.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(611, 481);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(98, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Get Domains";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(729, 481);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(98, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "Get File List";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // msMainMenu
            // 
            this.msMainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.dataPowerToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.msMainMenu.Location = new System.Drawing.Point(0, 0);
            this.msMainMenu.Name = "msMainMenu";
            this.msMainMenu.Size = new System.Drawing.Size(865, 24);
            this.msMainMenu.TabIndex = 9;
            this.msMainMenu.Text = "Main Menu";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.returnToToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // returnToToolStripMenuItem
            // 
            this.returnToToolStripMenuItem.Name = "returnToToolStripMenuItem";
            this.returnToToolStripMenuItem.Size = new System.Drawing.Size(99, 22);
            this.returnToToolStripMenuItem.Text = "Login";
            this.returnToToolStripMenuItem.Click += new System.EventHandler(this.returnToToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(99, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // dataPowerToolStripMenuItem
            // 
            this.dataPowerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.getDomainsToolStripMenuItem});
            this.dataPowerToolStripMenuItem.Name = "dataPowerToolStripMenuItem";
            this.dataPowerToolStripMenuItem.Size = new System.Drawing.Size(72, 20);
            this.dataPowerToolStripMenuItem.Text = "DataPower";
            // 
            // getDomainsToolStripMenuItem
            // 
            this.getDomainsToolStripMenuItem.Name = "getDomainsToolStripMenuItem";
            this.getDomainsToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.getDomainsToolStripMenuItem.Text = "Get Domains";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // lvLocalDrive
            // 
            this.lvLocalDrive.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem2});
            this.lvLocalDrive.Location = new System.Drawing.Point(12, 36);
            this.lvLocalDrive.Name = "lvLocalDrive";
            this.lvLocalDrive.Size = new System.Drawing.Size(177, 31);
            this.lvLocalDrive.TabIndex = 10;
            this.lvLocalDrive.UseCompatibleStateImageBehavior = false;
            this.lvLocalDrive.View = System.Windows.Forms.View.List;
            // 
            // lvDataPower
            // 
            this.lvDataPower.Location = new System.Drawing.Point(492, 36);
            this.lvDataPower.Name = "lvDataPower";
            this.lvDataPower.Size = new System.Drawing.Size(361, 60);
            this.lvDataPower.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvDataPower.TabIndex = 11;
            this.lvDataPower.UseCompatibleStateImageBehavior = false;
            this.lvDataPower.View = System.Windows.Forms.View.Details;
            // 
            // tvFileListing
            // 
            this.tvFileListing.AllowDrop = true;
            this.tvFileListing.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tvFileListing.Location = new System.Drawing.Point(314, 153);
            this.tvFileListing.Name = "tvFileListing";
            this.tvFileListing.Size = new System.Drawing.Size(254, 322);
            this.tvFileListing.TabIndex = 12;
            // 
            // treeView2
            // 
            this.treeView2.AllowDrop = true;
            this.treeView2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeView2.Location = new System.Drawing.Point(583, 153);
            this.treeView2.Name = "treeView2";
            this.treeView2.Size = new System.Drawing.Size(270, 322);
            this.treeView2.TabIndex = 13;
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(12, 153);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(287, 322);
            this.listView1.TabIndex = 14;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // cmbDrives
            // 
            this.cmbDrives.FormattingEnabled = true;
            this.cmbDrives.Location = new System.Drawing.Point(314, 126);
            this.cmbDrives.Name = "cmbDrives";
            this.cmbDrives.Size = new System.Drawing.Size(254, 21);
            this.cmbDrives.TabIndex = 15;
            this.cmbDrives.SelectedIndexChanged += new System.EventHandler(this.cmbDrives_SelectedIndexChanged);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(865, 527);
            this.ControlBox = false;
            this.Controls.Add(this.cmbDrives);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.treeView2);
            this.Controls.Add(this.tvFileListing);
            this.Controls.Add(this.lvDataPower);
            this.Controls.Add(this.lvLocalDrive);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cmbDataPowerDomains);
            this.Controls.Add(this.msMainMenu);
            this.MainMenuStrip = this.msMainMenu;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DataPowerFileManager";
            this.Load += new System.EventHandler(this.frmMain_Load_1);
            this.msMainMenu.ResumeLayout(false);
            this.msMainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbDataPowerDomains;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.MenuStrip msMainMenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem returnToToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dataPowerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem getDomainsToolStripMenuItem;
        private System.Windows.Forms.ListView lvLocalDrive;
        private System.Windows.Forms.ListView lvDataPower;
        private System.Windows.Forms.TreeView tvFileListing;
        private System.Windows.Forms.TreeView treeView2;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ComboBox cmbDrives;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}

