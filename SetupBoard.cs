namespace Patches;

internal static class SetupBoard
{
    public static Board Game1()
    {
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
        
        return new Board(6, 6, pieces);
    }
    
        public static Board Game2()
    {
        var pieces = new List<Piece>
        {
            //Row 1
            new Piece
            {
                Number = 1,
                BottomRight = new Location { X = 1, Y = 0 },
                Shape = Shape.TallRectangle,
                Size = 2,
                TopLeft = new Location { X = 1, Y = 0 }
            },
            
            //Row 2
            new Piece
            {
                Number = 2,
                BottomRight = new Location { X = 0, Y = 1 },
                Shape = Shape.Any,
                Size = 2,
                TopLeft = new Location { X = 0, Y = 1 }
            },
            
            new Piece
            {
                Number = 3,
                BottomRight = new Location { X = 2, Y = 1 },
                Shape = Shape.Square,
                Size = 4,
                TopLeft = new Location { X = 2, Y = 1 }
            },
            
            new Piece
            {
                Number = 4,
                BottomRight = new Location { X = 4, Y = 1 },
                Shape = Shape.Any,
                Size = 4,
                TopLeft = new Location { X = 4, Y = 1 }
            },
            
            // Row 5
            
            new Piece
            {
                Number = 5,
                BottomRight = new Location { X = 1, Y = 4 },
                Shape = Shape.Any,
                Size = 6,
                TopLeft = new Location { X = 1, Y = 4 }
            },
            
            new Piece
            {
                Number = 6,
                BottomRight = new Location { X = 3, Y = 4 },
                Shape = Shape.TallRectangle,
                Size = 6,
                TopLeft = new Location { X = 3, Y = 4 }
            },
            
            
            new Piece
            {
                Number = 7,
                BottomRight = new Location { X = 5, Y = 4 },
                Shape = Shape.Any,
                Size = 6,
                TopLeft = new Location { X = 5, Y = 4 }
            },
            
            //Row 6
            new Piece
            {
                Number = 8,
                BottomRight = new Location { X = 4, Y = 5 },
                Shape = Shape.WideRectangle,
                Size = 6,
                TopLeft = new Location { X = 4, Y = 5 }
            },
            
        };
        
        return new Board(6, 6, pieces);
    }
    
}