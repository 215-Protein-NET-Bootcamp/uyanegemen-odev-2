using homework2_NET.Models;
using homework2_NET.Repository.Abstract;

namespace homework2_NET.Repository.Concrete
{
    public class EmployeeRepository : IBaseRepository<Employee>
    {
        public List<Employee> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Employee GetByIdAsync(int entityId)
        {
            throw new NotImplementedException();
        }

        public Task InsertAsync(Employee entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveAsync(Employee entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Employee entity)
        {
            throw new NotImplementedException();
        }
    }
}
