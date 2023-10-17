using mongodb_dotnet_sample.Models;
using mongodb_dotnet_sample.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace mongodb_dotnet_sample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly VehiclesService _gameService;

        public VehiclesController(VehiclesService gamesService)
        {
            _gameService = gamesService;
        }

        [HttpGet]
        public ActionResult<List<Vehicle>> Get() =>
            _gameService.Get();

        [HttpGet("{id:length(24)}", Name = "GetGame")]
        public ActionResult<Vehicle> Get(string id)
        {
            var game = _gameService.Get(id);

            if (game == null)
            {
                return NotFound();
            }

            return game;
        }

        [HttpPost]
        public ActionResult<Vehicle> Create(Vehicle game)
        {
            _gameService.Create(game);

            return CreatedAtRoute("GetGame", new { id = game.Id.ToString() }, game);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Vehicle gameIn)
        {
            var game = _gameService.Get(id);

            if (game == null)
            {
                return NotFound();
            }

            _gameService.Update(id, gameIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var game = _gameService.Get(id);

            if (game == null)
            {
                return NotFound();
            }

            _gameService.Delete(game.Id);

            return NoContent();
        }
    }
}