using mongodb_dotnet_sample.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace mongodb_dotnet_sample.Services
{
    public class VehiclesService
    {
        private readonly IMongoCollection<Vehicle>? _games;

        public VehiclesService(IVehiclesDatabaseSettings settings)
        {
            try {
                var client = new MongoClient(settings.ConnectionString);
                var database = client.GetDatabase(settings.DatabaseName);

                _games = database.GetCollection<Vehicle>(settings.VehiclesCollectionName);
            }catch(Exception e) {
                Console.WriteLine(e.StackTrace);
                throw e;
            }
            
        }

        public List<Vehicle> Get() => _games.Find(game => true).ToList();

        public Vehicle Get(string id) => _games.Find(game => game.Id == id).FirstOrDefault();

        public Vehicle Create(Vehicle game)
        {
            _games.InsertOne(game);
            return game;
        }

        public void Update(string id, Vehicle updatedGame) => _games.ReplaceOne(game => game.Id == id, updatedGame);

        public void Delete(Vehicle gameForDeletion) => _games.DeleteOne(game => game.Id == gameForDeletion.Id);

        public void Delete(string id) => _games.DeleteOne(game => game.Id == id);
    }
}