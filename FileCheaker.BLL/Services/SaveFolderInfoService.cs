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
    public class SaveFolderInfoService
    {
        Folder folder;
        IFilterService filterService;
        IFolderFileManager folderFileManager;

        public SaveFolderInfoService(IFolderFileManager folderFileManager, IFilterService filterService = null, string folder = @"C:\")
        {
            this.folderFileManager = folderFileManager;
            this.folder = new Folder();
            this.folder.FullName = folder;
            if (filterService == null)
            {
                filterService = new NullFilter(null);
            }
            this.filterService = filterService;
        }
        public void Start()
        {
            FolderInfo(this.folder);
            folderFileManager.Save(@"D:\", "Info.txt", folder);

        }
        void FolderInfo(Folder folder)
        {
            folder = GetWinFolders(folder);
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
                    FolderInfo(folder.chieldFolders[i]);
                }
                i += 1;
            }

        }

        Folder GetWinFolders(Folder folder)
        {
            try
            {
                if (Directory.Exists(folder.FullName))
                {
                    folder.files = Directory.GetFiles(folder.FullName);
                    string[] dirs = Directory.GetDirectories(folder.FullName);
                    folder.chieldFolders = new Folder[dirs.Length];
                    for (int i = 0; i < dirs.Length; i++)
                    {
                        // System.Console.WriteLine(dirs[i]);
                        folder.chieldFolders[i] = new Folder();
                        folder.chieldFolders[i].FullName = @dirs[i];
                    }
                }
            }
            catch (UnauthorizedAccessException)
            {
                folder.securety = true;
            }

            return folder;
        }
    }
}
