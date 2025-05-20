using FileCheaker.BLL.Interfaces;
using FileCheaker.DAL.FileManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCheaker.BLL.Filters
{
    class NullFilter : IFilterService
    {

        public NullFilter(IListFileManager fileManager)
        {
        }
        public List<string> Filters { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public List<string> AddFilter(string param)
        {
            return new List<string>();
        }

        public bool Filter(string folderName)
        {
            return false;
        }

        public List<string> GetFilters()
        {
            return new List<string>();
        }

        public void SetFilters(List<string> filters, bool save)
        {
            return;
        }
    }
}
