using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FileCheaker.DAL.FileManager
{
    public class ListFileManager : IListFileManager
    {
        public List<string> Load(string path, string fileName)
        {
            List<string> list = new List<string>();
            FileStream outFile;
            try
            {
                outFile = new FileStream(path + fileName, FileMode.Open);
            }
            catch
            {
                outFile = File.Create(path + fileName);
            }
            try
            {
                XmlSerializer formatter = new XmlSerializer(list.GetType());
                list = (List<string>)formatter.Deserialize(outFile);
            }
            catch
            {
                outFile.Close();
            }
            outFile.Close();
            return list;
        }

        public void Save(string path, string fileName, List<string> list)
        {
            if (list.Count < 1)
            {
                return;
            }
            List<string> listB = Load(path, fileName);
            if(listB.Count < 1)
            {
                listB = list;
            }
            else
            {
                listB.AddRange(list);
            }
            FileStream inFile;
            try
            {
                inFile = File.Create(path + fileName);
            }
            catch
            {
                inFile = File.Create(fileName);
            }
            XmlSerializer formatter = new XmlSerializer(listB.GetType());
            formatter.Serialize(inFile, listB);
            inFile.Close();
        }
    }
}
