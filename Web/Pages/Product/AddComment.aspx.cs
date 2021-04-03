using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.ProductService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Product
{
    public partial class AddComment : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                long productId = long.Parse(Request.Params.Get("product"));

                tagBox.Visible = false;
                commentBody.Visible = false;
                btnAddComment.Visible = false;

                lnkBack.NavigateUrl = "~/Pages/Product/ProductDetails.aspx?product=" + productId.ToString();

                if (!SessionManager.IsUserAuthenticated(Context))
                {
                    lblUnlogedUser.Visible = true;
                    tagBox.Visible = false;
                    commentBody.Visible = false;
                    btnAddComment.Visible = false;
                    return;
                }
                /* Get the Service */
                IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
                IProductService productService = iocManager.Resolve<IProductService>();

                productService.FindCommentByProductAndUser(productId,
                        SessionManager.GetUserSession(Context).UserProfileId);

                lblCommented.Visible = true;
            }
            catch (ArgumentNullException)
            {
                lblNoProduct.Visible = true;
                tagBox.Visible = false;
                commentBody.Visible = false;
                btnAddComment.Visible = false;
                lnkBack.NavigateUrl = "~/Pages/Product/ProductSearch.aspx";
            }
            catch (InstanceNotFoundException)
            {
                tagBox.Visible = true;
                commentBody.Visible = true;
                btnAddComment.Visible = true;
            }
        }

        protected void BtnAddCommentClick(object sender, EventArgs e)
        {
            if (commentBody.Text == string.Empty)
            {
                return;
            }

            if (SessionManager.IsUserAuthenticated(Context))
            {
                long productId = long.Parse(Request.Params.Get("product"));

                /* Get the Service */
                IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
                IProductService productService = iocManager.Resolve<IProductService>();

                if (tagBox.Text == string.Empty)
                {
                    productService.AddComment(productId, SessionManager.GetUserSession(Context).UserProfileId,
                        commentBody.Text);
                }
                else
                {
                    List<string> strTags = tagBox.Text.Split(' ').ToList();
                    List<long> tags = new List<long>();

                    foreach (string strTag in strTags)
                    {
                        try
                        {
                            tags.Add(productService.AddTag(strTag.ToLower()));
                        }
                        catch (DuplicateInstanceException)
                        {
                            tags.Add(productService.FindTagByName(strTag).tagId);
                        }
                    }

                    productService.AddComment(productId, SessionManager.GetUserSession(Context).UserProfileId,
                        commentBody.Text, tags);
                }

                Response.Redirect(
                   Response.ApplyAppPathModifier("~/Pages/Product/ProductComments.aspx?product=" + productId.ToString()));

            }
        }
    }
}