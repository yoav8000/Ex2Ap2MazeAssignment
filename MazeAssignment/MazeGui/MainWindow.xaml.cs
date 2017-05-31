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
    /// <seealso cref="System.Windows.Window" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Click event of the SettingsButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            GeneralSettingsWindow g = GeneralSettingsWindow.GetInstance();
            g.ShowDialog();
            this.ShowDialog();
            

        }

        /// <summary>
        /// Handles the Click event of the SinglePlayerButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void SinglePlayerButton_Click(object sender, RoutedEventArgs e)
        {
                this.Hide();
                ISettingsModel settingsModel = new SettingsModel();
                SinglePlayerGamesSettingsWindow theSettingsModelWindow = new SinglePlayerGamesSettingsWindow(settingsModel);
                theSettingsModelWindow.ShowDialog();
                
            
        }

        /// <summary>
        /// Handles the Click event of the MultiPlayerButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void MultiPlayerButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            ISettingsModel settingsModel = new SettingsModel();
            MultiPlayerGamesSettingsWindow theSettingsModelWindow = new MultiPlayerGamesSettingsWindow(settingsModel);
            theSettingsModelWindow.ShowDialog();            

        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Window.Closed" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnClosed(EventArgs e)
        {
            App.Current.Shutdown();
        }

    }
}
