using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompareCSV.Data
{
    public interface IRepository
    {
        List<string[]> GetData(string path);
    }
}
