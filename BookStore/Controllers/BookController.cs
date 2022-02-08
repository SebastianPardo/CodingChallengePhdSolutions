using BookStore.Data;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
  public class BookController : Controller
  {
    private readonly BookManager BookManager;
    private readonly BookByOrderManager BookByOrderManager;

    public BookController(DataModel context)
    {
      this.BookManager = new BookManager(context);
      this.BookByOrderManager = new BookByOrderManager(context);
    }
    public IActionResult Index()
    {
      if (Request.Cookies.TryGetValue("bookStoreSession", out string cookie))
      {
        var quantity = 0;
        foreach(var bookByOrder in BookByOrderManager.GetByOrder(Int16.Parse(cookie), true))
        {
          quantity = quantity + bookByOrder.Quatity;
        }
        ViewBag.cartQuanty = quantity;
      }
      else
      {
        ViewBag.cartQuanty = 0;
      }
      return View(BookManager.GetAll());
    }

    [HttpGet]
    public JsonResult GetAll()
    {
      return Json(BookManager.GetAll());
    }

    [HttpPut]
    public JsonResult Update(Book book)
    {
      return Json(BookManager.GetAll());
    }
  }
}
