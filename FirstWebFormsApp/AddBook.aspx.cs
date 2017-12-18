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
    public partial class AddBook : Page
    {
        ADOGenresRepository genresRep = new ADOGenresRepository();
        ADOAuthorsRepository authorsRep = new ADOAuthorsRepository();
        ADOBooksRepository bookRep = new ADOBooksRepository();

         protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SetGenres();
                SetAuthors();

                if (IsPostBack)
                {
                    AddBookInDB();
                }
            }
            catch(Exception exc)
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

        private void AddBookInDB()
        {
               
            Book book = new Book
            {
                TitleBook = tbTitleBook.Text,
                AuthorId = Int32.Parse(ddlAuthor.SelectedValue),
                GenreId = Int32.Parse(ddlGenre.SelectedValue),
                DateRealise = DateTime.Parse(tbDateRealise.Text)
            };
            bookRep.AddBook(book);
            Response.Redirect("Default.aspx");
        }

        private void ShowError(Exception e)
        {
            lb_Error.Text = "Возникла непредвиденная ошибка!" + "<br><br>" +
                            "Текст ошибки: " + e.Message;
        }
    }
}