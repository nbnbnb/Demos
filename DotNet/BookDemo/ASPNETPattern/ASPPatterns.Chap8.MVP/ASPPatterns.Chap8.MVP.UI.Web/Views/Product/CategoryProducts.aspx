<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Shop.Master" AutoEventWireup="true" CodeBehind="CategoryProducts.aspx.cs" Inherits="ASPPatterns.Chap8.MVP.UI.Web.Views.Product.CategoryProducts" %>

<%@ Register Src="~/Views/Shared/ProductList.ascx" TagName="ProductList" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>All
        <asp:Literal ID="litCategoryName" runat="server" /></h2>
    <uc1:ProductList ID="plCategoryProducts" runat="server" />
</asp:Content>
