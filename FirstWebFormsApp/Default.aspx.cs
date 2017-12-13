using FirstWebFormsApp.DBHelper;
using FirstWebFormsApp.Models;
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FirstWebFormsApp
{
    public partial class _Default : Page
    {
        ADOBooksRepository bookRep = new ADOBooksRepository();

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
                RepeaterBooks.DataSource = bookRep.GetBooks();
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
    }
}