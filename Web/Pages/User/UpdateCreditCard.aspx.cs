using System;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using System.Collections.Generic;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.View.ApplicationObjects;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.User
{
    public partial class UpdateCreditCard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string cardId = Request.QueryString["card"];

                CreditCard card = SessionManager.FindCreditCard(cardId);

                txtCreditNumber.Text = card.cardNumber.ToString();
                txtExpirationDate.Text = card.expirationDate.Date.ToString("MM/yy");
                txtVerificationCode.Text = card.verificationCode.ToString();
                if (card.defaultCard == 1)
                {
                    checkDefault.Checked = true;
                }

                Locale locale = SessionManager.GetLocale(Context);
                UpdateComboCreditType(locale.Language, card.cardType);
            }
        }

        protected void BtnUpdateClick(object sender, EventArgs e)
        {

            if (Page.IsValid)
            {
                try
                {
                    long cardId = Convert.ToInt64(Request.QueryString["card"]);
                    long number = Convert.ToInt64(txtCreditNumber.Text);
                    int verification = Convert.ToInt32(txtVerificationCode.Text);
                    System.DateTime date = DateTime.ParseExact(txtExpirationDate.Text, "MM/yy", null);


                    CreditCardDetails creditCardDetails =
                        new CreditCardDetails(comboCreditType.SelectedValue, number,
                            verification, date, 0, SessionManager.GetUserSession(Context).UserProfileId);


                    SessionManager.UpdateCreditCardDetails(Context, cardId, checkDefault.Checked, creditCardDetails);

                    Response.Redirect(
                       Response.ApplyAppPathModifier("~/Pages/User/ListCreditCards.aspx"));
                }
                catch (DuplicateInstanceException)
                {
                    lblNumberError.Visible = true;
                }

            }

        }

        private void UpdateComboCreditType(String selectedLanguage, String selectValue)
        {
            this.comboCreditType.DataSource = CreditType.GetCreditType(selectedLanguage);
            this.comboCreditType.DataTextField = "text";
            this.comboCreditType.DataValueField = "value";
            this.comboCreditType.DataBind();
            this.comboCreditType.SelectedValue = selectValue;
        }
    }
}