﻿using Es.Udc.DotNet.PracticaMaD.Model.ShoppingService;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Web.Properties;
using System;
using System.Data;
using System.Reflection;
using System.Web;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Shopping
{
    public partial class DeliveryDetails : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int startIndex, count;

                int deliveryId = Int32.Parse(Request.Params.Get("deliveryId"));

                lnkPrevious.Visible = false;
                lnkNext.Visible = false;

                /* Get Start Index */
                try
                {
                    startIndex = Int32.Parse(Request.Params.Get("startIndex"));
                }
                catch (ArgumentNullException)
                {
                    startIndex = 0;
                }

                /* Get Count */
                try
                {
                    count = Int32.Parse(Request.Params.Get("count"));
                }
                catch (ArgumentNullException)
                {
                    count = Settings.Default.PracticaMaD_defaultCount;
                }

                /* Get Deliveries Info */
                DeliveryLineBlock deliveryBlock =
                    SessionManager.GetAllDeleviryLines(deliveryId, startIndex, count);

                this.gvDeliverie.DataSource = deliveryBlock.DeliveryLines;
                this.gvDeliverie.DataBind();

                /* "Previous" link */
                if ((startIndex - count) >= 0)
                {
                    String url =
                        "~/Pages/Shopping/DeliveryDetails.aspx?" + "&deliveryId=" + deliveryId +
                        " &startIndex=" + (startIndex - count)+ "&count=" + count;

                    this.lnkPrevious.NavigateUrl =
                        Response.ApplyAppPathModifier(url);
                    this.lnkPrevious.Visible = true;
                }

                /* "Next" link */
                if (deliveryBlock.ExistMoreDeliveryLines)
                {
                    String url =
                        "~/Pages/Shopping/DeliveryDetails.aspx?" + "&deliveryId=" + deliveryId +
                        "&startIndex=" + (startIndex + count) + "&count=" +
                        count;

                    this.lnkNext.NavigateUrl =
                        Response.ApplyAppPathModifier(url);
                    this.lnkNext.Visible = true;
                }
            }
        }
    }
}