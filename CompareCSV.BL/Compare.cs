using CompareCSV.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompareCSV.BL
{
    public class Compare
    {
        ListOfArray listArray = new ListOfArray();

        public ListOfArray ParseCSV(string path)
        {
            try
            {
                using (StreamReader readFile = new StreamReader(path))
                {
                    string line;
                    string[] row;

                    while ((line = readFile.ReadLine()) != null)
                    {
                        row = line.Split(';');
                        listArray.list.Add(row);
                    }
                }
            }
            catch (Exception e)
            {
               MessageBox.Show(e.Message);
            }

            return listArray;
        }


        public DataTable ConvertListToDataTable(ListOfArray value)
        {
            // New table.
            DataTable table = new DataTable();

            // Get max columns.
            int columns = 0;
            foreach (var array in value.list)
            {
                if (array.Length > columns)
                {
                    columns = array.Length;
                }
            }

            for (int i = 0; i < 4; i++)
            {
                string columnName = "";
                if (i == 0)
                {
                    columnName = "ColumnName";
                }
                else if (i == 1)
                {
                    columnName = "Value in Column with Max Length";
                }
                else if (i == 2)
                {
                    columnName = "Suggested Type";
                }
                else if (i == 3)
                {
                    columnName = "Max Lenght";
                }

                table.Columns.Add(columnName);
            }

            // Add transpond rows.
            for (int i = 0; i < columns; i++)
            {
                table.Rows.Add(value.list[0].ElementAt(i), GetFirstValueFromListOfArrraysByMaxLentgh(value, ColMaxLenght(value, i), i), TryParseStringInListOfArrrays(value.list[1].ElementAt(i), i), ColMaxLenght(value, i));
            }

            return table;
        }


        public int ColMaxLenght(ListOfArray arr, int i)
        {
            return arr.list.Skip(1).Max(p => p[i].Length);
        }


        public string TryParseStringInListOfArrrays(string value, int i)
        {
            Double dbl;
            Int32 integ;
            Int64 integ64;
            String caseSwitch = "";

            bool resultInt = Int32.TryParse(value.ToString().Replace('.', ','), out integ);
            bool resultInt64 = Int64.TryParse(value.ToString().Replace('.', ','), out integ64);
            bool resultDouble = Double.TryParse(value.ToString().Replace('.', ','), out dbl);


            if (resultInt == true)
            {
                caseSwitch = "System.Int32";
            }
            else if (resultInt64 == true)
            {
                caseSwitch = "System.Int64";
            }
            else if (resultDouble == true)
            {
                caseSwitch = "System.Double";
            }
            else
            {
                caseSwitch = "System.String";
            }

            return caseSwitch;
        }


        public string GetFirstValueFromListOfArrraysByMaxLentgh(ListOfArray array, int maxLentgh, int i)
        {
            return array.list.Skip(1).First(p => p[i].Length == maxLentgh).ElementAt(i);
        }

    }
}
