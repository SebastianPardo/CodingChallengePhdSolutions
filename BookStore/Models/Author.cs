using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
  [Table("Author")]
  public class Author
  {
    [Key]
    public int Id { get; set; }
    public int Name { get; set; }

    public DateTime BirthDate { get; set; }

    public virtual ICollection<Book> Book { get; set; }

  }
}
