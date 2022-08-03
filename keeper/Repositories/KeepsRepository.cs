using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using keeper.Interfaces;
using keeper.Models;

namespace keeper.Repositories
{
  public class KeepsRepository : IRepository<Keep>
  {

    private readonly IDbConnection _db;

    public KeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    public Keep Create(Keep tData)
    {
      string sql = @"
      INSERT INTO keeps
      (name, description, img, creatorId)
      VALUES
      (@name, @description, @img, @creatorId);
      SELECT LAST_INSERT_ID();";

      int newId = _db.ExecuteScalar<int>(sql, tData);
      tData.id = newId;
      return tData;
    }

    public void Delete(int id)
    {
      string sql = "DELETE FROM keeps WHERE id = @id LIMIT 1";
      _db.Execute(sql, new { id });
    }

    public void Edit(Keep tData)
    {
      string sql = @"
      UPDATE keeps
      SET 
      name = @name,
      description = @description,
      img = @img
      WHERE id = @id;";
      _db.Execute(sql, tData);
    }

    public List<Keep> GetAll()
    {
      string sql = @"
      SELECT
       k.*,
       a.*
       FROM keeps k
       JOIN accounts a ON k.creatorId = a.id ";
      return _db.Query<Keep, Account, Keep>(sql, (keep, acc) =>
      {
        keep.creator = acc;
        return keep;
      }).ToList();
    }

    public Keep GetById(int id)
    {
      string sql = @"
      SELECT
       k.*,
       a.*,
       a.id AS accountId
       FROM keeps k
       JOIN accounts a ON k.creatorId = a.id 
      WHERE k.id = @id LIMIT 1";
      return _db.Query<Keep, Account, Keep>(sql, (keep, acc) =>
      {
        keep.creator = acc;
        return keep;
      }, new { id }).FirstOrDefault();
    }

    Keep IRepository<Keep>.Create(Keep tData)
    {
      throw new System.NotImplementedException();
    }

    Keep IRepository<Keep>.Edit(Keep tData)
    {
      throw new System.NotImplementedException();
    }
  }
}