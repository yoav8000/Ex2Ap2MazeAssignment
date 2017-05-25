using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TheServer.TheModel;
using TheServer.TheMazeGame;
using MazeLib;
namespace TheServer.TheController.Commands.MultiPlayerCommands
{
    /// <summary>
    /// StartCommand class.
    /// </summary>
    /// <seealso cref="TheServer.TheController.Commands.MultiPlayerCommands.MultiPlayerCommand" />
    class StartCommand : MultiPlayerCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StartCommand"/> class.
        /// </summary>
        /// <param name="imodel">The imodel.</param>
        public StartCommand(IModel imodel):base(imodel){}

        /// <summary>
        /// Executes the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="player">The player.</param>
        /// <returns></returns>
        public override string Execute(string[] args, Player player)
        {
            if (args.Length != 3)
            {
                return "Error: Incorrect number of arguments";
            }

            string mazeName = args[0];
            int rows = 0;
            int cols = 0;
            try
            {
                rows = Convert.ToInt32(args[1]);
                cols = Convert.ToInt32(args[2]);
            }
            catch (FormatException exception)
            {
                return "Error: One of the second and third argument isn't a valid number";
            }
            string result = IModel.GenerateMultiPlayerMaze(mazeName, rows, cols, player);
            WaitForOtherPlayerToJoinTheGame(player);

            return result;

        }


        private void WaitForOtherPlayerToJoinTheGame(Player p)
        {
            while (p.NeedToWait)
            {
                System.Threading.Thread.Sleep(1000);
            }
        }

    }

}

