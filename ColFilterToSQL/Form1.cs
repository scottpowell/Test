using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ColFilterToSQL;

namespace ColFilterToSQL
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {

            try
            {
                AdvancedColumnFilterParser filter = new AdvancedColumnFilterParser(tbInputColumn.Text,AdvancedColumnFilterParser.DataType.Number, tbInputFilter.Text);
                tbSQLColumn.Text = filter.ColumnName;
                tbSQLExpression.Text = filter.SQLExpression;

                lbParameters.Items.Clear();
                foreach (AdvancedColumnFilterParameter parameter in filter.ColumnFilterParameters)
                {
                    lbParameters.Items.Add(parameter.FilterName + " - " + parameter.FilterValue);
                }

            }
            catch (Exception ex)
            {
                tbSQLExpression.Text = ex.Message;
            }

        }
    }
}
