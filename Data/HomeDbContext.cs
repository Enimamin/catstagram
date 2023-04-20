using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CATSTAGRAM.Models;

namespace CATSTAGRAM.Data
{
    public class HomeDbContext : DbContext
    {
        public HomeDbContext (DbContextOptions<HomeDbContext> options)
            : base(options)
        {
        }

        public DbSet<CATSTAGRAM.Models.CatPhoto> CatPhoto { get; set; } = default!;

        public static implicit operator HomeDbContext(ApplicationDbContext v)
        {
            throw new NotImplementedException();
        }
    }
}
