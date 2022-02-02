using BookStore.Data;
using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore
{  
  public class OrderManager
  {
    private readonly DataModel context;

    public OrderManager(DataModel context)
    {
      this.context = context;
    }

    public Order add(Order order)
    {
      order = context.Order.Add(order).Entity;
      context.SaveChanges();
      return order;
        
    }

    public Order GetByNumber(int number)
    {
      return context.Order.Single(order=> order.Number == number );
    }

    public Order Update(Order order)
    {
      order = context.Order.Update(order).Entity;
      context.SaveChanges();
      return order;
    }
  }
}
