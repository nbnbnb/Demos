<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<NerdDinner.Models.Dinner>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Create
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

						   <h2>Host a Dinner</h2> 

							  <%= Html.ValidationSummary(true) %> 

							  <% using (Html.BeginForm()) {%> 

								   <fieldset> 
										 <p> 
											  <%= Html.LabelFor(m => m.Title) %> 
											  <%= Html.TextBoxFor(m => m.Title) %> 
											  <%= Html.ValidationMessageFor(m => m.Title) %> 
										 </p> 
										 <p> 
											  <%= Html.LabelFor(m => m.EventDate) %> 
											  <%= Html.TextBoxFor(m => m.EventDate) %> 
											  <%= Html.ValidationMessageFor(m => m.EventDate, "*") %> 
										 </p> 
										 <p> 
											  <%= Html.LabelFor(m => m.Description) %> 
											  <%= Html.TextAreaFor(m => m.Description) %> 
											  <%= Html.ValidationMessageFor(m => m.Description, "*") %> 
										 </p> 

										 <p> 
											  <%= Html.LabelFor(m => m.Address) %> 
											  <%= Html.TextBoxFor(m => m.Address) %> 
											  <%= Html.ValidationMessageFor(m => m.Address, "*") %> 
										 </p> 
										 <p> 
											  <%= Html.LabelFor(m => m.Country) %> 
											  <%= Html.TextBoxFor(m => m.Country) %> 
											  <%= Html.ValidationMessageFor(m => m.Country, "*") %> 
										 </p> 
										 <p> 
											  <%= Html.LabelFor(m => m.ContactPhone) %> 
											  <%= Html.TextBoxFor(m => m.ContactPhone) %> 
											  <%= Html.ValidationMessageFor(m => m.ContactPhone, "*") %> 
										 </p> 
										 <p> 
											  <%= Html.LabelFor(m => m.Latitude) %> 
											  <%= Html.TextBoxFor(m => m.Latitude) %> 
											  <%= Html.ValidationMessageFor(m => m.Latitude, "*") %> 
										 </p> 
										 <p> 
											  <%= Html.LabelFor(m => m.Longitude) %> 
											  <%= Html.TextBoxFor(m => m.Longitude) %> 
											  <%= Html.ValidationMessageFor(m => m.Longitude, "*") %> 

									   </p> 
									   <p> 
											 <input type="submit" value="Save" /> 
									   </p> 
								 </fieldset> 

						   <% } %> 
</asp:Content>

