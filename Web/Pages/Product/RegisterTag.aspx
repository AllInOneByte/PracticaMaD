<%@ Page Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true"
    CodeBehind="RegisterTag.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Product.RegisterTag"
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
        <form id="RegisterTagForm" method="post" runat="server">

            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclTag" runat="server" meta:resourcekey="lclTag" />
                </span><span
                        class="entry">
                        <asp:TextBox ID="txtTag" runat="server" Width="100px" Columns="16"
                            meta:resourcekey="txtTagResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvTag" runat="server" ControlToValidate="txtTag"
                            Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>"
                            meta:resourcekey="rfvTagResource1"></asp:RequiredFieldValidator>
                        <asp:Label ID="lblTagError" runat="server" ForeColor="Red" Style="position: relative"
                            Visible="False" meta:resourcekey="lblTagError"></asp:Label></span>
            </div>
            <div class="button">
                <asp:Button ID="btnRegisterTag" runat="server" OnClick="BtnRegisterTagClick" meta:resourcekey="btnRegisterTag" />
            </div>
        </form>
    </div>
</asp:Content>