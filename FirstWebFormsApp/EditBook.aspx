<%@ Page Title="EditBook" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditBook.aspx.cs" Inherits="FirstWebFormsApp.EditBook" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="main-container">
         <asp:ValidationSummary HeaderText="Исправьте следующие ошибки:" CssClass="error" runat="server" />
        <input id="Id" type="hidden" value="<% if (!IsPostBack) Response.Write(IdBook); %>"/> 
        <label for="TitleBook">Название книги: </label><br />
        <input id="TitleBook" name="TitleBook" value="<% if (!IsPostBack) Response.Write(TitleBook); %>" /><br />

        <label for="GenreId">Жанр: </label><br />
        <select id="GenreId" name="GenreId">
            <%
                foreach (var elem in SetGenres())
                {
                    if (elem.Id == GenreId)
                        Response.Write(String.Format("<option value={0} selected>{1}</option>", elem.Id, elem.Title));
                    else
                        Response.Write(String.Format("<option value={0}>{1}</option>", elem.Id, elem.Title));
                }
            %>
        </select> <br />

        <label for="AuthorId">Автор: </label><br />
        <select id="AuthorId" name="AuthorId">
            <%
                foreach (var elem in SetAuthors())
                {
                    if (elem.Id == AuthorId)
                        Response.Write(String.Format("<option value={0} selected>{1} {2}</option>", elem.Id, elem.FirstName, elem.LastName));
                    else
                        Response.Write(String.Format("<option value={0}>{1}</option>", elem.Id, elem.FirstName, elem.LastName));
                }
            %>
            <!--
            <asp:Repeater ID="repeaterAuthor" ItemType="FirstWebFormsApp.Models.Author"
                SelectMethod="SetAuthors" runat="server">
                <ItemTemplate>
                    <option value = <%# Item.Id %>><%# Item.FirstName + " " + Item.LastName %></option>
                </ItemTemplate>
            </asp:Repeater>
            -->
        </select> <br />

        <label for="DateRealise">Дата создания: </label><br />
        <input id="DateRealise" name="DateRealise" type="date" value="<% if (!IsPostBack) Response.Write(DateRealise); %>"/><br />

        <asp:Button ID="btnAdd" runat="server" Text="Изменить книгу" /><br />
        <a href="Default.aspx">Назад</a> <br />
        <asp:Label ID="lb_Error" class="error" runat="server"></asp:Label>
    </div>
</asp:Content>
