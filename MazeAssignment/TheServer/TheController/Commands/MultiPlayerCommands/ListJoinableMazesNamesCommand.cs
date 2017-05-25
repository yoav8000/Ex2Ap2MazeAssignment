using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TheServer.TheModel;
using TheServer.TheMazeGame;
using Newtonsoft.Json.Linq;

namespace TheServer.TheController.Commands.MultiPlayerCommands
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="TheServer.TheController.Commands.MultiPlayerCommands.MultiPlayerCommand" />
    public class ListJoinableMazesNamesCommand:MultiPlayerCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListJoinableMazesNamesCommand"/> class.
        /// </summary>
        /// <param name="imodel">The imodel.</param>
        public ListJoinableMazesNamesCommand(IModel imodel) : base(imodel) { }
        /// <summary>
        /// Executes the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="player">The player.</param>
        /// <returns></returns>
        public override string Execute(string[] args, Player player)
        {
            if(args.Length != 0)
            {
                return "Error the amount of arguments is not right";
            }
            List<string> result = IModel.GetNamesOfJoinableMazes(player);
            if (result == null)
            {
                return null;
            }
            return JsonConvert.SerializeObject(result, Formatting.Indented);
        }

    }
}
