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
    [Route("api/[controller]")]

    public class RecipesController : ControllerBase
    {
        private readonly RecipesService _rs;

        private readonly IngredientsService _ingser;

        private readonly StepsService _ss;

        public RecipesController(RecipesService rs, IngredientsService ingser, StepsService ss)
        {
            _rs = rs;
            _ingser = ingser;
            _ss = ss;
        }

        [HttpGet]

        public ActionResult<List<Recipe>> Get(string query = "")
        {
            try
            {
                List<Recipe> recipes = _rs.GetAll();
                return Ok(recipes);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<Recipe>> Get(int id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();

                Recipe recipe = _rs.GetById(id);
                return Ok(recipe);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}/ingredients")]

        public async Task<ActionResult<Ingredient>> GetIngredients(int id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                List<Ingredient> ingredients = _ingser.GetIngredientsByRecipeId(id, userInfo.Id);
                return Ok(ingredients);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}/steps")]

        public async Task<ActionResult<Step>> GetSteps(int id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                List<Step> steps = _ss.GetStepByRecipeId(id, userInfo.Id);
                return Ok(steps);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Recipe>> CreateAsync([FromBody] Recipe recipeData)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                recipeData.CreatorId = userInfo.Id;
                Recipe newRecipe = _rs.Create(recipeData);
                newRecipe.Creator = userInfo;
                newRecipe.CreatedAt = new DateTime();
                newRecipe.UpdatedAt = new DateTime();

                return Ok(newRecipe);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }

        }

        [HttpPut("{id}")]
        [Authorize]

        public async Task<ActionResult<Recipe>> EditAsync(int id, [FromBody] Recipe recipeData)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                recipeData.Id = id;
                recipeData.CreatorId = userInfo.Id;
                Recipe update = _rs.Edit(recipeData);
                return Ok(update);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]

        public async Task<ActionResult<Recipe>> DeleteAsync(int id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                Recipe deleteRecipe = _rs.Delete(id, userInfo.Id);
                return Ok(deleteRecipe);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
    }
}