namespace Patches;

internal readonly struct PossiblePlacement
{
    public required Location TopLeft { get; init; }
    public required Location BottomRight { get; init; }
    
    public int Size()
    {
        var width = (BottomRight.X - TopLeft.X) + 1;
        var height = (BottomRight.Y - TopLeft.Y) + 1;
        return width * height;
    }
    
    public IEnumerable<(int x, int y)> Iterate()
    {
        for (var x = TopLeft.X; x <= BottomRight.X; x++)
        {
            for (var y = TopLeft.Y; y <= BottomRight.Y; y++)
            {
                yield return (x, y);
            }
        }
    }
    
}