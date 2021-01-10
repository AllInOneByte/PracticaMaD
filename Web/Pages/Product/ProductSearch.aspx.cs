using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.ProductService;
using Es.Udc.DotNet.PracticaMaD.Web.Properties;
using System;
using System.Collections.Generic;
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
            lblNoProducts.Visible = false;

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
                count = Settings.Default.PracticaMaD_defaultCount;
            }

            /* Get the Service */
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IProductService productService = iocManager.Resolve<IProductService>();

            /* Get Products Info */
            ProductBlock productBlock =
                productService.FindAllProducts(startIndex, count);

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
                    "/Pages/Product/ProductSearch.aspx" + "?startIndex=" + (startIndex - count) +
                    "&count=" + count;

                lnkPrevious.NavigateUrl = Response.ApplyAppPathModifier(url);
                lnkPrevious.Visible = true;
            }

            /* "Next" link */
            if (productBlock.ExistMoreProducts)
            {
                string url =
                    Settings.Default.PracticaMaD_applicationURL +
                    "/Pages/Product/ProductSearch.aspx" + "?startIndex=" + (startIndex + count) +
                    "&count=" + count;

                lnkNext.NavigateUrl =
                    Response.ApplyAppPathModifier(url);
                lnkNext.Visible = true;
            }

            // Load data for the DropDownList control only once, when the 
            // page is first loaded.
            if (!IsPostBack)
            {
                List<Category> categories = productService.FindAllCategories();

                // Create a table to store data for the DropDownList control.
                DataTable dt = new DataTable();

                // Define the columns of the table.
                dt.Columns.Add(new DataColumn("CategoryNameField", typeof(string)));
                dt.Columns.Add(new DataColumn("CategoryIdField", typeof(long)));

                // Populate the table.
                dt.Rows.Add(CreateRow("-", -1, dt));

                foreach (Category category in categories)
                {
                    dt.Rows.Add(CreateRow(category.categoryName, category.categoryId, dt));
                }

                // Create a DataView from the DataTable to act as the data source
                // for the DropDownList control.
                DataView dv = new DataView(dt);

                CategoryDropDownList.DataSource = dt;
                CategoryDropDownList.DataTextField = "CategoryNameField";
                CategoryDropDownList.DataValueField = "CategoryIdField";

                // Bind the data to the control.
                CategoryDropDownList.DataBind();

                // Set the default selected item.
                CategoryDropDownList.SelectedIndex = 0;
            }
        }

        protected DataRow CreateRow(string text, long value, DataTable dt)
        {
            // Create a DataRow using the DataTable.
            DataRow dr = dt.NewRow();

            dr[0] = text;
            dr[1] = value;

            return dr;
        }

        protected void BtnSearchClick(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string keyword;
                long catId;

                int startIndex, count;

                lnkPrevious.Visible = false;
                lnkNext.Visible = false;
                lblNoProducts.Visible = false;

                /* Get keyword */
                try
                {
                    keyword = Request.Params.Get("keyword");
                }
                catch (ArgumentNullException)
                {
                    keyword = txtSearch.Text;
                }

                /* Get category id */
                try
                {
                    catId = int.Parse(Request.Params.Get("category"));
                }
                catch (ArgumentNullException)
                {
                    catId = int.Parse(CategoryDropDownList.SelectedItem.Value);
                }

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
                    count = Settings.Default.PracticaMaD_defaultCount;
                }

                /* Get the Service */
                IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
                IProductService productService = iocManager.Resolve<IProductService>();

                /* Get Accounts Info */
                ProductBlock productBlock =
                    productService.FindAllProductsByKeyword(keyword, catId, startIndex, count);

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
                        "/Pages/Product/ProductSearch.aspx" + "?startIndex=" + (startIndex - count) +
                        "&count=" + count + "&keyword=" + keyword + "&category=" + catId;

                    lnkPrevious.NavigateUrl = Response.ApplyAppPathModifier(url);
                    lnkPrevious.Visible = true;
                }

                /* "Next" link */
                if (productBlock.ExistMoreProducts)
                {
                    string url =
                        Settings.Default.PracticaMaD_applicationURL +
                        "/Pages/Product/ProductSearch.aspx" + "?startIndex=" + (startIndex + count) +
                        "&count=" + count + "&keyword=" + keyword + "&category=" + catId;

                    lnkNext.NavigateUrl =
                        Response.ApplyAppPathModifier(url);
                    lnkNext.Visible = true;
                }
            }
        }
    }
}