using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace A6_DUNNW
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void customersBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.customersBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.northWind_SmallDataSet);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                this.orderTableAdapter.Fill(this.northWind_SmallDataSet.Orders);
                this.customersTableAdapter.Fill(this.northWind_SmallDataSet.Customers);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOrderCost_Click(object sender, EventArgs e)
		{
			String strConn;
			OleDbConnection cnnNorthWind;
			
			String unitPrice;
			OleDbCommand cmdNorthWindUP;
			OleDbDataReader rdrNorthWindUP;
			
			String lineItems;
			OleDbCommand cmdNorthWindLI;
			OleDbDataReader rdrNorthWindLI;
			
			String totalItemsShipped;
			OleDbCommand cmdNorthWindTIS;
			OleDbDataReader rdrNorthWindTIS;
			
			String avgDisc;
			OleDbCommand cmdNorthWindAD;
			OleDbDataReader rdrNorthWindAD;
			
			strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=NorthWindSmall.accdb";
			
			unitPrice = "Select sum([UnitPrice])" +
						" From [OrderDetails] " +
						" Where [OrderID] = " +
						ordersDataGridView.CurrentRow.Cells[0].Value;
						
			lineItems = "Select count([OrderID])" +
						" From [OrderDetails] " +
						" Where [OrderID] = " +
						ordersDataGridView.CurrentRow.Cells[0].Value;
						
			totalItemsShipped = "Select sum([Quantity])" +
						" From [OrderDetails] " +
						" Where [OrderID] = " +
						ordersDataGridView.CurrentRow.Cells[0].Value;
								
			avgDisc = "Select sum([Discount])" +
						" From [OrderDetails] " +
						" Where [OrderID] = " +
						ordersDataGridView.CurrentRow.Cells[0].Value;
						
			try
			{
				cnnNorthWind = new OleDbConnection(strConn);
				cmmNorthWind.Open();
				
				cmdNorthWindUP = new OleDbCommand(unitPrice, cnnNorthWind);
				cmdNorthWindLI = new OleDbCommand(lineItems, cnnNorthWind);
				cmdNorthWindTIS = new OleDbCommand(totalItemsShipped, cnnNorthWind);
				cmdNorthWindAD = new OleDbCommand(avgDisc, cnnNorthWind);
				
				rdrNorthWindUP = cmdNorthWindUP.ExecuteReader();
				rdrNorthWindLI = cmdNorthWindLI.ExecuteReader();
				rdrNorthWindTIS = cmdNorthWindTIS.ExecuteReader();
				rdrNorthWindAD = cmdNorthWindAD.ExecuteReader();
				
				MessageBox.Show("Total Product Cost: $" + rdrNorthWindUP[0] + "\n # of line items: " 
				+ rdrNorthWindLI[0] + "\n Total Items Shipped: " + rdrNorthWindTIS[0] + "\n Average Discount %: " 
				+ rdrNorthWindAD[0] + " %", "Order Details for order#" + ordersDataGridView.CurrentRow.Cells[0].Value);
				
				cnnNorthWind.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
	}
}
