public record BuildingProperties(double groundArea,int numberOfLevels, string usage);

public enum Usage
{
    Hospital,
    Housing,
    Shop
}