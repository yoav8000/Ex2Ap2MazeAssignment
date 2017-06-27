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
    /// <seealso cref="System.Windows.Window" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class SinglePlayerGameWindow : Window
    {
        private SinglePlayerViewModel vm;

        /// <summary>
        /// Initializes a new instance of the <see cref="SinglePlayerGameWindow"/> class.
        /// </summary>
        /// <param name="settingModel">The setting model.</param>
        /// <param name="mazeName">Name of the maze.</param>
        public SinglePlayerGameWindow(ISettingsModel settingModel,string mazeName, int rows, int cols)
        {
            this.vm = new SinglePlayerViewModel(new SinglePlayerModel(settingModel, mazeName));
           
            vm.ConnectionErrorOccurred += delegate (object sender, PropertyChangedEventArgs e)
            {

                if (MessageBox.Show("There was an error with the connection to the server", "Connection Error", MessageBoxButton.OK) == MessageBoxResult.OK)
                {
                    this.Close();
                }

            };
            this.DataContext = this.vm;
            vm.StartNewGame(mazeName, rows,cols);
            if (vm.VM_Is_Enabled)
            {
                InitializeComponent();
               
            }
            else
            {
                Close();
            }
        }

        /// <summary>
        /// Handles the Loaded event of the MazeBoard control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void MazeBoard_Loaded(object sender, RoutedEventArgs e)
        {
          
            Window window = Window.GetWindow(this);
            window.KeyDown += HandleKeyPress;
        }


        /// <summary>
        /// Handles the Click event of the MainMenuButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void MainMenuButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to go back to the main menu?", "Go back to main menu", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {

                Close();
                 MainWindow mainWin = new MainWindow();
                mainWin.ShowDialog();

            }
        }

        /// <summary>
        /// Handles the Click event of the SolveMazeButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void SolveMazeButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to solve the maze?", "Solve the maze", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {

                vm.SolveMaze();

            }

        }


        /// <summary>
        /// Handles the Click event of the RestartGameButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void RestartGameButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to restart the game?", "Restart the game", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {

                vm.RestartMaze();

            }

        }


        /// <summary>
        /// Raises the <see cref="E:System.Windows.Window.Closed" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnClosed(EventArgs e)
        {
            Close();
            MainWindow mainWin = new MainWindow();
            mainWin.ShowDialog();
        }



        /// <summary>
        /// Handles the key press.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void HandleKeyPress(object sender, KeyEventArgs e)
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

