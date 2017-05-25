using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TheServer.TheController.Commands;
using TheServer.TheModel;
using TheServer.TheView;
using TheServer.TheMazeGame;

namespace TheServer.TheController
{
    public interface IController
    {
        

        string ExecuteCommand(string command, Player player);

        Dictionary<string, ICommand> CommandDictionary
        {
            get;
        }

        IModel IModel
        {
            get;
            set;
        }

        IClientHandler IClientHandler
        {
            get;
            set;
        }

        ICommand GetCommand(string commandLine);
        

    }
}
