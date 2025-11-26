namespace Eventify.Services
{
    public interface IGenericService<T>
    {
        public List<T> GetByFilter_Search();
        public List<T> Get3();
        public T GetByUserId(int id);
        public T GetById(int id);
        public int Insert(T obj);
        public int Update(int id);
        public int Delete(int id);
    }
}
