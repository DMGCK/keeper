using System.Collections.Generic;

namespace keeper.Interfaces
{
  public interface IService<T>
  {
    public List<T> GetAll();
    public T GetById(int id);
    public T Create(T tData);
    public T Edit(T tData, int id);
    public void Delete(int id, string userId);
  }
}