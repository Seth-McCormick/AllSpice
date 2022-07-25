using System;
using AllSpice.Models;
using AllSpice.Repositories;

namespace AllSpice.Services
{

    public class FavoritesService
    {
        private readonly FavoritesRepository _repo;

        public FavoritesService(FavoritesRepository repo)
        {
            _repo = repo;
        }

        internal Favorite GetById(int id)
        {
            Favorite found = _repo.GetById(id);
            if (found == null)
            {
                throw new Exception("Invalid Id");
            }
            return found;
        }

        internal Favorite Create(Favorite favoriteData)
        {
            Favorite exists = _repo.FindExisting(favoriteData);
            if (exists != null)
            {
                return exists;
            }
            return _repo.Create(favoriteData);
        }


        internal void Delete(int id, string userId)
        {
            Favorite toDelete = GetById(id);
            if (toDelete.AccountId != userId)
            {
                throw new Exception("Invalid Action");
            }
            _repo.Delete(id);
        }
    }
}