using Es.Udc.DotNet.PracticaMaD.Model.ShoppingService;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Es.Udc.DotNet.ModelUtil.IoC;
using System;
using System.Data;
using System.Reflection;
using System.Web;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Shopping
{
    public partial class ListDeliveries : System.Web.UI.Page
    {
        private ObjectDataSource pbpDataSource = new ObjectDataSource();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int startIndex, count;

                lnkPrevious.Visible = false;
                lnkNext.Visible = false;
                lblNoDeliveries.Visible = false;
            }

            /* Get Deliveries Info */
            DeliveryBlock deliveryBlock =
                SessionManager.GetAllDelevireis(Context);

            if (deliveryBlock.Deliveries.Count == 0)
            {
                lblNoDeliveries.Visible = true;
                return;
            }
        }
    }
}