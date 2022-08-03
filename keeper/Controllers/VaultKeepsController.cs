using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using keeper.Models;
using keeper.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace keeper.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class VaultKeepsController : ControllerBase
  {
    VaultKeepsService _vss;

    public VaultKeepsController(VaultKeepsService vss)
    {
      _vss = vss;
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<VaultKeep>> Create([FromBody] VaultKeep vkData)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        vkData.creatorId = userInfo.Id;
        VaultKeep newKeep = _vss.Create(vkData, userInfo.Id);
        return Ok(newKeep);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult<string>> Delete(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();

        _vss.Delete(id, userInfo.Id);
        return Ok($"Deleted {id}");
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<ActionResult<VaultKeep>> Edit([FromBody] VaultKeep vkData, int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        vkData.id = id;
        VaultKeep update = _vss.Edit(vkData, userInfo.Id);
        return Ok(update);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet]
    public ActionResult<List<VaultKeep>> GetAll()
    {
      try
      {
        List<VaultKeep> vKeeps = _vss.GetAll();
        return Ok(vKeeps);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}")]
    public ActionResult<VaultKeep> GetById(int id)
    {
      try
      {
        VaultKeep vKeep = _vss.GetById(id);
        return Ok(vKeep);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}