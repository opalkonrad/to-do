using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace to_do.Models
{
    public class ToDo
    {
        public int Id { get; set; }
        public bool IsDone { get; set; }

        [StringLength(100), Required]
        public string Description { get; set; }

        public virtual IdentityUser User { get; set; }
    }
}
