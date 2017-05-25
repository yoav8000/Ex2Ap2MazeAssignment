using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TheServer.TheModel;
using TheServer.TheMazeGame;

namespace TheServer.TheController.Commands.SinglePlayerCommands
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="TheServer.TheController.Commands.SinglePlayerCommands.SinglePlayerCommand" />
    public class SolveMazeCommand:SinglePlayerCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SolveMazeCommand"/> class.
        /// </summary>
        /// constructor.
        /// <param name="imodel">The imodel.</param>
        /// constructor.
        public SolveMazeCommand(IModel imodel) : base(imodel) { }

        /// <summary>
        /// Executes the specified arguments.
        /// </summary>
        /// uses the algorithm to solve the maze.
        /// <param name="args">The arguments.</param>
        /// <param name="player">The player.</param>
        /// <returns></returns>
        /// executes the command.
        public override string Execute(string[] args, Player player)
        {
            if (args.Length != 2)
            {
                return "Error: Incorrect number of arguments";
            }

            string mazeName = args[0];
            int algorithmIndicator = -1;
            string theAlgorithm;
            try
            {
                algorithmIndicator = Convert.ToInt32(args[1]);
            }


            catch (FormatException exception)
            {
                return "Error: The second argument isn't a number";
            }
            switch (algorithmIndicator)
            {
                case 0:
                    {
                        theAlgorithm = "bfs";
                        break;
                    }
                case 1:
                    {
                        theAlgorithm = "dfs";
                        break;
                    }
                default:
                    {
                        return "Error: There is no such Algorithm";
                    }
            }

            return IModel.SolveMaze(mazeName, theAlgorithm, player);
        }



    }
}
