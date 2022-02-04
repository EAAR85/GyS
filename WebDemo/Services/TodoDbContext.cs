using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDemo.Entity;

namespace WebDemo.Services
{
    public class TodoDBContext : DbContext
    {
        public TodoDBContext(DbContextOptions options) : base(options) { }

        public DbSet<Cliente> ToDoItems { get; set; }
    }

}
