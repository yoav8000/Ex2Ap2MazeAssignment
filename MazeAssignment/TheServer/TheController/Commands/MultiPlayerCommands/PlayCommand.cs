using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TheServer.TheModel;
using TheServer.TheMazeGame;
using Newtonsoft.Json.Linq;
using MazeLib;

namespace TheServer.TheController.Commands.MultiPlayerCommands
{

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="TheServer.TheController.Commands.MultiPlayerCommands.MultiPlayerCommand" />
    class PlayCommand : MultiPlayerCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayCommand"/> class.
        /// </summary>
        /// <param name="imodel">The imodel.</param>
        public PlayCommand(IModel imodel):base(imodel){ }


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
            Direction direction;
            if (Direction.TryParse(args[0], out direction))
            {
                string result = IModel.Play(args, player);
                return result;
            }
            return "Error: the direction is not defined";

         
        }

    }
}
