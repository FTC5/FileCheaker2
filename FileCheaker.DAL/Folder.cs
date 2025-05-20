using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCheaker.DAL
{
    [Serializable]
    public class Folder
    {
        public enum FolderTag
        {
            NEW, NOTFOUNT, WARNING, PH, Securety
        };
        //public bool check;
        public bool securety;
        public string FullName;
        public string[] files;
        public Folder[] chieldFolders;
    }
}
