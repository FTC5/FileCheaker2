using FileCheaker.BLL.Filters;
using FileCheaker.BLL.Interfaces;
using FileCheaker.BLL.Services;
using FileCheaker.DAL.FileManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        IFilterService filterService;
        IListFileManager listManager;
        IFolderFileManager folderManager;
        SaveFolderInfoService saveFolderInfo;
        CheckService checkService;
        TreeNodeService treeNodeService;
        public Form1()
        {
            InitializeComponent();
            filterService = new FilterService(listManager);
            folderManager = new FolderFileManager();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            listManager = new ListFileManager();
            checkService = new CheckService(listManager, folderManager, filterService);
            treeNodeService = new TreeNodeService(listManager);
            CreateTree();
        }
        private async void CreateTree()
        {
            await Task.Run(() => checkService.Start());
            treeView1.Nodes.Clear();
            treeView1.Nodes.Add(await Task.Run(() => treeNodeService.getWarning().Result));
            treeView2.Nodes.Clear();
            treeView2.Nodes.Add(await Task.Run(() => treeNodeService.getNotFound().Result));
            treeView3.Nodes.Clear();
            treeView3.Nodes.Add(await Task.Run(() => treeNodeService.getNewFolder().Result));
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            saveFolderInfo = new SaveFolderInfoService(folderManager, filterService);
            SaveInfo();
            
        }
        private async void SaveInfo()
        {
            await Task.Run(() => saveFolderInfo.Start());
            int i = 0;
        }
    }
}
