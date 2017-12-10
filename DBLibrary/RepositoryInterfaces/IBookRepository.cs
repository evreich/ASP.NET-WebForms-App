﻿using DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLibrary.RepositoryInterfaces
{
    interface IBookRepository : IDisposable
    {
        List<Book> GetBooks();
    }
}
