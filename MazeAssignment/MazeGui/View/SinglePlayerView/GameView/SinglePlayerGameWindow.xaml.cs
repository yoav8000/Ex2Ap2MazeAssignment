using System;
using System.Collections.Generic;
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
using MazeGui.TheViewModel.SinglePlayerVM;
using System.ComponentModel;
using MazeGui.Model.SinglePlayerModel;
using MazeGui.Model.SettingsModel;

namespace MazeGui.View.SinglePlayerView.GameView
{
    /// <summary>
    /// Interaction logic for SinglePlayerGameWindow.xaml
    /// </summary>
    public partial class SinglePlayerGameWindow : Window
    {
        private SinglePlayerViewModel vm;

        public SinglePlayerGameWindow(ISettingsModel settingModel,string mazeName)
        {
            this.vm = new SinglePlayerViewModel(new SinglePlayerModel(settingModel, mazeName));
           
            vm.ConnectionErrorOccurred += delegate (object sender, PropertyChangedEventArgs e)
            {

                if (MessageBox.Show("There was an error with the connection to the server", "Connection Error", MessageBoxButton.OK) == MessageBoxResult.OK)
                {
                   
                }

            };
            vm.StartNewGame();
            this.DataContext = this.vm;
            if (vm.VM_Is_Enabled)
            {
                InitializeComponent();
               
            }
            else
            {
                Close();
            }
        }

        private void MazeBoard_Loaded(object sender, RoutedEventArgs e)
        {
          
            Window window = Window.GetWindow(this);
            window.KeyDown += HandleKeyPress;
        }


        private void MainMenuButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to go back to the main menu?", "Go back to main menu", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {

                Close();
                 MainWindow mainWin = new MainWindow();
                mainWin.ShowDialog();

            }
        }

        private void SolveMazeButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to solve the maze?", "Solve the maze", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {

                vm.SolveMaze();

            }

        }


        private void RestartGameButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to restart the game?", "Restart the game", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {

                vm.RestartMaze();

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
                if (direction != "" && MazeBoard.PlayerPosition != MazeBoard.GoalPosition)
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

