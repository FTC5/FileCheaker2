using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using FileCheaker.DAL;

namespace FileCheaker.DAL.FileManager
{
    public class FolderFileManager : IFolderFileManager
    {
        private static string defalt_file= "Info.fc";

        public static string Defalt_file { get => defalt_file; set => defalt_file = value; }

        public Folder Load(string path,string fileName)
        {
            Folder folder = new Folder();
            FileStream outFile;
            try
            {
                outFile = new FileStream(path+fileName, FileMode.Open);
            }
            catch
            {
                return Load(folder);
            }
            try
            {
                XmlSerializer formatter = new XmlSerializer(folder.GetType());
                folder = (Folder)formatter.Deserialize(outFile);
            }
            catch
            {
                outFile.Close();
            }
            outFile.Close();
            return folder;
        }
        private Folder Load(Folder folder)
        {
            FileStream outFile;
            try
            {
                outFile = new FileStream(Defalt_file, FileMode.Open);
            }
            catch
            {
                return folder;
            }
            try
            {
                XmlSerializer formatter = new XmlSerializer(folder.GetType());
                folder = (Folder)formatter.Deserialize(outFile);
            }
            catch
            {
                outFile.Close();
            }
            outFile.Close();
            return folder;
        }
        public void Save(string path,string fileName, Folder folder)
        {
            FileStream inFile;
            try
            {
                inFile = File.Create(path+fileName);
            }
            catch
            {
                inFile = File.Create(fileName);
            }
            XmlSerializer formatter = new XmlSerializer(folder.GetType());
            formatter.Serialize(inFile, folder);
            inFile.Close();
        }
    }
}
