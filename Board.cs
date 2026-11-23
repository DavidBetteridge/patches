namespace Patches;

internal class Board(int boardWidth, int boardHeight, List<Piece> pieces)
{
    public int Width { get; } = boardWidth;
    public int Height { get; } = boardHeight;
    public List<Piece> Pieces { get; } = pieces;
    
    public IEnumerable<(int x, int y)> Iterate()
    {
        for (var x = 0; x < Width; x++)
        {
            for (var y = 0; y < Height; y++)
            {
                yield return (x, y);
            }
        }
    }
    
}
