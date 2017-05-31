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
    /// <seealso cref="System.Windows.Window" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class MultiPlayerGameWindow : Window
    {
        //members.
        private MultiPlayerViewModel vm;
        private bool keyDownEventWasRegister;
        private bool lostTheGame;
        private bool registerToConnectionError;

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiPlayerGameWindow"/> class.
        /// </summary>
        /// <param name="settingModel">The setting model.</param>
        /// <param name="vm">The vm.</param>
        /// <param name="mazeName">Name of the maze.</param>
        /// <param name="buttonPressed">The button pressed.</param>
        public MultiPlayerGameWindow(ISettingsModel settingModel, MultiPlayerViewModel vm, string mazeName, string buttonPressed)
        {
            registerToConnectionError = false;
            this.vm = vm;
            keyDownEventWasRegister = false;
            //register to the delegates.
            if (!registerToConnectionError)
            {
                vm.ConnectionErrorOccurred += delegate (object sender, PropertyChangedEventArgs e)
                {

                    if (MessageBox.Show("There was an error with the connection to the server", "Connection Error", MessageBoxButton.OK) == MessageBoxResult.OK)
                    {

                        Dispatcher.Invoke(() => { CloseTheGame(); });

                    }
                };
                registerToConnectionError = true;
            }
            vm.GameWasClosed += delegate (string message)
            {
                if ((vm.VM_OtherPlayerPosition).Row == (vm.VM_GoalPosition).Row && (vm.VM_OtherPlayerPosition).Col == (vm.VM_GoalPosition).Col)
                {

                    if (MessageBox.Show("Unfortunatly you lost the game", "You Lost", MessageBoxButton.OK) == MessageBoxResult.OK)
                    {
                        try
                        {

                            vm.NullifyClient();
                            Dispatcher.Invoke(() => { CloseTheGame(); });
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

                        Dispatcher.Invoke(() => { CloseTheGame(); });
                    }
                }


            };
            this.vm.VM_Rows = settingModel.MazeRows.ToString();
            this.vm.VM_Cols = settingModel.MazeCols.ToString();

            //get the command that was called.
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
            if (vm.VM_Maze != null)
            {
                InitializeComponent();
            }


        }

        /// <summary>
        /// Handles the Loaded event of the MazeBoard control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void MazeBoard_Loaded(object sender, RoutedEventArgs e)
        {
            if (keyDownEventWasRegister == false)
            {
                Window window = Window.GetWindow(this);
                window.KeyDown += HandleKeyPress;
                keyDownEventWasRegister = true;
            }
        }

        /// <summary>
        /// Handles the key press and moves the player accordingly.
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
            if (direction != "" && MyBoard.PlayerPosition != MyBoard.GoalPosition)
            {
                vm.MovePlayer(direction);
                if (vm.VM_PlayerPosition.Row == vm.VM_GoalPosition.Row && vm.VM_PlayerPosition.Col == vm.VM_GoalPosition.Col)
                {

                    if (MessageBox.Show("Congratulations! you have reached the Destination", "Congratulations!", MessageBoxButton.OK) == MessageBoxResult.OK)
                    {
                        vm.NullifyClient();
                        Dispatcher.Invoke(() => { CloseTheGame(); });
                    }

                }
            }

        }



        private void CloseTheGame()
        {
            this.Close();
        }


        /// <summary>
        /// Raises the <see cref="E:System.Windows.Window.Closed" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnClosed(EventArgs e)
        {
            if (vm.VM_ConnectionError == null) // so that when we close the game automaticly we wouldn't send the message twice.
            {
                vm.CloseGame();
            }
            vm.NullifyClient();
            Dispatcher.Invoke(() => { CloseTheGame(); });

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
                vm.CloseGame();
                vm.NullifyClient();
                this.Hide();
                Dispatcher.Invoke(() => { CloseTheGame(); });
                lostTheGame = false;
                MainWindow mainWin = new MainWindow();
                mainWin.ShowDialog();
            }
        }
    }
}
