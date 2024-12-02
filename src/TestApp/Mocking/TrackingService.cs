using System;
using System.IO;
using System.Text.Json;

namespace TestApp.Mocking
{
    public interface IFileReader
    {
        string ReadAllText(string path);
    }

    public class RealFileReader : IFileReader
    {
        public string ReadAllText(string path)
        {
            return File.ReadAllText("tracking.txt");
        }
    }
    
    public class TrackingService
    {
        private readonly IFileReader _fileReader;

        public TrackingService()
            : this(new RealFileReader())
        {
        }

        public TrackingService(IFileReader fileReader)
        {
            _fileReader = fileReader;
        }

        public Location Get()
        {
            try
            {
                string json = _fileReader.ReadAllText("tracking.txt");

                Location location = JsonSerializer.Deserialize<Location>(json);

                if (location == null)
                    throw new ApplicationException("Error parsing the location");

                return location;
            }
            catch (JsonException e)
            {
                throw new ApplicationException("Error parsing the location", e);
            }
        }
    }


    public class Location
    {
        public Location(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public override string ToString()
        {
            return $"{Latitude} {Longitude}";
        }

    }
}
