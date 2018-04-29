using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComapreCSV
{
    static class Compare
    {
        static public List<string[]> parseCSV(string path)
        {
            List<string[]> parsedData = new List<string[]>();

            try
            {
                using (StreamReader readFile = new StreamReader(path))
                {
                    string line;
                    string[] row;

                    while ((line = readFile.ReadLine()) != null)
                    {
                        row = line.Split(';');
                        parsedData.Add(row);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            return parsedData;
        }


        public static DataTable ConvertListToDataTable(List<string[]> list)
        {
            // New table.
            DataTable table = new DataTable();

            // Get max columns.
            int columns = 0;
            foreach (var array in list)
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
                table.Rows.Add(list[0].ElementAt(i), GetFirstValueFromListOfArrraysByMaxLentgh(list, ColMaxLenght(list, i),i), TryParseStringInListOfArrrays(list[1].ElementAt(i), i), ColMaxLenght(list, i));
            }

            return table;
        }


        public static int ColMaxLenght(List<string[]> arr, int i)
        {
            return arr.Skip(1).Max(p => p[i].Length);
        }


        public static string TryParseStringInListOfArrrays(string value, int i)
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


        public static string GetFirstValueFromListOfArrraysByMaxLentgh(List<string[]> array, int maxLentgh, int i)
        {
            return array.Skip(1).First(p => p[i].Length == maxLentgh).ElementAt(i);
        }

    }
}
