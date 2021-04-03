using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.ProductService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Product
{
    public partial class ModifyComment : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    long commentId = long.Parse(Request.Params.Get("comment"));

                    /* Get the Service */
                    IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
                    IProductService productService = iocManager.Resolve<IProductService>();

                    Comment comment = null;

                    try
                    {
                        comment = productService.FindCommentById(commentId);
                    }
                    catch (InstanceNotFoundException)
                    {
                        lblNoComment.Visible = true;
                        tagBox.Visible = false;
                        commentBody.Visible = false;
                        btnEditComment.Visible = false;

                        return;
                    }

                    if (!SessionManager.IsUserAuthenticated(Context) ||
                        SessionManager.GetUserSession(Context).UserProfileId != comment.userId)
                    {
                        lblUnlogedUser.Visible = true;
                        tagBox.Visible = false;
                        commentBody.Visible = false;
                        btnEditComment.Visible = false;

                        return;
                    }

                    commentBody.Text = comment.comment1;
                    string tagStr = "";

                    foreach (Tag tag in comment.Tags)
                    {
                        tagStr = tagStr + tag.tagName + " ";
                    }

                    tagBox.Text = tagStr.Trim(' ');
                    ViewState["tags"] = tagStr.Trim(' ');
                }
                catch (ArgumentNullException)
                {
                    lblNoComment.Visible = true;
                    tagBox.Visible = false;
                    commentBody.Visible = false;
                    btnEditComment.Visible = false;
                }
            }
        }

        protected void BtnEditCommentClick(object sender, EventArgs e)
        {
            if (commentBody.Text == string.Empty)
            {
                return;
            }

            if (SessionManager.IsUserAuthenticated(Context))
            {
                long commentId = long.Parse(Request.Params.Get("comment"));
                /* Get the Service */
                IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
                IProductService productService = iocManager.Resolve<IProductService>();

                string savedTags = (string)ViewState["tags"];

                if (savedTags != tagBox.Text)
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

                    productService.UpdateComment(commentId, commentBody.Text, tags);
                }
                else
                {
                    productService.UpdateComment(commentId, commentBody.Text);
                }

                long productId = productService.FindCommentById(commentId).productId;

                Response.Redirect(
                   Response.ApplyAppPathModifier("~/Pages/Product/ProductComments.aspx?product=" + productId.ToString()));
            }
        }
    }
}