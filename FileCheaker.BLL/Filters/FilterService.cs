using FileCheaker.BLL.Interfaces;
using FileCheaker.DAL.FileManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCheaker.BLL.Filters
{
    public class FilterService : IFilterService
    {
        IListFileManager fileManager;
        List<string> filters;

        public FilterService(IListFileManager fileManager)
        {
            this.fileManager = fileManager;
            filters = new List<string>();/////////////////
            filters.Add("Temp");
            filters.Add("Cache");
        }

        public List<string> Filters
        {
            get
            {
                if (filters == null)
                {
                    filters = GetFilters();
                }
                return filters;

            }
            set
            {
                SetFilters(value, false);
            }
        }

        public List<string> GetFilters()
        {
            return null;
        }
        public List<string> AddFilter(string param)
        {
            return null;
        }
        public void SetFilters(List<string> filters, bool save)
        {
            this.Filters = filters;
            if (!save)
            {
                return;
            }
        }
        public bool Filter(string folderName)
        {
            int count = Filters.Count;
            for (int i = 0; i < count; i++)
            {
                if (folderName.Contains(Filters[i]))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
