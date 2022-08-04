using System;
using System.Collections.Generic;
using keeper.Models;
using keeper.Services;
using Microsoft.AspNetCore.Mvc;

namespace keeper.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ProfilesController : ControllerBase
  {
    ProfilesService _ps;

    public ProfilesController(ProfilesService ps)
    {
      _ps = ps;
    }

    [HttpGet("{id}")]
    public ActionResult<Creator> GetProfile(string id)
    {
      try
      {
        Creator profile = _ps.GetProfile(id);
        return profile;
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}/keeps")]
    public ActionResult<List<Keep>> GetProfileKeeps(string id)
    {
      try
      {
        List<Keep> keeps = _ps.GetProfileKeeps(id);
        return Ok(keeps);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}/vaults")]
    public ActionResult<List<Vault>> GetProfileVaults(string id)
    {
      try
      {

        // System.Console.WriteLine("testing, where fail 1");

        // if (userInfo.Id == null)
        // {
        //   userInfo.Id = "";
        // }


        List<Vault> vaults = _ps.GetProfileVaults(id, "");
        System.Console.WriteLine("testing, where fail 2");

        return vaults;
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}
