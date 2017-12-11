using FirstWebFormsApp.DB;
using FirstWebFormsApp.Models;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FirstWebFormsApp
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowBooks();
            }
        }

        private void ShowError(Exception e)
        {
            lb_Error.Text = "Возникла непредвиденная ошибка!" + "<br><br>" +
                            "Текст ошибки: " + e.Message;
        }

        private void ShowBooks()
        {
            try
            {
                ADOBooksRepository rep = new ADOBooksRepository();
                RepeaterBooks.DataSource = rep.GetBooks();
                RepeaterBooks.DataBind();
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
                if (book.Title == null || book.Genre == null || book.Author == null || book.DateRealise < 0)
                    throw new InvalidOperationException("Привязанные данные имеют пустые или некорректыне поля");
                ((Label)e.Item.FindControl("TitleBook")).Text = book.Title;
                ((Label)e.Item.FindControl("GenreBook")).Text = book.Genre;
                ((Label)e.Item.FindControl("AuthorBook")).Text = book.Author;
                ((Label)e.Item.FindControl("DateRealiseBook")).Text = book.DateRealise.ToString();
            }
        }
    }
}