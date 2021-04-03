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
            <br />
            <br />
            <div class="field">
                <asp:HyperLink ID="lnkBack" runat="server" NavigateUrl="~/Pages/User/ListCreditCards.aspx" meta:resourcekey="lnkBack"/>
            </div>
            <br />
            <div class="field">
                   <asp:Label ID="lblNumberError" runat="server" ForeColor="Red" Style="position: relative"
                            Visible="False" meta:resourcekey="lblNumberError"></asp:Label>
                    <asp:Label ID="lblDateError" runat="server" ForeColor="Red" Style="position: relative"
                               Visible="False" meta:resourcekey="lblDateError"></asp:Label>
            </div>
            <br />
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclCreditNumber" runat="server" meta:resourcekey="lclCreditNumber" /></span><span
                        class="entry">
                        <asp:TextBox ID="txtCreditNumber" runat="server" Width="100px" Columns="16"
                            meta:resourcekey="txtCreditNumberResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredCreditNumberValidator1" runat="server" ControlToValidate="txtCreditNumber"
                            Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>"
                            meta:resourcekey="rfvCreditNumberResource1"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularCreditNumberValidator1"
                            ControlToValidate="txtCreditNumber" runat="server"
                            ValidationExpression="\d+" meta:resourcekey="revNumberError"></asp:RegularExpressionValidator>
                        <asp:RegularExpressionValidator ID="RegularCreditNumberValidator2"
                            ControlToValidate="txtCreditNumber" runat="server"
                            ValidationExpression="^[\s\S]{12,19}$" meta:resourcekey="revNumberTamError"></asp:RegularExpressionValidator></span>                       
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
                            ValidationExpression="\d+" meta:resourcekey="revNumberError"></asp:RegularExpressionValidator>
                        <asp:RegularExpressionValidator ID="RegularVerificationCodeValidator2"
                            ControlToValidate="txtVerificationCode" runat="server"
                            ValidationExpression="^[\s\S]{3,4}$" meta:resourcekey="revVerificationTamError"></asp:RegularExpressionValidator></span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclExpirationDate" runat="server" meta:resourcekey="lclExpirationDate" /></span><span
                        class="entry">
                        <asp:TextBox ID="txtExpirationDate" runat="server" Width="100px" Columns="16"
                            meta:resourcekey="txtExpirationDateResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredExpirationDateValidator1" runat="server" ControlToValidate="txtExpirationDate"
                            Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>"
                            meta:resourcekey="rfvExpirationDateResource1"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpirationDateValidator1" runat="server" ControlToValidate="txtExpirationDate"
                            Display="Dynamic" ValidationExpression="\d+\d+/\d+\d+"
                            meta:resourcekey="revExpirationDate"></asp:RegularExpressionValidator></span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclCheckDefault" runat="server" meta:resourcekey="lclCheckDefault" /></span><span
                        class="entry">
                    <asp:CheckBox ID="checkDefault" runat="server" AutoPostBack="false"/></span>
            </div>
            <div class="button">
                <asp:Button ID="btnRegisterCreditCard" runat="server" OnClick="BtnRegisterCreditCardClick" meta:resourcekey="btnRegisterCreditCard" />
            </div>
        </form>
    </div>
</asp:Content>