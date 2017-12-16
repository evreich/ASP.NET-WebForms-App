<%@ Page Title="EditBook" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditBook.aspx.cs" Inherits="FirstWebFormsApp.EditBook" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="main-container">
        <asp:ValidationSummary 
            HeaderText="Исправьте следующие ошибки:" 
            CssClass="error" 
            runat="server" 
            ShowMessageBox="false" 
            DisplayMode="BulletList" 
            ShowSummary="true" />

        <asp:HiddenField ID="hfIdBook" runat="server" />

        <asp:Label ID="lbTitleBook" Text="Название книги: " AssociatedControlID="tbTitleBook" runat="server"></asp:Label><br />
        <asp:TextBox ID="tbTitleBook" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator 
             ID="rvTitleBook"
             runat="server"
             ControlToValidate="tbTitleBook"
             CssClass="error" 
             ErrorMessage="Введите название книги"
             EnableClientScript="true"
             SetFocusOnError="true"
             Text="*" >
        </asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator
             ID="revTitleBook"
             runat="server"
             ControlToValidate="tbTitleBook"
             CssClass="error" 
             ErrorMessage="Некорректное название"
             EnableClientScript="true"
             SetFocusOnError="true"
             ValidationExpression="^[A-Za-zА-Яа-я\s\d]+"
             Text="*" >
        </asp:RegularExpressionValidator>
        <br />

        <asp:Label ID="lbGenre" Text="Жанр: " AssociatedControlID="ddlGenre" runat="server" ></asp:Label><br />
        <asp:DropDownList ID="ddlGenre" runat="server"></asp:DropDownList> <br />

        <asp:Label ID="lbAuthor" Text="Автор: " AssociatedControlID="ddlAuthor" runat="server"></asp:Label><br />
        <asp:DropDownList ID="ddlAuthor" runat="server"></asp:DropDownList> <br />

        <ajaxToolkit:CalendarExtender ID="calendar1" runat="server" TargetControlID="tbDateRealise" Format="yyyy-MM-dd" DefaultView="Years"/>

        <asp:Label ID="lbDAteRealise" Text="Дата создания: " AssociatedControlID="tbDateRealise" runat="server"></asp:Label><br />
        <asp:TextBox ID="tbDateRealise" TextMode="Date" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator 
             ID="rvDateRealise"
             runat="server"
             ControlToValidate="tbDateRealise"
             CssClass="error" 
             ErrorMessage="Выберите дату создания"
             EnableClientScript="true"
             SetFocusOnError="true"
             Text="*" >
        </asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator
             ID="reDateRealise"
             runat="server"
             ControlToValidate="tbDateRealise"
             CssClass="error" 
             ErrorMessage="Некорректное значение"
             EnableClientScript="true"
             SetFocusOnError="true"
             ValidationExpression="^\d{4}-\d{2}-\d{2}"
             Text="*" >
        </asp:RegularExpressionValidator>
        <br />

        <asp:Button ID="btnAdd" runat="server" Text="Изменить книгу" /><br />
        <asp:HyperLink ID="hlToDefault" NavigateUrl="~/Default.aspx" Text="Назад" runat="server"></asp:HyperLink> <br />
        <asp:Label ID="lb_Error" class="error" runat="server"></asp:Label>
    </div>
</asp:Content>
