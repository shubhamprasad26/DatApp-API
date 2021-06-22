using DatApp_API.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatApp_API.Data
{
    public class Datacontext : DbContext
    {
        public Datacontext( DbContextOptions<Datacontext> options) : base(options) {   }
        public DbSet<User> user { get; set; }

    }
}
