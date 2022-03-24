using System.Collections.Generic;

namespace CP_CW_7902_DAL.Repositories
{
    public interface IRepository<T> where T : class
    {
        void Insert(List<T> entities);
        void Truncate();
        List<T> GetAll();
    }
}
