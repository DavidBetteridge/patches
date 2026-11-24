namespace Patches;

internal record Piece
{
    public required uint Number { get; init; }
    public required uint? Size { get; init; }
    public required Shape Shape { get; init; }
    public required Location TopLeft { get; set; }
    public required Location BottomRight { get; set; }

    public IEnumerable<(uint x, uint y)> Iterate()
    {
        for (var x = TopLeft.X; x <= BottomRight.X; x++)
        {
            for (var y = TopLeft.Y; y <= BottomRight.Y; y++)
            {
                yield return (x, y);
            }
        }
    }

    public void GrowToInclude(Location include)
    {
        var growTopLeft = include.X < TopLeft.X || include.Y < TopLeft.Y;
        if (growTopLeft)
            TopLeft = TopLeft.GrowToTheTopLeft(include);
        else
            BottomRight = BottomRight.GrowToTheBottomRight(include);
    }
    
}