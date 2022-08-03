using System.Collections.Generic;

namespace keeper.Interfaces
{
  public interface IRepository<T>
  {
    T Create(T tData);
    T Edit(T tData);
    void Delete(int id);
    List<T> GetAll();
    T GetById(int id);
  }
}