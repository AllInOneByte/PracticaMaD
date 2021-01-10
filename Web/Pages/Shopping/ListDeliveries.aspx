<%@ Page Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true"
    Codebehind="ListDeliveries.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Shopping.ListDeliveries"
    meta:resourcekey="Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation"
    runat="server">
    - <asp:Localize ID="lclMenuExplanation" runat="server" meta:resourcekey="lclMenuExplanation" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_MenuLinks" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_BodyContent"
    runat="server">
    <div id="form">
        <form id="ListDeliveriesForm" method="GET" runat="server">
            <p>
            <asp:Label ID="lblNoDeliveries" meta:resourcekey="lblNoDeliveries" runat="server" Visible="False"></asp:Label>
            </p>
            <asp:GridView ID="gvDeliveries" runat="server" GridLines="None" AutoGenerateColumns="False">
                <Columns>
                    <asp:HyperLinkField DataTextField="DeliveryId" DataNavigateUrlFields="DeliveryId" DataNavigateUrlFormatString="~/Pages/Shopping/DeliveryDetails.aspx?deliveryId={0}"  HeaderText="<%$ Resources:, id %>" />
                   <asp:BoundField DataField="Description" HeaderText="<%$ Resources:, description %>" />
                    <asp:BoundField DataField="DeliveryPrice" HeaderText="<%$ Resources:, price %>" />
                    <asp:BoundField DataField="DeliveryDate" HeaderText="<%$ Resources:, date %>" />
                    <asp:BoundField DataField="CardNumber" HeaderText="<%$ Resources:, card %>" />
                    <asp:BoundField DataField="DeliveryAddress" HeaderText="<%$ Resources:, address %>" />
                </Columns>
            </asp:GridView>
            <br />
            <div class="previousNextLinks">
                <span class="previousLink">
                    <asp:HyperLink ID="lnkPrevious" meta:resourcekey="lnkPrevious" runat="server" Visible="False"></asp:HyperLink>
                </span><span class="nextLink">
                    <asp:HyperLink ID="lnkNext" meta:resourcekey="lnkNext" runat="server" Visible="False"></asp:HyperLink>
                </span>
            </div>
            <br />
            <br />
            <asp:HyperLink ID="lnkMain" runat="server" 
            NavigateUrl="~/Pages/MainPage.aspx"
            meta:resourcekey="lnkMain"/>
        </form>
    </div>
</asp:Content>
