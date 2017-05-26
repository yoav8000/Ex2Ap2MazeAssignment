using MazeGui.ConnectionErrorInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientDll;
using MazeLib;
using System.ComponentModel;
using MazeGui.Model.SettingsModel;

namespace MazeGui.Model.AnAbstractModelClass
{
   public abstract class AbstractModel: INotifyConnectionError, INotifyPropertyChanged
    {

        private string mazeName;
        private string rows;
        private string cols;
        private Position initialPosition;
        private Position goalPosition;
        private Client myClient;
        private string ipAddress;
        private int portNumber;
        private string maze;
        private Maze resultMaze;
        private Position playerPosition;
        private string connectionError;
        private bool isEnabled;
        private Position otherPlayerPosition;

        public event PropertyChangedEventHandler PropertyChanged;

        public event CriticalErrorHandler ConnectionErrorOccurred;


        public AbstractModel(ISettingsModel settingsModel, string mazeName)
        {
            MazeName = mazeName;
            IpAddress = settingsModel.ServerIp;
            PortNumber = settingsModel.ServerPort;
            Is_Enabled = true;
            Rows = settingsModel.MazeRows.ToString();
            Cols = settingsModel.MazeCols.ToString();
        }

        public bool Is_Enabled
        {
            get
            {
                return this.isEnabled;
            }
            set
            {
                isEnabled = value;
                NotifyPropertyChanged("Is_Enabled");
            }
        }


        public string MazeName
        {
            get
            {
                return mazeName;
            }
            set
            {
                mazeName = value;
                NotifyPropertyChanged("MazeName");
            }
        }

        public string Rows
        {
            get
            {
                return rows;
            }
            set
            {
                rows = value;
                NotifyPropertyChanged("Rows");
            }
        }

        public string Cols
        {
            get
            {
                return cols;
            }
            set
            {
                cols = value;
                NotifyPropertyChanged("Cols");
            }
        }


        public string IpAddress
        {
            get
            {
                return this.ipAddress;
            }
            set
            {
                ipAddress = value;

            }
        }


        public int PortNumber
        {
            get
            {
                return this.portNumber;
            }
            set
            {
                this.portNumber = value;
            }
        }


        public string Maze
        {
            get
            {
                return maze;
            }

            set
            {
                maze = value;
                NotifyPropertyChanged("Maze");
            }
        }

        public Maze ResultMaze
        {
            get
            {
                return this.resultMaze;
            }
            set
            {
                this.resultMaze = value;
            }
        }

        public Position InitialPosition
        {
            get
            {
                return initialPosition;
            }

            set
            {
                initialPosition = value;
                NotifyPropertyChanged("InitialPosition");
            }
        }

        public Position GoalPosition
        {
            get
            {
                return goalPosition;
            }

            set
            {
                goalPosition = value;
                NotifyPropertyChanged("GoalPosition");
            }
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

        public Client MyClient
        {
            get
            {
                return myClient;
            }

            set
            {
                this.myClient = value;
            }

        }

        public Position PlayerPosition
        {
            get
            {
                return this.playerPosition;
            }

            set
            {
                this.playerPosition = value;
                NotifyPropertyChanged("PlayerPosition");
            }
        }

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }


        public void NotifyConnectionError(string message)
        {
            if (this.ConnectionErrorOccurred != null)
            {
                this.ConnectionErrorOccurred(this, new PropertyChangedEventArgs(message));
            }
        }


        public void MovePlayer(string keyDirection)
        {
            if (PlayerCanMove(playerPosition, keyDirection))
            {
                switch (keyDirection)
                {
                    case "Down":
                        {

                            PlayerPosition = new Position(PlayerPosition.Row + 1, PlayerPosition.Col);
                            break;
                        }
                    case "Up":
                        {

                            PlayerPosition = new Position(PlayerPosition.Row - 1, PlayerPosition.Col);
                            break;
                        }
                    case "Right":
                        {

                            PlayerPosition = new Position(PlayerPosition.Row, PlayerPosition.Col + 1);
                            break;
                        }
                    case "Left":
                        {

                            PlayerPosition = new Position(PlayerPosition.Row, PlayerPosition.Col - 1);
                            break;
                        }
                    default:
                        {

                            break;
                        }

                }

            }


        }

        public bool PlayerCanMove(Position currentPosition, string direction)
        {
            int currentColPosition = currentPosition.Col;
            int currentRowPosition = currentPosition.Row;

            switch (direction)
            {
                case "Left":
                    {
                        return ((currentColPosition - 1 >= 0) && ('0' == Maze[(currentRowPosition * int.Parse(Cols)) + currentColPosition - 1]
                           || '*' == Maze[(currentRowPosition * int.Parse(Cols)) + currentColPosition - 1]
                           || '#' == Maze[(currentRowPosition * int.Parse(Cols)) + currentColPosition - 1])
                     );
                    }

                case "Up":
                    {
                        return ((currentRowPosition - 1 >= 0) && ('0' == Maze[((currentRowPosition - 1) * int.Parse(Cols)) + currentColPosition]
                            || '*' == Maze[((currentRowPosition - 1) * int.Parse(Cols)) + currentColPosition]
                            || '#' == Maze[((currentRowPosition - 1) * int.Parse(Cols)) + currentColPosition])
                  );
                    }
                case "Down":
                    {
                        return ((currentRowPosition + 1 < int.Parse(Rows)) && ('0' == Maze[((currentRowPosition + 1) * int.Parse(Cols)) + currentColPosition]
                             || '*' == Maze[((currentRowPosition + 1) * int.Parse(Cols)) + currentColPosition]
                             || '#' == Maze[((currentRowPosition + 1) * int.Parse(Cols)) + currentColPosition])
              );
                    }
                case "Right":
                    {
                        return ((currentColPosition + 1 < int.Parse(Cols)) && ('0' == Maze[(currentRowPosition * int.Parse(Cols)) + currentColPosition + 1]
                              || '*' == Maze[(currentRowPosition * int.Parse(Cols)) + currentColPosition + 1]
                              || '#' == Maze[(currentRowPosition * int.Parse(Cols)) + currentColPosition + 1])
                        );
                    }
            }
            return false;
        }


        public void Connect(string ip, int port)
        {
            string result = null;
            //string result = this.myClient.CreateANewConnection();
           // Stop = false;
            if (result.Contains("ConnectionError"))
            {
                Is_Enabled = false;
                ConnectionError = result;

            }
        }


        public void Disconnect()
        {
            myClient.CloseConnection();
        }


        public string RecieveMessageFromServer()
        {
            if (Is_Enabled)
            {
                string resultFromServer = myClient.ReadMessage();
                if (resultFromServer != null)
                {
                    if (resultFromServer.Contains("ConnectionError"))
                    {
                        Is_Enabled = false;
                        ConnectionError = resultFromServer;
                    }
                    return resultFromServer;
                }
            }
            return null;
        }

        public void SendMessageToServer(string message)
        {
            string result = myClient.WriteMessage(message);
            if (result.Contains("Connection Error"))
            {
                Is_Enabled = false;
                ConnectionError = result;
            }
        }

       

        public string ConnectionError
        {
            get
            {
                return this.connectionError;
            }
            set
            {
                this.connectionError = value;
                this.isEnabled = false;
                NotifyConnectionError("ConnectionError");
            }
        }



    }
}
