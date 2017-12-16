﻿<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="FirstWebFormsApp._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="main-container">
        <h2>Поиск книг: </h2>
        <asp:TextBox ID="tbFindBookByTitle" runat="server" CssClass="right-margin"></asp:TextBox>
        <asp:DropDownList ID="ddlFindBookByGenre" runat="server" CssClass="right-margin"></asp:DropDownList>
        <asp:Button ID="btnFindBook" runat="server" Text="Найти книги" /><br />
        <hr />
        <asp:Repeater ID="RepeaterBooks" OnItemDataBound="RepeaterEvent_ItemDataBound" runat="server">
            <HeaderTemplate>
                <h2>Список книг: </h2>
                
                <table class="books-container">
                    <asp:TableHeaderRow ID="tableHeader" runat="server" >
                        <asp:TableHeaderCell ID="hdTitle" runat="server">
                            <asp:LinkButton ID="linkTitle" runat="server" OnClick="linkTitle_Click">
                                <asp:Label ID="lbTitle" runat="server">Название</asp:Label>
                            </asp:LinkButton>
                        </asp:TableHeaderCell>
                        <asp:TableHeaderCell ID="hdGenre" runat="server">
                            <asp:LinkButton ID="linkGenre" runat="server" OnClick="linkGenre_Click">
                                <asp:Label ID="lbGenre" runat="server">Жанр</asp:Label>
                            </asp:LinkButton>
                        </asp:TableHeaderCell>
                        <asp:TableHeaderCell ID="hdAuthor" runat="server">
                            <asp:LinkButton ID="linkAuthor" runat="server" OnClick="linkAuthor_Click">
                                <asp:Label ID="lbAuthor" runat="server">Автор</asp:Label>
                            </asp:LinkButton>
                        </asp:TableHeaderCell>
                        <asp:TableHeaderCell ID="hdDate" runat="server">
                            <asp:LinkButton ID="linkDate" runat="server" OnClick="linkDate_Click">
                                <asp:Label ID="lbDate" runat="server">Дата выпуска</asp:Label>
                            </asp:LinkButton>
                        </asp:TableHeaderCell>
                    </asp:TableHeaderRow>
                
            </HeaderTemplate> 
            <ItemTemplate>
                <asp:TableRow ID="rowBook" CssClass="book-row" runat="server" ClientIDMode="Static">
                    <asp:TableCell ID="cellTitle" runat="server" ClientIDMode="Static">
                        <asp:LinkButton ID="linkToEdit" runat="server" OnClick="linkToEdit_Click">
                            <asp:HiddenField ID="IdBook" runat="server"/>
                            <asp:Label ID="TitleBook" runat="server"></asp:Label>
                        </asp:LinkButton> 
                    </asp:TableCell>
                    <asp:TableCell ID="cellGenre" runat="server" ClientIDMode="Static">
                        <asp:Label ID="GenreBook" runat="server"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell ID="cellAuthor" runat="server" ClientIDMode="Static">
                        <asp:Label ID="AuthorBook" runat="server"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell ID="cellDataRealise" runat="server" ClientIDMode="Static">
                        <asp:Label ID="DateRealiseBook" runat="server"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>

        <br />
        <asp:Label ID="lbPager" runat="server" Text="Страницы результата"></asp:Label><br />
        <asp:Table ID="tableForPager" runat="server">
            <asp:TableRow ID="trPager" runat="server">
                <asp:TableCell ID="tdPager" runat="server">
                    <asp:PlaceHolder ID="phPager" runat="server" />
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
               
        <hr />
        <asp:Button ID="btnGoToAddBook" OnClick="btnGoToAddBook_Click" Text="Добавить книгу" runat="server"></asp:Button>
        <asp:Label ID="lb_Error" class="error" runat="server"></asp:Label>
    </div>
</asp:Content>
