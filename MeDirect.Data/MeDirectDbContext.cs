using MeDirect.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeDirect.Data
{
    public class MeDirectDbContext:DbContext
    {
        public DbSet<GameSetting> GameSettings { get; set; }
        public MeDirectDbContext(DbContextOptions<MeDirectDbContext> options):base(options)
        {}

        
    }
}
