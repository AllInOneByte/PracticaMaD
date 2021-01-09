<%@ Page Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true"
    CodeBehind="ProductSearch.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Product.ProductSearch"
    meta:resourcekey="Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation" runat="server">
    <asp:Localize ID="lclMenuExplanation" runat="server" meta:resourcekey="lclMenuExplanation" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_MenuLinks" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <div id="form">
        <form id="ProductSearchForm" method="get" runat="server">

            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclSearch" runat="server" meta:resourcekey="lclSearch" />
                </span>

                <span class="entry">
                    <asp:TextBox ID="txtSearch" runat="server" Width="100" Columns="16"></asp:TextBox>
                    <asp:DropDownList ID="CategoryDropDownList" AutoPostBack="True" runat="server">
                    </asp:DropDownList>
                </span>

                <span class="button">
                    <asp:Button ID="btnSearch" runat="server" OnClick="BtnSearchClick" meta:resourcekey="btnSearch" />
                </span>
            </div>

            <div>
                <p>
                    <asp:Label ID="lblNoProducts" meta:resourcekey="lblNoProducts" runat="server"></asp:Label>
                </p>
                <asp:GridView ID="gvProducts" runat="server" GridLines="None" AutoGenerateColumns="False">
                    <Columns>
                        <asp:HyperLinkField DataTextField="productName"
                            HeaderText="<%$ Resources:Common, productName %>"
                            DataNavigateUrlFields="productId"
                            DataNavigateUrlFormatString="/Pages/ProductDetails.aspx?product={0}" />
                        <asp:BoundField DataField="Category.categoryName"
                            HeaderText="<%$ Resources:Common, categoryName %>" />
                        <asp:BoundField DataField="productDate" HeaderText="<%$ Resources:Common, productDate %>"
                            DataFormatString="{0:d/M/yyyy}" />
                        <asp:BoundField DataField="productPrice" HeaderText="<%$ Resources:Common, productPrice %>"
                            DataFormatString="{0:C}"/>
                    </Columns>
                </asp:GridView>
            </div>

            <!-- "Previous" and "Next" links. -->
            <div class="previousNextLinks">
                <span class="previousLink">
                    <asp:HyperLink ID="lnkPrevious" Text="<%$ Resources:Common, Previous %>"
                        runat="server" Visible="False"></asp:HyperLink>
                </span>
                <span class="nextLink">
                    <asp:HyperLink ID="lnkNext" Text="<%$ Resources:Common, Next %>" runat="server"
                        Visible="False"></asp:HyperLink>
                </span>
            </div>
        </form>
    </div>

</asp:Content>
