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
        }

        protected void BtnRegisterCreditCardClick(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    SessionManager.RegisterCreditCard(Context, comboCreditType.SelectedValue,
                        txtCreditNumber.Text, txtVerificationCode.Text, checkDefault.Checked, txtExpirationDate.Text);

                    Response.Redirect(Response.
                        ApplyAppPathModifier("~/Pages/MainPage.aspx"));
                }
                catch (DuplicateInstanceException)
                {
                    lblNumberError.Visible = true;
                }
            }
        }
    }
}