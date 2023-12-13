using AoC2023.Util;

namespace AoC2023.Puzzles;

public class Puzzle13 : PuzzleBase<IEnumerable<IEnumerable<string>>, int, int>
{
    protected override string Filename => "Input/puzzle-input-13.txt";
    protected override string PuzzleTitle => "--- Day 13: Point of Incidence ---";

    private static int[][] ConvertToIntGrid(IEnumerable<string> input, Dictionary<char, int> map)
    {
        var pattern = input.ToArray();
        var x = pattern.Length;

        var grid = new int[x][];
        foreach (var (s, i) in pattern.Select((s, i) => (s, i)))
        {
            grid[i] = s.Select(c => map[c]).ToArray();
        }

        return grid;
    }

    private static int HammingDistance(int[] a, int[] b)
    {
        return a.Zip(b, (x1, x2) => x1 ^ x2).Sum();
    }

    private static int FindMirror(int[][] grid, int hd)
    {
        for (var i = 1; i < grid.Length; i++)
        {
            if (HammingDistance(grid[i], grid[i-1]) <= hd)
            {
                var k = Math.Min(i, grid.Length - i);

                var a = grid[(i - k)..i];
                var b = grid[i..(i + k)];

                var chd = a.Select((t, j) => HammingDistance(t, b[a.Length - 1 - j])).Sum();

                if (chd == hd)
                {
                    return i;
                }
            }
        }

        return 0;
    }

    private int RunMirrorSearch(IEnumerable<IEnumerable<string>> input, int hd)
    {
        var h = 0;
        var v = 0;
        foreach (var pattern in input)
        {
            var grid = ConvertToIntGrid(pattern, new Dictionary<char, int> { {'.', 0}, {'#', 1 } });

            h += FindMirror(grid, hd);
            var transpose = grid.Transpose().Select(x => x.ToArray()).ToArray();
            v += FindMirror(transpose, hd);
        }
        
        return h * 100 + v;
    }

    public override int PartOne(IEnumerable<IEnumerable<string>> input)
    {
        return RunMirrorSearch(input, hd: 0);
    }

    public override int PartTwo(IEnumerable<IEnumerable<string>> input)
    {
        return RunMirrorSearch(input, hd: 1);
    }
    
    public override IEnumerable<IEnumerable<string>> Preprocess(IPuzzleInput input, int part = 1)
    {
        return input.GetChunkedInput();
    }
}