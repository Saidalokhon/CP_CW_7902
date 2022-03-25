using CP_CW_7902_DAL.DBO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace CP_CW_7902_DAL
{
    #region DbContext
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() :
            base(new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build()
                .GetConnectionString("SwipesDatabase")).Options)
        { }

        public ApplicationDbContext(string connectionString) :
            base(new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(connectionString).Options)
        { }

        public virtual DbSet<Swipe> Swipes { get; set; }
    }
    #endregion
    #region IDesignTimeDbContextFactory
    public class MyDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        ApplicationDbContext IDesignTimeDbContextFactory<ApplicationDbContext>.CreateDbContext(string[] args) =>
            new ApplicationDbContext();
    }
    #endregion
}
