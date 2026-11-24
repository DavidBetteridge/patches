namespace Patches;

internal class Tracker
{
    private Location _topLeft;
    private Location _bottomRight;
    private string? _ruleName;
    private Piece? _piece;
    private readonly Board _board;
    
    private readonly List<Step> _steps = [];
    private readonly List<Setup> _setups = [];

    public Tracker(Board board)
    {
        _board = board;
        foreach (var piece in board.Pieces)
        {
            _setups.Add(new Setup
            {
                PieceNumber = piece.Number,
                Size = piece.Size,
                Shape = piece.Shape,
                X = piece.TopLeft.X,
                Y = piece.TopLeft.Y
            });
        }
    }

    public void StartWatching(string ruleName, Piece piece)
    {
        _ruleName = ruleName;
        _topLeft = piece.TopLeft;
        _bottomRight = piece.BottomRight;
        _piece = piece;
    }

    public void FinishWatching()
    {
        if (!Equals(_topLeft, _piece!.TopLeft))
        {
            _steps.Add(new Step
            {
                RuleName = _ruleName,
                PieceNumber = _piece.Number,
                X = _piece.TopLeft.X,
                Y = _piece.TopLeft.Y,
            });
        }
        
        if (!Equals(_bottomRight, _piece!.BottomRight))
        {
            _steps.Add(new Step
            {
                RuleName = _ruleName,
                PieceNumber = _piece.Number,
                X = _piece.BottomRight.X,
                Y = _piece.BottomRight.Y,
            });
        }
    }

    public void OutputToConsole()
    {
        Console.WriteLine($"DefineBoard Width:={_board.Width} Height:={_board.Height}");
        
        foreach (var piece in _setups)
        {
            Console.WriteLine($"AddPiece PieceNumber:={piece.PieceNumber} X:={piece.X} Y:={piece.Y} Shape:={piece.Shape.ToString()} Size:={piece.Size.ToString()}");
        }
        
        foreach (var step in _steps)
        {
            Console.WriteLine($"Apply{step.RuleName} PieceNumber:={step.PieceNumber} X:={step.X} Y:={step.Y}");
        }
    }
}

internal record Setup
{
    public uint PieceNumber { get; init; }
    public uint? Size { get; init; }
    public Shape Shape { get; init; }
    public uint X { get; init; }
    public uint Y { get; init; }
}

internal record Step
{
    public required string? RuleName { get; init; }
    public required uint PieceNumber { get; init; }
    public required uint X { get; init; }
    public required uint Y { get; init; }
}