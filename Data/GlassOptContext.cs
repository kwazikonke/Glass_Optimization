using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GlassOpt.Models;

namespace GlassOpt.Data
{
    public class GlassOptContext : DbContext
    {
        public GlassOptContext (DbContextOptions<GlassOptContext> options)
            : base(options)
        {
        }
        public DbSet<GlassOpt.Models.Panel> Panel { get; set; } = default!;
        public DbSet<GlassOpt.Models.StockSheet> StockSheet { get; set; } = default!;
    }
}
