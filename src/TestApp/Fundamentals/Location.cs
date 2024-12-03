namespace TestApp.Fundamentals;

public record Location(double Latitude, double Longitude)
{
    public double Altitude { get; init; }
}
