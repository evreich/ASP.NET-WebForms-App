<%@ Page Title="AddBook" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddBook.aspx.cs" Inherits="FirstWebFormsApp.AddBook" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="main-container">
        <asp:ValidationSummary HeaderText="Исправьте следующие ошибки:" CssClass="error" runat="server" />
        <label for="TitleBook">Название книги: </label><br />
        <input id="TitleBook" name="TitleBook" /><br />

        <label for="GenreId">Жанр: </label><br />
        <select id="GenreId" name="GenreId">
            <asp:Repeater ID="repeaterGenres" ItemType="FirstWebFormsApp.Models.Genre"
                SelectMethod="SetGenres" runat="server">
                <ItemTemplate>
                    <option value = <%# Item.Id %>><%# Item.Title %></option>
                </ItemTemplate>
            </asp:Repeater>
        </select> <br />

        <label for="AuthorId">Автор: </label><br />
        <select id="AuthorId" name="AuthorId">
            <asp:Repeater ID="repeaterAuthor" ItemType="FirstWebFormsApp.Models.Author"
                SelectMethod="SetAuthors" runat="server">
                <ItemTemplate>
                    <option value = <%# Item.Id %>><%# Item.FirstName + " " + Item.LastName %></option>
                </ItemTemplate>
            </asp:Repeater>
        </select> <br />

        <label for="DateRealise">Дата создания: </label><br />
        <input id="DateRealise" name="DateRealise" type="date" /><br />

        <asp:Button ID="btnAdd" runat="server" Text="Добавить книгу" /><br />
        <a href="Default.aspx">Назад</a> <br />
        <asp:Label ID="lb_Error" class="error" runat="server"></asp:Label>
    </div>
</asp:Content>
