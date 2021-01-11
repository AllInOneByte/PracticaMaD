<%@ Page Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true"
    Codebehind="ListCreditCards.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.User.ListCreditCards"
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
        <form id="ListCreditCardsForm" method="GET" runat="server">
            <br />
            <br />
            <asp:HyperLink ID="lnkRegisterCreditCard" runat="server" 
                NavigateUrl="~/Pages/User/RegisterCreditCard.aspx"
                meta:resourcekey="lnkRegisterCreditCard"/>
            <br />
            <br />
            <asp:Table ID="lclTableCreditCards" runat="server" meta:resourcekey="lclTableCreditCards">
                <asp:TableHeaderRow ID="Header1" runat="server">
                    <asp:TableHeaderCell ID="lclHeaderNumber" meta:resourceKey="lclHeaderNumber"></asp:TableHeaderCell>
                    <asp:TableHeaderCell ID="lclHeaderType" meta:resourceKey="lclHeaderType"></asp:TableHeaderCell>
                    <asp:TableHeaderCell ID="lclHeaderExpiration" meta:resourceKey="lclHeaderExpiration"></asp:TableHeaderCell>
                    <asp:TableHeaderCell ID="lclHeaderVerification" meta:resourceKey="lclHeaderVerification"></asp:TableHeaderCell>
                    <asp:TableHeaderCell ID="lclHeaderDefault" meta:resourceKey="lclHeaderDefault"></asp:TableHeaderCell>
                </asp:TableHeaderRow>
            </asp:Table>
            <asp:HyperLink ID="lnkMain" runat="server" 
            NavigateUrl="~/Pages/MainPage.aspx"
            meta:resourcekey="lnkMain"/>
        </form>
    </div>
</asp:Content>

