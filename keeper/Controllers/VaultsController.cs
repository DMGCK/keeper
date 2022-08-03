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
  public class VaultsController : ControllerBase
  {

    VaultsService _vs;

    public VaultsController(VaultsService vs)
    {
      _vs = vs;
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Vault>> Create([FromBody] Vault vData)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        vData.creatorId = userInfo.Id;
        vData.creator = userInfo;
        Vault newVault = _vs.Create(vData);
        return Ok(newVault);
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

        _vs.Delete(id, userInfo.Id);
        return Ok($"Deleted {id}");
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<ActionResult<Vault>> Edit([FromBody] Vault vData, int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        vData.creatorId = userInfo.Id;
        vData.id = id;
        Vault update = _vs.Edit(vData, id);
        return Ok(update);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet]
    public ActionResult<List<Vault>> GetAll()
    {
      try
      {
        List<Vault> vaults = _vs.GetAll();
        return Ok(vaults);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Vault>> GetById(int id)
    {
      try
      {
        System.Console.WriteLine("running v GetById");
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        if (userInfo != null)
        {
          Vault authVault = _vs.GetById(id, userInfo.Id);
          return authVault;
        }
        Vault vault = _vs.GetById(id, "");
        return Ok(vault);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{vaultId}/keeps")]
    public async Task<ActionResult<List<VaultKeepViewModel>>> GetKeepsByVault(int vaultId)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();

        if (userInfo != null)
        {
          List<VaultKeepViewModel> vKeepsAuth = _vs.GetByVaultId(vaultId, userInfo.Id);
          return Ok(vKeepsAuth);

        }
        List<VaultKeepViewModel> vKeeps = _vs.GetByVaultId(vaultId, "");
        return Ok(vKeeps);

      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}