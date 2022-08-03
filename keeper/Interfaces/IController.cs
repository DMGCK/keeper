using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace keeper.Interfaces
{
  public interface IController<T>
  {
    public ActionResult<List<T>> GetAll();
    public ActionResult<T> GetByIdAsync(int id);
    public ActionResult<T> Create([FromBody] T needsAuth);
    public ActionResult<T> Edit([FromBody] T needsAuth, int id);
    public ActionResult<string> Delete(int needsAuth);

  }
}