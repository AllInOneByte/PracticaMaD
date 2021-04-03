using System;
using Es.Udc.DotNet.PracticaMaD.Model.ProductService;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using Es.Udc.DotNet.PracticaMaD.Web.Properties;
using System.Globalization;
using System.Threading;

namespace Es.Udc.DotNet.PracticaMaD.Web
{
    public partial class PracticaMaD : System.Web.UI.MasterPage
    {
        public static readonly string USER_SESSION_ATTRIBUTE = "userSession";

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!SessionManager.IsUserAuthenticated(Context))
            {
                

                if (lblDash2 != null)
                    lblDash2.Visible = false;
                if (lnkUpdate != null)
                    lnkUpdate.Visible = false;
                if (lblDash3 != null)
                    lblDash3.Visible = false;
                if (lnkListDeliveries != null)
                    lnkListDeliveries.Visible = false;
                if (lblDash5 != null)
                    lblDash5.Visible = false;
                if (lnkLogout != null)
                    lnkLogout.Visible = false;
            }
            else
            {
                if (SessionManager.IsAdminAuthenticated(Context))
                {

                    if (lblDash2 != null)
                        lblDash2.Visible = false;
                    if (lnkUpdate != null)
                        lnkUpdate.Visible = false;
                    if (lblDash3 != null)
                        lblDash3.Visible = false;
                    if (lnkListDeliveries != null)
                        lnkListDeliveries.Visible = false;
                }

                if (lblWelcome != null)
                    lblWelcome.Text =
                        GetLocalResourceObject("lblWelcome.Hello.Text").ToString()
                        + " " + SessionManager.GetUserSession(Context).FirstName;
                if (lblDash1 != null)
                    lblDash1.Visible = false;
                if (lnkAuthenticate != null)
                    lnkAuthenticate.Visible = false;
            }

            cloudTag();
        }


        protected void cloudTag()
        {
 
            HyperLink item;
            TableRow row;
            TableCell cell;
            
            int count = 10;
            int start = 0;

            TagBlock block;
            do
            {
                block = SessionManager.FindAllTags(start, count);
                row = new TableRow();

                foreach (Tag tag in block.Tags)
                {
                    cell = new TableCell();
                    item = new HyperLink();

                    item.Text = tag.tagName;
                    item.NavigateUrl = "~/Pages/Product/ProductSearch.aspx?tagId="+tag.tagId;
                    item.CssClass = GetCssClass(tag.Comments.Count, item);
                    cell.Controls.Add(item);
                    row.Cells.Add(cell);
                };

                tags.Rows.Add(row);
                start++;

            } while( block.ExistMoreTags );
            
        }

        private string GetCssClass(int tagCount, HyperLink item)
        {
            if (tagCount <= 5)
            {
                item.Style.Add("color", "#009900");
                return "TagSize1";
            }
            if (tagCount <= 7)
            {
                item.Style.Add("color", "#FF828B");
                return "TagSize2";
            }
            if (tagCount <= 15)
            {
                item.Style.Add("color", "#00B0BA");
                return "TagSize3";
            }
            if (tagCount <= 30)
            {
                item.Style.Add("color", "#FF5C77");
                return "TagSize4";
            }
            else
            {
                item.Style.Add("color", "#FFD872");
                return "TagSize5";
            }
        }
    }
}