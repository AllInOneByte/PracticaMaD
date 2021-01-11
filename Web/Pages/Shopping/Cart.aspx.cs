using System;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using System.Collections.Generic;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.View.ApplicationObjects;
using Es.Udc.DotNet.PracticaMaD.Model.ShoppingService;
using Es.Udc.DotNet.PracticaMaD.Model.ShoppingService.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Shopping
{
    public partial class Cart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            List<ShoppingCart> cart = SessionManager.GetShoppingCart(Context);

            if (cart.Count == 0)
            {
                lblEmpty.Visible = true;
                lclCart.Visible = false;
                btnBuy.Visible = false;
                return;
            }

            TableRow row;

            TableCell product, amount, gitf, sum, rest, delete;

            CheckBox checkGift;

            LinkButton bsum, brest;
            Button bdelete;

            foreach (ShoppingCart line in cart)
            {
                row = new TableRow();

                product = new TableCell();
                product.Text = line.Product.productName;
                row.Cells.Add(product);

                rest = new TableCell();
                brest = new LinkButton();
                brest.CommandArgument = line.Product.productId.ToString();
                brest.Click += new EventHandler(this.OnClickRest);
                brest.Text = "-";
                rest.Controls.Add(brest);
                row.Cells.Add(rest);

                amount = new TableCell();
                amount.Text = line.Amount.ToString();
                row.Cells.Add(amount);

                sum = new TableCell();
                bsum = new LinkButton();
                bsum.CommandArgument = line.Product.productId.ToString();
                bsum.Click += new EventHandler(this.OnClickSum);
                bsum.Text = "+";
                sum.Controls.Add(bsum);
                row.Cells.Add(sum);

                gitf = new TableCell();
                checkGift = new CheckBox();
                checkGift.ID = line.Product.productId.ToString();
                checkGift.CheckedChanged += new EventHandler(this.Check_Change);
                checkGift.Checked = line.Gift;
                gitf.Controls.Add(checkGift);
                row.Cells.Add(gitf);

                delete = new TableCell();
                bdelete = new Button();
                bdelete.CommandArgument = line.Product.productId.ToString();
                bdelete.Click += new EventHandler(this.OnClickDelete);
                bdelete.Text = GetLocalResourceObject("btnDelete.Text").ToString();
                bdelete.BackColor = System.Drawing.Color.Red;
                delete.Controls.Add(bdelete);
                row.Cells.Add(delete);

                lclCart.Rows.Add(row);
            }
            
        }

        protected void Check_Change(object sender, EventArgs e)
        {
            CheckBox check = sender as CheckBox;

            long productId = Convert.ToInt64(check.ID);

            SessionManager.ForGift(Context, productId, check.Checked);
        }

        protected void OnClickSum(object sender, EventArgs e)
        {
            try
            {
                LinkButton button = (LinkButton)sender;

                long productId = Convert.ToInt64(button.CommandArgument);

                SessionManager.ModifyAmount(Context, productId, 1);

                Response.Redirect(Response.
                         ApplyAppPathModifier("~/Pages/Shopping/Cart.aspx"));
            }
            catch (StockEmptyException)
            {
                lblAmountError.Visible = true;
            }
        }

        protected void OnClickRest(object sender, EventArgs e)
        {
            try
            {
                LinkButton button = (LinkButton)sender;

                long productId = Convert.ToInt64(button.CommandArgument);

                SessionManager.ModifyAmount(Context, productId, -1);

                Response.Redirect(Response.
                         ApplyAppPathModifier("~/Pages/Shopping/Cart.aspx"));
            }
            catch (StockEmptyException)
            {
                lblAmountError.Visible = true;
            }
        }

        protected void OnClickDelete(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            long productId = Convert.ToInt64(button.CommandArgument);

            SessionManager.DeleteProductOfCart(Context, productId);

            Response.Redirect(Response.
                     ApplyAppPathModifier("~/Pages/Shopping/Cart.aspx"));
        }

        protected void BtnBuyClick(object sender, EventArgs e)
        {
            Response.Redirect(Response.
                     ApplyAppPathModifier("~/Pages/Shopping/Buy.aspx"));
        }
    }
}