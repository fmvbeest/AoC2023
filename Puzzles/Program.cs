using System.Reflection;
using AoC2023.Puzzles;

namespace AoC2023;

public static class Program
{
     public static void Main(string[] args)
     {
          var puzzles = Assembly.GetExecutingAssembly().GetTypes()
               .Where(t => t.GetInterfaces().Contains(typeof(IPuzzle)) 
                           && !t.Name.StartsWith(nameof(PuzzleBase<object,object, object>)))
               .OrderBy(p => int.Parse(p.Name[6..])).ToList();

          var implementedPuzzles = puzzles.Select(p => int.Parse(p.Name[6..])).ToArray();
          
          Console.WriteLine("*** AoC2023 ***");
          Console.WriteLine($"Choose a puzzle to run [{string.Join(", ", implementedPuzzles)}]");
          string? choice = null;

          while (string.IsNullOrEmpty(choice))
          {
               Console.Write("Enter puzzle: ");
               choice = Console.ReadLine();
          }

          if (int.TryParse(choice, out var selection))
          {
               if (implementedPuzzles.Contains(selection))
               {
                    var selectedPuzzle = puzzles.First(p => int.Parse(p.Name[6..]).Equals(selection));
                    var instance = Activator.CreateInstance(selectedPuzzle) as IPuzzle;
                    instance?.Run();
                    Environment.Exit(0);
               }
          }

          foreach (var instance in puzzles.Select(puzzle => Activator.CreateInstance(puzzle) as IPuzzle))
          {
               instance?.Run();
          }
          
     }
}