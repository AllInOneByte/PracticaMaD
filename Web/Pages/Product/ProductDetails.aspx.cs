using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.ProductService;
using Es.Udc.DotNet.PracticaMaD.Web.Properties;
using System;
using System.Globalization;
using System.Web;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using System.Collections.Generic;
using Es.Udc.DotNet.PracticaMaD.Model.ShoppingService;
using Es.Udc.DotNet.PracticaMaD.Model.ShoppingService.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Product
{
    public partial class ProductDetails : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            lblProductError.Visible = false;
            TableProductInfo.Visible = false;
            hlComments.Visible = false;

            long productId;

            /* Get productId */
            try
            {
                productId = int.Parse(Request.Params.Get("product"));
            }
            catch (ArgumentNullException)
            {
                lblProductError.Visible = true;
                return;
            }

            TableProductInfo.Visible = true;

            /* Get the Service */
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IProductService productService = iocManager.Resolve<IProductService>();

            /* Get Products Info */
            Model.Product product = productService.FindProduct(productId);

            cellProductID.Text = product.productId.ToString();
            cellProductName.Text = product.productName;
            cellCategoryName.Text = product.Category.categoryName;
            cellProductDate.Text = product.productDate.ToString("d/M/yyyy");
            cellProductPrice.Text = product.productPrice.ToString("C", CultureInfo.CurrentCulture);
            cellProductQuantity.Text = product.productQuantity.ToString();

            hlComments.NavigateUrl =
                Settings.Default.PracticaMaD_applicationURL + "/Pages/Product/ProductComments.aspx" +
                "?product=" + product.productId;

            btnAddCart.CommandArgument = product.productId.ToString();

            lnkUpdate.NavigateUrl += product.productId;
            if (SessionManager.IsAdminAuthenticated(Context))
            {
                lnkUpdate.Visible = true;
            }

            hlComments.Visible = true;
        }

        protected void BtnAddCartClick(object sender, EventArgs e)
        {
            try
            {
                long id = Convert.ToInt64(btnAddCart.CommandArgument);
                int amount = Convert.ToInt32(txtAmount.Text);

                SessionManager.AddToShoppingCart(Context, id, amount, checkGift.Checked);

                Response.Redirect(
                   Response.ApplyAppPathModifier("~/Pages/Shopping/Cart.aspx"));

            }
            catch (StockEmptyException)
            {
                lblAmountError.Visible = true;
            }
        }
    }
}