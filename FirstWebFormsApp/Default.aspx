<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="FirstWebFormsApp._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="main-container">
        <table class="books-container">             
            <thead>
                <tr>
                    <th>Название</th>
                    <th>Жанр</th>
                    <th>Автор</th>
                    <th>Дата выпуска</th>
                </tr>
            </thead>
            <tbody>
                <%=ShowBooks()%>
            </tbody>
        </table>
    </div>

</asp:Content>
