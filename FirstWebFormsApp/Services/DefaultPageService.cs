using DbSevicesLib;
using ModelsForAppLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace FirstWebFormsApp.Services
{
    class DefaultPageService
    {
        ADOBooksRepository bookRep;
        ADOGenresRepository genreRep;

        public int PageSize { get; }

        public DefaultPageService()
        {
            bookRep = new ADOBooksRepository();
            genreRep = new ADOGenresRepository();
            PageSize = 10;
        }

        public int GetCountPages(int countBooks)
        {
            int pageCount = countBooks % PageSize == 0 ? (countBooks / PageSize) : (countBooks / PageSize) + 1;
            return pageCount;
        }

        public int GetCountBooks(string titleToFind, string genreToFind)
        {
            return bookRep.GetBooksCount(titleToFind, genreToFind);
        }

        public List<Book> GetBooksByFilters(int pageIndex, string titleToFind, string genreToFind, bool isReverse, string lbName)
        {
            var books = bookRep.GetBooks(pageIndex, PageSize, titleToFind, genreToFind);
            
            if(isReverse)
            {
                switch (lbName)
                {
                    case "linkTitle":
                        return books.OrderByDescending(x => x.TitleBook).ToList();
                    case "linkAuthor":
                        return books.OrderByDescending(x => x.Author).ToList();
                    case "linkGenre":
                        return books.OrderByDescending(x => x.Genre).ToList();
                    case "linkDate":
                        return books.OrderByDescending(x => x.DateRealise).ToList();
                    default:
                        return books.OrderByDescending(x => x.Id).ToList();
                }
            }
            else
            {
                switch (lbName)
                {
                    case "linkTitle":
                        return books.OrderBy(x => x.TitleBook).ToList();
                    case "linkAuthor":
                        return books.OrderBy(x => x.Author).ToList();
                    case "linkGenre":
                        return books.OrderBy(x => x.Genre).ToList();
                    case "linkDate":
                        return books.OrderBy(x => x.DateRealise).ToList();
                    default:
                        return books.OrderBy(x => x.Id).ToList();
                }
            }
        }

        public DataTable GetGenres()
        {
            return genreRep.GetGenres();
        }
        
        public bool GetDirectionForSort(string predField, string currField, bool predDirection)
        {
            if (predField != currField)
            {
                return false;
            }
            else
            {
                return !predDirection;
            }
        }
        
    }
}