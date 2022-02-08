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
    public void Create(BookByOrder bookByOrder)
    {
      int orderId = 0;
      Order newOrder = new Order
      {
        Id = 0,
        DateOrder = DateTime.Now,
        Number = 0
      };
      if (!Request.Cookies.TryGetValue("bookStoreSession", out string cookie))
      {
        newOrder = OrderManager.add(newOrder);
        newOrder.Number = newOrder.Id;
        OrderManager.Update(newOrder);
        bookByOrder.IdOrder = newOrder.Id;
        orderId = newOrder.Id;
      }
      else
      {
        orderId = Int16.Parse(cookie);
      }
           
      var oldBookByOrder = BookByOrderManager.GetCompleteByOrderBook(orderId, bookByOrder.IdBook);
      if(oldBookByOrder == null)
      {
        bookByOrder.Quatity = 1;
        bookByOrder.IdOrder = orderId;
        bookByOrder.IsPreorder = true;
        bookByOrder = BookByOrderManager.add(bookByOrder);
      }
      else
      {
        oldBookByOrder.Quatity++;
        bookByOrder = BookByOrderManager.Update(oldBookByOrder);
      }
      Response.Cookies.Append("bookStoreSession", bookByOrder.IdOrder.ToString());
      Response.Redirect("./../");
    }

    [HttpPut]
    public JsonResult Update (BookByOrder bookByOrder)
    {
      var oldBookByOrder = BookByOrderManager.GetCompleteByOrderBook(bookByOrder.IdOrder, bookByOrder.IdBook);
      oldBookByOrder.Quatity = bookByOrder.Quatity;
      return Json(BookByOrderManager.Update(oldBookByOrder)!=null);
    }

    [HttpPost]
    public void CheckOut(Order order)
    {
      if (Request.Cookies.TryGetValue("bookStoreSession", out string cookie))
      {
        order = OrderManager.GetByNumber(Int16.Parse(cookie));
        order.BookByOrder = BookByOrderManager.GetByOrder(order.Id, true);
        foreach (var item in order.BookByOrder)
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
      }      
      Response.Cookies.Delete("bookStoreSession");
      Response.Redirect("./../");
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
