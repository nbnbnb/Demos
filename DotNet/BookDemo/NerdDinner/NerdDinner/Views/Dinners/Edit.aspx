<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<NerdDinner.Models.Dinner>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit: <%= Model.Title %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Edit</h2>

    <% using (Html.BeginForm()) {%>
        <%= Html.ValidationSummary("Please correct the errors and try again.") %>
        
        <fieldset>
            <legend>Fields</legend>

            <div class="editor-label">
                <%= Html.LabelFor(model => model.Title) %>
            </div>
            <div class="editor-field">
                <%--<%= Html.TextBoxFor(model => model.Title, new { size=30,@class="myclass"})%>--%>
                <%--<%= Html.ValidationMessageFor(model => model.Title) %>--%>
                <%= Html.EditorFor(model=>model.Title) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.EventDate) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.EventDate, String.Format("{0:g}", Model.EventDate)) %>
                <%= Html.ValidationMessageFor(model => model.EventDate) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.Description) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.Description) %>
                <%= Html.ValidationMessageFor(model => model.Description) %>
            </div>

            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.ContactPhone) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.ContactPhone) %>
                <%= Html.ValidationMessageFor(model => model.ContactPhone) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.Address) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.Address) %>
                <%= Html.ValidationMessageFor(model => model.Address) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.Country) %>
            </div>

            <div class="editor-field">
                <%= Html.DropDownListFor(model => model.Country, ViewData["Country"] as SelectList)%>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.Latitude) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.Latitude, String.Format("{0:F}", Model.Latitude)) %>
                <%= Html.ValidationMessageFor(model => model.Latitude) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.Longitude) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.Longitude, String.Format("{0:F}", Model.Longitude)) %>
                <%= Html.ValidationMessageFor(model => model.Longitude) %>
            </div>
            <p>
                <input type="submit" value="Save" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%= Html.ActionLink("Back to List", "Index") %>
    </div>

</asp:Content>

