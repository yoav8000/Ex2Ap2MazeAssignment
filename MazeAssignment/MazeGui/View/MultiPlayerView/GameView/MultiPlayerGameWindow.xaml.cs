using MazeGui.Model.MultiPlayerModel;
using MazeGui.Model.SettingsModel;
using MazeGui.TheViewModel.MultiPlayerVM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MazeGui.View.MultiPlayerView.GameView
{
    /// <summary>
    /// Interaction logic for MultiPlayerGameWindow.xaml
    /// </summary>
    public partial class MultiPlayerGameWindow : Window
    {
        private MultiPlayerViewModel vm;
        private bool keyDownEventWasRegister;
        public MultiPlayerGameWindow(ISettingsModel settingModel,MultiPlayerViewModel vm , string mazeName, string buttonPressed)
        {
            this.vm = vm;
            keyDownEventWasRegister = false;
            vm.ConnectionErrorOccurred += delegate (object sender, PropertyChangedEventArgs e)
            {

                if (MessageBox.Show("There was an error with the connection to the server", "Connection Error", MessageBoxButton.OK) == MessageBoxResult.OK)
                {
                    try
                    {
                       
                        this.Close();
                    }
                    catch
                    {

                    }
                }
            };

            vm.GameWasClosed += delegate (string message) {
                if ((vm.VM_OtherPlayerPosition).Row == (vm.VM_GoalPosition).Row && (vm.VM_OtherPlayerPosition).Col == (vm.VM_GoalPosition).Col)
                {
                    if (MessageBox.Show("Unfortunatly you lost the game", "You Lost", MessageBoxButton.OK) == MessageBoxResult.OK)
                    {
                        try
                        {

                            this.Close();
                        }
                        catch
                        {

                        }

                    }
                }
                else
                {

                    if (MessageBox.Show("The other player closed the game", "Other player closed the game", MessageBoxButton.OK) == MessageBoxResult.OK)
                    {
                        try
                        {

                            this.Close();
                        }
                        catch
                        {

                        }


                    }
                }

                
            };


            switch (buttonPressed)
            {
                case "Start":
                    {
                        vm.StartMultiplayerGame(mazeName);
                        break;
                    }
                case "Join":
                    {
                        vm.JoinMaze(mazeName);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            this.DataContext = vm;
            InitializeComponent();


        }

        private void MazeBoard_Loaded(object sender, RoutedEventArgs e)
        {
            if (keyDownEventWasRegister == false)
            {
                Window window = Window.GetWindow(this);
                window.KeyDown += HandleKeyPress;
                keyDownEventWasRegister = true;
            }
        }

        private void HandleKeyPress(object sender, KeyEventArgs e)
        {
            if (vm.VM_Is_Enabled)
            {
                string direction = "";
                switch (e.Key)
                {
                    case Key.Down:
                        {
                            direction = "Down";
                            break;
                        }
                    case Key.Up:
                        {
                            direction = "Up";
                            break;
                        }
                    case Key.Right:
                        {
                            direction = "Right";
                            break;
                        }
                    case Key.Left:
                        {
                            direction = "Left";
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
                if (direction != "" && MyBoard.PlayerPosition != MyBoard.GoalPosition)
                {
                    vm.MovePlayer(direction);
                    if (vm.VM_PlayerPosition.Row == vm.VM_GoalPosition.Row && vm.VM_PlayerPosition.Col == vm.VM_GoalPosition.Col)
                    {
                        if (MessageBox.Show("Congratulations! you have reached the Destination", "Congratulations!", MessageBoxButton.OK) == MessageBoxResult.No)
                        {

                        }

                    }
                }
            }
        }

    }
}
