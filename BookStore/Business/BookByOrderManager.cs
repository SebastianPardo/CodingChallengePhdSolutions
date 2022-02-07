using BookStore.Data;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;
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

    public List<BookByOrder> GetByOrder(int OrderId, bool IsPreOrder)
    {
      return context.BookByOrder.Join(context.Book, bookByOrder => bookByOrder.IdBook, book => book.Id, (bookByOrder, book) =>
      new BookByOrder
      {
        Id = bookByOrder.Id,
        IdOrder = bookByOrder.IdOrder,
        Book = book,
        IsPreorder = bookByOrder.IsPreorder,
        Quatity = bookByOrder.Quatity
      }).Where(relation => relation.IdOrder == OrderId && relation.IsPreorder == IsPreOrder).ToList();
    }

    public BookByOrder GetCompleteByOrderBook(int OrderId, int bookId)
    {
      try
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
        }).Where(relation => relation.IdOrder == OrderId && relation.IdBook == bookId).FirstOrDefault();
      }
      catch (Exception e)
      {
        return null;
      }
    }

    public Task<List<BookByOrder>> GetHistoric()
    {
      try
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
      catch (Exception e)
      {
        return null;
      }
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
