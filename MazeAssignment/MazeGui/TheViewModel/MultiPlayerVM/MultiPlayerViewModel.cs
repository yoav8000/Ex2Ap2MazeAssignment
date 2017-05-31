using MazeGui.Model.MultiPlayerModel;
using MazeLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeGui.NoifyGameClosedInterface;

namespace MazeGui.TheViewModel.MultiPlayerVM
{
    /// <summary> 
    /// MultiPlayerViewModel implements ViewModel and INotifyGameWasClosed.
    /// </summary>
    /// <seealso cref="MazeGui.ViewModel.GeneralVM.ViewModel" />
    /// <seealso cref="MazeGui.NoifyGameClosedInterface.INotifyGameWasClosed" />
    public class MultiPlayerViewModel : MazeGui.ViewModel.GeneralVM.ViewModel , INotifyGameWasClosed
    {
        //members.
        private MultiPlayerModel model;

        public event GameWasClosedEventHandler GameWasClosed;

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiPlayerViewModel"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public MultiPlayerViewModel(MultiPlayerModel model)
        {
            this.model = model;

            model.ConnectionErrorOccurred += delegate (object sender, PropertyChangedEventArgs e)
            {
                NotifyConnectionError("VM_" + "ConnectionError");
            };

            model.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };

            model.GameWasClosed += delegate (string message)
            {
                NotifyGameWasClosed("VM_" + "GameWasClosed");
            };

        }



        /// <summary>
        /// Notifies the game was closed.
        /// </summary>
        /// <param name="propName">Name of the property.</param>
        public void NotifyGameWasClosed(string propName)
        {
            if (this.GameWasClosed != null)
            {
                VM_Is_Enabled = false;
                this.GameWasClosed("GameWasClosed");
            }
        }


        public void NullifyClient()
        {
         
            Model.NullifyClient();
            
        }

        /// <summary>
        /// Gets a value indicating whether [vm game was closed].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [vm game was closed]; otherwise, <c>false</c>.
        /// </value>
        public bool VM_GameWasClosed
        {
            get
            {
                return Model.FinishGame;
            }
        }

        /// <summary>
        /// Closes the game.
        /// </summary>
        public void CloseGame()
        {
            model.CloseCommand();
        }

        /// <summary>
        /// Moves the player.
        /// </summary>
        /// <param name="direction">The direction.</param>
        public void MovePlayer(string direction)
        {
            model.PlayCommand(direction);
        }

        /// <summary>
        /// Gets or sets a value indicating whether [vm is enabled].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [vm is enabled]; otherwise, <c>false</c>.
        /// </value>
        public bool VM_Is_Enabled
        {
            get
            {
                return Model.Is_Enabled;
            }
            set
            {
                Model.Is_Enabled = value;
            }
        }

        /// <summary>
        /// Gets the vm connection error.
        /// </summary>
        /// <value>
        /// The vm connection error.
        /// </value>
        public string VM_ConnectionError
        {
            get
            {
                return Model.ConnectionError;
            }
        }

        /// <summary>
        /// Gets or sets the model.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        public MultiPlayerModel Model
        {
            get
            {
                return this.model;
            }
            set
            {
                this.model = value;
            }
        }

        /// <summary>
        /// Gets or sets the vm rows.
        /// </summary>
        /// <value>
        /// The vm rows.
        /// </value>
        public string VM_Rows
        {
            get
            {
                return Model.Rows;
            }
            set
            {
                Model.Rows = value;
            }
        }

        /// <summary>
        /// Gets or sets the vm cols.
        /// </summary>
        /// <value>
        /// The vm cols.
        /// </value>
        public string VM_Cols
        {
            get
            {
                return Model.Cols;
            }
            set
            {
                model.Cols = value;
            }

        }

        /// <summary>
        /// Gets the name of the vm maze.
        /// </summary>
        /// <value>
        /// The name of the vm maze.
        /// </value>
        public string VM_MazeName
        {
            get
            {
                return Model.MazeName;
            }

        }


        /// <summary>
        /// Gets the vm maze.
        /// </summary>
        /// <value>
        /// The vm maze.
        /// </value>
        public string VM_Maze
        {
            get
            {
                return Model.Maze;
            }
        }

        /// <summary>
        /// Gets the vm other player position.
        /// </summary>
        /// <value>
        /// The vm other player position.
        /// </value>
        public Position VM_OtherPlayerPosition
        {
            get
            {
                return Model.OtherPlayerPosition;
            }
        }

        /// <summary>
        /// Gets the vm list of games.
        /// </summary>
        /// <value>
        /// The vm list of games.
        /// </value>
        public ObservableCollection<string> VM_ListOfGames
        {
            get
            {
                return Model.GamesList;
            }

        }

        /// <summary>
        /// Gets the vm player position.
        /// </summary>
        /// <value>
        /// The vm player position.
        /// </value>
        public Position VM_PlayerPosition
        {
            get
            {
                return Model.PlayerPosition;
            }

        }

        /// <summary>
        /// Gets the vm initial position.
        /// </summary>
        /// <value>
        /// The vm initial position.
        /// </value>
        public Position VM_InitialPosition
        {
            get
            {

                return Model.InitialPosition;
            }

        }

        /// <summary>
        /// Gets the vm goal position.
        /// </summary>
        /// <value>
        /// The vm goal position.
        /// </value>
        public Position VM_GoalPosition
        {
            get
            {
                return Model.GoalPosition;
            }

        }

        /// <summary>
        /// Gets or sets the server ip.
        /// </summary>
        /// <value>
        /// The server ip.
        /// </value>
        public string ServerIP
        {
            get { return Model.IpAddress; }
            set
            {
                Model.IpAddress = value;
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
                return Model.PortNumber;
            }
            set
            {
                this.Model.PortNumber = value;
            }
        }



        /// <summary>
        /// Starts the multiplayer game.
        /// </summary>
        /// <param name="mazeName">Name of the maze.</param>
        public void StartMultiplayerGame(string mazeName)
        {
            Model.GenerateMultiplayerMaze(mazeName);
        }

        /// <summary>
        /// Joins the maze.
        /// </summary>
        /// <param name="mazeName">Name of the maze.</param>
        public void JoinMaze(string mazeName)
        {
            Model.JoinMazeCommand(mazeName);
        }

    }
}
