using FirstWebFormsApp.DBHelper;
using FirstWebFormsApp.Models;
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

        public List<Genre> SetGenres()
        {
            var genres = new List<Genre>();
            try
            {
                genres = new List<Genre>(genresRep.GetGenres());
            }
            catch (Exception exc)
            {
                ShowError(exc);
            }
            return genres;
        }

        public List<Author> SetAuthors()
        {
            var authors = new List<Author>();
            try
            {
                authors = new List<Author>(authorsRep.GetAuthors());
            }
            catch (Exception exc)
            {
                ShowError(exc);
            }
            return authors;

        }

        private void AddBookInDB()
        {
            Book book = new Book();
            IValueProvider provider = new FormValueProvider(ModelBindingExecutionContext);
            if(TryUpdateModel(book, provider))
            {
                bookRep.AddBook(book);
                Response.Redirect("Default.aspx");
            }

        }

        private void ShowError(Exception e)
        {
            lb_Error.Text = "Возникла непредвиденная ошибка!" + "<br><br>" +
                            "Текст ошибки: " + e.Message;
        }
    }
}