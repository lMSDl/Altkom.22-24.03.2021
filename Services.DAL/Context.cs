using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DAL
{
    public class Context : DbContext
    {
        public DbSet<Order> Orders { get; set; }
    }
}
