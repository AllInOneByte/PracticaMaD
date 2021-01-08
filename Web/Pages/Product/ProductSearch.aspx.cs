using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.ProductService;
using Es.Udc.DotNet.PracticaMaD.Web.Properties;
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Product
{
    public partial class ProductSearch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int startIndex, count;

            lnkPrevious.Visible = false;
            lnkNext.Visible = false;

            /* Get Start Index */
            try
            {
                startIndex = int.Parse(Request.Params.Get("startIndex"));
            }
            catch (ArgumentNullException)
            {
                startIndex = 0;
            }

            /* Get Count */
            try
            {
                count = int.Parse(Request.Params.Get("count"));
            }
            catch (ArgumentNullException)
            {
                count = int.Parse(ConfigurationManager.AppSettings["defaultCount"]);
            }

            /* Get the Service */
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IProductService accountService = iocManager.Resolve<IProductService>();

            /* Get Accounts Info */
            ProductBlock productBlock =
                accountService.FindAllProducts(startIndex, count);

            if (productBlock.Products.Count == 0)
            {
                lblNoProducts.Visible = true;
                return;
            }

            gvProducts.DataSource = productBlock.Products;
            gvProducts.DataBind();

            /* "Previous" link */
            if ((startIndex - count) >= 0)
            {
                string url =
                    Settings.Default.PracticaMaD_applicationURL +
                    "/Pages/ProductSearch.aspx" + "?startIndex=" + (startIndex - count) +
                    "&count=" + count;

                lnkPrevious.NavigateUrl = Response.ApplyAppPathModifier(url);
                lnkPrevious.Visible = true;
            }

            /* "Next" link */
            if (productBlock.ExistMoreProducts)
            {
                string url =
                    Settings.Default.PracticaMaD_applicationURL +
                    "/Pages/ShowAccountsByUserID.aspx" + "?startIndex=" + (startIndex + count) +
                    "&count=" + count;

                lnkNext.NavigateUrl =
                    Response.ApplyAppPathModifier(url);
                lnkNext.Visible = true;
            }

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