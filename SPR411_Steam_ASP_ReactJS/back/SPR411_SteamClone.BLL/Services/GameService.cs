using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SPR411_SteamClone.BLL.Dtos.Game;
using SPR411_SteamClone.BLL.Settings;
using SPR411_SteamClone.DAL.Entities;
using SPR411_SteamClone.DAL.Repositories;

namespace SPR411_SteamClone.BLL.Services
{
    public class GameService
    {
        private readonly GameRepository _gameRepository;
        private readonly GenreRepository _genreRepository;
        private readonly FileService _fileService;
        private readonly IMapper _mapper;

        public GameService(GameRepository gameRepository, IMapper mapper, GenreRepository genreRepository, FileService fileService)
        {
            _gameRepository = gameRepository;
            _mapper = mapper;
            _genreRepository = genreRepository;
            _fileService = fileService;
        }

        private async Task SaveImagesAsync(GameEntity entity, CreateGameDto dto)
        {
            string guid = Guid.NewGuid().ToString();
            string folderPath = Path.Combine(StaticFilesSettings.Games, guid);
            if (dto.PreviewImage != null)
            {
                var res = await _fileService.SaveImageAsync(dto.PreviewImage, folderPath);
                if (res.IsSuccess)
                {
                    var previewImage = new GameImageEntity
                    {
                        IsPreview = true,
                        Name = $"{guid}/{res.Payload!}",
                    };
                    entity.Images.Add(previewImage);
                }
            }

            if (dto.Images.Count > 0)
            {
                var res = await _fileService.SaveImagesAsync(dto.Images, folderPath);

                foreach (var r in res)
                {
                    if (r.IsSuccess)
                    {
                        var image = new GameImageEntity
                        {
                            IsPreview = false,
                            Name = $"{guid}/{r.Payload!}"
                        };
                        entity.Images.Add(image);
                    }
                }
            }
        }

        public async Task<ServiceResponse> CreateAsync(CreateGameDto dto)
        {
            var entity = _mapper.Map<GameEntity>(dto);

            entity.Genres = await _genreRepository.Genres
                .Where(g => dto.Genres.Select(g => g.ToLower()).Contains(g.Name.ToLower()))
                .ToListAsync();

            // images
            await SaveImagesAsync(entity, dto);

            var res = await _gameRepository.CreateAsync(entity);

            if(!res)
            {
                return ServiceResponse.Error("Не вдалося додати гру");
            }

            return ServiceResponse.Success($"Гра '{dto.Name}' успішно додана");
        }
		
		public async Task<ServiceResponse> GetAllAsync()
        {
            var entities = await _gameRepository.Games
                .Include(g => g.Developer)
                .Include(g => g.Genres)
                .Include(g => g.Images)
                .AsNoTracking()
                .ToListAsync();

            var dtos = _mapper.Map<List<GameDto>>(entities);
            return ServiceResponse.Success("Список ігор отримано", dtos);
        }

        public async Task<ServiceResponse> GetByIdAsync(int id)
        {
            var entity = await _gameRepository.Games
                .Include(g => g.Developer)
                .Include(g => g.Genres)
                .Include(g => g.Images)
                .AsNoTracking()
                .FirstOrDefaultAsync(g => g.Id == id);

            if (entity == null)
            {
                return ServiceResponse.Error($"Гра з id '{id}' не знайдена");
            }

            var dto = _mapper.Map<GameDto>(entity);
            return ServiceResponse.Success("Гру отримано", dto);
        }

        public async Task<ServiceResponse> UpdateAsync(UpdateGameDto dto)
        {
            var entity = await _gameRepository.Games
                .Include(g => g.Genres)
                .Include(g => g.Images) // Обов'язково додаємо Include для картинок
                .FirstOrDefaultAsync(g => g.Id == dto.Id);

            if (entity == null) return ServiceResponse.Error($"Гра з id '{dto.Id}' не знайдена");

            _mapper.Map(dto, entity);

            entity.Genres = await _genreRepository.Genres
                .Where(g => dto.Genres.Select(genre => genre.ToLower()).Contains(g.Name.ToLower()))
                .ToListAsync();

            string folderGuid = entity.Images.FirstOrDefault()?.Name.Split('/')[0] ?? Guid.NewGuid().ToString();
            string folderPath = Path.Combine(StaticFilesSettings.Games, folderGuid);

            // Якщо прилетіло нове прев'ю
            if (dto.PreviewImage != null)
            {
                var oldPreview = entity.Images.FirstOrDefault(i => i.IsPreview);
                if (oldPreview != null)
                {
                    // Видаляємо фізичний файл старого прев'ю
                    _fileService.DeleteFile(oldPreview.Name.Split('/')[1], folderPath);
                    entity.Images.Remove(oldPreview); // Видаляємо запис з БД
                }

                // Зберігаємо нове прев'ю
                var res = await _fileService.SaveImageAsync(dto.PreviewImage, folderPath);
                if (res.IsSuccess)
                {
                    entity.Images.Add(new GameImageEntity { IsPreview = true, Name = $"{folderGuid}/{res.Payload}" });
                }
            }

            
            if (dto.Images != null && dto.Images.Count > 0)
            {
                var res = await _fileService.SaveImagesAsync(dto.Images, folderPath);
                foreach (var r in res)
                {
                    if (r.IsSuccess)
                    {
                        entity.Images.Add(new GameImageEntity { IsPreview = false, Name = $"{folderGuid}/{r.Payload}" });
                    }
                }
            }

            bool resUpdate = await _gameRepository.UpdateAsync(entity);
            if (!resUpdate) return ServiceResponse.Error("Не вдалося змінити гру");

            var responseDto = _mapper.Map<GameDto>(entity);
            return ServiceResponse.Success($"Гра '{entity.Name}' успішно змінена", responseDto);
        }

        public async Task<ServiceResponse> DeleteAsync(int id)
        {
            // Обов'язково робимо Include(g => g.Images), щоб отримати шляхи до картинок
            var entity = await _gameRepository.Games
                .Include(g => g.Images)
                .FirstOrDefaultAsync(g => g.Id == id);

            if (entity == null)
            {
                return ServiceResponse.Error($"Гра з id '{id}' не знайдена");
            }

            if (entity.Images.Count > 0)
            {
                string folderGuid = entity.Images.First().Name.Split('/')[0];
                _fileService.DeleteFolder(Path.Combine(StaticFilesSettings.Games, folderGuid));
            }

            bool res = await _gameRepository.DeleteAsync(entity);
            if (!res) return ServiceResponse.Error($"Не вдалося видалити гру '{entity.Name}'");

            return ServiceResponse.Success($"Гра '{entity.Name}' успішно видалена");
        }
    }
}
