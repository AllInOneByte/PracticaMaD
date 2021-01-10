using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.View.ApplicationObjects;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Log;
using System;
using System.Globalization;


namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Product
{
    public partial class RegisterTag : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnRegisterTagClick(object sender, EventArgs e)
        {
            try
            {
                SessionManager.CreateTag(txtTag.Text.ToLower());

                Response.Redirect(Response.
                        ApplyAppPathModifier("~/Pages/MainPage.aspx"));

            }
            catch (DuplicateInstanceException)
            {
                lblTagError.Visible = true;
            }
        }
    }
}