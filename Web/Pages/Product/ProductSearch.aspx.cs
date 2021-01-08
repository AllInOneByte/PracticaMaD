using System;
using System.Collections;
using System.Data;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Product
{
    public partial class ProductSearch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Load data for the DropDownList control only once, when the 
            // page is first loaded.
            if (!IsPostBack)
            {
                // Specify the data source and field names for the Text 
                // and Value properties of the items (ListItem objects) 
                // in the DropDownList control.
                CategoryDropDownList.DataSource = CreateDataSource();
                CategoryDropDownList.DataTextField = "CategoryNameField";
                CategoryDropDownList.DataValueField = "CategoryIdField";

                // Bind the data to the control.
                CategoryDropDownList.DataBind();

                // Set the default selected item.
                CategoryDropDownList.SelectedIndex = 0;
            }
        }

        protected ICollection CreateDataSource()
        {
            // Create a table to store data for the DropDownList control.
            DataTable dt = new DataTable();

            // Define the columns of the table.
            dt.Columns.Add(new DataColumn("CategoryNameField", typeof(string)));
            dt.Columns.Add(new DataColumn("CategoryIdField", typeof(long)));

            // Populate the table with sample values.
            dt.Rows.Add(CreateRow("-", -1, dt));

            // Create a DataView from the DataTable to act as the data source
            // for the DropDownList control.
            DataView dv = new DataView(dt);

            return dv;
        }

        protected DataRow CreateRow(string text, long value, DataTable dt)
        {
            // Create a DataRow using the DataTable defined in the 
            // CreateDataSource method.
            DataRow dr = dt.NewRow();
            
            dr[0] = text;
            dr[1] = value;

            return dr;
        }
    }
}