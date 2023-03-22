using CalculationCalorieasApp.Medels.Entitys;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculationCalorieasApp.Medels
{
    public class AppDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            string fullPath = Path.GetFullPath("..\\..\\..\\DataBase.sqlite");
            optionsBuilder.UseSqlite($"Filename='{fullPath}'");
        }
    }
}
