using System;
using System.Collections.Generic;
using keeper.Models;

namespace keeper.Repositories
{
  public class VaultsService
  {
    VaultsRepository _vr;

    public VaultsService(VaultsRepository vr)
    {
      _vr = vr;
    }

    public Vault Create(Vault tData)
    {
      Vault vault = _vr.Create(tData);
      return vault;

    }

    public void Delete(int id, string userId)
    {
      GetByIdForValidate(id, userId);
      _vr.Delete(id);
    }

    public Vault Edit(Vault tData, int userId)
    {
      Console.WriteLine("Validate line");
      Vault original = GetByIdForValidate(tData.id, tData.creatorId);
      Console.WriteLine("Post Validate line");

      original.name = tData.name ?? original.name;
      original.description = tData.description ?? original.description;
      original.isPrivate = tData.isPrivate ?? original.isPrivate;

      _vr.Edit(original);
      return original;


    }

    public List<Vault> GetAll()
    {
      List<Vault> vaults = _vr.GetAll();
      return vaults;
    }

    public Vault GetById(int id, string userId)
    {
      Vault vault = _vr.GetById(id);
      if (vault == null)
      {
        throw new Exception("Invalid ID");
      }
      if (vault.isPrivate == true && vault?.creatorId != userId)
      {
        throw new Exception("Invalid ID");
      }

      return vault;
    }

    public Vault GetByIdForValidate(int id, string userId)
    {
      Vault vault = _vr.GetById(id);
      if (vault == null)
      {
        throw new Exception("Invalid ID");
      }
      if (vault.creatorId != userId)
      {
        throw new Exception("Invalid User");
      }
      return vault;
    }

    internal List<VaultKeepViewModel> GetByVaultId(int vaultId, string userId)
    {
      // v null check
      Vault vault = GetById(vaultId, userId);

      List<VaultKeepViewModel> vKeeps = _vr.GetKeepsByVaultId(vaultId);
      return vKeeps;
    }
  }
}