using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TheServer.TheController;
using TheServer.TheController.Commands.SinglePlayerCommands;
using TheServer.TheMazeGame;

namespace TheServer.TheView
{
    /// <summary>
    /// ClientHandler class.
    /// </summary>
    /// <seealso cref="TheServer.TheView.IClientHandler" />
    class ClientHandler :IClientHandler
    {
        private IController icontroller;


        /// <summary>
        /// Initializes a new instance of the <see cref="ClientHandler"/> class.
        /// </summary>
        /// <param name="icontroller">The icontroller.</param>
        public ClientHandler(IController icontroller)
        {
            this.icontroller = icontroller;
        }


        /// <summary>
        /// Handles the client.
        /// </summary>
        /// <param name="player">The player.</param>
        public void HandleClient(Player player)
        {
         
            new Task(() =>
            {
                NetworkStream stream = player.Client.GetStream();
                StreamReader streamReader = new StreamReader(stream);
                StreamWriter streamWriter = new StreamWriter(stream);
                while (player.Communicate)
                {

                    try
                    {
                        string commandLine = streamReader.ReadLine();//recieve command from the client.


                        Console.WriteLine($"Got command: {commandLine}");
                        //got the command


                        string result = icontroller.ExecuteCommand(commandLine, player);//executed it.
               
                        if (result != null)
                        {
                            Console.WriteLine($"the result is: {result}");
                            streamWriter.WriteLine(result.Replace(Environment.NewLine, ""));//writes the result back to the client 
                            streamWriter.Flush();
                        }

                  

                            if (IController.GetCommand(commandLine) is SinglePlayerCommand)//check if the command has to be open.
                            {
                                System.Threading.Thread.Sleep(1000);
                                player.Communicate = false;
                                player.Client.Close();
                            }

                
                    }
                    catch
                    {

                        System.Threading.Thread.Sleep(1000);
                        player.Communicate = false;
                        player.Client.Close();
                        break;
                    }
                }
            }).Start();
        }

        /// <summary>
        /// Gets the i controller.
        /// </summary>
        /// <value>
        /// The i controller.
        /// </value>
        public IController IController
        {
            get
            {
                return this.icontroller;
            }
        }
    }
}