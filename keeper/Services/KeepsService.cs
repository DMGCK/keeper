using System;
using System.Collections.Generic;
using keeper.Interfaces;
using keeper.Models;
using keeper.Repositories;

namespace keeper.Services
{
  public class KeepsService : IService<Keep>
  {
    KeepsRepository _kr;

    public KeepsService(KeepsRepository ks)
    {
      _kr = ks;
    }

    public Keep Create(Keep tData)
    {
      Keep keep = _kr.Create(tData);
      return keep;
    }

    public void Delete(int id, string userId)
    {
      Keep keep = GetByIdForValidate(id, userId);
      _kr.Delete(id);
    }

    public Keep Edit(Keep tData, int id)
    {
      Keep original = GetByIdForValidate(id, tData.creatorId);

      original.name = tData.name ?? original.name;
      original.description = tData.description ?? original.description;
      original.img = tData.img ?? original.img;

      _kr.Edit(original);

      return original;

    }

    internal void IncrementKeeps(int? keepId)
    {
      _kr.IncrementKeepsCount(keepId);
    }

    internal void DecrementKeeps(int? keepId)
    {
      _kr.DecrementKeepsCount(keepId);
    }

    public List<Keep> GetAll()
    {
      List<Keep> keeps = _kr.GetAll();
      return keeps;
    }

    public Keep GetById(int id)
    {
      Keep keep = _kr.GetById(id);
      if (keep == null)
      {
        throw new Exception("Invalid ID");
      }
      IncreaseViews(id);
      keep.views += 1;
      return keep;
    }

    private void IncreaseViews(int id)
    {
      _kr.IncreaseViews(id);
    }

    public Keep GetByIdForValidate(int id, string userId)
    {
      Keep keep = _kr.GetById(id);
      if (keep == null)
      {
        throw new Exception("Invalid ID");
      }
      if (keep.creatorId != userId)
      {
        throw new Exception("Invalid User");
      }
      return keep;
    }
  }
}