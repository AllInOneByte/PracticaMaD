<%@ Page Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true"
    CodeBehind="ProductDetails.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Product.ProductDetails"
    meta:resourcekey="Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation" runat="server">
    <asp:Localize ID="lclMenuExplanation" runat="server" meta:resourcekey="lclMenuExplanation" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_MenuLinks" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <br />
    <asp:HyperLink ID="lnkBack" runat="server" 
            NavigateUrl="~/Pages/Product/ProductSearch.aspx"
            meta:resourcekey="lnkBack"/>
    <br />
    <br />
    <asp:HyperLink ID="lnkUpdate" runat="server" 
            NavigateUrl="~/Pages/Product/ModifyProduct.aspx?productId=" Visible="false"
            meta:resourcekey="lnkUpdate"/>
    <br />
    <br />
    <div class="errors">
        <asp:Label ID="lblProductError" runat="server" ForeColor="Red" Style="position: relative"
        Visible="False" meta:resourcekey="lblProductError"></asp:Label>
        <asp:Label ID="lblAmountError" runat="server" ForeColor="Red" Style="position: relative"
                                Visible="False" meta:resourcekey="lblAmountError"></asp:Label>
    </div>
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
    <div id="AddCart">
        <form id="AddProductCartForm" method="post" runat="server">
             <div class="field">
                <span class="label">
                    <asp:Localize ID="lblAmount" runat="server" meta:resourcekey="lblAmount" /></span><span
                        class="entry">
                        <asp:TextBox ID="txtAmount" runat="server" Width="100px" Columns="16"
                            meta:resourcekey="txtAmountResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredAmountValidator1" runat="server" ControlToValidate="txtAmount"
                            Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>"
                            meta:resourcekey="rfvAmountResource1"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularAmountValidator1"
                            ControlToValidate="txtAmount" runat="server"
                            ValidationExpression="\d+" meta:resourcekey="revNumberError"></asp:RegularExpressionValidator></span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclGift" runat="server" meta:resourcekey="lclGift" /></span><span
                        class="entry">
                    <asp:CheckBox ID="checkGift" runat="server" AutoPostBack="false"/></span>
            </div>
            <div class="button">
                <asp:Button ID="btnAddCart" runat="server" OnClick="BtnAddCartClick" meta:resourcekey="btnAddCart"/>
            </div>
        </form>
    </div>
    <br />
    <br />
    <div class="field">
        <asp:HyperLink ID="hlAddComment" runat="server" meta:resourcekey="hlAddComment" />
        <asp:Label ID="lblDash1" runat="server" Text=" - "></asp:Label>
        <asp:HyperLink ID="hlComments" runat="server" meta:resourcekey="hlComments" />
    </div>
    <br />
</asp:Content>
