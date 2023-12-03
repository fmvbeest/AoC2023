using System.Reflection;

namespace DayGenerator;

public static class Program
{
    private static string FillTemplate(string template, int newDay)
    {
        return template.Replace("{{SOLUTION}}", FileUtils.GetSolutionName())
            .Replace("{{DAYNUMBER-SHORT}}", $"{newDay}")
            .Replace("{{DAYNUMBER-LONG}}", $"{newDay:D2}");
    }
    
    public static void Main(string[] args)
    {
        Console.WriteLine($"*** {FileUtils.GetSolutionName()} DayGenerator ***");
        
        var puzzleBaseDir = Path.Combine(FileUtils.GetSolutionDirectory(), "Puzzles");
        var puzzleDir = Path.Combine(puzzleBaseDir, "Puzzles");
        var testBaseDir = Path.Combine(FileUtils.GetSolutionDirectory(), "TestPuzzles");
        var templateDir = Path.Combine(FileUtils.GetProjectDirectory(), "Templates");

        var newDay = Directory.GetFiles(puzzleDir, "Puzzle*.cs")
            .Select(file => file.Split(Path.DirectorySeparatorChar).Last()
                .Replace("Puzzle", string.Empty)
                .Replace(".cs", string.Empty))
            .Except(new [] {"Base", "Input"})
            .Select(int.Parse)
            .Max() + 1;

        var filePath = Path.Combine(puzzleDir, $"Puzzle{newDay}.cs");
        
        File.WriteAllText(filePath, FillTemplate(File.ReadAllText(Path.Combine(templateDir, "Puzzle.txt")), newDay));
        Console.WriteLine($"Written {filePath}.");
                
        filePath = Path.Combine(testBaseDir, $"TestPuzzle{newDay}.cs");
        File.WriteAllText(filePath, FillTemplate(File.ReadAllText(Path.Combine(templateDir, "TestPuzzle.txt")), newDay));
        Console.WriteLine($"Written {filePath}.");
                
        filePath = Path.Combine(puzzleBaseDir, "Input", $"puzzle-input-{newDay:D2}.txt");
        File.WriteAllText(filePath, string.Empty);
        Console.WriteLine($"Written {filePath}.");
                
        filePath = Path.Combine(testBaseDir, "Input", $"test-input-{newDay:D2}.txt");
        File.WriteAllText(filePath, string.Empty);
        Console.WriteLine($"Written {filePath}.");
    }
}

public static class FileUtils
{
    private static readonly string ProjectDirectory;
    private static readonly string SolutionDirectory;
    private static readonly string SolutionName;

    static FileUtils()
    {
        ProjectDirectory = Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName;
        if (ProjectDirectory == null) throw new DirectoryNotFoundException("project directory");
        
        SolutionDirectory = Directory.GetParent(ProjectDirectory)?.FullName;
        if (SolutionDirectory == null) throw new DirectoryNotFoundException("solution directory");
        
        var solutionFile = Directory.GetFiles(SolutionDirectory, "*.sln").First();
        
        SolutionName = solutionFile.Split(Path.DirectorySeparatorChar).Last()
            .Replace(".sln", string.Empty);
    }
    
    public static string GetProjectDirectory() => ProjectDirectory;

    public static string GetSolutionDirectory() => SolutionDirectory;

    public static string GetSolutionName() => SolutionName;
}