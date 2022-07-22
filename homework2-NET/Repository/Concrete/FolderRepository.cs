using homework2_NET.Models;
using homework2_NET.Repository.Abstract;

namespace homework2_NET.Repository.Concrete
{
    public class FolderRepository : IBaseRepository<Folder>
    {
        public List<Folder> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Folder GetByIdAsync(int entityId)
        {
            throw new NotImplementedException();
        }

        public Task InsertAsync(Folder entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveAsync(Folder entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Folder entity)
        {
            throw new NotImplementedException();
        }
    }
}
