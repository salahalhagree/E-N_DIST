using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace An_Improved_N_DIST
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           // Enhensment(Improved)  N-DIST (E-N-DIST) 2019
            int n = 2;
          
            for (int a = 0; a < dataGridView4.RowCount - 1; a++)
            {

                
                string source = Convert.ToString(dataGridView4.Rows[a].Cells[1].Value.ToString());
                string target = Convert.ToString(dataGridView4.Rows[a].Cells[2].Value.ToString());


                int sl = source.Length;
                int tl = target.Length;

                if (sl == 0 || tl == 0)
                {
                    if (sl == tl)
                    {
                        textBox1.Text = "1";
                    }
                    else
                    {
                        textBox1.Text = "1";
                    }
                }

                int cost = 0;
                int costid = 0;
                if (sl < n || tl < n)
                {
                    for (int ii = 0, ni = Math.Min(sl, tl); ii < ni; ii++)
                    {
                        if (source[ii] == target[ii])
                        {
                            cost++;
                        }
                    }
                    textBox1.Text = Convert.ToString((float)cost / Math.Max(sl, tl));
                }

                char[] sa = new char[sl + n - 1];
                float[] p; //'previous' cost array, horizontally
                float[] d; // cost array, horizontally
                float[] _d; //placeholder to assist in swapping p and d

                //construct sa with prefix
                for (int ii = 0; ii < sa.Length; ii++)
                {
                    if (ii < n - 1)
                    {
                        sa[ii] = (char)0; //add prefix
                    }
                    else
                    {
                        sa[ii] = source[ii - n + 1];
                    }
                }
                p = new float[sl + 1];
                d = new float[sl + 1];

                // indexes into strings s and t
                int i; // iterates through source
                int j; // iterates through target

                char[] t_j = new char[n]; // jth n-gram of t

                for (i = 0; i <= sl; i++)
                {
                    p[i] = i;
                }
                //for loop  1
                for (j = 1; j <= tl; j++)
                {
                    //construct t_j n-gram 
                    if (j < n)
                    {
                        for (int ti = 0; ti < n - j; ti++)
                        {
                            t_j[ti] = (char)0; //add prefix
                           
                        }
                        for (int ti = n - j; ti < n; ti++)
                        {
                            t_j[ti] = target[ti - (n - j)];
                          
                        }
                    }
                    else
                    {
                        t_j = target.Substring(j - n, n).ToCharArray();
                       

                    }
                    d[0] = j;
                    //for loop  2
                    for (i = 1; i <= sl; i++)
                    {
                        //cost = 0;
                        cost = n;
                        costid = n;
                        int tn = n;

                        dataG.Rows.Add();
                        dataG.Rows[i - 1].Cells[0].Value = sa[i];

                        //compare sa to t_j
                        //for loop  3
                        for (int ni = 0; ni < n; ni++)
                        {
                            // if (sa[i - 1 + ni] != t_j[ni])
                            if (ni < n - 1)
                            {
                                if ((sa[i - 1 + ni] == t_j[ni]))
                                {
                                    cost--;

                                }
                                else if ((sa[i - 1 + ni] == t_j[ni + 1]))
                                {
                                    cost--;

                                }
                                else if ((sa[i - 1 + ni + 1] == t_j[ni]))
                                {
                                    cost--;

                                }
                                if (sa[i - 1 + ni] == 0)
                                { //discount matches on prefix
                                    tn--;
                                }

                                //خاصة بالحذف والاضافة نشوف
                                if (sa[i - 1 + ni] == t_j[ni + 1])
                                {
                                    costid--;

                                }

                            }
                            else if (ni >= n - 1)// من اجل التناضر symtric 
                            {

                                if ((sa[i - 1 + ni] == t_j[ni]))
                                {
                                    cost--;
                                }
                                else if ((sa[i - 1 + ni] == t_j[ni - 1]))
                                {
                                    cost--;
                                }
                                else if ((sa[i - 1 + ni - 1] == t_j[ni]))
                                {
                                    cost--;
                                }

                                //خاصة بالحذف والاضافة نشوف
                                if (sa[i - 1 + ni] == t_j[ni - 1])
                                {
                                    costid--;

                                }


                            }



                            dataG.Columns[j].HeaderText = Convert.ToString(t_j[ni]);
                        }
                        float ec = (float)cost / tn;
                        float ecid = (float)costid / tn;
                        d[i] = Math.Min(Math.Min(d[i - 1] + ecid, p[i] + ecid), p[i - 1] + ec);
                        dataG.Rows[i - 1].Cells[j - 1].Value = d[i];//
                      
                    }
                    // copy current distance counts to 'previous row' distance counts
                    _d = p;
                    p = d;
                    d = _d;
                    //dataG.Rows.Clear();

                }

                // our last action in the above loop was to switch d and p, so p now
                // actually has the most recent cost counts

                dataGridView4.Rows[a].Cells[5].Value = (float)p[sl];
                dataGridView4.Rows[a].Cells[6].Value = 1.0f - ((float)p[sl] / Math.Max(tl, sl));

            }
      
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //  N-DIST 2005
           
            int n = 2;

           
            for (int a = 0; a < dataGridView4.RowCount - 1; a++)
            {

              
                string source = Convert.ToString(dataGridView4.Rows[a].Cells[1].Value.ToString());
                string target = Convert.ToString(dataGridView4.Rows[a].Cells[2].Value.ToString());


                int sl = source.Length;
                int tl = target.Length;

                if (sl == 0 || tl == 0)
                {
                    if (sl == tl)
                    {
                        textBox1.Text = "1";
                    }
                    else
                    {
                        textBox1.Text = "1";
                    }
                }

                int cost = 0;
                if (sl < n || tl < n)
                {
                    for (int ii = 0, ni = Math.Min(sl, tl); ii < ni; ii++)
                    {
                        if (source[ii] == target[ii])
                        {
                            cost++;
                        }
                    }
                    textBox1.Text = Convert.ToString((float)cost / Math.Max(sl, tl));
                }

                char[] sa = new char[sl + n - 1];
                float[] p; //'previous' cost array, horizontally
                float[] d; // cost array, horizontally
                float[] _d; //placeholder to assist in swapping p and d

                //construct sa with prefix
                for (int ii = 0; ii < sa.Length; ii++)
                {
                    if (ii < n - 1)
                    {
                        sa[ii] = (char)0; //add prefix
                    }
                    else
                    {
                        sa[ii] = source[ii - n + 1];
                    }
                }
                p = new float[sl + 1];
                d = new float[sl + 1];

                // indexes into strings s and t
                int i; // iterates through source
                int j; // iterates through target

                char[] t_j = new char[n]; // jth n-gram of t

                for (i = 0; i <= sl; i++)
                {
                    p[i] = i;
                }
                //for loop  1
                for (j = 1; j <= tl; j++)
                {
                    //construct t_j n-gram 
                    if (j < n)
                    {
                        for (int ti = 0; ti < n - j; ti++)
                        {
                            t_j[ti] = (char)0; //add prefix
                            
                        }
                        for (int ti = n - j; ti < n; ti++)
                        {
                            t_j[ti] = target[ti - (n - j)];
                           
                        }
                    }
                    else
                    {
                        t_j = target.Substring(j - n, n).ToCharArray();
                       

                    }
                    d[0] = j;
                    //for loop  2
                    for (i = 1; i <= sl; i++)
                    {
                        cost = 0;
                        int tn = n;

                        dataG.Rows.Add();
                        dataG.Rows[i - 1].Cells[0].Value = sa[i];

                        //compare sa to t_j
                        //for loop  3
                        for (int ni = 0; ni < n; ni++)
                        {
                            if (
                                sa[i - 1 + ni] != t_j[ni])
                            {
                                cost++;
                            }
                            else if (sa[i - 1 + ni] == 0)
                            { //discount matches on prefix
                                tn--;
                            }
                            dataG.Columns[j].HeaderText = Convert.ToString(t_j[ni]);
                        }
                        float ec = (float)cost / tn;
                        d[i] = Math.Min(Math.Min(d[i - 1] + 1, p[i] + 1), p[i - 1] + ec);
                        dataG.Rows[i - 1].Cells[j - 1].Value = d[i];//
                        
                    }
                    // copy current distance counts to 'previous row' distance counts
                    _d = p;
                    p = d;
                    d = _d;
                    dataG.Rows.Clear();

                }

                // our last action in the above loop was to switch d and p, so p now
                // actually has the most recent cost counts

                dataGridView4.Rows[a].Cells[3].Value = (float)p[sl];
                dataGridView4.Rows[a].Cells[4].Value = 1.0f - ((float)p[sl] / Math.Max(tl, sl));
               
            }
          
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {


            String sq = "SELECT * FROM Dataset5  ORDER BY no2019  ";
            Program.cmd1.CommandText = sq;
            Program.cmd1.Connection = Program.con;
            Program.ad1.SelectCommand = Program.cmd1;
            Program.ad1.Fill(Program.ds, "Dataset5");
            dataGridView4.DataSource = Program.ds.Tables["Dataset5"];
        }
    }
}
