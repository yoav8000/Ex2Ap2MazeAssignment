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

namespace MazeGui.Model.MultiPlayerModel
{

    public class MultiPlayerModel: AbstractModel
    {
        private ObservableCollection<string> gamesList;
        private Position otherPlayerPosition;

        public MultiPlayerModel(ISettingsModel settingsModel, string mazeName) : base(settingsModel, mazeName)
        {

            this.gamesList = new ObservableCollection<string>();
            if (MyClient == null)
            {
                MyClient = new Client(IpAddress, PortNumber);
                if (MyClient.Communicate == false)
                {
                    ConnectionError = "ConnectionError";
                    return;
                }
            }
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
                        ConnectionError = "ConnectionError";
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
                    Rows = ResultMaze.Rows.ToString();
                    Cols = ResultMaze.Cols.ToString();
                    StartListeningToServer();
                }
               
            }
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
                        ConnectionError = "ConnectionError";
                        return;
                    }
                }
                SendMessageToServer("join" + " " + MazeName );
                string resultFromServer = RecieveMessageFromServer();
                if (resultFromServer.Contains("Maze"))
                {
                    ResultMaze = MazeLib.Maze.FromJSON(resultFromServer);
                    string temp = ResultMaze.ToString();
                    Maze = temp.Replace(Environment.NewLine, "");
                    InitialPosition = ResultMaze.InitialPos;
                    GoalPosition = ResultMaze.GoalPos;
                    PlayerPosition = ResultMaze.InitialPos;
                    Rows = ResultMaze.Rows.ToString();
                    Cols = ResultMaze.Cols.ToString();
                    StartListeningToServer();
                }

            }
        }


        public void StartListeningToServer()
        {

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
