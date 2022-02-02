using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
  [Table("Order")]
  public class Order
  {
    [Key]
    public int Id { get; set; }
    public int Number { get; set; }

    public DateTime DateOrder { get; set; }

    public virtual ICollection<BookByOrder> BookByOrder { get; set; }

  }
}
