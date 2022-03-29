using CP_CW_7902_DAL.DBO;
using System.Collections.Generic;
using System.Linq;

namespace CP_CW_7902_DAL.Repositories
{
    public class SwipesRepository : IRepository<Swipe>
    {
        private string ConnectionString;
        public SwipesRepository(string connectionString)
        {
            ConnectionString = connectionString;
        }

        #region GetAll
        /// <summary>
        /// The method gets all records from the table.
        /// </summary>
        public List<Swipe> GetAll()
        {
            using (ApplicationDbContext context = new ApplicationDbContext(ConnectionString))
            {
                return context.Swipes.ToList();
            }
        }
        #endregion
        #region Insert
        /// <summary>
        /// The method inserts a new list of swipes into the table.
        /// </summary>
        /// <param name="entities">The list of swipes to be inserted</param>
        public void Insert(List<Swipe> entities)
        {
            using (ApplicationDbContext context = new ApplicationDbContext(ConnectionString))
            {
                context.Swipes.AddRange(entities);
                context.SaveChanges();
            }

        }
        #endregion
        #region Truncate
        /// <summary>
        /// The method truncates (cleans) the table.
        /// </summary>
        public void Truncate()
        {
            using (ApplicationDbContext context = new ApplicationDbContext(ConnectionString))
            {
                foreach (Swipe swipe in context.Swipes.ToList()) context.Remove(swipe);
                context.SaveChanges();
            }
        }
        #endregion
    }
}
