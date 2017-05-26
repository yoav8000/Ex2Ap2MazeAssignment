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
                MyClient.Communicate = false;
                
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

        public void RestartMaze()
        {
            Is_Enabled = true;
            PlayerPosition = InitialPosition;
        }


        private string GetSolution()
        {
            string resultFromServer;
            string solution = null;
            if (Is_Enabled)
            {
                SendMessageToServer("solve" + " " + MazeName + " " + SearchAlgorithm);
                resultFromServer = RecieveMessageFromServer();
                if (resultFromServer != null)
                {
                    resultFromServer = resultFromServer.Replace(@"\", "");
                    string[] arr = resultFromServer.Split(':');
                    arr = arr[2].Split(',');
                    solution = arr[0];
                }   
            }
            return solution;
        }

        public void StartAnimating(string solution)
        {
            Is_Enabled = false;
            StringBuilder sb = new StringBuilder();
            Task task = new Task(() =>//creating a listening thread that keeps running.
            {
                for (int i = 0; i < solution.Length; i++)
                {
                    string direction = "";
                    switch (solution[i])
                    {
                        case '0':
                            {
                                direction = "Left";
                                break;
                            }

                        case '1':
                            {
                                direction = "Right";
                                break;
                            }
                        case '2':
                            {
                                direction = "Up";
                                break;
                            }
                        case '3':
                            {
                                direction = "Down";
                                break;
                            }
                        default:
                            {
                                break;
                            }



                    }
                    if (direction != "")
                    {
                        sb.Append(solution[i]);
                        MovePlayer(direction);
                        System.Threading.Thread.Sleep(200);
                    }
                }
                MazeSolution = sb.ToString();
            });
            task.Start();
        }

        public void SolveMaze()
        {
            string solution = null;
          

            if (MazeSolution == null)
            {
                solution = GetSolution();

            }
            else
            {
                solution = MazeSolution;
            }
            if (solution != null)
            {
                if (PlayerPosition.Row != InitialPosition.Row || PlayerPosition.Col != InitialPosition.Col)
                {
                    RestartMaze();
                    Is_Enabled = false;
                }

                StartAnimating(solution);
            }
        }

    }
}