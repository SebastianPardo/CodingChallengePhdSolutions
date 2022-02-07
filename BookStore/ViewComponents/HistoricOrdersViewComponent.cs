using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;
using BookStore.Data;
using BookStore;

namespace ViewComponentSample.ViewComponents
{
  public class HistoricOrdersViewComponent : ViewComponent
  {
    private readonly BookByOrderManager BookByOrderManager;

    public HistoricOrdersViewComponent(DataModel context)
    {
      this.BookByOrderManager = new BookByOrderManager(context);
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
      var items = await BookByOrderManager.GetHistoric();
      return View(items);
    }    
  }
}