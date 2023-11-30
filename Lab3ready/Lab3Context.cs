using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    public class Lab3Context : DbContext
    {
        private object options;

        public Lab3Context() { }

        public Lab3Context(object options)
        {
            this.options = options;
        }

        public DbSet<Book> Books { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(connectionString: "Data Source = lab3.db");
        }
    }
}
