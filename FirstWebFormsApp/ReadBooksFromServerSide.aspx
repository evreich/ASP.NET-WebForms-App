<%@ Page Title="ReadBooksFromServerSide" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReadBooksFromServerSide.aspx.cs" Inherits="FirstWebFormsApp.ReadBooksFromServerSide" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="main-container">
        <asp:Label ID="lbMsgNotFoundBooks" runat="server" CssClass="error" Visible="false" Text="Книги не найдены"></asp:Label>

        <asp:Button ID="btnShowBooks" runat="server" OnClick="btnShowBooks_Click" Text="Отобразить список книг" />
        <asp:Repeater ID="RepeaterBooks" OnItemDataBound="RepeaterEvent_ItemDataBound" runat="server">
            <HeaderTemplate>           
                <h2>Список книг: </h2>
                <table class="books-container">
                    <asp:TableHeaderRow ID="tableHeader" runat="server" >
                        <asp:TableHeaderCell ID="hdTitle" runat="server">
                            <asp:Label ID="lbTitle" runat="server">Название</asp:Label>
                        </asp:TableHeaderCell>
                        <asp:TableHeaderCell ID="hdGenre" runat="server">
                            <asp:Label ID="lbGenre" runat="server">Жанр</asp:Label>
                        </asp:TableHeaderCell>
                        <asp:TableHeaderCell ID="hdAuthor" runat="server">
                            <asp:Label ID="lbAuthor" runat="server">Автор</asp:Label>
                        </asp:TableHeaderCell>
                        <asp:TableHeaderCell ID="hdDate" runat="server">
                            <asp:Label ID="lbDate" runat="server">Дата выпуска</asp:Label>
                        </asp:TableHeaderCell>
                    </asp:TableHeaderRow>
                
            </HeaderTemplate> 
            <ItemTemplate>
                <asp:TableRow ID="rowBook" CssClass="book-row" runat="server" ClientIDMode="Static">
                    <asp:TableCell ID="cellTitle" runat="server" ClientIDMode="Static">
                        <asp:HiddenField ID="IdBook" runat="server"/>
                        <asp:Label ID="TitleBook" runat="server"></asp:Label>
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
        <asp:Label ID="lb_Error" class="error" runat="server"></asp:Label>
        <br />
        <asp:HyperLink ID="hlToDefault" NavigateUrl="~/Default.aspx" Text="Назад" runat="server"></asp:HyperLink> <br />
    </div>
</asp:Content>