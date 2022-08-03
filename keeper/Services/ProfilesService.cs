using System.Collections.Generic;
using keeper.Models;
using keeper.Repositories;

namespace keeper.Services
{
  public class ProfilesService
  {
    ProfilesRepository _pr;

    public ProfilesService(ProfilesRepository pr)
    {
      _pr = pr;
    }

    internal Creator GetProfile(string id)
    {
      Creator profile = _pr.GetProfile(id);
      return profile;
    }

    internal List<Keep> GetProfileKeeps(string id)
    {
      List<Keep> keeps = _pr.GetProfileKeeps(id);
      return keeps;
    }

    internal List<Vault> GetProfileVaults(string searchId, string userId)
    {
      List<Vault> vaults = _pr.GetProfileVaults(searchId);


      if (searchId != userId)
      {
        List<Vault> filtered = vaults.FindAll((v) =>
        {
          if (v.isPrivate == false)
          {
            return true;
          }
          return false;
        });
        return filtered;
      }


      return vaults;
    }
  }
}