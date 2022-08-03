using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using keeper.Interfaces;
using keeper.Models;

namespace keeper.Repositories
{
  public class VaultsRepository : IRepository<Vault>
  {

    IDbConnection _db;

    public VaultsRepository(IDbConnection db)
    {
      _db = db;
    }

    public Vault Create(Vault tData)
    {
      string sql = @"
      INSERT INTO vaults
      (name, description, isPrivate, img, creatorId)
      VALUES
      (@name, @description, @isPrivate, @img, @creatorId);
      SELECT LAST_INSERT_ID();";

      int newId = _db.ExecuteScalar<int>(sql, tData);
      tData.id = newId;
      return tData;
    }

    public void Delete(int id)
    {
      string sql = "DELETE FROM vaults WHERE id = @id LIMIT 1";
      _db.Execute(sql, new { id });
    }

    public void Edit(Vault tData)
    {
      string sql = @"
      UPDATE vaults
      SET 
      name = @name,
      description = @description,
      isPrivate = @isPrivate
      WHERE id = @id;";
      _db.Execute(sql, tData);
    }

    public List<Vault> GetAll()
    {
      string sql = @"
      SELECT
       v.*,
       a.*
       FROM vaults v
       JOIN accounts a ON v.creatorId = a.id ";
      return _db.Query<Vault, Account, Vault>(sql, (vault, acc) =>
      {
        vault.creator = acc;
        return vault;
      }).ToList();
    }

    public Vault GetById(int id)
    {
      string sql = @"
      SELECT
       v.*,
       a.*,
       a.id AS accountId
       FROM vaults v
       JOIN accounts a ON v.creatorId = a.id 
      WHERE v.id = @id LIMIT 1";
      return _db.Query<Vault, Account, Vault>(sql, (vault, acc) =>
      {
        vault.creator = acc;
        return vault;
      }, new { id }).FirstOrDefault();
    }

    Vault IRepository<Vault>.Edit(Vault tData)
    {
      throw new System.NotImplementedException();
    }

    internal List<VaultKeepViewModel> GetKeepsByVaultId(int vaultId)
    {
      string sql = @"
      SELECT 
        k.*,
        a.*,
        vk.id
      FROM vaultkeep vk
      JOIN keeps k ON k.id = vk.keepId
      JOIN accounts a ON k.creatorId
      WHERE vk.vaultId = @vaultId";
      return _db.Query<VaultKeepViewModel, Account, int, VaultKeepViewModel>(sql, (keep, acc, vkId) =>
      {
        keep.creator = acc;
        keep.vaultKeepId = vkId;
        return keep;
      }, new { vaultId }).ToList();

    }
  }
}