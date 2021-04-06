<%@ Page Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true"
    CodeBehind="ProductSearch.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Product.ProductSearch"
    meta:resourcekey="Page" EnableViewState="true" %>

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
                    <asp:TextBox ID="txtSearch" runat="server" Width="30%" Columns="16" meta:resourcekey="txtSearch"></asp:TextBox>
                    <asp:DropDownList ID="CategoryDropDownList" AutoPostBack="False" runat="server" Width="30%"></asp:DropDownList>
                    <asp:Button ID="btnSearch" runat="server" OnClick="BtnSearchClick" meta:resourcekey="btnSearch" Width="30%" />
                </span>
            </div>
            <br />
            <br />
            <asp:HyperLink ID="lnkBack" runat="server"
                NavigateUrl="~/Pages/MainPage.aspx"
                meta:resourcekey="lnkBack" />
            <br />
            <br />
            <asp:Label ID="lblAmountError" runat="server" ForeColor="Red" Style="position: relative"
                Visible="False" meta:resourcekey="lblAmountError"></asp:Label>
            <div>
                <p>
                    <asp:Label ID="lblNoProducts" meta:resourcekey="lblNoProducts" runat="server"></asp:Label>
                </p>
                <asp:GridView ID="gvProducts" runat="server" GridLines="None" AutoGenerateColumns="False" Width="100%"
                    OnRowDataBound="gvProducts_RowDataBound" OnRowCreated="gvProducts_RowCreated" ViewStateMode="Disabled">
                    <Columns>
                        <asp:BoundField DataField="productId" ItemStyle-CssClass="hiddencol" />
                        <asp:HyperLinkField DataTextField="productName"
                            HeaderText="<%$ Resources:Common, productName %>"
                            DataNavigateUrlFields="productId"
                            DataNavigateUrlFormatString="/Pages/Product/ProductDetails.aspx?product={0}" />
                        <asp:BoundField DataField="Category.categoryName"
                            HeaderText="<%$ Resources:Common, categoryName %>" />
                        <asp:BoundField DataField="productDate" HeaderText="<%$ Resources:Common, productDate %>"
                            DataFormatString="{0:d/M/yyyy}" />
                        <asp:BoundField DataField="productPrice" HeaderText="<%$ Resources:Common, productPrice %>"
                            DataFormatString="{0:C}" />
                        <asp:TemplateField ShowHeader="false">
                            <ItemTemplate>
                                <asp:Button ID="btnAddCart" runat="server" OnClick="BtnAddCartClick" meta:resourcekey="btnAddCart" />
                            </ItemTemplate>
                        </asp:TemplateField>
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
