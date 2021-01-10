using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using System;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Product
{
    public partial class ModifyProduct : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            long productId = Int64.Parse(Request.Params.Get("productId"));

            var product = SessionManager.FindProduct(productId);

            txtName.Text = product.productName;
            txtPrice.Text = product.productPrice.ToString();
            txtQuantity.Text = product.productQuantity.ToString();

            btnUpdate.CommandArgument = product.productId.ToString();
        }

        protected void BtnUpdateClick(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    long producId = Convert.ToInt64(btnUpdate.CommandArgument);
                    int quantaty = Convert.ToInt32(txtQuantity.Text);
                    decimal price = Convert.ToDecimal(txtPrice.Text);

                    SessionManager.UpdateProduct(producId, txtName.Text, price, quantaty);

                    Response.Redirect(Response.
                        ApplyAppPathModifier("~/Pages/Product/ProductDetails.aspx?product="+producId));
                }
                catch (DuplicateInstanceException)
                {
                    lblNameError.Visible = true;
                }
            }
        }
    }
}