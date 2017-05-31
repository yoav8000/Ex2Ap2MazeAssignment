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
    /// <summary>
    /// an abstract model.
    /// </summary>
    /// <seealso cref="MazeGui.ConnectionErrorInterface.INotifyConnectionError" />
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public abstract class AbstractModel: INotifyConnectionError, INotifyPropertyChanged
    {
        /// <summary>
        /// members
        /// </summary>
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

        public event PropertyChangedEventHandler PropertyChanged;

        public event CriticalErrorHandler ConnectionErrorOccurred;


        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractModel"/> class.
        /// </summary>
        /// <param name="settingsModel">The settings model.</param>
        /// <param name="mazeName">Name of the maze.</param>
        public AbstractModel(ISettingsModel settingsModel, string mazeName)
        {
            MazeName = mazeName;
            IpAddress = settingsModel.ServerIp;
            PortNumber = settingsModel.ServerPort;
            Is_Enabled = true;
            Rows = settingsModel.MazeRows.ToString();
            Cols = settingsModel.MazeCols.ToString();
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is enabled; otherwise, <c>false</c>.
        /// </value>
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


        /// <summary>
        /// Gets or sets the name of the maze.
        /// </summary>
        /// <value>
        /// The name of the maze.
        /// </value>
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

        /// <summary>
        /// Gets or sets the rows.
        /// </summary>
        /// <value>
        /// The rows.
        /// </value>
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

        /// <summary>
        /// Gets or sets the cols.
        /// </summary>
        /// <value>
        /// The cols.
        /// </value>
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


        /// <summary>
        /// Gets or sets the ip address.
        /// </summary>
        /// <value>
        /// The ip address.
        /// </value>
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


        /// <summary>
        /// Gets or sets the port number.
        /// </summary>
        /// <value>
        /// The port number.
        /// </value>
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

        /// <summary>
        /// Gets or sets the result maze.
        /// </summary>
        /// <value>
        /// The result maze.
        /// </value>
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

        /// <summary>
        /// Gets or sets the goal position.
        /// </summary>
        /// <value>
        /// The goal position.
        /// </value>
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



        /// <summary>
        /// Gets or sets my client.
        /// </summary>
        /// <value>
        /// My client.
        /// </value>
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

        /// <summary>
        /// Notifies the property changed.
        /// </summary>
        /// <param name="propName">Name of the property.</param>
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }


        /// <summary>
        /// Notifies the connection error.
        /// </summary>
        /// <param name="message">The message.</param>
        public void NotifyConnectionError(string message)
        {
            if (this.ConnectionErrorOccurred != null)
            {
                this.ConnectionErrorOccurred(this, new PropertyChangedEventArgs(message));
            }
        }


        /// <summary>
        /// Moves the player.
        /// </summary>
        /// <param name="keyDirection">The key direction.</param>
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

        /// <summary>
        /// Players the can move.
        /// </summary>
        /// <param name="currentPosition">The current position.</param>
        /// <param name="direction">The direction.</param>
        /// <returns></returns>
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
         
            if (result.Contains("ConnectionError"))
            {
                Is_Enabled = false;
                ConnectionError = result;

            }
        }


        /// <summary>
        /// Disconnects this instance.
        /// </summary>
        public void Disconnect()
        {
            if (MyClient != null)
            {
                myClient.CloseConnection();
            }
        }


        /// <summary>
        /// Recieves the message from server.
        /// </summary>
        /// <returns></returns>
        public string RecieveMessageFromServer()
        {
            if (Is_Enabled)
            {
                string resultFromServer = myClient.ReadMessage();
                if (resultFromServer != null)
                {
                    if (resultFromServer.Contains("Connection Error"))
                    {
                        Is_Enabled = false;
                        ConnectionError = resultFromServer;
                        Disconnect();
                    }
                    return resultFromServer;
                }
            }
            return null;
        }

        /// <summary>
        /// Sends the message to server.
        /// </summary>
        /// <param name="message">The message.</param>
        public void SendMessageToServer(string message)
        {
            try
            {
                string result = myClient.WriteMessage(message);
                if (result.Contains("Connection Error"))
                {
                    Is_Enabled = false;
                    ConnectionError = result;
                    Disconnect();
                }
            }
            catch
            {

            }
        }



        /// <summary>
        /// Gets or sets the connection error.
        /// </summary>
        /// <value>
        /// The connection error.
        /// </value>
        public string ConnectionError
        {
            get
            {
                return this.connectionError;
            }
            set
            {
                if (value != null)
                {
                    this.connectionError = value;
                    this.isEnabled = false;
                    NotifyConnectionError("ConnectionError");
                }
            }
        }



    }
}
