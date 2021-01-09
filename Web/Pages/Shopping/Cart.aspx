<%@ Page Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true"
    Codebehind="Cart.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Shopping.Cart"
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
        <form id="CartForm" method="GET" runat="server">
            <asp:Label ID="lblEmpty" meta:resourcekey="lblEmpty" runat="server" Visible="false"></asp:Label>
            <br />
            <asp:Table ID="lclCart" runat="server" Visible="true" meta:resourcekey="lclCart">
                <asp:TableHeaderRow ID="ShoppingCartHeader" runat="server">
                    <asp:TableHeaderCell ID="lclProductName" meta:resourceKey="lclProductName"></asp:TableHeaderCell>
                    <asp:TableHeaderCell ID="lclRest" Text=""></asp:TableHeaderCell>
                    <asp:TableHeaderCell ID="lclAmount" meta:resourceKey="lclAmount"></asp:TableHeaderCell>
                    <asp:TableHeaderCell ID="lclSum" Text=""></asp:TableHeaderCell>
                    <asp:TableHeaderCell ID="lclGift" meta:resourceKey="lclGift"></asp:TableHeaderCell>
                    <asp:TableHeaderCell ID="lclRemove" Text=""></asp:TableHeaderCell>
                </asp:TableHeaderRow>
            </asp:Table>
            <br />
            <br />
            <div class="button">
                <asp:Button ID="btnBuy" runat="server" OnClick="BtnBuyClick" Visible="true" meta:resourcekey="btnBuy" />
            </div>
            <br />
            <br />
            <asp:HyperLink ID="lnkBack" runat="server" 
            NavigateUrl="~/Pages/MainPage.aspx"
            meta:resourcekey="lnkBack"/>
        </form>
    </div>
</asp:Content>
