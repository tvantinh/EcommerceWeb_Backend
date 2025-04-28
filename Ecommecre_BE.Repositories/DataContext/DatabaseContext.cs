using Ecommerce_BE.Contract.Repositories.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Ecommecre_BE.Repositories.DataContext
{
    public class DatabaseContext: DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }
        public DatabaseContext()
        {
        }
        public DbSet<User> users { get; set; }
    }
}
