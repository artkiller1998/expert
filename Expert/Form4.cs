using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Expert
{
    public partial class Form4 : Form
    {
        public Form4(DataTable dt)
        {
            InitializeComponent();

            DataTable dt4 = new DataTable();
            dt4 = dt.Copy();
            for (int j = 0; j < dt.Rows.Count; j++)
            { 
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                   dt4.Rows[j][i] = dt.Rows[j][i];
                }
            }
            int count_compares = dt4.Columns.Count-2;
            int count_columns = dt4.Columns.Count;

            for (int i = 1; i < count_compares + 1; i++)
            {
                dt4.Columns.Add(String.Format("d{0}", i));
                dt4.Columns.Add(String.Format("d{0}^2", i));
            }

            int d = 0;
            int s = 1;
            int sum_d2 = 0;
            double spirmen = 0;
            for (int step = 0; step/2 < count_compares; step = step + 2)
            {
                int d_pos = count_columns + step;
                for (int row = 0; row < dt4.Rows.Count; row ++)
                {
                    d = Int32.Parse(dt4.Rows[row][s].ToString()) - Int32.Parse(dt4.Rows[row][s + 1].ToString());
                    dt4.Rows[row][d_pos] = d;
                }
                s = s + 1;
                for (int row = 0; row < dt4.Rows.Count; row++)
                {
                    d = Int32.Parse(dt4.Rows[row][d_pos].ToString()) * Int32.Parse(dt4.Rows[row][d_pos].ToString());
                    dt4.Rows[row][d_pos+1] = d;
                    sum_d2 += d;
                }
            }
            dataGridView1.DataSource = dt4;
            int dr = dt4.Rows.Count;
            if (sum_d2 != 0)
            {
                spirmen = 1 - ( (6*sum_d2)/ ( (dt.Columns.Count-2) * dr * (Math.Pow(Int32.Parse(dr.ToString()),2) - 1) ) );
            }
            label1.Text = "Коэффициент Спирмена = " + spirmen.ToString();
        }
    }
}
