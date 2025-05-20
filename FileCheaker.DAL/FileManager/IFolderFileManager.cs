using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCheaker.DAL.FileManager
{
    public interface IFolderFileManager
    {
        void Save(string path,string fileName, Folder folder);
        Folder Load(string path,string fileName);
    }
}
