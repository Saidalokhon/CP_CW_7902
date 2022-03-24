using CP_CW_7902_DAL.DBO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CP_CW_7902_DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() :
            base(new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build()
                .GetConnectionString("SwipesDatabase")).Options) { }

        public ApplicationDbContext(string connectionString) :
            base(new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(connectionString).Options) { }

        public virtual DbSet<Swipe> Swipes { get; set; }
    }

    public class MyDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        ApplicationDbContext IDesignTimeDbContextFactory<ApplicationDbContext>.CreateDbContext(string[] args) =>
            new ApplicationDbContext();
    }
}
