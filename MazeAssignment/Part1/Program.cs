using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeGeneratorLib;
using MazeLib;
using SearchAlgorithmsLib;
namespace Part1
{
    class Program
    {
        /// <summary>
        /// Mains the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        static void Main(string[] args)
        {
            DFSMazeGenerator mazeGen = new DFSMazeGenerator();
            Maze maze = mazeGen.Generate(50, 50);
            Console.WriteLine(maze.ToString());
            MazeAdapter mazeAdapter = new MazeAdapter(maze);
            SearchAlgorithmFactory<Position> factory = new SearchAlgorithmFactory<Position>();
            ISearcher<Position> bfsSearcher = factory.GetSearchAlgorithm("bfs");
            ISearcher<Position> dfsSearcher = factory.GetSearchAlgorithm("dfs");
            Solution<Position> solution1 = bfsSearcher.Search(mazeAdapter);
            SolutionAdapter solutionAd1 = new SolutionAdapter(solution1, "bla");
            Solution<Position> solution2 = dfsSearcher.Search(mazeAdapter);
            SolutionAdapter solutionAd2 = new SolutionAdapter(solution2, "bla");
            string a = solutionAd1.ToJson();
            Console.WriteLine("the bfs solved the maze int {0}", solution1.NodesEvaluated);
            Console.WriteLine("the dfs solved the maze int {0}", solution2.NodesEvaluated);
            Console.ReadKey();
        }
    }
}