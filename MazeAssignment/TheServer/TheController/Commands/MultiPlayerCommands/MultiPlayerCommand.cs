using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TheServer.TheModel;
using TheServer.TheMazeGame;
namespace TheServer.TheController.Commands.MultiPlayerCommands
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="TheServer.TheController.Commands.AbstractCommand" />
    public abstract class MultiPlayerCommand:AbstractCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MultiPlayerCommand"/> class.
        /// </summary>
        /// <param name="imodel">The imodel.</param>
        public MultiPlayerCommand(IModel imodel):base(imodel) { }
        /// <summary>
        /// Executes the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="player">The player.</param>
        /// <returns></returns>
        public override abstract string Execute(string[] args, Player player);

    }
}
