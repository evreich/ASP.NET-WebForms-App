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
    public partial class EditBook : Page
    {
        ADOGenresRepository genresRep = new ADOGenresRepository();
        ADOAuthorsRepository authorsRep = new ADOAuthorsRepository();
        ADOBooksRepository bookRep = new ADOBooksRepository();

        protected int IdBook { get; set; }
        protected int AuthorId { get; set; }
        protected int GenreId { get; set; }
        protected string TitleBook { get; set; }
        protected string DateRealise { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    var id = Int32.Parse(Request.Cookies["idValue"].Value);
                    ShowBook(id); 
                }
                if (IsPostBack)
                {
                    EditBookInDB();
                }
            }
            catch (Exception exc)
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

        private void EditBookInDB()
        {
            Book book = new Book { Id = (int)Session["idBook"] };
            IValueProvider provider = new FormValueProvider(ModelBindingExecutionContext);
            if (TryUpdateModel(book, provider))
            {
                bookRep.EditBook(book);
                Response.Redirect("Default.aspx");
            }

        }

        private void ShowBook(int idBook)
        {
            Book book = bookRep.GetBook(idBook);
            IdBook = book.Id;
            Session["idBook"] = IdBook;
            TitleBook = book.TitleBook;
            DateRealise = book.DateRealise.ToString(String.Format("yyyy-MM-dd"));
            GenreId = book.GenreId;
            AuthorId = book.AuthorId;
        }

        
        private void ShowError(Exception e)
        {
            lb_Error.Text = "Возникла непредвиденная ошибка!" + "<br><br>" +
                            "Текст ошибки: " + e.Message;
        }
    }
}