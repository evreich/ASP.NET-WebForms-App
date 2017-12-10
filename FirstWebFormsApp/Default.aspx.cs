using DBLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FirstWebFormsApp
{
    public partial class _Default : Page
    {
        protected string ShowBooks()
        {
            BooksRepository rep = new BooksRepository(new BookContext());

            StringBuilder html = new StringBuilder();
            var books = rep.GetBooks();
            foreach (var book in books)
            {
                html.Append(String.Format("<tr class=\"book-row\"><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td></tr>",
                    book.Title,
                    book.Genre.Title,
                    book.Author.FirstName + " " + book.Author.LastName, 
                    book.DateRealise.Year
               ));
            }
            return html.ToString();
        }
    }
}