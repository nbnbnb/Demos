<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Shop.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="ASPPatterns.Chap8.FrontController.UI.Web.Views.Home.Index" %>

<%@ Register Src="~/views/Shared/ProductList.ascx" TagName="ProductList" TagPrefix="uc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Today’s Top Products</h2>
    <uc1:ProductList ID="ProductList1" runat="server" />
</asp:Content>
