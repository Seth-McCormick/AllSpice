using System;
using System.Threading.Tasks;
using AllSpice.Models;
using AllSpice.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AllSpice.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class IngredientsController : ControllerBase
    {
        private readonly IngredientsService _ingser;

        public IngredientsController(IngredientsService ingser)
        {
            _ingser = ingser;
        }


        [HttpGet("{id}")]

        public async Task<ActionResult<Ingredient>> Get(int id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                Ingredient ingredient = _ingser.GetIngredient(id);
                return Ok(ingredient);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



        [HttpPost]
        public async Task<ActionResult<Ingredient>> Create([FromBody] Ingredient ingredientData)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                Ingredient ingredient = _ingser.Create(ingredientData, userInfo.Id);
                return Ok(ingredient);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpPut("{id}")]

        public async Task<ActionResult<Ingredient>> EditAsync(int id, [FromBody] Ingredient ingredientData)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                ingredientData.Id = id;

                Ingredient update = _ingser.Edit(ingredientData);
                return Ok(update);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult<Ingredient>> DeleteAsync(int id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                Ingredient deleteIngredient = _ingser.Delete(id);
                return Ok(deleteIngredient);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}