using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.ProductService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Es.Udc.DotNet.PracticaMaD.Web.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Product
{
    public partial class ProductComments : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int startIndex, count;
            long productId;

            lnkPrevious.Visible = false;
            lnkNext.Visible = false;
            lblNoComments.Visible = false;
            btnDelete.Visible = false;
            btnModify.Visible = false;

            /* Get productId */
            try
            {
                productId = long.Parse(Request.Params.Get("product"));
            }
            catch (ArgumentNullException)
            {
                lblNoComments.Visible = true;
                lnkBack.NavigateUrl = "~/Pages/Product/ProductSearch.aspx";
                return;
            }

            lnkBack.NavigateUrl = "~/Pages/Product/ProductDetails.aspx?product=" + productId.ToString();

            /* Get Start Index */
            try
            {
                startIndex = int.Parse(Request.Params.Get("startIndex"));
            }
            catch (ArgumentNullException)
            {
                startIndex = 0;
            }

            /* Get Count */
            try
            {
                count = int.Parse(Request.Params.Get("count"));
            }
            catch (ArgumentNullException)
            {
                count = Settings.Default.PracticaMaD_defaultCount;
            }

            /* Get the Service */
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IProductService productService = iocManager.Resolve<IProductService>();

            /* Get Products Info */
            CommentBlock commentBlock =
                productService.FindAllProductComments(productId, startIndex, count);

            if (commentBlock.Comments.Count == 0)
            {
                lblNoComments.Visible = true;
                return;
            }

            gvProducts.DataSource = commentBlock.Comments;
            gvProducts.DataBind();

            /* "Previous" link */
            if ((startIndex - count) >= 0)
            {
                string url =
                    "/Pages/Product/ProductComments.aspx" + "?product=" + productId +
                    "&startIndex=" + (startIndex - count) + "&count=" + count;

                lnkPrevious.NavigateUrl = Response.ApplyAppPathModifier(url);
                lnkPrevious.Visible = true;
            }

            /* "Next" link */
            if (commentBlock.ExistMoreComments)
            {
                string url =
                    "/Pages/Product/ProductComments.aspx" + "?product=" + productId +
                    "&startIndex=" + (startIndex + count) + "&count=" + count;

                lnkNext.NavigateUrl = Response.ApplyAppPathModifier(url);
                lnkNext.Visible = true;
            }

            Comment comment = null;

            if (SessionManager.IsUserAuthenticated(Context))
            {
                try
                {
                    comment = productService.FindCommentByProductAndUser(productId,
                        SessionManager.GetUserSession(Context).UserProfileId);

                    cellOwnCommentId.Text = comment.commentId.ToString();
                    cellOwnCommentProfileName.Text = comment.UserProfile.loginName;
                    cellOwnCommentBody.Text = comment.comment1;
                    cellOwnCommentDate.Text = comment.commentDate.ToString("d/M/yyyy");
                    cellOwnCommentTags.Text = "";
                    foreach (Tag tag in comment.Tags)
                    {
                        cellOwnCommentTags.Text += tag.tagName + " <br/>";
                    }

                    ownComment.Visible = true;
                    btnDelete.Visible = true;
                    btnModify.Visible = true;
                }
                catch (InstanceNotFoundException)
                {
                    ownComment.Visible = false;
                }
            }
            else
            {
                ownComment.Visible = false;
            }
        }

        protected void gvProducts_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                /* Get the Service */
                IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
                IProductService productService = iocManager.Resolve<IProductService>();

                Comment comment = productService.FindCommentById(long.Parse(e.Row.Cells[0].Text));

                // Find ListBox
                TextBox box = (TextBox)e.Row.FindControl("TextTags");

                box.Text = "";
                foreach (Tag tag in comment.Tags)
                {
                    box.Text += tag.tagName + "\n";
                }
            }
        }

        protected void gvProducts_RowCreated(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[0].Visible = false;
        }

        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IProductService productService = iocManager.Resolve<IProductService>();

            long productId = long.Parse(Request.Params.Get("product"));

            long id =  Convert.ToInt64(cellOwnCommentId.Text);
            productService.DeleteComment(id);

            
            Response.Redirect(Response.
                        ApplyAppPathModifier("/Pages/Product/ProductComments.aspx" + "?product=" + productId));
        }

        protected void BtnModify_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Pages/Product/ModifyComment.aspx?comment=" + cellOwnCommentId.Text);
        }
    }
}