using Patches;
    
var board = SetupBoard.Game1();

// Keep looping until we don't find a valid move
var moved = true;
while (moved)
{
    moved = false;
    
    // Create a 2d array of the board, and mark on it any pieces we are 100% certain about.
    var occupied = new uint[board.Width, board.Height];
    foreach (var piece in board.Pieces)
    {
        foreach (var (x, y) in piece.Iterate())
            occupied[x, y] = piece.Number;
    } 

    // Display our marked up array to the console.
    Solver.Display(occupied); 
    
    /*
     * Our solver uses two rules
     * 1. If a cell can only be reached by a single piece,  then that piece should grow to cover that square.
     * 2. If all possible growths of a piece include a given square,  then that piece should grow to cover that square.
     *
     * Note, we don't try and solve the board in the fewest moves,  we just try to make process on a single piece in each round.
     */
    
    
    // For Rule (1) we need track how many pieces could possibly grow into each cell.  We only need to track three conditions
    // -1 - multiple pieces can grow into the cell
    //  0 - no pieces can grow into the cell.  (This is bad news!)
    // +ve - the id of the only piece which can grow into the cell.
    var conflicts = new int[board.Width, board.Height];
    
    foreach (var piece in board.Pieces)
    {
        // For each piece,  we work out all the possible ways it can grow on the board...
        var placements = Solver.PossiblePlacements(board.Width, board.Height, piece)
            .Where(placement => !Equals(placement.TopLeft, piece.TopLeft) || !Equals(placement.BottomRight, piece.BottomRight) )
            
            // ... whilst ensuring any size 
            .Where(placement => piece.Size is null || piece.Size == placement.Size())
            
            // and shape constraints are honoured.
            .Where(placement => Solver.PlacementShapeIs(placement.TopLeft, placement.BottomRight, piece.Shape))
            
            .Where(placement => Solver.NotAlreadyOccupied(piece.Number, placement, occupied))
            .ToList();

        if (placements.Count > 0)
        {
            // For Rule (2), do all the possibilities for this piece include the same common cell? 
            var commonLocation = Solver.FindCommonCell(board, placements, occupied);
            if (commonLocation is not null)
            {
                // All possible places for this cell,  include this location.
                piece.GrowToInclude(commonLocation.Value);
                moved = true;
            }

            // There is not a common cell,  so instead we need to calculate all the places this piece could reach
            // for Rule (1)
            foreach (var placement in placements)
            {
                foreach (var (x,y) in placement.Iterate())
                {
                    if (conflicts[x, y] == 0)
                        conflicts[x, y] = (int)piece.Number;  // The only piece so far to get to this cell
                    else if (conflicts[x, y] > 0 && conflicts[x, y] != piece.Number)
                        conflicts[x, y] = -1; // Multiple pieces can go here. 
                }
            }
            
        }
    }

    // Rule (1) we need to check if there is a cell which can only be reached by a single piece growing.
    foreach (var (x,y) in board.Iterate())
    {
        if (conflicts[x, y] > 0)
        {
            // Only a single piece can go here.
            var piece = board.Pieces.Single(p => p.Number == conflicts[x, y]);
            piece.GrowToInclude(new Location { X = x, Y = y});
            
            moved = true;
        }
    }
}
