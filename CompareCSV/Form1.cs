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

namespace ComapreCSV
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            List<string[]> res = Compare.parseCSV(@"c:\tmp\ex.csv");
            DataTable table = Compare.ConvertListToDataTable(res);
            dataGridView1.DataSource = table;

            List<string[]> res_1 = Compare.parseCSV(@"c:\tmp\ex_1.csv");
            DataTable table_1 = Compare.ConvertListToDataTable(res_1);
            dataGridView2.DataSource = table_1;

            this.ActiveControl = button1;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            var list_1 = dataGridView1.Rows.Cast<DataGridViewRow>().Where(row => row.Index >= 0) ;
            var list_2 = dataGridView2.Rows.Cast<DataGridViewRow>().Where(row => row.Index >= 0);

            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (!list_1.Any(s => s.Cells["ColumnName"].Value.Equals(row.Cells["ColumnName"].Value)))
                {
                    row.Cells["ColumnName"].Style.BackColor = Color.Cyan;
                }

                if (
                    list_1.Where(s => s.Cells["ColumnName"].Value.Equals(row.Cells["ColumnName"].Value)).Any(n => !n.Cells["Value in Column with Max Length"].Value.Equals(row.Cells["Value in Column with Max Length"].Value))
                    )
                {
                    row.Cells["Value in Column with Max Length"].Style.BackColor = Color.Lavender;
                }

                if (
                    list_1.Where(s => s.Cells["ColumnName"].Value.Equals(row.Cells["ColumnName"].Value)).Any(n => !n.Cells["Suggested Type"].Value.Equals(row.Cells["Suggested Type"].Value))
                    )
                {
                    row.Cells["Suggested Type"].Style.BackColor = Color.Lavender;
                }

                if (
                    list_1.Where(s => s.Cells["ColumnName"].Value.Equals(row.Cells["ColumnName"].Value)).Any(n => !n.Cells["Max Lenght"].Value.Equals(row.Cells["Max Lenght"].Value))
                    )
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

                if (
                    list_2.Where(s => s.Cells["ColumnName"].Value.Equals(row.Cells["ColumnName"].Value)).Any(n => !n.Cells["Value in Column with Max Length"].Value.Equals(row.Cells["Value in Column with Max Length"].Value))
                    )
                {
                    row.Cells["Value in Column with Max Length"].Style.BackColor = Color.Lavender;
                }

                if (
                    list_2.Where(s => s.Cells["ColumnName"].Value.Equals(row.Cells["ColumnName"].Value)).Any(n => !n.Cells["Suggested Type"].Value.Equals(row.Cells["Suggested Type"].Value))
                    )
                {
                    row.Cells["Suggested Type"].Style.BackColor = Color.Lavender;
                }

                if (
                    list_2.Where(s => s.Cells["ColumnName"].Value.Equals(row.Cells["ColumnName"].Value)).Any(n => !n.Cells["Max Lenght"].Value.Equals(row.Cells["Max Lenght"].Value))
                    )
                {
                    row.Cells["Max Lenght"].Style.BackColor = Color.Lavender;
                }
            }
            
            //???
            var result = dataGridView1.Rows.Cast<DataGridViewRow>().Select(p => p.Cells["ColumnName"].Value.ToString()).ToList().Intersect(dataGridView2.Rows.Cast<DataGridViewRow>().Select(p => p.Cells["ColumnName"].Value.ToString()).ToList());
            //dataGridView1.Rows.Cast<DataGridViewRow>().Where(p => p.Cells["ColumnName"].Value.ToString().ToList().Intersect(result)).Selected = true;

            dataGridView1.ClearSelection();
            dataGridView2.ClearSelection();
        }


        

    
    }
}
