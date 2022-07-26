using System.Collections.Generic;
using System.Data;
using System.Linq;
using AllSpice.Models;
using Dapper;

namespace AllSpice.Repositories
{
    public class RecipesRepository
    {
        private readonly IDbConnection _db;

        public RecipesRepository(IDbConnection db)
        {
            _db = db;
        }

        internal List<Recipe> GetAll(string query = "")
        {
            string stringQuery = "%" + query + "%";
            string sql = @"
           SELECT
           r.*,
           a.*
           FROM recipes r
           JOIN accounts a ON r.creatorId = a.id
           WHERE title LIKE @stringQuery
           ";
            return _db.Query<Recipe, Profile, Recipe>(sql, (recipe, profile) =>
            {
                recipe.Creator = profile;
                return recipe;
            }, new { stringQuery }).ToList();
        }

        internal Recipe GetById(int tripId)
        {
            string sql = @"
          SELECT 
          r.*,
          a.*
          FROM recipes r
          JOIN accounts a ON r.creatorId = a.id
          WHERE r.id = @tripId
          ";
            return _db.Query<Recipe, Profile, Recipe>(sql, (recipe, profile) =>
            {
                recipe.Creator = profile;
                return recipe;
            }, new { tripId }).FirstOrDefault();
        }

        internal void Edit(Recipe original)
        {
            string sql = @"
        UPDATE recipes 
        SET
        picture = @Picture,
        title = @Title,
        subtitle = @Subtitle,
        category = @Category
        WHERE id = @Id
        
        ";
            _db.Execute(sql, original);
        }


        internal Recipe Create(Recipe recipeData)
        {
            string sql = @"
        INSERT INTO recipes
        (picture, title, subtitle, category, creatorId)
        VALUES
        (@Picture, @Title, @Subtitle, @Category, @CreatorId); 
        SELECT LAST_INSERT_ID();         
        ";
            int id = _db.ExecuteScalar<int>(sql, recipeData);
            recipeData.Id = id;
            return recipeData;
        }



        internal List<RecipeFavoriteViewModel> GetFavoritesByAccountId(string userId)
        {
            string sql = @"
          SELECT
          a.*,
          r.*,
          f.id as FavoriteId
          FROM favorites f
          JOIN recipes r ON r.id = f.recipeId
          JOIN accounts a ON a.id = r.creatorId
          WHERE f.accountId = @userId;
          ";

            return _db.Query<Account, RecipeFavoriteViewModel, RecipeFavoriteViewModel>(sql, (profile, recipe) =>
            {
                recipe.Creator = profile;
                return recipe;
            }, new { userId }).ToList();
        }



        internal void Delete(int id)
        {
            string sql = "DELETE FROM recipes WHERE id = @id LIMIT 1";
            _db.Execute(sql, new { id });
        }
    }
}