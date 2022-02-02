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

    public BookController(DataModel context)
    {
      this.BookManager = new BookManager(context);
    }
    public IActionResult Index()
    {
      return View();
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
