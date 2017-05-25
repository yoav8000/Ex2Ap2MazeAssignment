using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TheServer.TheController;
using TheServer.TheMazeGame;


namespace TheServer.TheView
{
    /// <summary>
    /// IClientHandler interface.
    /// </summary>
    public interface IClientHandler
    {
        /// <summary>
        /// Handles the client.
        /// </summary>
        /// <param name="player">The player.</param>
        void HandleClient(Player player);

        /// <summary>
        /// Gets the i controller.
        /// </summary>
        /// <value>
        /// The i controller.
        /// </value>
        IController IController
        {
            get;
        }


    }

    




}