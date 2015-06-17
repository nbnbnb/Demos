<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<NerdDinner.Models.Dinner>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Details
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


                        <h2><%= Model.Title %></h2> 
                        <p> 
                             <strong>When:</strong> 
                             <%= Model.EventDate.ToShortDateString() %> 

                             <strong>@</strong> 
                             <%= Model.EventDate.ToShortTimeString() %> 
                        </p> 
                        <p> 
                             <strong>Where:</strong> 
                             <%= Model.Address %>, 
                             <%= Model.Country %> 
                        </p> 
                         <p> 
                             <strong>Description:</strong> 
                             <%= Model.Description %> 
                        </p> 
                        <p> 
                             <strong>Organizer:</strong> 
                             <%= Model.HostedBy %> 
                             (<%= Model.ContactPhone %>) 
                        </p> 

                        <%= Html.ActionLink("Edit Dinner", "Edit", new { id=Model.DinnerID })%> | 
                        <%= Html.ActionLink("Delete Dinner","Delete", new { id=Model.DinnerID})%> |
                        <%= Html.ActionLink("Create New","Create") %>


</asp:Content>

