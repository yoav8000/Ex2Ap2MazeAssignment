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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MazeGui.View.GeneralSettingsView;
using MazeGui.Model.SettingsModel;
using MazeGui.View.SinglePlayerView.GameSettingsView;
using MazeGui.View.GeneralSettingsView.GameSettingsView;

namespace MazeGui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            GeneralSettingsWindow g = GeneralSettingsWindow.GetInstance();
            g.ShowDialog();
            

        }

        private void SinglePlayerButton_Click(object sender, RoutedEventArgs e)
        {
                this.Hide();
                ISettingsModel settingsModel = new SettingsModel();
                SinglePlayerGamesSettingsWindow theSettingsModelWindow = new SinglePlayerGamesSettingsWindow(settingsModel);
                theSettingsModelWindow.ShowDialog();
            
        }

        private void MultiPlayerButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            ISettingsModel settingsModel = new SettingsModel();
            MultiPlayerGamesSettingsWindow theSettingsModelWindow = new MultiPlayerGamesSettingsWindow(settingsModel);
            theSettingsModelWindow.ShowDialog();

        }

        protected override void OnClosed(EventArgs e)
        {
            App.Current.Shutdown();
        }

    }
}
