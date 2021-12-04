using MeDirect.Core.Models;
using MeDirect.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MeDirect.Tests
{
    public class MeDirectTestContext:MeDirectDbContext
    {
        public MeDirectTestContext(DbContextOptions<MeDirectDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            seedData<GameSetting>(modelBuilder, "../../../data/gamesetting.json");
        }

        private void seedData<T>(ModelBuilder modelBuilder, string file) where T : class
        {
            using (StreamReader reader = new StreamReader(file))
            {
                var json = reader.ReadToEnd();
                var data = JsonConvert.DeserializeObject<T[]>(json);
                modelBuilder.Entity<T>().HasData(data);
            }

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("exampleDatabase");
        }
    }
}
