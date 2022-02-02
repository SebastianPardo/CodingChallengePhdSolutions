using Microsoft.EntityFrameworkCore;
using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Data
{
  public class DataModel : DbContext
  {
    public DataModel(DbContextOptions<DataModel> options) : base(options)
    {
    }
    public DbSet<Book> Book { get; set; }
  }
}
