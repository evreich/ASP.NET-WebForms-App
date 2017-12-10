using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModels
{
    public class Genre
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public virtual List<Book> Books { get; set; }

        public Genre()
        {
            Books = new List<Book>();
        }
    }
}
