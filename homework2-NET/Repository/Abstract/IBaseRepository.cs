using System.Linq.Expressions;

namespace homework2_NET.Repository.Abstract
{
    public interface IBaseRepository<Entity> where Entity : class
    {
        Entity GetByIdAsync(int entityId);
        List<Entity> GetAllAsync();
        Task InsertAsync(Entity entity);
        void RemoveAsync(Entity entity);
        void Update(Entity entity);
       
    }
}
