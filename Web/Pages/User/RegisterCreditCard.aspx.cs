using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.View.ApplicationObjects;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Log;
using System;
using System.Globalization;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.User
{
    public partial class RegisterCreditCard : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Locale locale = SessionManager.GetLocale(Context);
                UpdateComboCreditType(locale.Language);
            }
        }

        private void UpdateComboCreditType(String selectedLanguage)
        {
            this.comboCreditType.DataSource = CreditType.GetCreditType(selectedLanguage);
            this.comboCreditType.DataTextField = "text";
            this.comboCreditType.DataValueField = "value";
            this.comboCreditType.DataBind();
            this.comboCreditType.SelectedIndex = 0;
        }

        protected void BtnRegisterCreditCardClick(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    long number = Convert.ToInt64(txtCreditNumber.Text);
                    int verification = Convert.ToInt32(txtVerificationCode.Text);
                    System.DateTime date = DateTime.ParseExact(txtExpirationDate.Text, "MM/yy", null);

                    SessionManager.RegisterCreditCard(Context, comboCreditType.SelectedValue,
                      number, verification, checkDefault.Checked, date);

                    Response.Redirect(Response.
                        ApplyAppPathModifier("~/Pages/User/ListCreditCards.aspx"));
                }
                catch (DuplicateInstanceException)
                {
                    lblNumberError.Visible = true;
                }
            }
        }
    }
}