using CP_CW_7902_DAL.DBO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CP_CW_7902_DAL.Repositories
{
    public class SwipesRepository : IRepository<Swipe>
    {
        private string ConnectionString;
        public SwipesRepository(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public List<Swipe> GetAll()
        {
            using (ApplicationDbContext context = new ApplicationDbContext(ConnectionString))
            {
                return context.Swipes.ToList();
            }
        }

        public void Insert(List<Swipe> entities)
        {
            using (ApplicationDbContext context = new ApplicationDbContext(ConnectionString))
            {
                context.Swipes.AddRange(entities);
                context.SaveChanges();
            }
            
        }

        public void Truncate()
        {
            using (ApplicationDbContext context = new ApplicationDbContext(ConnectionString))
            {
                foreach(Swipe swipe in context.Swipes.ToList()) context.Remove(swipe) ;
                context.SaveChanges();
            }
        }
    }
}
