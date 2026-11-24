namespace Patches;

internal static class LeanExporter
{
    public static void Write(string fileName, uint width, uint height, IEnumerable<Setup> setups,
        IEnumerable<Step> steps)
    {
        using var writer = new StreamWriter(fileName);
        writer.WriteLine("import Patches");
        writer.WriteLine();
        writer.WriteLine("open Patches");
        writer.WriteLine();

        writer.WriteLine("def board0 : Board :=");
        writer.WriteLine("  { width := " + width + ", height := " + height + ", pieces := [");
        foreach (var piece in setups)
        {
            var size = piece.Size is null ? "none" : $"some {piece.Size.Value}";
            writer.WriteLine(
                $"    {{ number := {piece.PieceNumber}, shape := .{ShapeName(piece.Shape)}, targetSize := {size}, " +
                $"rect := {{ left := {piece.X}, top := {piece.Y}, right := {piece.X}, bottom := {piece.Y} }} }},");
        }
        writer.WriteLine("  ] }");
        writer.WriteLine();
        writer.WriteLine("theorem initialBoardIsValid : initialBoardValid board0 = true := by decide");

        var previousBoard = "board0";
        var index = 0;
        foreach (var step in steps)
        {
            index++;
            var nextBoard = $"board{index}";
            var rule = step.RuleName switch
            {
                "Rule1" => "Rule1Valid",
                "Rule2" => "Rule2Valid",
                _ => throw new ArgumentOutOfRangeException(nameof(step.RuleName), step.RuleName, null)
            };
            writer.WriteLine();
            writer.WriteLine(
                $"theorem move{index}IsValid : {rule} {previousBoard} {step.PieceNumber} {step.X} {step.Y} = true := by decide");
            writer.WriteLine(
                $"def {nextBoard} : Board := applyMove {previousBoard} {step.PieceNumber} {step.X} {step.Y}");
            previousBoard = nextBoard;
        }

        writer.WriteLine();
        writer.WriteLine($"theorem solutionIsValid : solutionValid {previousBoard} = true := by decide");
    }

    private static string ShapeName(Shape shape) => shape switch
    {
        Shape.Any => "any",
        Shape.Square => "square",
        Shape.TallRectangle => "tallRectangle",
        Shape.WideRectangle => "wideRectangle",
        _ => throw new ArgumentOutOfRangeException(nameof(shape), shape, null)
    };
}