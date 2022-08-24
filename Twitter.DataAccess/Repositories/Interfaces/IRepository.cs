using Twitter.DataAccess.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Twitter.DataAccess.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {     
        public void Add(TEntity entity);
    }
}