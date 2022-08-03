using System;
using System.Collections.Generic;
using keeper.Models;

namespace keeper.Repositories
{
  public class VaultKeepsService
  {
    VaultKeepsRepository _vkr;
    VaultsService _vs;

    public VaultKeepsService(VaultKeepsRepository vkr, VaultsService vs)
    {
      _vkr = vkr;
      _vs = vs;
    }

    internal VaultKeep Create(VaultKeep vkData, string userId)
    {
      _vs.GetByIdForValidate((int)vkData.vaultId, userId);
      VaultKeep vKeep = _vkr.Create(vkData);
      return vKeep;
    }

    internal void Delete(int id, string userId)
    {
      _vkr.Delete(id);
    }

    internal VaultKeep Edit(VaultKeep vkData, string userId)
    {
      VaultKeep original = GetById(vkData.id);

      original.keepId = vkData.keepId ?? original.keepId;
      original.vaultId = vkData.vaultId ?? original.vaultId;
      _vkr.Edit(original);
      return original;
    }

    internal List<VaultKeep> GetAll()
    {
      List<VaultKeep> vKeeps = _vkr.GetAll();
      return vKeeps;
    }

    internal VaultKeep GetById(int id)
    {
      VaultKeep vKeep = _vkr.GetById(id);
      if (vKeep == null)
      {
        throw new Exception("Invalid ID");
      }
      return vKeep;
    }
  }
}