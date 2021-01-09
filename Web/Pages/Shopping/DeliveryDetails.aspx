<%@ Page Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true"
    Codebehind="DeliveryDetails.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Shopping.DeliveryDetails"
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
        <form id="DeliverieForm" method="GET" runat="server">
            <asp:GridView ID="gvDeliverie" runat="server" GridLines="None" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="ProductName" HeaderText="<%$ Resources:, product %>" />
                    <asp:BoundField DataField="DeliveryLineAmount" HeaderText="<%$ Resources:, amount %>" />
                    <asp:BoundField DataField="DeliveryLinePrice" HeaderText="<%$ Resources:, price %>" />
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
            <asp:HyperLink ID="lnkBack" runat="server" 
            NavigateUrl="~/Pages/Shopping/ListDeliveries.aspx"
            meta:resourcekey="lnkBack"/>
        </form>
    </div>
</asp:Content>
