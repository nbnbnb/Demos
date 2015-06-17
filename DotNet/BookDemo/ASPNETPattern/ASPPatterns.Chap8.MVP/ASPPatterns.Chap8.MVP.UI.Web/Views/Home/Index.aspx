<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Shop.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="ASPPatterns.Chap8.MVP.UI.Web.Views.Home.Index" %>

<%@ Register Src="~/Views/Shared/ProductList.ascx" TagName="ProductList" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Today’s Top Products</h2>
    <uc1:ProductList ID="plBestSellingProducts" runat="server" />
</asp:Content>
