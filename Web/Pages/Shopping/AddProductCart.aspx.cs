using System;
using System.Web;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Es.Udc.DotNet.PracticaMaD.Model.ShoppingService;
using Es.Udc.DotNet.PracticaMaD.Model.ShoppingService.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Shopping
{
    public partial class AddProductCart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblQuantity.Text += Request.QueryString["productQuantity"].ToString();
            }
        }

        protected void BtnAddCartClick(object sender, EventArgs e)
        {
            try
            {
                long id = Convert.ToInt64(Request.QueryString["productId"]);
                int amount = Convert.ToInt32(txtAmount.Text);

                SessionManager.AddToShoppingCart(Context, id, amount, checkGift.Checked);

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
    }
}