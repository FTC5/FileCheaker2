using FileCheaker.BLL.Filters;
using FileCheaker.BLL.Interfaces;
using FileCheaker.DAL;
using FileCheaker.DAL.FileManager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCheaker.BLL.Services
{
    public class CheckService
    {
        Folder oldFolder;
        IFilterService filterService;
        IListFileManager listManager;
        IFolderFileManager folderManager;
        string mainfolder;
        List<string> warning = new List<string>();
        List<string> notFound = new List<string>();
        List<string> newFolder = new List<string>();

        public CheckService(IListFileManager listManager,IFolderFileManager folderManager, IFilterService filterService = null)
        {
            this.listManager = listManager;
            this.folderManager = folderManager;
            this.filterService = filterService;
            this.oldFolder = folderManager.Load(@"D:\", "Info.txt");
            mainfolder = oldFolder.FullName;
            if (filterService == null)
            {
                filterService = new NullFilter(null);
            }
            this.filterService = filterService;
        }

        public List<string> Warning { get => warning; }
        public List<string> NotFound { get => notFound; }
        public List<string> NewFolder { get => newFolder; }
        public void Start()
        {
            CheackFolderInfo(oldFolder);
            SaveResult();
        }
        void CheackFolderInfo(Folder folder)
        {
            if(warning.Count+ notFound.Count+ newFolder.Count > 1000)
            {
                SaveResult();
            }
            CheakWinFolders(folder);
            if (folder.securety == true)
            {
                return;
            }
            int lenght = folder.chieldFolders.Length;
            int i = 0;
            while (lenght != i)
            {
                if (filterService.Filter(folder.chieldFolders[i].FullName) == false)
                {
                    CheackFolderInfo(folder.chieldFolders[i]);
                }
                i += 1;
            }
            
        }

        void CheakWinFolders(Folder folder)
        {
            if (folder.securety)
            {
                return;
            }
            if (Directory.Exists(folder.FullName))
            {
                try
                {
                    string[] files = Directory.GetFiles(folder.FullName);
                    Comparison(files, folder.files);
                    string[] dirs = Directory.GetDirectories(folder.FullName);
                    string[] oldFolder = new string[folder.chieldFolders.Length];
                    for (int i = 0; i < folder.chieldFolders.Length; i++)
                    {
                        oldFolder[i] = folder.chieldFolders[i].FullName;
                    }
                    Comparison(dirs, oldFolder);
                }
                catch (Exception ex)
                {
                    warning.Add(ex.ToString());
                }
            }
        }
        void Comparison(string[] newInfo, string[] oldInfo)
        {
            int num;
            int j = 0;
            int i = 0;
            for (; i < oldInfo.Length && j < newInfo.Length; i++, j++)
            {
                if (newInfo[j] != oldInfo[i])
                {
                    num = ComparisonCheak(oldInfo, newInfo[j], i);
                    if (num == -1)
                    {
                        i--;
                        newFolder.Add(newInfo[j]);
                    }
                    else
                    {
                        for (int k = i-1; k < num; ++k)
                        {
                            notFound.Add(oldInfo[k]);
                        }
                        i = num;
                    }
                }
            }
            for (; j < newInfo.Length; j++)
            {
                newFolder.Add(newInfo[j]);
            }
            for (; i < oldInfo.Length; i++)
            {
                notFound.Add(oldInfo[i]);
            }

        }
        int ComparisonCheak(string[] oldInfo, string text, int i = 0)
        {
            for (int j = i-1; j < oldInfo.Length; ++j)
            {
                if (oldInfo[j] == text)
                {
                    return j;
                }
            }
            return -1;
        }
        void SaveResult()
        {
            listManager.Save("", "Warning.fc", warning);
            listManager.Save("", "NotFound.fc", notFound);
            listManager.Save("", "NewFolder.fc", newFolder);
            warning.Clear();
            notFound.Clear();
            newFolder.Clear();
        }
    }
}
