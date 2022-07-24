using System;
using System.Collections.Generic;
using AllSpice.Models;
using AllSpice.Repositories;

namespace AllSpice.Services
{

    public class IngredientsService
    {

        private readonly RecipesService _rs;

        private readonly IngredientsRepository _repo;

        public IngredientsService(RecipesService rs, IngredientsRepository repo)
        {
            _rs = rs;
            _repo = repo;
        }
        internal Ingredient GetIngredient(int ingredientId)
        {
            Ingredient found = _repo.GetIngredient(ingredientId);
            if (found == null)
            {
                throw new Exception("Invalid Id");
            }
            return found;
        }
        internal Ingredient Create(Ingredient ingredientData, string userId)
        {
            _rs.GetById(ingredientData.RecipeId);
            return _repo.Create(ingredientData);
        }

        internal List<Ingredient> GetIngredientsByRecipeId(int recipeId, string userId)
        {

            _rs.GetById(recipeId);
            return _repo.GetIngredientsByRecipeId(recipeId);
        }


        internal Ingredient Edit(Ingredient ingredientData)
        {
            Ingredient original = GetIngredient(ingredientData.Id);

            original.Name = ingredientData.Name ?? original.Name;
            original.Quantity = ingredientData.Quantity ?? original.Quantity;


            _repo.Edit(original);

            return original;
        }

        internal Ingredient Delete(int id)
        {
            Ingredient original = GetIngredient(id);

            _repo.Delete(id);

            return original;
        }
    }
}