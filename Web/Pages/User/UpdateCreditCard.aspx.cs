using System;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using System.Collections.Generic;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.View.ApplicationObjects;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using Es.Udc.DotNet.PracticaMaD.Model;
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
                txtExpirationDate.Text = card.expirationDate.ToString();
                txtVerificationCode.Text = card.verificationCode.ToString();
                if (card.defaultCard == 1)
                {
                    checkDefault.Checked = true;
                }

                Locale locale = SessionManager.GetLocale(Context);
                UpdateComboCreditType(locale.Language);
            }
        }

        protected void BtnUpdateCardClick(object sender, EventArgs e)
        {

            if (Page.IsValid)
            {
                long cardId = Convert.ToInt64(Request.QueryString["card"]);
                long number = Convert.ToInt64(txtCreditNumber);
                int verification = Convert.ToInt32(txtVerificationCode);
                System.DateTime date = Convert.ToDateTime(txtExpirationDate);
                byte defaultCard = 0;
                if (checkDefault.Checked)
                {
                    defaultCard = 1;
                }
                

                CreditCardDetails creditCardDetails =
                    new CreditCardDetails(comboCreditType.SelectedValue, number, 
                        verification, date, defaultCard, SessionManager.GetUserSession(Context).UserProfileId);

                SessionManager.UpdateCreditCardDetails(Context, cardId, creditCardDetails);

                Response.Redirect(
                    Response.ApplyAppPathModifier("~/Pages/User/ListCreditCards.aspx"));

            }
        }

        private void UpdateComboCreditType(String selectedLanguage)
        {
            this.comboCreditType.DataSource = CreditType.GetCreditType(selectedLanguage);
            this.comboCreditType.DataTextField = "text";
            this.comboCreditType.DataValueField = "value";
            this.comboCreditType.DataBind();
        }
    }
}