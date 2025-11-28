using Eventify.Models.Entities;

namespace Eventify.Services
{
    public interface IGenericService<T>
    {
        public List<T> Get3();
        public T GetById(int id);
        public List<T> GetByUserId(int id);
        public int Insert(T obj);
        public int Update(T obj);
        public int Delete(int id);
    }
}
