﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
  [Table("BookByOrder")]
  public class BookByOrder
  {
    [Key]
    public int Id { get; set; }

    [Required]
    [ForeignKey("Order")]
    public int IdOrder { get; set; }

    [Required]
    [ForeignKey("Book")]
    public int IdBook { get; set; }
    public int Quatity { get; set; }
    public bool IsPreorder { get; set; }

    public virtual Book Book { get; set; }
    
    public virtual Order Order { get; set; }
  }
}
