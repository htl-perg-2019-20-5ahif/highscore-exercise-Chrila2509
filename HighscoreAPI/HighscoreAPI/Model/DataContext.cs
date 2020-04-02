using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HighScoreAPI.Model
{
    public class DataContext : DbContext
    {
        public DbSet<HighScore> HighScore { get; set; }

        public DataContext(DbContextOptions<DataContext> options)
        : base(options)
        { }

    }
}
