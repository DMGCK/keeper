using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using keeper.Models;

namespace keeper.Repositories
{
  public class VaultKeepsRepository
  {
    IDbConnection _db;

    public VaultKeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal VaultKeep Create(VaultKeep vkData)
    {
      string sql = @"
      INSERT INTO vaultkeep
      (keepId, vaultId, creatorId)
      VALUES
      (@keepId, @vaultId, @creatorId);
      SELECT LAST_INSERT_ID();";
      int newId = _db.ExecuteScalar<int>(sql, vkData);
      vkData.id = newId;
      return vkData;
    }

    internal void Delete(int id)
    {
      System.Console.WriteLine("made it to repository");
      string sql = @"
      DELETE FROM vaultkeep WHERE id = @id LIMIT 1";
      _db.Execute(sql, new { id });
      System.Console.WriteLine("the problem did not show up here");
    }

    internal void Edit(VaultKeep update)
    {
      string sql = @"
      UPDATE keeps
      SET 
      vaultId = @vaultId,
      keepId = @keepId,
      WHERE id = @id;";
      _db.Execute(sql, update);
    }

    internal List<VaultKeep> GetAll()
    {
      string sql = @"
      SELECT * FROM vaultkeep";
      return _db.Query<VaultKeep>(sql).ToList();
    }

    internal VaultKeep GetById(int id)
    {
      string sql = @"
      SELECT * FROM vaultkeep
      WHERE id = @id LIMIT 1";
      return _db.Query<VaultKeep>(sql, new { id }).FirstOrDefault();
    }
  }
}