using CompareCSV.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompareCSV.BL
{
    public static class Check
    {
        public static bool CheckColumns(IEnumerable<DataGridViewRow> value, DataGridViewRow row, string col1,string col2)
        {
            return value.Where(s => s.Cells[col1].Value.Equals(row.Cells[col1].Value)).Any(n => !n.Cells[col2].Value.Equals(row.Cells[col2].Value));
        }
    }
}
