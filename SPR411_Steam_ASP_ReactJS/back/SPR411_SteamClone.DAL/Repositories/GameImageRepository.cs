using Microsoft.EntityFrameworkCore;
using SPR411_SteamClone.DAL.Entities;

namespace SPR411_SteamClone.DAL.Repositories
{
    public class GameImageRepository : GenericRepository<GameImageEntity>
    {
        public GameImageRepository(AppDbContext context) 
            : base(context)
        {
        }

        public IQueryable<GameImageEntity> GameImages => GetAll();

        public async Task<List<GameImageEntity>> GetImagesByGameIdAsync(int gameId)
        {
            return await GameImages
                .AsNoTracking()
                .Where(img => img.GameId == gameId)
                .ToListAsync();
        }
    }
}