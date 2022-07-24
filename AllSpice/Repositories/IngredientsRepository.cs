using System.Collections.Generic;
using System.Data;
using System.Linq;
using AllSpice.Models;
using Dapper;

namespace AllSpice.Repositories
{
    public class IngredientsRepository
    {

        private readonly IDbConnection _db;

        public IngredientsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal Ingredient GetIngredient(int ingredientId)
        {
            string sql = "SELECT * FROM ingredients WHERE Id = @ingredientId";
            return _db.QueryFirstOrDefault<Ingredient>(sql, new { ingredientId });
        }

        internal List<Ingredient> GetIngredientsByRecipeId(int recipeId)
        {
            string sql = "SELECT * FROM ingredients WHERE recipeId = @recipeId";
            return _db.Query<Ingredient>(sql, new { recipeId }).ToList();
        }

        internal Ingredient Create(Ingredient ingredientData)
        {
            string sql = @"
            INSERT INTO ingredients
            (name, quantity, recipeId)
            VALUES
            (@Name, @Quantity, @RecipeId);
            SELECT LAST_INSERT_ID();
            ";

            int id = _db.ExecuteScalar<int>(sql, ingredientData);
            ingredientData.Id = id;
            return ingredientData;
        }

        internal void Edit(Ingredient original)
        {
            string sql = @"
          UPDATE ingredients
          SET
          name = @NAME,
          quantity = @Quantity
          WHERE id = @Id
          ";
            _db.Execute(sql, original);
        }

        internal void Delete(int id)
        {
            string sql = "DELETE FROM ingredients WHERE id = @id LIMIT 1";
            _db.Execute(sql, new { id });
        }
    }
}