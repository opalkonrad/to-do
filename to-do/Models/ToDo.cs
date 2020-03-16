using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace to_do.Models
{
    public class ToDo
    {
        public int Id { get; set; }
        public bool IsDone { get; set; }
        public string Description { get; set; }

        public virtual IdentityUser User { get; set; }
    }
}
