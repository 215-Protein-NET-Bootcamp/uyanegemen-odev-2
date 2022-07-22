using homework2_NET.Models;
using homework2_NET.Repository.Abstract;

namespace homework2_NET.Repository.Concrete
{
    public class DepartmentRepository : IBaseRepository<Department>
    {
        public List<Department> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Department GetByIdAsync(int entityId)
        {
            throw new NotImplementedException();
        }

        public Task InsertAsync(Department entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveAsync(Department entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Department entity)
        {
            throw new NotImplementedException();
        }
    }
}
