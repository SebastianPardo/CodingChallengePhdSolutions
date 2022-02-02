using BookStore.Data;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
  public class OrderController : Controller
  {
    private readonly OrderManager OrderManager;
    private readonly BookByOrderManager BookByOrderManager;
    private readonly BookManager BookManager;

    public OrderController(DataModel context)
    {
      this.OrderManager = new OrderManager(context);
      this.BookByOrderManager = new BookByOrderManager(context);
      this.BookManager = new BookManager(context);
    }

    public IActionResult Index()
    {
      return View();
    }

    [HttpPost]
    public JsonResult Create(BookByOrder bookByOrder)
    {
      var order = bookByOrder.Order;
      if (order.Number == 0)
      {
        order.DateOrder = DateTime.Now;
        order = OrderManager.add(bookByOrder.Order);
        order.Number = order.Id;
        OrderManager.Update(order);
        bookByOrder.IdOrder = order.Id;
      }
      bookByOrder.Order = null;
      
      var oldBookByOrder = BookByOrderManager.GetByOrderAndBook(order.Number, bookByOrder.IdBook);
      if(oldBookByOrder == null)
      {
        bookByOrder.Quatity = 1;
        bookByOrder.IdOrder = order.Number;
        bookByOrder = BookByOrderManager.add(bookByOrder);
      }
      else
      {
        oldBookByOrder.Quatity++;
        bookByOrder = BookByOrderManager.Update(oldBookByOrder);
      }
      var jsonString = JsonConvert.SerializeObject(order, Formatting.None, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
      return Json(jsonString);
    }
    public JsonResult GetByNumber(int number)
    {
      var order = OrderManager.GetByNumber(number);
      order.BookByOrder = BookByOrderManager.GetByOrder(order.Id);
      return Json(order);
    }
  }
}
