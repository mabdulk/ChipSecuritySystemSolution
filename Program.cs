using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Assumptions:
 * ------------
 * 1. Chip Orientation:
 *    - Chips are directional and must be used in the order they are defined.
 *    - For example, [Yellow, Red] is NOT the same as [Red, Yellow].
 *    - Chips cannot be flipped during placement.
 *
 * 2. Duplicate Chips:
 *    - Duplicate chips may exist in the input.
 *    - Each chip instance is treated as unique, even if its StartColor and EndColor match another chip.
 *
 * 3. Chip Usage:
 *    - Each chip can be used at most once in a single path.
 *    - The same chip cannot be reused in multiple positions within the same attempt.
 *
 * 4. Goal:
 *    - The path must start at the "Blue" marker and end at the "Green" marker.
 *    - Chips must be placed end-to-end such that adjacent colors match.
 *    - The objective is to find a valid path that uses the maximum number of chips possible.
 *
 * 5. Failure Case:
 *    - If no valid path from Blue to Green exists, the system should output the predefined error message.
 */

namespace ChipSecuritySystem
{
    class Program
    {
        static void Main(string[] args)
        {

            // you can use any color combination you like to test my solution
            var chips = new List<ColorChip> {
                        new ColorChip(Color.Blue, Color.Yellow),
                        new ColorChip(Color.Red, Color.Green),
                        new ColorChip(Color.Yellow, Color.Red),
                        new ColorChip(Color.Orange, Color.Purple)
                    };


            var solution = Solve(chips);


            if (solution.Count == 0)
            {
                Console.WriteLine(Constants.ErrorMessage);
            }
            else
            {
                PrintSolution(solution);
            }
        }

        private static void PrintSolution(List<ColorChip> solution)
        {
            Console.Write("Blue ");

            foreach (var chip in solution)
            {
                Console.Write($"[{chip.ToString()}] ");
            }

            Console.Write("Green\n");
        }

        static List<ColorChip> Solve(List<ColorChip> chips)
        {
            if (chips == null || chips.Count == 0) 
                return new List<ColorChip>();

            var best = new List<ColorChip>();
            DFS(Color.Blue, chips, new List<ColorChip>(), ref best);
            return best;
        }

        // Note: This implementation prioritizes readability and correctness.
        // If performance becomes a concern (e.g., with large input sizes),
        // chip tracking can be optimized to avoid full list copying.
        static void DFS( Color currentColor, List<ColorChip> remainingChips, List<ColorChip> currentPath,ref List<ColorChip> bestPath)
        {
            if (currentColor == Color.Green)
            {
                if (currentPath.Count > bestPath.Count)
                    bestPath = new List<ColorChip>(currentPath);
                return;
            }

            foreach (var chip in remainingChips)
            {
                if (chip.StartColor == currentColor)
                {
                    // Try this chip
                    currentPath.Add(chip);
                    var nextChips = new List<ColorChip>(remainingChips);
                    nextChips.Remove(chip);

                    DFS(chip.EndColor, nextChips, currentPath, ref bestPath);

                    // Backtrack
                    currentPath.RemoveAt(currentPath.Count - 1);
                }
            }
        }


    }
}
