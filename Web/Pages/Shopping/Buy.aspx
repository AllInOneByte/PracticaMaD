<%@ Page Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true"
    Codebehind="Buy.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Shopping.Buy"
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
        <form id="BuyForm" method="POST" runat="server">
            <div class="field">
                <asp:HyperLink ID="lnkRegisterCreditCard" runat="server" 
                    NavigateUrl="~/Pages/User/RegisterCreditCard.aspx"
                    meta:resourcekey="lnkRegisterCreditCard" Visible="false"/>
                <div class="field">
                <br />
                <br />
                <asp:Label ID="lblAmountError" runat="server" ForeColor="Red" Style="position: relative"
                        Visible="False" meta:resourcekey="lblAmountError"></asp:Label>
                <br />
                <span class="label"><asp:Localize ID="lclDescription" runat="server" meta:resourcekey="lclDescription" /></span><span class="entry">
                    <asp:TextBox ID="txtDescription" runat="server" Width="100" Columns="16"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="DescriptionValidator1" runat="server"
                        ControlToValidate="txtDescription" Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>"/></span>
                </div>
                <div class="field">
                <span class="label"><asp:Localize ID="lclTotal" runat="server" meta:resourcekey="lclTotal" /></span><span class="entry">
                    <asp:TextBox ID="txtPrice" ReadOnly="true" runat="server" Width="100" Columns="16"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                        ControlToValidate="txtPrice" Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>"/></span>
                </div>
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
                        <asp:Label ID="lblNumberError" runat="server" ForeColor="Red" Style="position: relative"
                            Visible="False" meta:resourcekey="lblNumberError"></asp:Label>
                        <asp:Label ID="lblUserError" runat="server" ForeColor="Red" Style="position: relative"
                            Visible="False" meta:resourcekey="lblUserError"></asp:Label></span>
            </div>
            <div class="field">
                <span class="label"><asp:Localize ID="lclAddress" runat="server" meta:resourcekey="lclAddress" /></span><span class="entry">
                    <asp:TextBox ID="txtAddress" runat="server" Width="100" Columns="16"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvAddress" runat="server"
                        ControlToValidate="txtAddress" Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>"/></span>
            </div>
            <div class="button">
                <asp:Button ID="btnBuy" runat="server" OnClick="BtnBuyClick" meta:resourcekey="btnBuy"/>
            </div>
            <br />
            <br />
            <asp:HyperLink ID="lnkBack" runat="server" 
                    NavigateUrl="~/Pages/Shopping/Cart.aspx"
                    meta:resourcekey="lnkBack" Visible="false"/>
        </form>
    </div>
</asp:Content>
