using System.Data;
using keeper.Models;
using Dapper;
using System.Collections.Generic;
using System.Linq;

namespace keeper.Repositories
{
  public class AccountsRepository
  {
    private readonly IDbConnection _db;

    public AccountsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal Account GetByEmail(string userEmail)
    {
      string sql = "SELECT * FROM accounts WHERE email = @userEmail";
      return _db.QueryFirstOrDefault<Account>(sql, new { userEmail });
    }

    internal Account GetById(string id)
    {
      string sql = "SELECT * FROM accounts WHERE id = @id";
      return _db.QueryFirstOrDefault<Account>(sql, new { id });
    }

    internal Account Create(Account newAccount)
    {
      string sql = @"
            INSERT INTO accounts
              (name, picture, email, id)
            VALUES
              (@Name, @Picture, @Email, @Id)";
      _db.Execute(sql, newAccount);
      return newAccount;
    }

    internal Account Edit(Account update)
    {
      string sql = @"
            UPDATE accounts
            SET 
              name = @Name,
              picture = @Picture
            WHERE id = @Id;";
      _db.Execute(sql, update);
      return update;
    }

    internal List<Vault> GetMyVaults(string id)
    {
      string sql = @"
      SELECT *
      FROM vaults
      WHERE creatorId = @id";
      return _db.Query<Vault>(sql, new { id }).ToList();
    }

    internal List<VaultKeep> GetMyVaultKeeps(string id)
    {
      string sql = @"
      SELECT *
      FROM vaultkeep
      WHERE creatorId = @id";
      return _db.Query<VaultKeep>(sql, new { id }).ToList();
    }
  }
}
