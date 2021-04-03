using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.ProductService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.View.ApplicationObjects;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Log;
using System;
using System.Globalization;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Product
{
    public partial class ModifyProduct : SpecificCulturePage
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
                catch (Exception)
                {
                    lblNameError.Visible = true;
                }
            }
        }
    }
}