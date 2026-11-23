namespace Patches;

internal record Piece
{
    public required int Number { get; init; }
    public required int? Size { get; init; }
    public required Shape Shape { get; init; }
    public required Location TopLeft { get; set; }
    public required Location BottomRight { get; set; }

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