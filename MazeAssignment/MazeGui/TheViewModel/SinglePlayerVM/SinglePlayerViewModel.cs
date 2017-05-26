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
    public class SinglePlayerViewModel : MazeGui.ViewModel.GeneralVM.ViewModel
    {
        private SinglePlayerModel model;


        public SinglePlayerViewModel(SinglePlayerModel model)
        {
            this.model = model;

            model.ConnectionErrorOccurred += delegate (object sender, PropertyChangedEventArgs e)
            {
                NotifyConnectionError("VM_" + "IsEnabled");
            };

            model.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }

        public void StartNewGame()
        {
            if (model.Is_Enabled)
            {
             model.GenerateSinglePlayerMaze();
            }
        }

        public void MovePlayer(string direction)
        {
            model.MovePlayer(direction);
        }

        public bool VM_Is_Enabled
        {
            get
            {
                return Model.Is_Enabled;
            }
        }

        public string VM_ConnectionError
        {
            get
            {
                return Model.ConnectionError;
            }
        }

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

        public string VM_Rows
        {
            get
            {
                return Model.Rows;
            }
        }

        public string VM_Cols
        {
            get
            {
                return Model.Cols;
            }

        }

        public string VM_MazeName
        {
            get
            {
                return Model.MazeName;
            }

        }


        public string VM_Maze
        {
            get
            {
                return Model.Maze;
            }
        }



        public Position VM_PlayerPosition
        {
            get
            {
                return Model.PlayerPosition;
            }

        }

        public Position VM_InitialPosition
        {
            get
            {

                return Model.InitialPosition;
            }

        }

        public Position VM_GoalPosition
        {
            get
            {
                return Model.GoalPosition;
            }

        }

        public string ServerIP
        {
            get { return Model.IpAddress; }
            set
            {
                Model.IpAddress = value;
            }
        }

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

        public void SolveMaze()
        {
            model.SolveMaze();
        }

        public void RestartMaze()
        {
            model.RestartMaze();
        }


    }
}
