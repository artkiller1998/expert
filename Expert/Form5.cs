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
    public partial class Form5 : Form
    {
        public Form5(DataTable dt)
        {
            InitializeComponent();

            DataTable dt5 = new DataTable();
            dt5 = dt.Copy();
            dt5.Columns.Add(String.Format("Сумма рангов"));
            dt5.Columns.Add(String.Format("Сумма рангов - MO"));
            //dt5.Columns.Add(String.Format("Конкордация"));
            float sum_rang;
            float sum = 0;

            for (int j = 0; j < dt.Rows.Count; j++)
            {
                sum_rang = 0;
                for (int i = 1; i < dt.Columns.Count; i++)
                {
                    dt5.Rows[j][i] = dt.Rows[j][i];
                    sum_rang += Int32.Parse(dt.Rows[j][i].ToString());
                }
                //Вычисление суммы рангов
                sum += sum_rang;
                dt5.Rows[j][dt.Columns.Count] = sum_rang;
            }
            //Вычисление мат. ожидания.
            float mo = sum / dt.Rows.Count;
            label1.Text = "Mат. ожидание = " + String.Format("{0:f3}", mo);

            double d_max = ((dt.Columns.Count - 1) * (dt.Columns.Count - 1) * (Math.Pow((dt.Rows.Count), 3) - dt.Rows.Count)) / (12 * (dt.Rows.Count - 1));

            double sum_kv = 0;
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                dt5.Rows[j][dt.Columns.Count + 1] = float.Parse(dt5.Rows[j][dt.Columns.Count].ToString()) - mo;
                sum_kv += Math.Pow(float.Parse(dt5.Rows[j][dt.Columns.Count + 1].ToString()), 2);
            }
            double disp = sum_kv / (double.Parse(dt.Rows.Count.ToString()) - 1);
            label2.Text = String.Format("Дисперсия = {0:f3}", (disp));
            label3.Text = String.Format("Конкордация {0:f3}", (disp / d_max));
            dataGridView1.DataSource = dt5;
        }
    }
}
