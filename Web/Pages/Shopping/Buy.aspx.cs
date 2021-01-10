using Es.Udc.DotNet.PracticaMaD.Model.ShoppingService;
using Es.Udc.DotNet.PracticaMaD.Model.ShoppingService.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.View.ApplicationObjects;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Log;
using System;
using System.Globalization;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Shopping
{
    public partial class Buy : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtAddress.Text = SessionManager.GetAddress(Context);
                long creditCard = SessionManager.GetCreditCardNumber(Context);
                if (creditCard != 0)
                {
                    txtCreditNumber.Text = creditCard.ToString();
                }
                txtPrice.Text = SessionManager.GetTotalPrice(Context).ToString();
            }
        }

        protected void BtnBuyClick(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    long number = Convert.ToInt64(txtCreditNumber.Text);

                    SessionManager.Buy(Context, number, txtDescription.Text, txtAddress.Text);

                    Response.Redirect(Response.
                          ApplyAppPathModifier("~/Pages/Shopping/ListDeliveries.aspx"));

                }
                catch (Exception ex)
                {
                    if (ex is UnmatchingUserAndCardException)
                    {
                        lblUserError.Visible = true;

                        return;
                    }
                    if (ex is InstanceNotFoundException)
                    {
                        lblNumberError.Visible = true;
                        lnkRegisterCreditCard.Visible = true;

                        return;
                    }
                    if (ex is StockEmptyException)
                    {
                        int index = ex.Message.LastIndexOf('|');
                        string men = ex.Message.Substring(index + 14);

                        lblAmountError.Visible = true;
                        lblAmountError.Text += men;

                        return;
                    }
                }
            }
        }
    }
}