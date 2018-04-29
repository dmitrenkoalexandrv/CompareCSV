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

            Compare comparator_1 = new Compare();
            Compare comparator_2 = new Compare();

            ListOfArray res = comparator_1.ParseCSV(@"c:\tmp\ex.csv");
            DataTable table = comparator_1.ConvertListToDataTable(res);
            dataGridView1.DataSource = table;

            ListOfArray res_1 = comparator_2.ParseCSV(@"c:\tmp\ex_1.csv");
            DataTable table_1 = comparator_2.ConvertListToDataTable(res_1);
            dataGridView2.DataSource = table_1;

            this.ActiveControl = button1;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            var list_1 = dataGridView1.Rows.Cast<DataGridViewRow>().Where(row => row.Index >= 0);
            var list_2 = dataGridView2.Rows.Cast<DataGridViewRow>().Where(row => row.Index >= 0);

            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (!list_1.Any(s => s.Cells["ColumnName"].Value.Equals(row.Cells["ColumnName"].Value)))
                {
                    row.Cells["ColumnName"].Style.BackColor = Color.Cyan;
                }

                if (Check.CheckColumns(list_1, row, "ColumnName", "Value in Column with Max Length"))
                {
                    row.Cells["Value in Column with Max Length"].Style.BackColor = Color.Lavender;
                }

                if (Check.CheckColumns(list_1, row, "ColumnName", "Suggested Type"))
                {
                    row.Cells["Suggested Type"].Style.BackColor = Color.Lavender;
                }

                if (Check.CheckColumns(list_1, row, "ColumnName", "Max Lenght"))
                {
                    row.Cells["Max Lenght"].Style.BackColor = Color.Lavender;
                }
            }


            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!list_2.Any(s => s.Cells["ColumnName"].Value.Equals(row.Cells["ColumnName"].Value)))
                {
                    row.Cells["ColumnName"].Style.BackColor = Color.Coral;
                }

                if (Check.CheckColumns(list_2, row, "ColumnName", "Value in Column with Max Length"))
                {
                    row.Cells["Value in Column with Max Length"].Style.BackColor = Color.Lavender;
                }

                if (Check.CheckColumns(list_2, row, "ColumnName", "Suggested Type"))
                {
                    row.Cells["Suggested Type"].Style.BackColor = Color.Lavender;
                }

                if (Check.CheckColumns(list_2, row, "ColumnName", "Max Lenght"))
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
