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
        <asp:Label ID="lblAmountError" runat="server" ForeColor="Red" Style="position: relative"
                            Visible="False" meta:resourcekey="lblAmountError"></asp:Label>
        <form id="CartForm" method="GET" runat="server">
            <asp:Label ID="lblEmpty" meta:resourcekey="lblEmpty" runat="server" Visible="false"></asp:Label>
            <br />
            <asp:Table ID="lclCart" runat="server" Visible="true" meta:resourcekey="lclCart">
                <asp:TableHeaderRow ID="ShoppingCartHeader" runat="server">
                    <asp:TableHeaderCell ID="lclProductName" meta:resourceKey="lclProductName"></asp:TableHeaderCell>
                    <asp:TableHeaderCell ID="lclProductPrice" meta:resourceKey="lclProductPrice"></asp:TableHeaderCell>
                    <asp:TableHeaderCell ID="lclRest" Text=""></asp:TableHeaderCell>
                    <asp:TableHeaderCell ID="lclAmount" meta:resourceKey="lclAmount"></asp:TableHeaderCell>
                    <asp:TableHeaderCell ID="lclSum" Text=""></asp:TableHeaderCell>
                    <asp:TableHeaderCell ID="lclAmountPrice" meta:resourceKey="lclAmountPrice"></asp:TableHeaderCell>
                    <asp:TableHeaderCell ID="lclGift" meta:resourceKey="lclGift"></asp:TableHeaderCell>
                    <asp:TableHeaderCell ID="lclRemove" Text=""></asp:TableHeaderCell>
                </asp:TableHeaderRow>
            </asp:Table>
            <br />
            <div id="TotalPrice"  style="float:left;">
                <span class='highlight'>
                    <asp:Label ID="lclTotalCartPrice" meta:resourcekey="lclTotalCartPrice" runat="server" Visible="true"></asp:Label>
                 </span>
                 <span class='highlight'>
                    <asp:Label ID="lblDash2" Text=" : " runat="server" Visible="true"></asp:Label>
                 </span>
                <span class='highlight'>
                    <asp:Label ID="lclTotalPrice" runat="server" Visible="true"></asp:Label>
                 </span>
            </div>
            <br />
            <br />
            <div class="button">
                <asp:Button ID="btnBuy" runat="server" OnClick="BtnBuyClick" Visible="true" meta:resourcekey="btnBuy" />
            </div>
            <br />
            <br />
            <div id="Returns">
                <span>
                    <asp:HyperLink ID="lnkBack" runat="server" 
                            NavigateUrl="~/Pages/Product/ProductSearch.aspx"
                            meta:resourcekey="lnkBack"/>
                </span>
                <span>
                    <asp:Label ID="lblDash1" runat="server" Text=" - "></asp:Label>
                </span>
                <span>
                    <asp:HyperLink ID="lnkMain" runat="server" 
                            NavigateUrl="~/Pages/MainPage.aspx"
                            meta:resourcekey="lnkMain"/>
                </span>
            </div>
            

        </form>
    </div>
</asp:Content>
