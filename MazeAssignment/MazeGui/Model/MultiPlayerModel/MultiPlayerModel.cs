using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeGui.Model.AnAbstractModelClass;
using MazeLib;
using System.Collections.ObjectModel;
using MazeGui.Model.SettingsModel;
using Newtonsoft.Json;
using ClientDll;
using MazeGui.NoifyGameClosedInterface;

namespace MazeGui.Model.MultiPlayerModel
{

    /// <summary>
    /// MultiPlayerModel implements AbstractModel and INotifyGameWasClosed.
    /// </summary>
    /// <seealso cref="MazeGui.Model.AnAbstractModelClass.AbstractModel" />
    /// <seealso cref="MazeGui.NoifyGameClosedInterface.INotifyGameWasClosed" />
    public class MultiPlayerModel : AbstractModel, INotifyGameWasClosed
    {
        private ObservableCollection<string> gamesList;
        private Position otherPlayerPosition;
        private bool finishGame;
        private bool lostTheGame;
        public event GameWasClosedEventHandler GameWasClosed;






        /// <summary>
        /// Initializes a new instance of the <see cref="MultiPlayerModel"/> class.
        /// </summary>
        /// <param name="settingsModel">The settings model.</param>
        /// <param name="mazeName">Name of the maze.</param>
        public MultiPlayerModel(ISettingsModel settingsModel, string mazeName) : base(settingsModel, mazeName)
        {

            this.gamesList = new ObservableCollection<string>();
            if (MyClient == null)
            {
                MyClient = new Client(IpAddress, PortNumber);
                if (MyClient.Communicate == false)
                {
                    ConnectionError = "Connection Error";
                    return;
                }
            }
            finishGame = false;
            lostTheGame = false;
            ListCommand();
        }



        /// <summary>
        /// Gets or sets a value indicating whether [finish game].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [finish game]; otherwise, <c>false</c>.
        /// </value>
        public bool FinishGame
        {
            get
            {
                return finishGame;
            }
            set
            {
                finishGame = value;
                NotifyGameWasClosed("GameWasClosed");
            }
        }




        /// <summary>
        /// Notifies the game was closed.
        /// </summary>
        /// <param name="propName">Name of the property.</param>
        public void NotifyGameWasClosed(string propName)
        {
            if (this.GameWasClosed != null)
            {
                this.GameWasClosed("GameWasClosed");
            }
        }



        /// <summary>
        /// Generates the multiplayer maze.
        /// </summary>
        /// <param name="mazeName">Name of the maze.</param>
        public void GenerateMultiplayerMaze(string mazeName, int rows, int cols)
        {
            MazeName = mazeName;
            if (Is_Enabled)
            {
                if (MyClient == null) ///check if the client is null.
                {
                    MyClient = new Client(IpAddress, PortNumber);
                    if (MyClient.Communicate == false)
                    {
                        ConnectionError = "Connection Error";
                        return;
                    }
                }
                ///send the command to the server.
                SendMessageToServer("start" + " " + MazeName + " " + rows + " " + cols);

                ///get the answer from the server.

                string resultFromServer = RecieveMessageFromServer();
                
                
                if (resultFromServer.Contains("Maze"))
                {
                    ResultMaze = MazeLib.Maze.FromJSON(resultFromServer);
                    string temp = ResultMaze.ToString();
                    Maze = temp.Replace(Environment.NewLine, "");
                    InitialPosition = ResultMaze.InitialPos;
                    GoalPosition = ResultMaze.GoalPos;
                    PlayerPosition = ResultMaze.InitialPos;
                    OtherPlayerPosition = PlayerPosition;
                    Rows = ResultMaze.Rows.ToString();
                    Cols = ResultMaze.Cols.ToString();
                    StartListeningToServer();
                }

            }
            else
            {
                //there was a connectoin error.
                ConnectionError = "Connection Error"; 
                return;
            }
        }


        /// <summary>
        /// Closes the game command.
        /// </summary>
        public void CloseCommand()
        {
            
            SendMessageToServer("close" + " " + MazeName);

        }




        /// <summary>
        /// a play command.
        /// </summary>
        /// <param name="direcion">The direcion.</param>
        public void PlayCommand(string direcion)
        {
             SendMessageToServer("play" + " " + direcion);
            MovePlayer(direcion); // move the player locally.
           
        }


