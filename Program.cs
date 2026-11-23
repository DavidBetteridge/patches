
using Patches;



const int boardWidth = 6;
const int boardHeight = 6;

var pieces = new List<Piece>
{
    new Piece
    {
        Number = 1,
        BottomRight = new Location { X = 0, Y = 0 },
        Shape = Shape.WideRectangle,
        Size = 2,
        TopLeft = new Location { X = 0, Y = 0 }
    },
    
    new Piece
    {
        Number = 2,
        BottomRight = new Location { X = 5, Y = 0 },
        Shape = Shape.Any,
        Size = null,
        TopLeft = new Location { X = 5, Y = 0 }
    },
    
    new Piece
    {
        Number = 3,
        BottomRight = new Location { X = 1, Y = 1 },
        Shape = Shape.TallRectangle,
        Size = 2,
        TopLeft = new Location { X = 1, Y = 1 }
    },
    
    new Piece
    {
        Number = 4,
        BottomRight = new Location { X = 4, Y = 1 },
        Shape = Shape.Square,
        Size = null,
        TopLeft = new Location { X = 4, Y = 1 }
    },
    
    new Piece
    {
        Number = 5,
        BottomRight = new Location { X = 2, Y = 2 },
        Shape = Shape.TallRectangle,
        Size = 2,
        TopLeft = new Location { X = 2, Y = 2 }
    },
    
    new Piece
    {
        Number = 6,
        BottomRight = new Location { X = 3, Y = 2 },
        Shape = Shape.TallRectangle,
        Size = 2,
        TopLeft = new Location { X = 3, Y = 2 }
    },
    
    
    new Piece
    {
        Number = 7,
        BottomRight = new Location { X = 2, Y = 3 },
        Shape = Shape.TallRectangle,
        Size = 3,
        TopLeft = new Location { X = 2, Y = 3 }
    },
    
    new Piece
    {
        Number = 8,
        BottomRight = new Location { X = 3, Y = 3 },
        Shape = Shape.TallRectangle,
        Size = 3,
        TopLeft = new Location { X = 3, Y = 3 }
    },
    
    new Piece
    {
        Number = 9,
        BottomRight = new Location { X = 1, Y = 4 },
        Shape = Shape.TallRectangle,
        Size = null,
        TopLeft = new Location { X = 1, Y = 4 }
    },
    
    new Piece
    {
        Number = 10,
        BottomRight = new Location { X = 4, Y = 4 },
        Shape = Shape.TallRectangle,
        Size = 3,
        TopLeft = new Location { X =4, Y = 4 }
    },
    
    new Piece
    {
        Number = 11,
        BottomRight = new Location { X = 0, Y = 5 },
        Shape = Shape.Any,
        Size = null,
        TopLeft = new Location { X = 0, Y = 5 }
    },
    
    new Piece
    {
        Number = 12,
        BottomRight = new Location { X = 5, Y = 5 },
        Shape = Shape.TallRectangle,
        Size = 3,
        TopLeft = new Location { X =5, Y = 5 }
    },
    
};

while (true)
{
    var occupied = new int[boardWidth, boardHeight];
    foreach (var piece in pieces)
    {
        for (var x = piece.TopLeft.X; x <= piece.BottomRight.X; x++)
        {
            for (var y = piece.TopLeft.Y; y <= piece.BottomRight.Y; y++)
            {
                occupied[x, y] = piece.Number;
            }
        }
    }

    Solver.Display(boardWidth, boardHeight, occupied); 
    
    foreach (var piece in pieces)
    {
        // How can this piece grow?
        var placements = Solver.PossiblePlacements(boardWidth, boardHeight, piece)
            .Where(placement => !Equals(placement.TopLeft, piece.TopLeft) || !Equals(placement.BottomRight, piece.BottomRight) )
            .Where(placement => piece.Size is null || piece.Size == Solver.Size(placement.TopLeft, placement.BottomRight))
            .Where(placement => Solver.PlacementShapeIs(placement.TopLeft, placement.BottomRight, piece.Shape))
            .Where(placement => Solver.NotAlreadyOccupied(piece.Number, placement.TopLeft, placement.BottomRight, occupied))
            .ToList();

        if (placements.Count > 0)
        {
            var commonLocation = Solver.FindCommonCell(boardWidth, boardHeight, placements, occupied);
            if (commonLocation is not null)
            {
                // All possible places for this cell,  include this location.
                var growTopLeft = commonLocation.Value.X < piece.TopLeft.X || commonLocation.Value.Y < piece.TopLeft.Y;
                if (growTopLeft)
                    piece.TopLeft = new Location
                    {
                        X = Math.Min(piece.TopLeft.X, commonLocation.Value.X),
                        Y = Math.Min(piece.TopLeft.Y, commonLocation.Value.Y)
                    };
                else
                    piece.BottomRight = new Location
                    {
                        X = Math.Max(piece.BottomRight.X, commonLocation.Value.X),
                        Y = Math.Max(piece.BottomRight.Y, commonLocation.Value.Y)
                    };
                
                break;
            }
        }
    }

    Console.ReadKey(true);
}




return;

static class Solver
{
    public static Location? FindCommonCell(int width, int height,
        List<(Location TopLeft, Location BottomRight)> placements,
        int[,] occupied)
    {
        // Work out the overlaps
        var counts = new int[width, height];
        foreach (var (topLeft, bottomRight) in placements)
        {
            for (var x = topLeft.X; x <= bottomRight.X; x++)
            {
                for (var y = topLeft.Y; y <= bottomRight.Y; y++)
                    counts[x, y] += 1;
            }
        }

        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
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
        }

        return null;
    }

    public static bool NotAlreadyOccupied(int pieceNumber, Location topLeft, Location bottomRight, int[,] occupied)
    {
        for (var x = topLeft.X; x <= bottomRight.X; x++)
        {
            for (var y = topLeft.Y; y <= bottomRight.Y; y++)
            {
                if (occupied[x, y] != 0 && occupied[x, y] != pieceNumber)
                    return false;
            }
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

    public static int Size(Location topLeft, Location bottomRight)
    {
        var width = (bottomRight.X - topLeft.X) + 1;
        var height = (bottomRight.Y - topLeft.Y) + 1;
        return width * height;
    }

    public static IEnumerable<(Location TopLeft, Location BottomRight)> PossiblePlacements(int width, int height,
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
                        yield return (
                            new Location { X = topLeftX, Y = topLeftY },
                            new Location { X = bottomRightX, Y = bottomRightY });
                    }
                }
            }
        }
    }

    public static void Display(int boardWidth, int boardHeight, int[,] occupied)
    {
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

namespace Patches
{
    internal struct Location
    {
        public required int X { get; set; }
        public required int Y { get; set; }
        
        
    }

    internal enum Shape
    {
        Any,
        Square,
        TallRectangle,
        WideRectangle
    }

    internal record Piece
    {
        public required int Number { get; set; }
        public required Location TopLeft { get; set; }
        public required Location BottomRight { get; set; }
        public required int? Size { get; set; }
        public required Shape Shape { get; set; }
    }
}


