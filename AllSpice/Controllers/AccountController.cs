using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AllSpice.Models;
using AllSpice.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AllSpice.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;
        private readonly RecipesService _rs;

        public AccountController(AccountService accountService, RecipesService rs)
        {
            _accountService = accountService;
            _rs = rs;
        }

        [HttpGet]

        public async Task<ActionResult<Account>> Get()
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                return Ok(_accountService.GetOrCreateProfile(userInfo));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpGet("favorites")]

        public async Task<ActionResult<Account>> GetRecipeFavorites()
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                List<RecipeFavoriteViewModel> favorites = _rs.GetFavoritesByAccountId(userInfo.Id);
                return Ok(favorites);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }


}