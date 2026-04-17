using Microsoft.AspNetCore.Mvc;
using SPR411_SteamClone.API.Extensions;
using SPR411_SteamClone.BLL.Dtos.Game;
using SPR411_SteamClone.BLL.Services;

namespace SPR411_SteamClone.API.Controllers
{
    [ApiController]
    [Route("api/game")]
    public class GameController : ControllerBase
    {
        private readonly GameService _gameService;

        public GameController(GameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var response = await _gameService.GetAllAsync();
            return this.GetResult(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            var response = await _gameService.GetByIdAsync(id);
            return this.GetResult(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] CreateGameDto dto)
        {
            var response = await _gameService.CreateAsync(dto);
            return this.GetResult(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromForm] UpdateGameDto dto)
        {
            var response = await _gameService.UpdateAsync(dto);
            return this.GetResult(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var response = await _gameService.DeleteAsync(id);
            return this.GetResult(response);
        }
    }
}