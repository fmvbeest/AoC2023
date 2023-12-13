namespace AoC2023.Puzzles;

public class PuzzleInput : IPuzzleInput
{
    private readonly string[] _allLines;
    private readonly string _filename;

    public PuzzleInput(string filename)
    {
        _allLines = File.ReadAllLines(filename);
        _filename = filename;
    }

    public IEnumerable<string> GetAllLines()
    {
        return _allLines;
    }
    
    public string GetText()
    {
        return File.ReadAllText(_filename);
    }

    public IEnumerable<IEnumerable<string>> GetChunkedInput()
    {
        var index = 0;
        return _allLines.
            GroupBy(x => !string.IsNullOrEmpty(x) ? index : index++)
            .Select(group => group.Where(s => !string.IsNullOrEmpty(s)));
    }

    public string GetFirstLine()
    {
        return _allLines[0];
    }
}