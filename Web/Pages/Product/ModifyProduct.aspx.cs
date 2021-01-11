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
            if (!IsPostBack)
            {
                long productId = Int64.Parse(Request.Params.Get("productId"));

                var product = SessionManager.FindProduct(productId);

                txtName.Text = product.productName;
                txtPrice.Text = product.productPrice.ToString();
                txtQuantity.Text = product.productQuantity.ToString();

                btnUpdate.CommandArgument = product.productId.ToString();
            }
        }

        protected void BtnUpdateClick(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    long productId = Int64.Parse(Request.Params.Get("productId"));

                    int quantity = Convert.ToInt32(txtQuantity.Text);
                    decimal price = Convert.ToDecimal(txtPrice.Text);

                    SessionManager.UpdateProduct(productId, txtName.Text, price, quantity);

                    Response.Redirect(Response.
                        ApplyAppPathModifier("~/Pages/Product/ProductDetails.aspx?product=" + productId));
                }
                catch (DuplicateInstanceException)
                {
                    lblNameError.Visible = true;
                }
            }
        }
    }
}