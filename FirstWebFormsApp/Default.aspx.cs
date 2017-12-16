using FirstWebFormsApp.DBHelper;
using FirstWebFormsApp.Models;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FirstWebFormsApp
{
    public partial class _Default : Page
    {
        ADOBooksRepository bookRep = new ADOBooksRepository();
        ADOGenresRepository genresRep = new ADOGenresRepository();

        private const int PAGE_SIZE = 10;
        private static int pageIndex;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            ShowGenres();
            phPager.Controls.Clear();
            CreatePagingControl();

            if (!IsPostBack)
            {
                pageIndex = 0;
                ShowBooks(bookRep.GetBooks(pageIndex, PAGE_SIZE));
            }
        }

        private void CreatePagingControl()
        {
            int rowCount = bookRep.GetBooksCount();
            rowCount = rowCount % PAGE_SIZE == 0 ? (rowCount / PAGE_SIZE) : (rowCount / PAGE_SIZE) + 1;

            for (int i = 0; i < rowCount; i++)
            {
                Button btn = new Button();
                btn.Click += new EventHandler(btn_Click);
                btn.ID = "lnkPage" + (i + 1).ToString();
                btn.Text = (i + 1).ToString();
                phPager.Controls.Add(btn);
            }
        }

        void btn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            int currentPage = int.Parse(btn.Text);
            pageIndex = currentPage - 1;
            ShowBooks(bookRep.GetBooks(pageIndex, PAGE_SIZE));
        }

        private void ShowGenres()
        {
            ddlFindBookByGenre.DataSource = genresRep.GetGenres();
            ddlFindBookByGenre.DataTextField = "Title";
            ddlFindBookByGenre.DataValueField = "Id";
            ddlFindBookByGenre.DataBind();
            ddlFindBookByGenre.Items.Add(new ListItem("Все", "all", true));
            ddlFindBookByGenre.SelectedIndex = ddlFindBookByGenre.Items.Count - 1;
        }

        private void ShowError(Exception e)
        {
            lb_Error.Text = "Возникла непредвиденная ошибка!" + "<br><br>" +
                            "Текст ошибки: " + e.Message;
        }

        private void ShowBooks(List<Book> books)
        {
            try
            {             
                RepeaterBooks.DataSource = books;
                RepeaterBooks.DataBind();
            }
            catch(Exception e)
            {
                ShowError(e);
            }
        }

        protected void RepeaterEvent_ItemDataBound(Object Sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var book = (Book)e.Item.DataItem;
                if (book.TitleBook == null || book.Genre == null || book.Author == null || book.DateRealise == null)
                    throw new InvalidOperationException("Привязанные данные имеют пустые или некорректыне поля");
                ((HiddenField)e.Item.FindControl("IdBook")).Value = book.Id.ToString();
                ((Label)e.Item.FindControl("TitleBook")).Text = book.TitleBook;
                ((Label)e.Item.FindControl("GenreBook")).Text = book.Genre;
                ((Label)e.Item.FindControl("AuthorBook")).Text = book.Author;
                ((Label)e.Item.FindControl("DateRealiseBook")).Text = book.DateRealise.Year.ToString();
            }
        }

        protected void linkToEdit_Click(object sender, EventArgs e)
        {
            LinkButton link = (LinkButton)sender;
            HiddenField id = (HiddenField)link.Controls[0];
            Response.Cookies["idValue"].Value = id.Value;
            Response.Redirect("EditBook.aspx");
        }

        protected void btnGoToAddBook_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddBook.aspx");
        }

        static bool isReverseTitle = false;
        static bool isReverseGenre = false;
        static bool isReverseAuthor = false;
        static bool isReverseDate = false;

        protected void linkTitle_Click(object sender, EventArgs e)
        {
            ReverseRowInBookTable(ADOBooksRepository.TITLE_FIELD, isReverseTitle);
            isReverseTitle = !isReverseTitle;
        }

        protected void linkAuthor_Click(object sender, EventArgs e)
        {
            ReverseRowInBookTable(ADOBooksRepository.AUTHOR_FIELD, isReverseAuthor);
            isReverseAuthor = !isReverseAuthor;
        }

        protected void linkGenre_Click(object sender, EventArgs e)
        {
            ReverseRowInBookTable(ADOBooksRepository.GENRE_FIELD, isReverseGenre);
            isReverseGenre = !isReverseGenre;
        }

        protected void linkDate_Click(object sender, EventArgs e)
        {
            ReverseRowInBookTable(ADOBooksRepository.DATEREALISE_FIELD, isReverseDate);
            isReverseDate = !isReverseDate;
        }

        private void ReverseRowInBookTable(string orderField, bool isReverse)
        {
            ShowBooks(bookRep.GetSortedBooks(pageIndex,PAGE_SIZE, orderField, isReverse));   
        }
    }
}