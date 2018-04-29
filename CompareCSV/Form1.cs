using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CompareCSV.BL;
using CompareCSV.Data;

namespace ComapreCSV
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
            IRepository repo1 = new Repository();
            IRepository repo2 = new Repository();

            Compare comparator1 = new Compare(repo1);
            Compare comparator2 = new Compare(repo2);

            ListOfArray res1 = comparator1.GenerateData(@"c:\tmp\ex.csv");
            DataTable table1 = comparator1.ConvertListToDataTable(res1);
            dataGridView1.DataSource = table1;

            ListOfArray res2 = comparator2.GenerateData(@"c:\tmp\ex_1.csv");
            DataTable table2 = comparator2.ConvertListToDataTable(res2);
            dataGridView2.DataSource = table2;

            this.ActiveControl = button1;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            var list1 = dataGridView1.Rows.Cast<DataGridViewRow>().Where(row => row.Index >= 0);
            var list2 = dataGridView2.Rows.Cast<DataGridViewRow>().Where(row => row.Index >= 0);

            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (!list1.Any(s => s.Cells["ColumnName"].Value.Equals(row.Cells["ColumnName"].Value)))
                {
                    row.Cells["ColumnName"].Style.BackColor = Color.Cyan;
                }

                if (Check.CheckColumns(list1, row, "ColumnName", "Value in Column with Max Length"))
                {
                    row.Cells["Value in Column with Max Length"].Style.BackColor = Color.Lavender;
                }

                if (Check.CheckColumns(list1, row, "ColumnName", "Suggested Type"))
                {
                    row.Cells["Suggested Type"].Style.BackColor = Color.Lavender;
                }

                if (Check.CheckColumns(list1, row, "ColumnName", "Max Lenght"))
                {
                    row.Cells["Max Lenght"].Style.BackColor = Color.Lavender;
                }
            }


            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!list2.Any(s => s.Cells["ColumnName"].Value.Equals(row.Cells["ColumnName"].Value)))
                {
                    row.Cells["ColumnName"].Style.BackColor = Color.Coral;
                }

                if (Check.CheckColumns(list2, row, "ColumnName", "Value in Column with Max Length"))
                {
                    row.Cells["Value in Column with Max Length"].Style.BackColor = Color.Lavender;
                }

                if (Check.CheckColumns(list2, row, "ColumnName", "Suggested Type"))
                {
                    row.Cells["Suggested Type"].Style.BackColor = Color.Lavender;
                }

                if (Check.CheckColumns(list2, row, "ColumnName", "Max Lenght"))
                {
                    row.Cells["Max Lenght"].Style.BackColor = Color.Lavender;
                }
            }

            // Результат сравнения колонок 2-ух DataGridViews по полю ColumnName (пока не используется)
            var result = dataGridView1.Rows.Cast<DataGridViewRow>().Select(p => p.Cells["ColumnName"].Value.ToString()).ToList().Intersect(dataGridView2.Rows.Cast<DataGridViewRow>().Select(p => p.Cells["ColumnName"].Value.ToString()).ToList());

            dataGridView1.ClearSelection();
            dataGridView2.ClearSelection();
        }


        

    
    }
}