        /// <summary>
        /// Joins the maze command.
        /// </summary>
        /// <param name="mazeName">Name of the maze.</param>
        public void JoinMazeCommand(string mazeName)
        {
            MazeName = mazeName;
            if (Is_Enabled)
            {
                if (MyClient == null) // check if the client is null.
                {
                    MyClient = new Client(IpAddress, PortNumber);
                    if (MyClient.Communicate == false)
                    {
                        ConnectionError = "Connection Error";
                        return;
                    }
                }
                SendMessageToServer("join" + " " + MazeName); // sends the join command to ther server.
                string resultFromServer = RecieveMessageFromServer(); //gets the maze back.
                if (resultFromServer.Contains("Maze")) //check if it is indeed a maze.
                {
                    //update members accordingly.
                    ResultMaze = MazeLib.Maze.FromJSON(resultFromServer);
                    string temp = ResultMaze.ToString();
                    Maze = temp.Replace(Environment.NewLine, "");
                    InitialPosition = ResultMaze.InitialPos;
                    GoalPosition = ResultMaze.GoalPos;
                    PlayerPosition = ResultMaze.InitialPos;
                    OtherPlayerPosition = PlayerPosition;
                    Rows = ResultMaze.Rows.ToString();
                    Cols = ResultMaze.Cols.ToString();
                    StartListeningToServer();
                }

            }
        }








        /// <summary>
        /// Starts listening in a different thread to ther server.
        /// </summary>
        public void StartListeningToServer()
        {
            Task task = new Task(() =>//create a reading thread from the server.
            {
                try //to check if the client is null.
                {
                    while (MyClient.Communicate) //while we need to communicate.
                    {
                        try //to get a message from the server.
                        {
                            string result = RecieveMessageFromServer();


                            if (result != null)
                            {
                                //connection error occurred did action in abstract model already.
                                if (result.Contains("Connection Error")) 
                                {
                                    break;
                                }

                                string direction = null;

                                //the other player moved.
                                if (result.Contains("Direction")) //move other player.
                                {
                                    if (result.Contains("Right"))
                                    {
                                        direction = "Right";
                                    }
                                    else if (result.Contains("Left"))
                                    {
                                        direction = "Left";
                                    }
                                    else if (result.Contains("Up"))
                                    {
                                        direction = "Up";
                                    }
                                    else if (result.Contains("Down"))
                                    {
                                        direction = "Down";
                                    }

                                    //moves the other player.
                                    if (PlayerCanMove(OtherPlayerPosition, direction))
                                    {
                                        switch (direction)
                                        {
                                            case "Down":
                                                {

                                                    OtherPlayerPosition = new Position(OtherPlayerPosition.Row + 1, OtherPlayerPosition.Col);
                                                    break;
                                                }
                                            case "Up":
                                                {

                                                    OtherPlayerPosition = new Position(OtherPlayerPosition.Row - 1, OtherPlayerPosition.Col);
                                                    break;
                                                }
                                            case "Right":
                                                {

                                                    OtherPlayerPosition = new Position(OtherPlayerPosition.Row, OtherPlayerPosition.Col + 1);
                                                    break;
                                                }
                                            case "Left":
                                                {

                                                    OtherPlayerPosition = new Position(OtherPlayerPosition.Row, OtherPlayerPosition.Col - 1);
                                                    break;
                                                }
                                            default:
                                                {

                                                    break;
                                                }
                                        }
                                        //check if reached to the goal position.
                                        if (OtherPlayerPosition.Row == GoalPosition.Row && OtherPlayerPosition.Col == GoalPosition.Col)
                                        {
                                            FinishGame = true;
                                            MyClient.Communicate = false;
                                        }

                                    }
                                }
                                //checks if the game was closed by the other player.
                                else if (result.Contains("The Game Was Closed")) //close the game.
                                {
                                    Is_Enabled = false;
                                    FinishGame = true;
                                    MyClient.Communicate = false;
                                  
                                }
                            }
                        }
                        catch
                        {

                        }
                    }
                }
                catch
                {
                    int x = 2;
                }
            });
            task.Start(); // starts the task.
        }

        /// <summary>
        /// Gets or sets the other player position.
        /// </summary>
        /// <value>
        /// The other player position.
        /// </value>
        public Position OtherPlayerPosition
        {
            get
            {
                return otherPlayerPosition;
            }

            set
            {
                otherPlayerPosition = value;
                NotifyPropertyChanged("OtherPlayerPosition");
            }
        }

        /// <summary>
        /// Nullifies the client.
        /// </summary>
        public void NullifyClient()
        {
            try
            {
                MyClient.StreamReader = null;
                MyClient.StreamWriter = null;
                MyClient = null;
            }
            catch
            {

            }
            
        }

        /// <summary>
        /// Gets or sets the games list.
        /// </summary>
        /// <value>
        /// The games list.
        /// </value>
        public ObservableCollection<string> GamesList
        {
            get
            {
                return this.gamesList;
            }
            set
            {
                this.gamesList = value;
            }
        }

        /// <summary>
        /// sends a list command to the server.
        /// </summary>
        public void ListCommand()
        {
    
           SendMessageToServer("list");
            string resultFromServer = RecieveMessageFromServer();
            if (resultFromServer != "[]")
            {
                List<string> list = JsonConvert.DeserializeObject<List<string>>(resultFromServer);
                int count = list.Count;
                foreach(string s in list)
                {
                    if (!gamesList.Contains(s))
                    {
                        gamesList.Add(s);
                    }
                }

                //for (int i = 0; i < count; i++)
                //{
                //    gamesList.Add(list[i]);
                //}
            }
        }

    }
}
