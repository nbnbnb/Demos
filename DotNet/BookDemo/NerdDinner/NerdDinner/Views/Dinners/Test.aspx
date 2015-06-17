<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<NerdDinner.Models.Dinner>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Test
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Test</h2>

    <% using (Html.BeginForm()) {%>
        <%= Html.ValidationSummary(true) %>

        <fieldset>
            <legend>Fields</legend>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.DinnerID) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.DinnerID) %>
                <%= Html.ValidationMessageFor(model => model.DinnerID) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.Title) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.Title) %>
                <%= Html.ValidationMessageFor(model => model.Title) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.EventDate) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.EventDate) %>
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
                <%= Html.LabelFor(model => model.HostedBy) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.HostedBy) %>
                <%= Html.ValidationMessageFor(model => model.HostedBy) %>
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
                <%= Html.TextBoxFor(model => model.Country) %>
                <%= Html.ValidationMessageFor(model => model.Country) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.Latitude) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.Latitude) %>
                <%= Html.ValidationMessageFor(model => model.Latitude) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.Longitude) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.Longitude) %>
                <%= Html.ValidationMessageFor(model => model.Longitude) %>
            </div>
            
            <p>
                <input type="submit" value="Create" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%= Html.ActionLink("Back to List", "Index") %>
    </div>

</asp:Content>

