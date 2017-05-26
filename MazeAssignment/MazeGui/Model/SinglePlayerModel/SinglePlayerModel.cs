using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeGui.Model.AnAbstractModelClass;
using MazeGui.Model.SettingsModel;
using ClientDll;

namespace MazeGui.Model.SinglePlayerModel
{
   public class SinglePlayerModel : AbstractModel
    {
        private int searchAlgorithm;
        private string mazeSolution;


        public SinglePlayerModel(ISettingsModel settingsModel, string mazeName) : base(settingsModel,mazeName)
        {
            SearchAlgorithm = settingsModel.SearchAlgorithm;
           
        }



        public string MazeSolution
        {
            get
            {
                return mazeSolution;
            }
            set
            {
                mazeSolution = value;
            }
        }


        public int SearchAlgorithm
        {
            get
            {
                return this.searchAlgorithm;
            }
            set
            {
                searchAlgorithm = value;

            }
        }


        public void GenerateSinglePlayerMaze()
        {
            if (Is_Enabled)
            {
                if(MyClient == null)
                {
                    MyClient = new Client(IpAddress, PortNumber);
                    if (MyClient.Communicate == false)
                    {
                        ConnectionError = "ConnectionError";
                    }
                }
                SendMessageToServer("generate" + " " + MazeName + " " + Rows + " " + Cols);
                string result = RecieveMessage();
                
            }
        }

        public string RecieveMessage()
        {

            string resultFromServer = RecieveMessageFromServer();
            if (Is_Enabled)
            {
                if (resultFromServer != null)
                {
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

                        return resultFromServer;

                    }
                    else if (resultFromServer.Contains("Solution"))
                    {
                        return resultFromServer;
                    }
                    return null;
                }
                return null;
            }
            return null;
        }

    }
}
