using CP_CW_7902_DAL.DBO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CP_CW_7902_DAL.Repositories
{
    public interface IRepository<T> where T : class
    {
        void Insert(List<T> entities);
        void Truncate();
        List<T> GetAll();
    }
}
