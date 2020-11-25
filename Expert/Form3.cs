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
    public partial class Form3 : Form
    {
        List<DataTable> ldt;
        DataTable dt;
        DataTable dt2;
        int sum = 0;
        float kendall = 0;

        public Form3(DataTable d_t)
        {
            dt = d_t;
            InitializeComponent();

            dt2 = new DataTable();
            dt2.Columns.Add(" ");
            int count__objects = dt.Rows.Count;
            string obj_i;
            string exp_j;

            for (int i = 1; i < count__objects + 1; i++)
            {
                obj_i = String.Format("Объект {0}", i);
                dt2.Rows.Add(obj_i);
                dt2.Columns.Add(obj_i);
            }

            int result = 0;
            
            ldt = new List<DataTable>();
            for (int j = 1; j < dt.Columns.Count; j++)
            {
                DataTable dat = new DataTable();
                dat = dt2.Copy();
                ldt.Add(dat);
                exp_j = String.Format("Эксперт {0}", j);
                
                for (int i = 0; i < count__objects ; i++)
                {
                    for (int l = 0; l < count__objects; l++)
                    {
                        if (Int32.Parse(dt.Rows[i][j].ToString()) < Int32.Parse(dt.Rows[l][j].ToString()))
                        {
                            result = 1;
                        }
                        else if (Int32.Parse(dt.Rows[i][j].ToString()) > Int32.Parse(dt.Rows[l][j].ToString()))
                        {
                            result = -1;
                        }
                        else
                        {
                            result = 0;
                        }
                        ldt[j-1].Rows[i][l + 1] = result;
                    }
                }
            }
            
            for (int j = 1; j < dt.Columns.Count + 1; j++)
            {
                comboBox1.Items.Add(j);
            }
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            sum = 0;
            int count__objects = dt.Rows.Count;
            int selectedState = Int32.Parse(comboBox1.SelectedItem.ToString());
            if (selectedState < dt.Columns.Count)
            {
                dataGridView1.DataSource = ldt[selectedState - 1];
            }
            else
            {
                DataTable dat = new DataTable();
                dat = dt2.Copy();
                
                for (int i = 0; i < count__objects; i++)
                {

                    for (int l = 0; l < count__objects; l++)
                    {
                        int mult = 1;
                        for (int j = 1; j < dt.Columns.Count; j++)
                        {
                            mult = mult * Int32.Parse(ldt[j - 1].Rows[i][l + 1].ToString());
                        }
                        dat.Rows[i][l + 1] = mult;
                        sum += mult;
                    }
                }
                if (sum != 0)
                {
                    kendall = 2 * sum / (count__objects * (count__objects - 1) * (dt.Columns.Count - 2) ); 
                }
                label1.Text = "Коэффициент Кендалла = " + kendall.ToString();
                dataGridView1.DataSource = dat;

            }

        }
    }
}
