<%@ Page Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true"
    CodeBehind="ProductComments.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Product.ProductComments"
    meta:resourcekey="Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation" runat="server">
    <asp:Localize ID="lclMenuExplanation" runat="server" meta:resourcekey="lclMenuExplanation" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_MenuLinks" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <div id="form">
        <form id="ProductCommentForm" method="get" runat="server">
             <asp:HyperLink ID="lnkBack" runat="server" meta:resourcekey="lnkBack"></asp:HyperLink>
            <br />
            <asp:Table ID="ownComment" runat="server" Visible="false" Width="100%">
                <asp:TableHeaderRow runat="server">
                    <asp:TableHeaderCell ID="cellCaptionOwnCommentProfileName" runat="server"
                        Text="<%$ Resources:Common, userProfileName %>"></asp:TableHeaderCell>

                    <asp:TableHeaderCell ID="cellCaptionOwnCommentBody" runat="server"
                        Text="<%$ Resources:Common, commentBody %>"></asp:TableHeaderCell>

                    <asp:TableHeaderCell ID="cellCaptionOwnCommentDate" runat="server"
                        Text="<%$ Resources:Common, commentDate %>"></asp:TableHeaderCell>

                    <asp:TableHeaderCell ID="cellCaptionOwnCommentTags" runat="server"
                        Text="<%$ Resources:Common, commentTags %>"></asp:TableHeaderCell>
                </asp:TableHeaderRow>
                <asp:TableRow runat="server">
                    <asp:TableCell ID="cellOwnCommentId" runat="server" Visible="false"></asp:TableCell>

                    <asp:TableCell ID="cellOwnCommentProfileName" runat="server"></asp:TableCell>

                    <asp:TableCell ID="cellOwnCommentBody" runat="server"></asp:TableCell>

                    <asp:TableCell ID="cellOwnCommentDate" runat="server"></asp:TableCell>

                    <asp:TableCell ID="cellOwnCommentTags" style="word-wrap:break-word" runat="server"></asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <br />
            <asp:Button ID="btnDelete" runat="server" meta:resourcekey="btnDelete" OnClick="BtnDelete_Click" />
            <asp:Button ID="btnModify" runat="server" meta:resourcekey="btnModify" OnClick="BtnModify_Click" />
            <br />
            <br />
            <p>
                <asp:Label ID="lblNoComments" meta:resourcekey="lblNoComments" runat="server"></asp:Label>
            </p>
            <br />
            <asp:GridView ID="gvProducts" runat="server" GridLines="None" AutoGenerateColumns="False"
                Width="100%" OnRowCreated="gvProducts_RowCreated" OnRowDataBound="gvProducts_RowDataBound">
                <Columns>
                    <asp:BoundField DataField="commentId" ItemStyle-CssClass="hiddencol"/>
                    <asp:BoundField DataField="UserProfile.loginName" HeaderText="<%$ Resources:Common, userProfileName %>" />
                    <asp:BoundField DataField="comment1" HeaderText="<%$ Resources:Common, commentBody %>" />
                    <asp:BoundField DataField="commentDate" HeaderText="<%$ Resources:Common, commentDate %>"
                        DataFormatString="{0:d/M/yyyy}" />
                    <asp:TemplateField HeaderText="<%$ Resources:Common, commentTags %>">
                            <ItemTemplate>
                                <asp:TextBox ID="TextTags" runat="server" style="resize:none" BackColor="White" ReadOnly="True" TextMode="MultiLine"></asp:TextBox>
                            </ItemTemplate>
                    </asp:TemplateField> 
                </Columns>
            </asp:GridView>
        </form>
    </div>

    <!-- "Previous" and "Next" links. -->
    <div class="previousNextLinks">
        <span class="previousLink">
            <asp:HyperLink ID="lnkPrevious" Text="<%$ Resources:Common, Previous %>"
                runat="server" Visible="False"></asp:HyperLink>
        </span>
        <span class="nextLink">
            <asp:HyperLink ID="lnkNext" Text="<%$ Resources:Common, Next %>"
                runat="server" Visible="False"></asp:HyperLink>
        </span>
    </div>
</asp:Content>
