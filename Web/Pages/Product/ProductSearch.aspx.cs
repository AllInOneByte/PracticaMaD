using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.ProductService;
using Es.Udc.DotNet.PracticaMaD.Model.ShoppingService.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Es.Udc.DotNet.PracticaMaD.Web.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Product
{
    public partial class ProductSearch : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int startIndex, count;
            string selectedValue = "-1";

            lnkPrevious.Visible = false;
            lnkNext.Visible = false;
            lblNoProducts.Visible = false;
            lblAmountError.Visible = false;

            /* Get the Service */
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IProductService productService = iocManager.Resolve<IProductService>();

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

            /* Get keyword */
            string keyword = Request.Params.Get("keyword");

            if (keyword != null)
            {
                txtSearch.Text = keyword;

                /* Get category id */
                try
                {
                    long catId = long.Parse(Request.Params.Get("category"));

                    selectedValue = Request.Params.Get("category");

                    /* Get Products Info */
                    ProductBlock productBlock = productService.FindAllProductsByKeyword(keyword, catId, startIndex, count);

                    if (productBlock.Products.Count == 0)
                    {
                        lblNoProducts.Visible = true;
                    }
                    else
                    {
                        gvProducts.DataSource = productBlock.Products;
                        gvProducts.DataBind();

                        /* "Previous" link */
                        if ((startIndex - count) >= 0)
                        {
                            string url =
                                "/Pages/Product/ProductSearch.aspx" + "?keyword=" + keyword + "&category=" + catId +
                                "&startIndex=" + (startIndex - count) + "&count=" + count;

                            lnkPrevious.NavigateUrl = Response.ApplyAppPathModifier(url);
                            lnkPrevious.Visible = true;
                        }

                        /* "Next" link */
                        if (productBlock.ExistMoreProducts)
                        {
                            string url =
                                "/Pages/Product/ProductSearch.aspx" + "?keyword=" + keyword + "&category=" + catId +
                                "&startIndex=" + (startIndex + count) + "&count=" + count;

                            lnkNext.NavigateUrl = Response.ApplyAppPathModifier(url);
                            lnkNext.Visible = true;
                        }
                    }
                }
                catch (ArgumentNullException)
                {
                    /* Get Products Info */
                    ProductBlock productBlock = productService.FindAllProductsByKeyword(keyword, startIndex, count);

                    if (productBlock.Products.Count == 0)
                    {
                        lblNoProducts.Visible = true;
                    }
                    else
                    {
                        gvProducts.DataSource = productBlock.Products;
                        gvProducts.DataBind();

                        /* "Previous" link */
                        if ((startIndex - count) >= 0)
                        {
                            string url =
                                "/Pages/Product/ProductSearch.aspx" + "?keyword=" + keyword +
                                "&startIndex=" + (startIndex - count) + "&count=" + count;

                            lnkPrevious.NavigateUrl = Response.ApplyAppPathModifier(url);
                            lnkPrevious.Visible = true;
                        }

                        /* "Next" link */
                        if (productBlock.ExistMoreProducts)
                        {
                            string url =
                                "/Pages/Product/ProductSearch.aspx" + "?keyword=" + keyword +
                                "&startIndex=" + (startIndex + count) + "&count=" + count;

                            lnkNext.NavigateUrl = Response.ApplyAppPathModifier(url);
                            lnkNext.Visible = true;
                        }
                    }
                }
            }
            /* If there is no keyword, search all products */
            else
            {
                try 
                {
                    long tagId = long.Parse(Request.Params.Get("tagId"));

                    /* Get Products Info */
                    ProductBlock productBlock = productService.FindAllProductsByTag(tagId, startIndex, count);

                    if (productBlock.Products.Count == 0)
                    {
                        lblNoProducts.Visible = true;
                    }
                    else
                    {
                        gvProducts.DataSource = productBlock.Products;
                        gvProducts.DataBind();

                        /* "Previous" link */
                        if ((startIndex - count) >= 0)
                        {
                            string url =
                                "/Pages/Product/ProductSearch.aspx" + "?tagId=" + tagId + "startIndex=" + (startIndex - count) +
                                "&count=" + count;

                            lnkPrevious.NavigateUrl = Response.ApplyAppPathModifier(url);
                            lnkPrevious.Visible = true;
                        }

                        /* "Next" link */
                        if (productBlock.ExistMoreProducts)
                        {
                            string url =
                                "/Pages/Product/ProductSearch.aspx" + "?tagId=" + tagId + "startIndex=" + (startIndex + count) +
                                "&count=" + count;

                            lnkNext.NavigateUrl = Response.ApplyAppPathModifier(url);
                            lnkNext.Visible = true;
                        }
                    }
                }
                catch (ArgumentNullException)
                {
                    /* Get Products Info */
                    ProductBlock productBlock = productService.FindAllProducts(startIndex, count);

                    if (productBlock.Products.Count == 0)
                    {
                        lblNoProducts.Visible = true;
                    }
                    else
                    {
                        gvProducts.DataSource = productBlock.Products;
                        gvProducts.DataBind();

                        /* "Previous" link */
                        if ((startIndex - count) >= 0)
                        {
                            string url =
                                "/Pages/Product/ProductSearch.aspx" + "?startIndex=" + (startIndex - count) +
                                "&count=" + count;

                            lnkPrevious.NavigateUrl = Response.ApplyAppPathModifier(url);
                            lnkPrevious.Visible = true;
                        }

                        /* "Next" link */
                        if (productBlock.ExistMoreProducts)
                        {
                            string url =
                                "/Pages/Product/ProductSearch.aspx" + "?startIndex=" + (startIndex + count) +
                                "&count=" + count;

                            lnkNext.NavigateUrl = Response.ApplyAppPathModifier(url);
                            lnkNext.Visible = true;
                        }
                    }
                }
            }

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

                CategoryDropDownList.DataSource = dv;
                CategoryDropDownList.DataTextField = "CategoryNameField";
                CategoryDropDownList.DataValueField = "CategoryIdField";

                // Bind the data to the control.
                CategoryDropDownList.DataBind();

                // Set the default selected item.
                CategoryDropDownList.SelectedValue = selectedValue;
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
                string keyword = txtSearch.Text;
                long catId = long.Parse(CategoryDropDownList.SelectedValue);
                
                if (keyword == "")
                {
                    return;
                }

                if (catId > 0)
                {
                    Response.Redirect("/Pages/Product/ProductSearch.aspx" + "?keyword=" + keyword + "&category=" + catId);
                }
                else
                {
                    Response.Redirect("/Pages/Product/ProductSearch.aspx" + "?keyword=" + keyword);
                }
            }
        }

        protected void BtnAddCartClick(object sender, EventArgs e)
        {
            try
            {
                Button button = (Button)sender;
                long id = long.Parse(button.Attributes["pId"]);

                SessionManager.AddToShoppingCart(Context, id, 1, false);

                Response.Redirect(
                   Response.ApplyAppPathModifier("~/Pages/Shopping/Cart.aspx"));

            }
            catch (StockEmptyException ex)
            {
                int index = ex.Message.LastIndexOf('|');
                string men = ex.Message.Substring(index + 15);

                lblAmountError.Text += men;
                lblAmountError.Visible = true;
            }
        }

        protected void gvProducts_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Button btn = (Button)e.Row.FindControl("btnAddCart");
                btn.Attributes["pId"] = e.Row.Cells[0].Text;
            }
        }

        protected void gvProducts_RowCreated(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[0].Visible = false;
        }
    }
}