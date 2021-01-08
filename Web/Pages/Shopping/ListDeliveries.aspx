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
    <br />
    <p>
        <asp:Label ID="lblNoDeliveries" meta:resourcekey="lblNoDeliveries" runat="server" Visible="false"></asp:Label>
    </p>
    <form runat="server">
        <asp:GridView ID="gvDeliveries" runat="server" GridLines="None" AutoGenerateColumns="False">
            <Columns>
               <asp:BoundField DataField="Description" HeaderText="<%$ Resources:, description %>" />
                    <asp:BoundField DataField="Price" HeaderText="<%$ Resources:, price %>" />
                    <asp:BoundField DataField="Date" HeaderText="<%$ Resources:, date %>" />
                    <asp:BoundField DataField="Address" HeaderText="<%$ Resources:, address %>" />
                    <asp:BoundField DataField="Card" HeaderText="<%$ Resources:, card %>" />
            </Columns>
        </asp:GridView>
    </form>
    <div class="previousNextLinks">
        <span class="previousLink">
            <asp:HyperLink ID="lnkPrevious" meta:resourcekey="lnkPrevious" runat="server"
                Visible="False"></asp:HyperLink>
        </span><span class="nextLink">
            <asp:HyperLink ID="lnkNext" meta:resourcekey="lnkNext" runat="server" Visible="False"></asp:HyperLink>
        </span>
    </div>
    <br />
    <br />
</asp:Content>
