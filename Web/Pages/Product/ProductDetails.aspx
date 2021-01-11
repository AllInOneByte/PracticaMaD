﻿<%@ Page Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true"
    CodeBehind="ProductDetails.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Product.ProductDetails"
    meta:resourcekey="Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation" runat="server">
    <asp:Localize ID="lclMenuExplanation" runat="server" meta:resourcekey="lclMenuExplanation" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_MenuLinks" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <asp:Label ID="lblProductError" runat="server" ForeColor="Red" Style="position: relative"
        Visible="False" meta:resourcekey="lblProductError"></asp:Label>
    <br />
    <br />
    <asp:Table ID="TableProductInfo" runat="server" Width="100%">
        <asp:TableHeaderRow runat="server">
            <asp:TableHeaderCell ID="cellCaptionProductID" runat="server"
                Text="<%$ Resources:Common, productId %>"></asp:TableHeaderCell>

            <asp:TableHeaderCell ID="cellCaptionProductName" runat="server"
                Text="<%$ Resources:Common, productName %>"></asp:TableHeaderCell>

            <asp:TableHeaderCell ID="cellCaptionProductCategory" runat="server"
                Text="<%$ Resources:Common, categoryName %>"></asp:TableHeaderCell>

            <asp:TableHeaderCell ID="cellCaptionProductDate" runat="server"
                Text="<%$ Resources:Common, productDate %>"></asp:TableHeaderCell>

            <asp:TableHeaderCell ID="cellCaptionProductPrice" runat="server"
                Text="<%$ Resources:Common, productPrice %>"></asp:TableHeaderCell>

            <asp:TableHeaderCell ID="cellCaptionProductQuantity" runat="server"
                Text="<%$ Resources:Common, productQuantity %>"></asp:TableHeaderCell>
        </asp:TableHeaderRow>
        <asp:TableRow runat="server">
            <asp:TableCell ID="cellProductID" runat="server"></asp:TableCell>

            <asp:TableCell ID="cellProductName" runat="server"></asp:TableCell>

            <asp:TableCell ID="cellCategoryName" runat="server"></asp:TableCell>

            <asp:TableCell ID="cellProductDate" runat="server"></asp:TableCell>

            <asp:TableCell ID="cellProductPrice" runat="server"></asp:TableCell>

            <asp:TableCell ID="cellProductQuantity" runat="server"></asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <br />
    <br />
    <asp:HyperLink ID="hlComments" runat="server" meta:resourcekey="hlComments" />
    <br />
    <br />
    <br />
    <asp:HyperLink ID="lnkAddCart" runat="server" 
            NavigateUrl="~/Pages/Shopping/AddProductCart.aspx?productId="
            meta:resourcekey="lnkAddCart"/>
    <br />
    <br />
    <br />
    <asp:HyperLink ID="lnkUpdate" runat="server" 
            NavigateUrl="~/Pages/Product/ModifyProduct.aspx?productId=" Visible="false"
            meta:resourcekey="lnkUpdate"/>
    <br />
    <br />
    <asp:HyperLink ID="lnkBack" runat="server" 
            NavigateUrl="~/Pages/Product/ProductSearch.aspx"
            meta:resourcekey="lnkBack"/>
</asp:Content>
