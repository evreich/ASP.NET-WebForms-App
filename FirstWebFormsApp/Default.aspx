﻿<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="FirstWebFormsApp._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="main-container">
        <asp:Repeater ID="RepeaterBooks" OnItemDataBound="RepeaterEvent_ItemDataBound" runat="server">
            <HeaderTemplate>
                <h2>Список книг: </h2>
                <hr />
                <table class="books-container">
                    <thead>
                        <tr>
                            <th>Название</th>
                            <th>Жанр</th>
                            <th>Автор</th>
                            <th>Дата выпуска</th>
                        </tr>
                    </thead>
            </HeaderTemplate> 
            <ItemTemplate>
                <tr class="book-row">
                    <td><asp:Label ID="TitleBook" runat="server"></asp:Label></td>
                    <td><asp:Label ID="GenreBook" runat="server"></asp:Label></td>
                    <td><asp:Label ID="AuthorBook" runat="server"></asp:Label></td>
                    <td><asp:Label ID="DateRealiseBook" runat="server"></asp:Label></td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
        <asp:label ID="lb_Error" class="error" runat="server"></asp:label>
    </div>
</asp:Content>
