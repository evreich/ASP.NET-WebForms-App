using DbSevicesLib;
using ModelsForAppLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BooksWebAPI.Controllers
{
    public class BooksController : ApiController
    {
        ADOBooksRepository booksRep = new ADOBooksRepository();
        // GET api/books
        [HttpGet]
        public IEnumerable<Book> Get()
        {
            return booksRep.GetAllBooks();
        }
    }
}
