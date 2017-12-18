using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsForAppLib
{
    public class Genre
    {
        public string Title { get; }
        public int Id { get; }

        public Genre(int _id, string _title)
        {
            Id = _id;
            Title = _title;
        }
    }
}
