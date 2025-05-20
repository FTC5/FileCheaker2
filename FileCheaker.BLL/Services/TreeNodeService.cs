using FileCheaker.DAL.FileManager;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileCheaker.BLL.Services
{
    public class TreeNodeService
    {
        IListFileManager listManager;

        public TreeNodeService(IListFileManager listManager)
        {
            this.listManager = listManager;
        }
        public async Task<TreeNode> getWarning()
        {
            TreeNode Root = new TreeNode(@"C:");//Edit
            var warning = await Task.Run(() => listManager.Load("", "Warning.fc"));
            await Task.Run(() => AddNodes(warning, Color.Green, Root));
            warning.Clear();
            return Root;
        }
        public async Task<TreeNode> getNotFound()
        {
            TreeNode Root = new TreeNode(@"C:");//Edit
            var notFound = await Task.Run(() => listManager.Load("", "NotFound.fc"));
            await Task.Run(() => AddNodes(notFound, Color.DarkBlue,Root));
            notFound.Clear();
            return Root;
        }
        public async Task<TreeNode> getNewFolder()
        {
            TreeNode Root = new TreeNode(@"C:");//Edit
            var newFolder = await Task.Run(() => listManager.Load("", "NewFolder.fc"));
            await Task.Run(() => AddNodes(newFolder, Color.Red, Root));
            newFolder.Clear();
            return Root;
        }
        public TreeNode Containe(TreeNode current, string text)
        {
            foreach (TreeNode dir in current.Nodes)
            {
                if (dir.Text == text)
                {
                    return dir;
                }
            }
            return null;
        }
        public void AddNodes(List<string> nodes, Color color, TreeNode Root)
        {
            TreeNode current = Root;
            TreeNode temp = null;
            char a = @"\"[0];
            String[] words;
            foreach (string item in nodes)
            {
                words = item.Split(new char[] { a });
                for (int i = 1; i < words.Length; i++)
                {
                    while (i + 1 != words.Length)
                    {
                        temp = Containe(current, words[i]);
                        if (temp == null)
                        {
                            break;
                        }
                        current = temp;
                        i += 1;
                    }
                    current.Nodes.Add(new TreeNode(words[i]));
                    current = current.Nodes[current.Nodes.Count - 1];
                }
                current.ForeColor = color;
                current = Root;
            }
            words = null;
        }
    }
}
