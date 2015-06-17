<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<NerdDinner.Models.Product>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Test2
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Test2</h2>

    <% using (Html.BeginForm()) {%>
        <%= Html.ValidationSummary(true) %>

        <fieldset>
            <legend>Fields</legend>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.Name) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.Name) %>
                <%= Html.ValidationMessageFor(model => model.Name) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.Color) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.Color) %>
                <%= Html.ValidationMessageFor(model => model.Color) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.Weight) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.Weight) %>
                <%= Html.ValidationMessageFor(model => model.Weight) %>
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

