using DataAccess.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Data
{
    internal class AppDbContext:IdentityDbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        { }
        public DbSet<Meal> Meal { get; set; }
        public DbSet<Savory> Savory { get; set; }
        public DbSet<Sweet> Sweet { get; set; }
    }
}
