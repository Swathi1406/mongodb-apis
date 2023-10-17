namespace mongodb_dotnet_sample.Models
{
    public class VehiclesDatabaseSettings : IVehiclesDatabaseSettings
    {
        public string VehiclesCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IVehiclesDatabaseSettings
    {
        string VehiclesCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}