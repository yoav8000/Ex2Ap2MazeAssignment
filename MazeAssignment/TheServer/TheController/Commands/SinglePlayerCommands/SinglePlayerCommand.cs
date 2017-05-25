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
    /// SinglePlayerCommand class.
    /// </summary>
    /// <seealso cref="TheServer.TheController.Commands.AbstractCommand" />
    public abstract class SinglePlayerCommand:AbstractCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SinglePlayerCommand"/> class.
        /// </summary>
        /// constructor.
        /// <param name="imodel">The imodel.</param>
        public SinglePlayerCommand(IModel imodel):base(imodel) { }
        /// <summary>
        /// Executes the specified arguments.
        /// </summary>
        /// executes the command.
        /// <param name="args">The arguments.</param>
        /// <param name="player">The player.</param>
        /// <returns></returns>
        public override abstract string Execute(string[] args,Player player);

    }
}
