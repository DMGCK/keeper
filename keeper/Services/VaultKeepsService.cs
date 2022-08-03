using System;
using System.Collections.Generic;
using keeper.Models;
using keeper.Services;

namespace keeper.Repositories
{
  public class VaultKeepsService
  {
    VaultKeepsRepository _vkr;
    VaultsService _vs;
    KeepsService _ks;

    public VaultKeepsService(VaultKeepsRepository vkr, VaultsService vs, KeepsService ks)
    {
      _vkr = vkr;
      _vs = vs;
      _ks = ks;
    }

    internal VaultKeep Create(VaultKeep vkData, string userId)
    {
      _vs.GetByIdForValidate((int)vkData.vaultId, userId);
      VaultKeep vKeep = _vkr.Create(vkData);

      _ks.IncrementKeeps(vkData.keepId);

      return vKeep;
    }

    internal void Delete(int id, string userId)
    {
      VaultKeep vKeep = GetById(id);
      if (vKeep.creatorId != userId)
      {
        throw new Exception("Invalid ID, Unauthorized");
      }
      _ks.DecrementKeeps(vKeep.keepId);
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
      System.Console.WriteLine("problem is in the getbyid");

      VaultKeep vKeep = _vkr.GetById(id);
      if (vKeep == null)
      {
        throw new Exception("Invalid ID");
      }
      return vKeep;
    }
  }
}