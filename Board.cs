namespace Patches;

internal class Board(string name, uint boardWidth, uint boardHeight, List<Piece> pieces)
{
    public string Name { get; } = name;
    public uint Width { get; } = boardWidth;
    public uint Height { get; } = boardHeight;
    public List<Piece> Pieces { get; } = pieces;
    
    public IEnumerable<(uint x, uint y)> Iterate()
    {
        for (uint x = 0; x < Width; x++)
        {
            for (uint y = 0; y < Height; y++)
            {
                yield return (x, y);
            }
        }
    }
    
}
