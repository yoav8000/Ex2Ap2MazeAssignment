using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeGui.ViewModel.GeneralVM;
using MazeGui.Model.SinglePlayerModel;
using System.ComponentModel;
using MazeLib;

namespace MazeGui.TheViewModel.SinglePlayerVM
{
    /// <summary>
    /// SinglePlayerViewModel implements ViewModel.
    /// </summary>
    /// <seealso cref="MazeGui.ViewModel.GeneralVM.ViewModel" />
    public class SinglePlayerViewModel : MazeGui.ViewModel.GeneralVM.ViewModel
    {
        //members.
        private SinglePlayerModel model;


        /// <summary>
        /// Initializes a new instance of the <see cref="SinglePlayerViewModel"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public SinglePlayerViewModel(SinglePlayerModel model)
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
        }

        /// <summary>
        /// Starts the new game.
        /// </summary>
        public void StartNewGame()
        {
            if (model.Is_Enabled)
            {
             model.GenerateSinglePlayerMaze();
            }
        }

        /// <summary>
        /// Moves the player.
        /// </summary>
        /// <param name="direction">The direction.</param>
        public void MovePlayer(string direction)
        {
            model.MovePlayer(direction);
        }

        /// <summary>
        /// Gets a value indicating whether [vm is enabled].
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
        public SinglePlayerModel Model
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
        /// Gets the vm rows.
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
        }

        /// <summary>
        /// Gets the vm cols.
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
        /// Solves the maze.
        /// </summary>
        public void SolveMaze()
        {
            model.SolveMaze();
        }

        /// <summary>
        /// Restarts the maze.
        /// </summary>
        public void RestartMaze()
        {
            model.RestartMaze();
        }


    }
}
