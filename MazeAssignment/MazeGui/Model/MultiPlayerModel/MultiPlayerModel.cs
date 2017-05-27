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

    public class MultiPlayerModel : AbstractModel, INotifyGameWasClosed
    {
        private ObservableCollection<string> gamesList;
        private Position otherPlayerPosition;
        private bool finishGame;
        private bool lostTheGame;
        public event GameWasClosedEventHandler GameWasClosed;

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




        public void NotifyGameWasClosed(string propName)
        {
            if (this.GameWasClosed != null)
            {
                this.GameWasClosed("GameWasClosed");
            }
        }





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






        public void GenerateMultiplayerMaze(string mazeName)
        {
            MazeName = mazeName;
            if (Is_Enabled)
            {
                if (MyClient == null)
                {
                    MyClient = new Client(IpAddress, PortNumber);
                    if (MyClient.Communicate == false)
                    {
                        ConnectionError = "Connection Error";
                        return;
                    }
                }
                SendMessageToServer("start" + " " + MazeName + " " + Rows + " " + Cols);
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
        }




        public void PlayCommand(string direcion)
        {
            SendMessageToServer("play" + " " + direcion);
            MovePlayer(direcion);
           
        }


        public void JoinMazeCommand(string mazeName)
        {
            MazeName = mazeName;
            if (Is_Enabled)
            {
                if (MyClient == null)
                {
                    MyClient = new Client(IpAddress, PortNumber);
                    if (MyClient.Communicate == false)
                    {
                        ConnectionError = "Connection Error";
                        return;
                    }
                }
                SendMessageToServer("join" + " " + MazeName);
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
        }

        private string check;
        public string Check
        {
            get
            {
                return this.check;
            }
            set
            {
                this.check = value;
            }
        }


        public void StartListeningToServer()
        {
            Task task = new Task(() =>//create a reading thread from the server.
            {
                while (MyClient.Communicate)
                {
                    try
                    {
                        string result = RecieveMessageFromServer();
                    

                        if (result != null)
                        {

                            if(result.Contains("Connection Error"))
                            {
                                break;
                            }

                            string direction = null;

                            if (result.Contains("Direction"))
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

                                    if(OtherPlayerPosition.Row == GoalPosition.Row && OtherPlayerPosition.Col == GoalPosition.Col)
                                    {
                                        Check = "aaa";

                                        FinishGame = true;
                                        MyClient.Communicate = false;
                                    }

                                }
                            }
                            else if (result.Contains("Close"))
                            {
                                Check = "aaa";
                                FinishGame = true;
                                MyClient.Communicate = false;
                            }
                        }
                    }
                    catch
                    {
                        
                    }
                }
            });
            task.Start();
        }

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

        public void ListCommand()
        {
           SendMessageToServer("list");
            string resultFromServer = RecieveMessageFromServer();
            if (resultFromServer != "[]")
            {
                List<string> list = JsonConvert.DeserializeObject<List<string>>(resultFromServer);
                int count = list.Count;
                for (int i = 0; i < count; i++)
                {
                    gamesList.Add(list[i]);
                }
            }
        }

    }
}
