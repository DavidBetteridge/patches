namespace Patches;

internal static class Solver
{
    public static Location? FindCommonCell(Board board, List<PossiblePlacement> placements, int[,] occupied)
    {
        // Work out the overlaps
        var counts = new int[board.Width, board.Height];
        foreach (var placement in placements)
        {
            foreach (var (x,y) in placement.Iterate())
                counts[x, y] += 1;
        }

        foreach (var (x, y) in board.Iterate())
        {
            if (occupied[x, y] == 0 && counts[x, y] == placements.Count)
            {
                // This square is currently empty,  but all possible places of this piece will include it.
                return new Location
                {
                    X = x,
                    Y = y
                };
            }
        }

        return null;
    }

    public static bool NotAlreadyOccupied(int pieceNumber, PossiblePlacement placement, int[,] occupied)
    {
        foreach (var (x, y) in placement.Iterate())
        {
            if (occupied[x, y] != 0 && occupied[x, y] != pieceNumber)
                return false;
        } 

        return true;
    }

    public static bool PlacementShapeIs(Location topLeft, Location bottomRight, Shape shape)
    {
        var width = (bottomRight.X - topLeft.X) + 1;
        var height = (bottomRight.Y - topLeft.Y) + 1;

        switch (shape)
        {
            case Shape.Any:
                return true;
            case Shape.Square:
                return width == height;
            case Shape.WideRectangle:
                return width > height;
            case Shape.TallRectangle:
                return width < height;
            default:
                throw new ArgumentOutOfRangeException(nameof(shape), shape, null);
        }
    }

    public static IEnumerable<PossiblePlacement> PossiblePlacements(int width, int height,
        Piece piece)
    {
        for (var bottomRightX = piece.BottomRight.X; bottomRightX < width; bottomRightX++)
        {
            for (var bottomRightY = piece.BottomRight.Y; bottomRightY < height; bottomRightY++)
            {
                for (var topLeftX = piece.TopLeft.X; topLeftX >= 0; topLeftX--)
                {
                    for (var topLeftY = piece.TopLeft.Y; topLeftY >= 0; topLeftY--)
                    {
                        yield return new PossiblePlacement {
                            TopLeft = new Location { X = topLeftX, Y = topLeftY },
                            BottomRight = new Location { X = bottomRightX, Y = bottomRightY } 
                            };
                    }
                }
            }
        }
    }

    public static void Display(int[,] occupied)
    {
        var boardWidth = occupied.GetUpperBound(0)+1;
        var boardHeight = occupied.GetUpperBound(1)+1;
        
        Console.WriteLine();
        Console.WriteLine();
            
        for (var y = 0; y < boardHeight; y++)
        {
            for (var x = 0; x < boardWidth; x++)
            {
                if (occupied[x, y] == 0)
                    Console.Write('.');
                else
                    Console.Write((char)('A' + (occupied[x, y] - 1)));
            }

            Console.WriteLine();
        }
    }
}