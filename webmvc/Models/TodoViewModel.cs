using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Data.Models;

namespace webmvc.Models
{
    public class TodoViewModel
    {
        public List<Todos> Todos { get; set; }
    }
}
