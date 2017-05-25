using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using System.Net.Sockets;
using TheServer.TheModel;
using TheServer.TheMazeGame;

namespace TheServer.TheController.Commands.SinglePlayerCommands
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="TheServer.TheController.Commands.SinglePlayerCommands.SinglePlayerCommand" />
    public class GenerateSinglePlayerMazeCommand:SinglePlayerCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenerateSinglePlayerMazeCommand"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public GenerateSinglePlayerMazeCommand(IModel model):base(model){}


        /// <summary>
        /// Executes the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="player">The player.</param>
        /// <returns></returns>
        public override string Execute(string[] args,Player player)
        {
            if (args.Length != 3)
            {
                return "Error: Incorrect number of arguments";
            }
            string name = args[0];
            try
            {
                int rows = Convert.ToInt32(args[1]);
                int cols = Convert.ToInt32(args[2]);
                try
                {
                    Maze maze = IModel.GenerateteSinglePlayerMaze(name, rows, cols, player);
                    return maze.ToJSON();
                }
                catch
                {
                    return "Error: there is another maze with the same name";
                }
            }
            catch
            {
                return "Error: One of the second or third argument isn't a valid number";
            }
           
          
        }
    }
}

