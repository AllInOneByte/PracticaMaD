<%@ Page Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true"
    Codebehind="ModifyProduct.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Product.ModifyProduct"
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
        <form id="ModifyProductForm" method="POST" runat="server">
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclName" runat="server" meta:resourcekey="lclName" />
                </span><span
                        class="entry">
                        <asp:TextBox ID="txtName" runat="server" Width="100px" Columns="16"
                            meta:resourcekey="txtNameResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName"
                            Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>"
                            meta:resourcekey="rfvNameResource1"></asp:RequiredFieldValidator>
                        <asp:Label ID="lblNameError" runat="server" ForeColor="Red" Style="position: relative"
                            Visible="False" meta:resourcekey="lblNameError"></asp:Label></span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclQuantity" runat="server" meta:resourcekey="lclQuantity" /></span><span
                        class="entry">
                        <asp:TextBox ID="txtQuantity" runat="server" Width="100px" Columns="16"
                            meta:resourcekey="txtQuantity"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredQuantityValidator1" runat="server" ControlToValidate="txtQuantity"
                            Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>"
                            meta:resourcekey="rfvQuantityResource1"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularQuantityValidator1"
                            ControlToValidate="txtQuantity" runat="server"
                            ValidationExpression="\d+" meta:resourcekey="revNumberError"></asp:RegularExpressionValidator></span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclPrice" runat="server" meta:resourcekey="lclPrice" /></span><span
                        class="entry">
                        <asp:TextBox ID="txtPrice" runat="server" Width="100px" Columns="16"
                            meta:resourcekey="txtPrice"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredPriceValidator1" runat="server" ControlToValidate="txtPrice"
                            Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>"
                            meta:resourcekey="rfvExpirationDateResource1"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularPriceValidator1" runat="server" ControlToValidate="txtPrice"
                            Display="Dynamic" ValidationExpression="\d+,\d+"
                            meta:resourcekey="revPriceError"></asp:RegularExpressionValidator></span>
            </div>
            <div class="button">
                <asp:Button ID="btnUpdate" runat="server" OnClick="BtnUpdateClick" meta:resourcekey="btnUpdate"/>
            </div>
            <br />
            <br />
            <asp:HyperLink ID="lnkBack" runat="server" 
                    NavigateUrl="~/Pages/Product/ProductSearch.aspx"
                    meta:resourcekey="lnkBack"/>
        </form>
    </div>
</asp:Content>
