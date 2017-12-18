using DbSevicesLib;
using ModelsForAppLib;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Linq;
using System.Web.UI.WebControls;
using FirstWebFormsApp.Services;

namespace FirstWebFormsApp
{
    public partial class _Default : Page
    {
        DefaultPageService service;
             
        protected void Page_Init(object sender, EventArgs e)
        {
            service = new DefaultPageService();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            lbMsgNotFoundBooks.Visible = false;
            
            CreatePagingControl(service.GetCountPages(service.GetCountBooks(hfTitleOfLastFind.Value, hfGenreOfLastFind.Value)));
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            ShowBooks(service.GetBooksByFilters(int.Parse(hfPageIndex.Value),
                                            hfTitleOfLastFind.Value,
                                            hfGenreOfLastFind.Value,
                                            bool.Parse(hfIsReverseSort.Value),
                                            hfPredFieldForSort.Value));

            ShowGenres();
        }

        protected void btnFindBook_Click(object sender, EventArgs e)
        {
            hfTitleOfLastFind.Value = tbFindBookByTitle.Text;
            hfGenreOfLastFind.Value = ddlFindBookByGenre.SelectedItem.Text;
            hfPageIndex.Value = "0";
            phPager.Controls.Clear();
            CreatePagingControl(service.GetCountPages(service.GetCountBooks(hfTitleOfLastFind.Value, hfGenreOfLastFind.Value)));
        }

        private void CreatePagingControl(int pageCount)
        {
            try
            {
                for (int i = 0; i < pageCount; i++)
                {
                    Button btn = new Button();
                    btn.Click += new EventHandler(btnPage_Click);
                    btn.ID = "btnPage" + (i + 1).ToString();
                    btn.Text = (i + 1).ToString();
                    phPager.Controls.Add(btn);
                }
            }
            catch (Exception exc)
            {
                ShowError(exc);
            }
        }

        protected void btnPage_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = sender as Button;
                int currentPage = int.Parse(btn.Text);
                hfPageIndex.Value = (currentPage - 1).ToString();
            }
            catch (Exception exc)
            {
                ShowError(exc);
            }
        }

        private void ShowGenres()
        {
            ddlFindBookByGenre.DataSource = service.GetGenres();
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
                    RepeaterBooks.DataSource = new List<Book>();
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

        protected void linkToEdit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton link = (LinkButton)sender;
                HiddenField id = (HiddenField)link.Controls[0];
                Response.Cookies["idValue"].Value = id.Value;
                Response.Redirect("EditBook.aspx");
            }
            catch (Exception exc)
            {
                ShowError(exc);
            }
        }

        protected void btnGoToAddBook_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddBook.aspx");
        }

             
        protected void linkColumnName_Click(object sender, EventArgs e)
        {
            try
            {
                var linkButton = (LinkButton)sender;
                var lbName = linkButton.ID;

                hfIsReverseSort.Value = service.GetDirectionForSort(hfPredFieldForSort.Value, 
                                                                    lbName, 
                                                                    bool.Parse(hfIsReverseSort.Value)
                                                                    ).ToString();
                hfPredFieldForSort.Value = lbName;
            }
            catch (Exception exc)
            {
                ShowError(exc);
            }
        }
    }
}