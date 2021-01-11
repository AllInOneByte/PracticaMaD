<%@ Page Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true"
    CodeBehind="AddProductCart.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Shopping.AddProductCart"
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
    <asp:Label ID="lblAmountError" runat="server" ForeColor="Red" Style="position: relative"
                            Visible="False" meta:resourcekey="lblAmountError"></asp:Label>
    <br />
    <br />
    <asp:Label ID="lblQuantity" runat="server" Style="position: relative" meta:resourcekey="lblQuantity"></asp:Label>
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
</asp:Content>
