using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;
using BookStore.Data;

namespace ViewComponentSample.ViewComponents
{
  public class HistoricOrdersViewComponent : ViewComponent
  {
    private readonly DataModel context;

    public HistoricOrdersViewComponent(DataModel context)
    {
      this.context = context;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
      var items = await GetOrdersAsync();
      return View(items);
    }
    private Task<List<BookByOrder>> GetOrdersAsync()
    {
      return context.BookByOrder.Join(context.Book,
        bookByOrder => bookByOrder.IdBook,
        book => book.Id,
        (bookByOrder, book) => new BookByOrder
        {
          IdOrder = bookByOrder.IdOrder,
          Order = null,
          Quatity = bookByOrder.Quatity,
          IsPreorder = bookByOrder.IsPreorder,
          Book = book
        }
        ).Join(context.Order,
        bookByOrder => bookByOrder.IdOrder,
        order => order.Id,
        (bookByOrder, order) => new BookByOrder
        {
          Quatity = bookByOrder.Quatity,
          IsPreorder = bookByOrder.IsPreorder,
          Book = bookByOrder.Book,
          Order = order
        }).Where(x => x.IsPreorder == false).ToListAsync();
    }
  }
}