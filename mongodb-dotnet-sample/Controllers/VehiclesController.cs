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
        private readonly ILogger<VehiclesController> _logger;


        public VehiclesController(VehiclesService gamesService, ILogger<VehiclesController> logger)
        {
            _gameService = gamesService;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<List<Vehicle>> Get() {

            try {
                return _gameService.Get();

            }
            catch (Exception e) {
                _logger.LogError(e.StackTrace);
                throw e;
            }
        }

        [HttpGet("{id:length(24)}", Name = "GetGame")]
        public ActionResult<Vehicle> Get(string id)
        {
            try
            {
                var game = _gameService.Get(id);

                if (game == null)
                {
                    return NotFound();
                }

                return game;

            }
            catch (Exception e)
            {
                _logger.LogError(e.StackTrace);
                throw e;
            }
            
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