using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KTetris
{
    public partial class frmBlockEditior : Form
    {
        public frmBlockEditior()
        {
            InitializeComponent();
        }
        private void cP_CellClickEvent(Cell c)
        {
            c.Value = "1";
            c.BackColor = Color.Blue;
        }
        PanelGrid cP;
        private void btnGen_Click(object sender, EventArgs e)
        {
            int Size=int.Parse (this.txtSize.Text );
             cP = new PanelGrid(this.doubleBufferedPanel1, Size, Size, 30);
            cP.CellClickEvent +=new PanelGrid.CellClickEventHandler(cP_CellClickEvent);
            int i;
            int j;
            foreach(Cell c in cP.Table.Cells )
            {
                c.Value = "";
                c.BackColor = Color.White;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int i;
            int j;
            for (i = 0; i < cP.Table.Rows.Count; i++)
            {
                for (j = 0; j < cP.Table.Rows[0].Cols.Count; j++)
                {
                    cP.Table.Rows[i].Cols[j].Value = "";
                    cP.Table.Rows[i].Cols[j].BackColor = Color.White;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            /*
                    InitialTableT(intTable, Size );
                    intTable.Rows[0].Cols[0] = 1;
                    intTable.Rows[0].Cols[1] = 1;
                    intTable.Rows[0].Cols[2] = 1;
                    intTable.Rows[1].Cols[2] = 1;
                    lstRolation.Add(intTable);
             */
            StringBuilder strB = new StringBuilder();
            string strIntend = "         ";
            strB.Append(Environment.NewLine);
            strB.Append(strIntend).Append("InitialTableT(intTable, Size );").Append(Environment.NewLine);
            int i;
            int j;
            foreach(Cell c in this.cP.Table.Cells )
            {
                if (c.Value == "")
                {
                    continue;
                }


                string strTemplate = strIntend + "intTable.Rows[!Y!].Cols[!X!] = 1;";

                strTemplate = strTemplate.Replace("!Y!", c.Position.Y.ToString())
                                         .Replace("!X!", c.Position.X.ToString());
                strB.Append(strTemplate).Append(Environment.NewLine);
            }

            strB.Append(strIntend).Append("lstRolation.Add(intTable);").Append(Environment.NewLine);
            this.textBox1.Text += strB.ToString();

        }
    }
}
