using FirstWebFormsApp.DBHelper;
using FirstWebFormsApp.Models;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Linq;
using System.Web.UI.WebControls;

namespace FirstWebFormsApp
{
    public partial class _Default : Page
    {
        ADOBooksRepository bookRep = new ADOBooksRepository();
        ADOGenresRepository genresRep = new ADOGenresRepository();

        private const int PAGE_SIZE = 10;

        private static int pageIndex;
        private static List<Book> booksOnPage;
        private static int countBooks;

        protected void Page_Load(object sender, EventArgs e)
        {
            lbMsgNotFoundBooks.Visible = false;
            if (!IsPostBack)
            {
                pageIndex = 0;
                countBooks = bookRep.GetBooksCount();
                booksOnPage = bookRep.GetBooks(pageIndex, PAGE_SIZE);
                ShowBooks(booksOnPage);
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            ShowGenres();
            phPager.Controls.Clear();
            CreatePagingControl();
        }

        private void CreatePagingControl()
        {
            var pageCount = countBooks % PAGE_SIZE == 0 ? (countBooks / PAGE_SIZE) : (countBooks / PAGE_SIZE) + 1;

            for (int i = 0; i < pageCount; i++)
            {
                Button btn = new Button();
                btn.Click += new EventHandler(btn_Click);
                btn.ID = "btnPage" + (i + 1).ToString();
                btn.Text = (i + 1).ToString();
                phPager.Controls.Add(btn);
            }
        }

        void btn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            int currentPage = int.Parse(btn.Text);
            pageIndex = currentPage - 1;
            booksOnPage = bookRep.GetBooks(pageIndex, PAGE_SIZE);
            ShowBooks(booksOnPage);
        }

        private void ShowGenres()
        {
            ddlFindBookByGenre.DataSource = genresRep.GetGenres();
            ddlFindBookByGenre.DataTextField = "Title";
            ddlFindBookByGenre.DataValueField = "Id";
            ddlFindBookByGenre.DataBind();
            ddlFindBookByGenre.Items.Add(new ListItem("Все", "all", true));
            ddlFindBookByGenre.SelectedIndex = ddlFindBookByGenre.Items.Count - 1;
        }

        private void ShowError(Exception e)
        {
            lb_Error.Text = "Возникла непредвиденная ошибка!" + "<br><br>" +
                            "Текст ошибки: " + e.Message;
        }

        private void ShowBooks(List<Book> books)
        {
            try
            {       
                if(books.Count > 0 )
                {
                    RepeaterBooks.DataSource = books;
                    RepeaterBooks.DataBind();
                }
                else
                {
                    lbMsgNotFoundBooks.Visible = true;
                    booksOnPage.RemoveAll(x => x.Id == x.Id);

                    RepeaterBooks.DataSource = booksOnPage;
                    RepeaterBooks.DataBind();
                }
            }
            catch(Exception e)
            {
                ShowError(e);
            }
        }

        protected void RepeaterEvent_ItemDataBound(Object Sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var book = (Book)e.Item.DataItem;
                if (book.TitleBook == null || book.Genre == null || book.Author == null || book.DateRealise == null)
                    throw new InvalidOperationException("Привязанные данные имеют пустые или некорректыне поля");
                ((HiddenField)e.Item.FindControl("IdBook")).Value = book.Id.ToString();
                ((Label)e.Item.FindControl("TitleBook")).Text = book.TitleBook;
                ((Label)e.Item.FindControl("GenreBook")).Text = book.Genre;
                ((Label)e.Item.FindControl("AuthorBook")).Text = book.Author;
                ((Label)e.Item.FindControl("DateRealiseBook")).Text = book.DateRealise.Year.ToString();
            }
            
        }

        protected void linkToEdit_Click(object sender, EventArgs e)
        {
            LinkButton link = (LinkButton)sender;
            HiddenField id = (HiddenField)link.Controls[0];
            Response.Cookies["idValue"].Value = id.Value;
            Response.Redirect("EditBook.aspx");
        }

        protected void btnGoToAddBook_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddBook.aspx");
        }

        static bool isReverseTitle = false;
        static bool isReverseGenre = false;
        static bool isReverseAuthor = false;
        static bool isReverseDate = false;

        protected void linkTitle_Click(object sender, EventArgs e)
        {
            if(!isReverseTitle)
            {
                booksOnPage = booksOnPage.OrderBy(x => x.TitleBook).ToList();
            }
            else
            {
                booksOnPage = booksOnPage.OrderByDescending(x => x.TitleBook).ToList();
            }
            ShowBooks(booksOnPage);
            isReverseTitle = !isReverseTitle;
        }

        protected void linkAuthor_Click(object sender, EventArgs e)
        {
            if (!isReverseAuthor)
            {
                booksOnPage = booksOnPage.OrderBy(x => x.Author).ToList();
            }
            else
            {
                booksOnPage = booksOnPage.OrderByDescending(x => x.Author).ToList();
            }
            ShowBooks(booksOnPage);

            isReverseAuthor = !isReverseAuthor;
        }

        protected void linkGenre_Click(object sender, EventArgs e)
        {
            if (!isReverseGenre)
            {
                booksOnPage = booksOnPage.OrderBy(x => x.Genre).ToList();
            }
            else
            {
                booksOnPage = booksOnPage.OrderByDescending(x => x.Genre).ToList();
            }
            ShowBooks(booksOnPage);
            isReverseGenre = !isReverseGenre;
        }

        protected void linkDate_Click(object sender, EventArgs e)
        {
            if (!isReverseDate)
            {
                booksOnPage = booksOnPage.OrderBy(x => x.DateRealise).ToList();
            }
            else
            {
                booksOnPage = booksOnPage.OrderByDescending(x => x.DateRealise).ToList();
            }
            ShowBooks(booksOnPage);
            isReverseDate = !isReverseDate;
        }
        
        protected void btnFindBook_Click(object sender, EventArgs e)
        {
            var title = tbFindBookByTitle.Text;
            var genre = ddlFindBookByGenre.SelectedItem.Text;

            pageIndex = 0;
            booksOnPage = bookRep.GetBooks(pageIndex, PAGE_SIZE, title, genre);
            ShowBooks(booksOnPage);
            countBooks = bookRep.GetBooksCount(title, genre);          
        }
    }
}