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
        // The constructor takes connection string from
        // config file.
        public ApplicationDbContext() :
            base(new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build()
                .GetConnectionString("SwipesDatabase")).Options)
        { }

        // The constructor takes connection string from
        // the parameter.
        public ApplicationDbContext(string connectionString) :
            base(new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(connectionString).Options)
        { }

        public virtual DbSet<Swipe> Swipes { get; set; }
    }
    #endregion
    #region IDesignTimeDbContextFactory
    // Implemented IDesignTimeDbContextFactory interface to create migrations and run them
    public class MyDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        /// <summary>
        /// The method returns new ApplicationDbContext object.
        /// </summary>
        ApplicationDbContext IDesignTimeDbContextFactory<ApplicationDbContext>.CreateDbContext(string[] args) =>
            new ApplicationDbContext();
    }
    #endregion
}
