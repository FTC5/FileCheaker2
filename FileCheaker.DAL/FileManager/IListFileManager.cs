using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCheaker.DAL.FileManager
{
    public interface IListFileManager
    {
        void Save(string path, string fileName, List<string> list);
        List<string> Load(string path, string fileName);
    }
}
