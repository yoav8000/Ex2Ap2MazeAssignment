using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using TheServer.TheModel;
using MazeLib;
using TheServer.TheMazeGame;

namespace TheServer.TheController.Commands.MultiPlayerCommands
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="TheServer.TheController.Commands.MultiPlayerCommands.MultiPlayerCommand" />
    public class JoinMazeCommand:MultiPlayerCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JoinMazeCommand"/> class.
        /// </summary>
        /// <param name="imodel">The imodel.</param>
        public JoinMazeCommand(IModel imodel):base(imodel) { }

        /// <summary>
        /// Executes the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="player">The player.</param>
        /// <returns></returns>
        public override string Execute(string[] args, Player player)
        {
            if (args.Length != 1)
            {
                return "Error: Incorrect number of arguments";
            }

            try
            {
                Maze maze = IModel.JoinMaze(args[0],player);
                if (maze == null)
                {
                    return null;
                }


                return maze.ToJSON();
            }
            catch(Exception e)
            {
                return $"Error: There is no such maze with the name - {args[0]}";
            }
        }


    }
}

