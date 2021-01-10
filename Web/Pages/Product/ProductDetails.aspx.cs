using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.ProductService;
using System;
using System.Globalization;
using System.Web;

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
                productId = long.Parse(Request.Params.Get("product"));
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
                "/Pages/Product/ProductComments.aspx" +
                "?product=" + product.productId;

            hlComments.Visible = true;
        }
    }
}