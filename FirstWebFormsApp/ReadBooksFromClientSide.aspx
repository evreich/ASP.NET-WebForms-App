<%@ Page Title="ReadBooksFromClientSide" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReadBooksFromClientSide.aspx.cs" Inherits="FirstWebFormsApp.ReadBooksFromClientSide" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
 <asp:PlaceHolder runat="server">
        <%: Scripts.Render("~/bundles/Site") %>
 </asp:PlaceHolder>
    
 <div class="main-container">
        <asp:Label ID="lbMsgNotFoundBooks" runat="server" CssClass="error" Visible="false" Text="Книги не найдены"></asp:Label>
        <input id="btnShowBooks" onclick="ShowBooks()" type="button" value="Отобразить список книг"  />
        <h4 id="lbWait" style="display: none;" >Подождите, данные загружаются...</h4>
        <table id="tableBooks">
            <thead>
                <tr>
                    <td>Название</td>
                    <td>Жанр</td>
                    <td>Автор</td>
                    <td>Дата выпуска</td>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td class="book-row">-</td>
                    <td class="book-row">-</td>
                    <td class="book-row">-</td>
                    <td class="book-row">-</td>
                </tr>

            </tbody>
        </table>

        <asp:Label ID="lb_Error" class="error" runat="server" ClientIDMode="Static"></asp:Label>
        <br />
        <asp:HyperLink ID="hlToDefault" NavigateUrl="~/Default.aspx" Text="Назад" runat="server"></asp:HyperLink> <br />
    </div>
</asp:Content>
