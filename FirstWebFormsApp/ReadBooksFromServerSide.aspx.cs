using ModelsForAppLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections;
using System.Web.Script.Serialization;
using System.Threading.Tasks;

namespace FirstWebFormsApp
{
    public partial class ReadBooksFromServerSide : System.Web.UI.Page
    {
        const string URI_TO_WEB_API = "http://localhost:51746/";
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnShowBooks_Click(object sender, EventArgs e)
        {
            ShowBooks(GetBooksFromWebAPIAsync().Result);
        }

        private async Task<List<Book>> GetBooksFromWebAPIAsync()
        {
            List<Book> books = new List<Book>();
            try
            {
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(URI_TO_WEB_API)
                };

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync("api/books").Result;

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    //use JavaScriptSerializer from System.Web.Script.Serialization
                    JavaScriptSerializer JSserializer = new JavaScriptSerializer();
                    //deserialize to your class
                    books = JSserializer.Deserialize<List<Book>>(data);                
                }
                else
                {
                    throw new HttpException(response.StatusCode.ToString());
                }
            }
            catch (Exception exc)
            {
                ShowError(exc);
            }
            return books;
        }

        private void ShowBooks(List<Book> books)
        {
            try
            {
                if (books.Count > 0)
                {
                    RepeaterBooks.DataSource = books;
                    RepeaterBooks.DataBind();
                }
                else
                {
                    lbMsgNotFoundBooks.Visible = true;
                    RepeaterBooks.DataSource = new List<Book>();
                    RepeaterBooks.DataBind();
                }
            }
            catch (Exception e)
            {
                ShowError(e);
            }
        }

        protected void RepeaterEvent_ItemDataBound(Object Sender, RepeaterItemEventArgs e)
        {
            try
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
            catch (Exception exc)
            {
                ShowError(exc);
            }
        }
        private void ShowError(Exception e)
        {
            lb_Error.Text = "Возникла непредвиденная ошибка!" + "<br><br>" +
                            "Текст ошибки: " + e.Message;
        }
    }
}