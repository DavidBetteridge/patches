namespace Patches;

internal readonly struct Location
{
    public required int X { get; init; }
    public required int Y { get; init; }

    public Location GrowToTheTopLeft(Location mustInclude)
    {
        return new Location
        {
            X = Math.Min(X, mustInclude.X),
            Y = Math.Min(Y, mustInclude.Y)
        };
    }
        
    public Location GrowToTheBottomRight(Location mustInclude)
    {
        return new Location
        {
            X = Math.Max(X, mustInclude.X),
            Y = Math.Max(Y, mustInclude.Y)
        };
    }
        
}