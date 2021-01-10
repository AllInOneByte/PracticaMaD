<%@ Page Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true"
    CodeBehind="ProductComments.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Product.ProductComments"
    meta:resourcekey="Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation" runat="server">
    <asp:Localize ID="lclMenuExplanation" runat="server" meta:resourcekey="lclMenuExplanation" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_MenuLinks" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <div>
        <p>
            <asp:Label ID="lblNoComments" meta:resourcekey="lblNoComments" runat="server"></asp:Label>
        </p>
        <asp:GridView ID="gvProducts" runat="server" GridLines="None" AutoGenerateColumns="False" Width="100%">
            <Columns>
                <asp:HyperLinkField DataTextField="productName"
                    HeaderText="<%$ Resources:Common, productName %>"
                    DataNavigateUrlFields="productId"
                    DataNavigateUrlFormatString="/Pages/ProductDetails.aspx?product={0}" />
                <asp:BoundField DataField="Category.categoryName"
                    HeaderText="<%$ Resources:Common, categoryName %>" />
                <asp:BoundField DataField="productDate" HeaderText="<%$ Resources:Common, productDate %>"
                    DataFormatString="{0:d/M/yyyy}" />
                <asp:BoundField DataField="productPrice" HeaderText="<%$ Resources:Common, productPrice %>"
                    DataFormatString="{0:C}" />
            </Columns>
        </asp:GridView>
    </div>

    <!-- "Previous" and "Next" links. -->
    <div class="previousNextLinks">
        <span class="previousLink">
            <asp:HyperLink ID="lnkPrevious" Text="<%$ Resources:Common, Previous %>"
                runat="server" Visible="False"></asp:HyperLink>
        </span>
        <span class="nextLink">
            <asp:HyperLink ID="lnkNext" Text="<%$ Resources:Common, Next %>" runat="server"
                Visible="False"></asp:HyperLink>
        </span>
    </div>
</asp:Content>
