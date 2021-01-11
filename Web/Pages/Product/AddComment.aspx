<%@ Page Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true"
    CodeBehind="AddComment.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Product.AddComment"
    meta:resourcekey="Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation" runat="server">
    <asp:Localize ID="lclMenuExplanation" runat="server" meta:resourcekey="lclMenuExplanation" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_MenuLinks" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <div id="form">
        <form id="AddCommentForm" method="post" runat="server">
            <p>
                <asp:Label ID="lblNoProduct" meta:resourcekey="lblNoProduct" runat="server" Visible="false"
                    ForeColor="Red"></asp:Label>
                <br />
                <asp:Label ID="lblUnlogedUser" meta:resourcekey="lblUnlogedUser" runat="server" Visible="false"
                    ForeColor="Red"></asp:Label>
                <br />
                <asp:Label ID="lblCommentAdded" meta:resourcekey="lblCommentAdded" runat="server" Visible="false"
                    ForeColor="Green"></asp:Label>
                <br />
                <asp:HyperLink ID="hlReturnToDetails" meta:resourcekey="hlReturnToDetails" runat="server" Visible="false"></asp:HyperLink>
            </p>
            <asp:TextBox ID="tagBox" runat="server" meta:resourcekey="commentTagBox"
                Width="50%" TextMode="SingleLine"></asp:TextBox>
            <br />
            <br />
            <asp:TextBox ID="commentBody" runat="server" meta:resourcekey="commentBodyTextBox"
                Width="50%" TextMode="MultiLine"></asp:TextBox>
            <div class="button">
                <asp:Button ID="btnAddComment" runat="server" OnClick="BtnAddCommentClick" meta:resourcekey="btnAddComment"/>
            </div>
        </form>
    </div>
</asp:Content>
