using DbSevicesLib;
using ModelsForAppLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FirstWebFormsApp
{
    public partial class EditBook : Page
    {
        ADOGenresRepository genresRep = new ADOGenresRepository();
        ADOAuthorsRepository authorsRep = new ADOAuthorsRepository();
        ADOBooksRepository bookRep = new ADOBooksRepository();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (IsPostBack)
                {
                    EditBookInDB();
                }

                SetGenres();
                SetAuthors();

                if (!IsPostBack)
                {
                    var id = Int32.Parse(Request.Cookies["idValue"].Value);
                    ShowBook(id); 
                }

            }
            catch (Exception exc)
            {
                ShowError(exc);
            }

        }

        public void SetGenres()
        {
            ddlGenre.DataSource = genresRep.GetGenres();
            ddlGenre.DataTextField = "Title";
            ddlGenre.DataValueField = "Id";
            ddlGenre.DataBind();
        }

        public void SetAuthors()
        {
            ddlAuthor.DataSource = authorsRep.GetAuthors();
            ddlAuthor.DataTextField = "Name";
            ddlAuthor.DataValueField = "Id";
            ddlAuthor.DataBind();
        }

        private void EditBookInDB()
        {
            Book book = new Book
            {
                Id = Int32.Parse(hfIdBook.Value),
                TitleBook = tbTitleBook.Text,
                AuthorId = Int32.Parse(ddlAuthor.SelectedValue),
                GenreId = Int32.Parse(ddlGenre.SelectedValue),
                DateRealise = DateTime.Parse(tbDateRealise.Text)
            };
            bookRep.EditBook(book);
            Response.Redirect("Default.aspx");
        }

        private void ShowBook(int idBook)
        {
            Book book = bookRep.GetBook(idBook);
            hfIdBook.Value = book.Id.ToString();
            tbTitleBook.Text = book.TitleBook;
            tbDateRealise.Text = book.DateRealise.ToString(String.Format("yyyy-MM-dd"));
            ddlGenre.SelectedValue = book.GenreId.ToString();
            ddlAuthor.SelectedValue = book.AuthorId.ToString();
        }

        
        private void ShowError(Exception e)
        {
            lb_Error.Text = "Возникла непредвиденная ошибка!" + "<br><br>" +
                            "Текст ошибки: " + e.Message;
        }
    }
}