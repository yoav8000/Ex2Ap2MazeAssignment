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
    /// closes the game.
    /// </summary>
    /// <seealso cref="TheServer.TheController.Commands.MultiPlayerCommands.MultiPlayerCommand" />
    class CloseCommand : MultiPlayerCommand
    {
        public CloseCommand(IModel imodel):base(imodel) { }

        /// <summary>
        /// Executes the specified arguments.
        /// </summary>
        /// executes the closing of the game.
        /// <param name="args">The arguments.</param>
        /// <param name="player">The player.</param>
        /// <returns></returns>
        public override string Execute(string[] args, Player player)
        {
            if (args.Length != 1)
            {
                return "Error: Incorrect number of arguments";
            }
           string result = IModel.Close(args[0], player);///activates the close method at the model.
            if (result.StartsWith("Error"))
            {
                return result;
            }
            if(result == null)
            {
                return result;
            }
            JObject jobject = new JObject();
            return jobject.ToString();
        }


    }
}
