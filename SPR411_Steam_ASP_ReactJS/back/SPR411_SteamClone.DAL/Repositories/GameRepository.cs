using Microsoft.EntityFrameworkCore;
using SPR411_SteamClone.DAL.Entities;

namespace SPR411_SteamClone.DAL.Repositories
{
    public class GameRepository : GenericRepository<GameEntity>
    {
        public GameRepository(AppDbContext context)
            : base(context)
        {
        }

        public IQueryable<GameEntity> Games => GetAll();

        public async Task<List<GameEntity>> GetGamesCheaperThanAsync(decimal maxPrice)
        {
            return await Games
                .AsNoTracking()
                .Where(g => g.Price < maxPrice)
                .ToListAsync();
        }

        public async Task<List<GameEntity>> GetGamesByGenreAsync(string genreName)
        {
            return await Games
                .Include(g => g.Genres)
                .AsNoTracking()
                .Where(g => g.Genres.Any(genre => genre.Name.ToLower() == genreName.ToLower()))
                .ToListAsync();
        }
        
        public async Task<List<GameEntity>> GetGamesByGenreIdAsync(int genreId)
        {
            return await Games
                .Include(g => g.Genres)
                .AsNoTracking()
                .Where(g => g.Genres.Any(genre => genre.Id == genreId))
                .ToListAsync();
        }
    }
}