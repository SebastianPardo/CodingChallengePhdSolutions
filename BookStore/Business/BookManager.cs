using BookStore.Data;
using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore
{  
  public class BookManager
  {
    private readonly DataModel context;

    public BookManager(DataModel context)
    {
      this.context = context;
    }

    public List<Book> GetAll()
    {
      return context.Book.Join(
        context.Author,
        book => book.AuthorId,
        author => author.Id,
        (book, author) => new Book
        {
          Id = book.Id,
          Author = author,
          CoverImage = book.CoverImage,
          Description = book.Description,
          Price = book.Price,
          Quantity = book.Quantity,
          ReleaseDate = book.ReleaseDate,
          Tittle = book.Tittle
        }).ToList();
    }

    public Book Update(Book book)
    {
      return context.Book.Update(book).Entity;
    }
  }
}
