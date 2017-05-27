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
   public class MultiPlayerViewModel : MazeGui.ViewModel.GeneralVM.ViewModel , INotifyGameWasClosed
    {
        private MultiPlayerModel model;

        public event GameWasClosedEventHandler GameWasClosed;

        public MultiPlayerViewModel(MultiPlayerModel model)
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

            model.GameWasClosed += delegate (string message)
            {
                NotifyGameWasClosed("VM_" + "GameWasClosed");
            };

        }



        public void NotifyGameWasClosed(string propName)
        {
            if (this.GameWasClosed != null)
            {
                this.GameWasClosed("GameWasClosed");
            }
        }



        public bool VM_GameWasClosed
        {
            get
            {
                return Model.FinishGame;
            }
        }



        public void MovePlayer(string direction)
        {
            model.PlayCommand(direction);
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

        public Position VM_OtherPlayerPosition
        {
            get
            {
                return Model.OtherPlayerPosition;
            }
        }

        public ObservableCollection<string> VM_ListOfGames
        {
            get
            {
                return Model.GamesList;
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



        public void StartMultiplayerGame(string mazeName)
        {
            Model.GenerateMultiplayerMaze(mazeName);
        }

        public void JoinMaze(string mazeName)
        {
            Model.JoinMazeCommand(mazeName);
        }

    }
}
