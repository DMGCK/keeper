using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using keeper.Models;

namespace keeper.Repositories
{
  public class ProfilesRepository
  {
    IDbConnection _db;

    public ProfilesRepository(IDbConnection db)
    {
      _db = db;
    }

    internal Creator GetProfile(string id)
    {
      string sql = @"
      SELECT * FROM accounts WHERE id = @id";
      return _db.QueryFirstOrDefault<Creator>(sql, new { id });
    }

    internal List<Keep> GetProfileKeeps(string id)
    {
      string sql = @"
      SELECT *
      FROM keeps k
      WHERE k.creatorId = @id";
      return _db.Query<Keep>(sql, new { id }).ToList();
    }

    internal List<Vault> GetProfileVaults(string id)
    {
      string sql = @"
      SELECT *
      FROM vaults v
      WHERE v.creatorId = @id";
      return _db.Query<Vault>(sql, new { id }).ToList();
    }
  }
}