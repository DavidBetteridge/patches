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
        var piece = _piece ?? throw new InvalidOperationException("No piece is being tracked.");
        var ruleName = _ruleName ?? throw new InvalidOperationException("No rule is being tracked.");

        if (!Equals(_topLeft, piece.TopLeft))
        {
            _steps.Add(new Step
            {
                RuleName = ruleName,
                PieceNumber = piece.Number,
                X = piece.TopLeft.X,
                Y = piece.TopLeft.Y,
            });
        }

        if (!Equals(_bottomRight, piece.BottomRight))
        {
            _steps.Add(new Step
            {
                RuleName = ruleName,
                PieceNumber = piece.Number,
                X = piece.BottomRight.X,
                Y = piece.BottomRight.Y,
            });
        }
    }

    public void OutputToLeanFile(string fileName)
    {
        const string leanDirectory = "Lean";
        Directory.CreateDirectory(leanDirectory);
        var outputPath = Path.Combine(leanDirectory, Path.GetFileName(fileName));
        LeanExporter.Write(outputPath, _board.Width, _board.Height, _setups, _steps);
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
    public required string RuleName { get; init; }
    public required uint PieceNumber { get; init; }
    public required uint X { get; init; }
    public required uint Y { get; init; }
}