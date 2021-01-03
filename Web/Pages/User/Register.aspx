<%@ Page Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true"
    CodeBehind="Register.aspx.cs" Inherits="Es.Udc.DotNet.MPracticaMaD.Web.Pages.User.Register"
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
        <form id="RegisterForm" method="post" runat="server">

            <div class="field">
                <asp:HyperLink ID="lnkUserExists" runat="server" meta:resourcekey="lnkUserExists" NavigateUrl="~/Pages/UserExists.aspx"></asp:HyperLink>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclUserName" runat="server" meta:resourcekey="lclUserName" />
                </span><span
                        class="entry">
                        <asp:TextBox ID="txtLogin" runat="server" Width="100px" Columns="16"
                            meta:resourcekey="txtLoginResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvUserName" runat="server" ControlToValidate="txtLogin"
                            Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>"
                            meta:resourcekey="rfvUserNameResource1"></asp:RequiredFieldValidator>
                        <asp:Label ID="lblLoginError" runat="server" ForeColor="Red" Style="position: relative"
                            Visible="False" meta:resourcekey="lblLoginError"></asp:Label></span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclPassword" runat="server" meta:resourcekey="lclPassword" /></span><span
                        class="entry">
                        <asp:TextBox TextMode="Password" ID="txtPassword" runat="server"
                            Width="100px" Columns="16" meta:resourcekey="txtPasswordResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword"
                            Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>"
                            meta:resourcekey="rfvPasswordResource1"></asp:RequiredFieldValidator></span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclRetypePassword" runat="server" meta:resourcekey="lclRetypePassword" /></span><span
                        class="entry">
                        <asp:TextBox TextMode="Password" ID="txtRetypePassword" runat="server" Width="100px"
                            Columns="16" meta:resourcekey="txtRetypePasswordResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvRetypePassword" runat="server" ControlToValidate="txtRetypePassword"
                            Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>"
                            meta:resourcekey="rfvRetypePasswordResource1"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="cvPasswordCheck" runat="server" ControlToCompare="txtPassword"
                            ControlToValidate="txtRetypePassword" meta:resourcekey="cvPasswordCheck"></asp:CompareValidator></span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclFirstName" runat="server" meta:resourcekey="lclFirstName" /></span><span
                        class="entry">
                        <asp:TextBox ID="txtFirstName" runat="server" Width="100px"
                            Columns="16" meta:resourcekey="txtFirstNameResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txtFirstName"
                            Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>"
                            meta:resourcekey="rfvFirstNameResource1"></asp:RequiredFieldValidator></span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclSurname" runat="server" meta:resourcekey="lclSurname" /></span><span
                        class="entry">
                        <asp:TextBox ID="txtSurname" runat="server" Width="100px" Columns="16"
                            meta:resourcekey="txtSurnameResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvSurname" runat="server" ControlToValidate="txtSurname"
                            Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>"
                            meta:resourcekey="rfvSurnameResource1"></asp:RequiredFieldValidator></span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclAddress" runat="server" meta:resourcekey="lclSAddress" /></span><span
                        class="entry">
                        <asp:TextBox ID="txtAddress" runat="server" Width="100px" Columns="16"
                            meta:resourcekey="txtAddressResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvAddress" runat="server" ControlToValidate="txtAddress"
                            Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>"
                            meta:resourcekey="rfvAddressResource1"></asp:RequiredFieldValidator></span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclEmail" runat="server" meta:resourcekey="lclEmail" /></span><span
                        class="entry">
                        <asp:TextBox ID="txtEmail" runat="server" Width="100px" Columns="16"
                            meta:resourcekey="txtEmailResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                            Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>"
                            meta:resourcekey="rfvEmailResource1"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                            Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                            meta:resourcekey="revEmail"></asp:RegularExpressionValidator></span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclLanguage" runat="server" meta:resourcekey="lclLanguage" /></span><span
                        class="entry">
                        <asp:DropDownList ID="comboLanguage" runat="server" AutoPostBack="True"
                            Width="100px" meta:resourcekey="comboLanguageResource1"
                            OnSelectedIndexChanged="ComboLanguageSelectedIndexChanged">
                        </asp:DropDownList></span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclCountry" runat="server" meta:resourcekey="lclCountry" /></span><span
                        class="entry">
                        <asp:DropDownList ID="comboCountry" runat="server" Width="100px"
                            meta:resourcekey="comboCountryResource1">
                        </asp:DropDownList></span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclCreditNumber" runat="server" meta:resourcekey="lclCreditNumber" /></span><span
                        class="entry">
                        <asp:TextBox ID="txtCreditCard" runat="server" Width="100px" Columns="16"
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
                        <asp:TextBox ID="TextBox3" runat="server" Width="100px" Columns="16"
                            meta:resourcekey="txtExpirationDateResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredExpirationDateValidator1" runat="server" ControlToValidate="txtExpirationDate"
                            Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>"
                            meta:resourcekey="rfvExpirationDateResource1"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpirationDateValidator1" runat="server" ControlToValidate="txtExpirationDate"
                            Display="Dynamic" ValidationExpression="\d+d+\d+d+\d+d+d+d+"
                            meta:resourcekey="revExpirationDatel"></asp:RegularExpressionValidator></span>
            </div>
            <div class="button">
                <asp:Button ID="btnRegister" runat="server" OnClick="BtnRegisterClick" meta:resourcekey="btnRegister" />
            </div>
        </form>
    </div>
</asp:Content>