using System;
using System.Collections.Generic;
using AllSpice.Models;
using AllSpice.Repositories;

namespace AllSpice.Services
{
    public class StepsService
    {

        private readonly RecipesService _rs;
        private readonly StepsRepository _repo;

        public StepsService(RecipesService rs, StepsRepository repo)
        {
            _rs = rs;
            _repo = repo;
        }
        internal Step GetStep(int stepId)
        {
            Step found = _repo.GetStep(stepId);
            if (found == null)
            {
                throw new Exception("Invalid Id");
            }
            return found;
        }

        internal List<Step> GetStepByRecipeId(int recipeId, string userId)
        {
            _rs.GetById(recipeId);
            return _repo.GetStepsByRecipeId(recipeId);
        }

        internal Step Create(Step stepData, string userId)
        {
            _rs.GetById(stepData.RecipeId);
            return _repo.Create(stepData);
        }

        internal Step Edit(Step stepData)
        {
            Step original = GetStep(stepData.Id);

            original.Body = stepData.Body ?? original.Body;
            original.Position = stepData.Position ?? original.Position;


            _repo.Edit(original);

            return original;
        }

        internal Step Delete(int id)
        {
            Step original = GetStep(id);

            _repo.Delete(id);

            return original;
        }
    }
}