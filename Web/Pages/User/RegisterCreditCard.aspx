<%@ Page Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true"
    CodeBehind="RegisterCreditCard.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.User.RegisterCreditCard"
    meta:resourcekey="Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation"
    runat="server">
    -
    <asp:Localize ID="lclMenuExplanation" runat="server" meta:resourcekey="lclMenuExplanation" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_MenuLinks" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_BodyContent"
    runat="server">
    <div id="form">
        <form id="RegisterCreditCardForm" method="post" runat="server">
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclCreditNumber" runat="server" meta:resourcekey="lclCreditNumber" /></span><span
                        class="entry">
                        <asp:TextBox ID="txtCreditNumber" runat="server" Width="100px" Columns="16"
                            meta:resourcekey="txtCreditNumberResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredCreditNumberValidator1" runat="server" ControlToValidate="txtCreditCard"
                            Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>"
                            meta:resourcekey="rfvCreditNumberResource1"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularCreditNumberValidator1"
                            ControlToValidate="lclCreditNumber" runat="server"
                            ErrorMessage="Only Numbers allowed"
                            ValidationExpression="\d+"></asp:RegularExpressionValidator></span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclCreditType" runat="server" meta:resourcekey="lclCreditType" /></span><span
                        class="entry">
                        <asp:DropDownList ID="comboCreditType" runat="server" Width="100px"
                            meta:resourcekey="comboCreditTypeResource1">
                        </asp:DropDownList></span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclVerificationCode" runat="server" meta:resourcekey="lclVerificationCode" /></span><span
                        class="entry">
                        <asp:TextBox ID="txtVerificationCode" runat="server" Width="100px" Columns="16"
                            meta:resourcekey="txtVerificationCodeResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredVerificationCodeValidator1" runat="server" ControlToValidate="txtVerificationCode"
                            Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>"
                            meta:resourcekey="rfvVerificationCodeResource1"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularVerificationCodeValidator1"
                            ControlToValidate="txtVerificationCode" runat="server"
                            ErrorMessage="Only Numbers allowed"
                            ValidationExpression="\d+"></asp:RegularExpressionValidator></span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclExpirationDate" runat="server" meta:resourcekey="lclVerificationCode" /></span><span
                        class="entry">
                        <asp:TextBox ID="txtExpirationDate" runat="server" Width="100px" Columns="16"
                            meta:resourcekey="txtExpirationDateResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredExpirationDateValidator1" runat="server" ControlToValidate="txtExpirationDate"
                            Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>"
                            meta:resourcekey="rfvExpirationDateResource1"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpirationDateValidator1" runat="server" ControlToValidate="txtExpirationDate"
                            Display="Dynamic" ValidationExpression="\d+\d+/\d+\d+/\d+\d+\d+\d+"
                            meta:resourcekey="revExpirationDatel"></asp:RegularExpressionValidator></span>
            </div>
            <div class="button">
                <asp:Button ID="btnRegisterCreditCard" runat="server" OnClick="BtnRegisterCreditCardClick" meta:resourcekey="btnRegister" />
            </div>
        </form>
    </div>
</asp:Content>