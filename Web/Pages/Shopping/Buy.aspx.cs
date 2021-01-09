﻿using Es.Udc.DotNet.PracticaMaD.Model.ShoppingService;
using Es.Udc.DotNet.PracticaMaD.Model.ShoppingService.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.View.ApplicationObjects;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Log;
using System;
using System.Globalization;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Shopping
{
    public partial class Buy : System.Web.UI.Page
    {
        Decimal price;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtAddress.Text = SessionManager.GetAddress(Context);
                txtCreditNumber.Text = SessionManager.GetCreditCardNumber(Context).ToString();
                price = SessionManager.GetTotalPrice(Context);
                txtPrice.Text = price.ToString();
            }
        }

        protected void BtnBuyClick(object sender, EventArgs e)
        {
             try
            {
                long number = Convert.ToInt64(txtCreditNumber.Text);

                SessionManager.Buy(Context,number, txtDescription.Text, txtAddress.Text, price);

            }
            catch (UnmatchingUserAndCardException)
            {
                lblUserError.Visible = true;

            }
            catch (InstanceNotFoundException)
            {
                lblNumberError.Visible = true;
                lnkRegisterCreditCard.Visible = true;
            }
        }
    }
}