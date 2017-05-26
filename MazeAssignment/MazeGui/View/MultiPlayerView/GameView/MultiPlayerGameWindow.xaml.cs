﻿using MazeGui.Model.MultiPlayerModel;
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

        public MultiPlayerGameWindow(ISettingsModel settingModel,MultiPlayerViewModel vm , string mazeName, string buttonPressed)
        {
            this.vm = vm;
           
            vm.ConnectionErrorOccurred += delegate (object sender, PropertyChangedEventArgs e)
            {

                if (MessageBox.Show("There was an error with the connection to the server", "Connection Error", MessageBoxButton.OK) == MessageBoxResult.OK)
                {
                    this.Close();
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
            Window window = Window.GetWindow(this);
            window.KeyDown += HandleKeyPress;
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

                }
            }
        }

    }
}