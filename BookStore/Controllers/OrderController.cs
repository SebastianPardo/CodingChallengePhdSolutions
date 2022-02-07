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
      
      var oldBookByOrder = BookByOrderManager.GetCompleteByOrderBook(order.Number, bookByOrder.IdBook);
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

    [HttpPut]
    public JsonResult Update (BookByOrder bookByOrder)
    {
      var oldBookByOrder = BookByOrderManager.GetCompleteByOrderBook(bookByOrder.IdOrder, bookByOrder.IdBook);
      oldBookByOrder.Quatity = bookByOrder.Quatity;
      return Json(BookByOrderManager.Update(oldBookByOrder)!=null);
    }

    [HttpPost]
    public JsonResult CheckOut(Order order)
    {
      order = OrderManager.GetByNumber(order.Number);
      order.BookByOrder = BookByOrderManager.GetByOrder(order.Id, true);
      foreach(var item in order.BookByOrder)
      {
        if (item.Quatity < item.Book.Quantity)
        {
          item.IsPreorder = false;
          BookByOrderManager.Update(item);
          item.Book.Quantity = item.Book.Quantity - item.Quatity;
          BookManager.Update(item.Book);
        }
        else
        {
          item.Quatity = 0;
        }
      }

      var jsonString = JsonConvert.SerializeObject(order, Formatting.None, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
      return Json(jsonString);
    }

    [HttpGet]
    public JsonResult GetByNumber(int number)
    {
      var order = OrderManager.GetByNumber(number);
      order.BookByOrder = BookByOrderManager.GetByOrder(order.Id, true);
      return Json(order);
    }

    public IActionResult HistoricOrders()
    {
      return View();
    }
  }
}
