using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.ProductService;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Es.Udc.DotNet.PracticaMaD.Web.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Product
{
    public partial class ProductComments : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int startIndex, count;
            long productId;

            lnkPrevious.Visible = false;
            lnkNext.Visible = false;
            lblNoComments.Visible = false;

            /* Get productId */
            try
            {
                productId = long.Parse(Request.Params.Get("product"));
            }
            catch (ArgumentNullException)
            {
                lblNoComments.Visible = true;
                return;
            }

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
                productService.FindAllProductComments(startIndex, count);

            if (commentBlock.Comments.Count == 0)
            {
                lblNoComments.Visible = true;
                return;
            }

            ViewState["comments"] = commentBlock.Comments;

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
                }
                catch (InstanceNotFoundException)
                {
                    // hacer desaparecer el comentario propio o algo
                }
            }
            else
            {
                // lo mismo que arriba
            }
        }

        protected void gvProducts_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                List<Comment> comments = (List<Comment>)ViewState["comments"];
                Comment comment = comments.ElementAt(e.Row.RowIndex);

                // Find ListBox
                ListBox lst = (ListBox) e.Row.FindControl("tagList");

                foreach (Tag tag in comment.Tags)
                {
                    lst.Items.Add(new ListItem(tag.tagName));
                }
            }
        }
    }
}