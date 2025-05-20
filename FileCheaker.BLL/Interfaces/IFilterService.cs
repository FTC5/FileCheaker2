using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCheaker.BLL.Interfaces
{
    public interface IFilterService
    {
        List<string> Filters { get; set; }
        List<string> GetFilters();
        List<string> AddFilter(string param);
        void SetFilters(List<string> filters, bool save);
        bool Filter(string folderName);
    }
}
