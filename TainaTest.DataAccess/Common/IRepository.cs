using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TainaTest.DataAccess.Common
{
    /// <summary> 
    /// Purpose: Interface contains common methods implemented by all repository classes. 
    /// </summary> 
    public interface IRepository<TEntity> where TEntity : class
    {
        #region Methods 
        bool Insert(TEntity entity);
        bool Update(TEntity entity);
        bool DeleteById(int id);
        TEntity SelectById(int id);
        List<TEntity> SelectAll();
        #endregion
    }
}
