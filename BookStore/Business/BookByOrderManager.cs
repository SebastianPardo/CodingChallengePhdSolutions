using BookStore.Data;
using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore
{
  public class BookByOrderManager
  {
    private readonly DataModel context;

    public BookByOrderManager(DataModel context)
    {
      this.context = context;
    }

    public List<BookByOrder> GetByOrder(int OrderId)
    {
      return context.BookByOrder.Join(context.Book, bookByOrder => bookByOrder.IdBook, book => book.Id, (bookByOrder, book) =>
      new BookByOrder
      {
        Id = bookByOrder.Id,
        IdOrder = bookByOrder.IdOrder,
        Book = book,
        IsPreorder = bookByOrder.IsPreorder,
        Quatity = bookByOrder.Quatity
      }).Where(relation => relation.IdOrder == OrderId).ToList();
    }

    public BookByOrder add(BookByOrder bookByOrder)
    {
      bookByOrder = context.BookByOrder.Add(bookByOrder).Entity;
      context.SaveChanges();
      return bookByOrder;
    }

    public BookByOrder Update(BookByOrder bookByOrder)
    {
      bookByOrder = context.BookByOrder.Update(bookByOrder).Entity;
      context.SaveChanges();
      return bookByOrder;
    }
  }
}
