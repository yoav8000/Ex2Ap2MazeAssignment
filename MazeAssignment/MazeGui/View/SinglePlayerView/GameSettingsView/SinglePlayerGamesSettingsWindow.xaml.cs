using MazeGui.Model.SettingsModel;
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
using MazeGui.View.SinglePlayerView.GameView;
using MazeGui.TheViewModel.SinglePlayerVM;
using MazeGui.Model.SinglePlayerModel;

namespace MazeGui.View.SinglePlayerView.GameSettingsView
{
    /// <summary>
    /// Interaction logic for SinglePlayerGamesSettingsWindow.xaml
    /// </summary>
    /// <seealso cref="System.Windows.Window" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class SinglePlayerGamesSettingsWindow : Window
    {
        //members.
        private ISettingsModel settingsModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="SinglePlayerGamesSettingsWindow"/> class.
        /// </summary>
        /// <param name="settingsModel">The settings model.</param>
        public SinglePlayerGamesSettingsWindow(ISettingsModel settingsModel)
        {
            InitializeComponent();
            this.settingsModel = settingsModel;
            MazeSettingsUC.DataContext = settingsModel;
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Window.Closed" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnClosed(EventArgs e)
        {
            MainWindow window = new MainWindow();
            window.ShowDialog();
        }


        /// <summary>
        /// Handles the Click event of the OkButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            string mazeName = MazeSettingsUC.txtMazeName.Text;
            int rows = int.Parse(MazeSettingsUC.txtMazeRows.Text);
            int cols = int.Parse(MazeSettingsUC.txtMazeRows.Text);
            this.Hide();
            SinglePlayerGameWindow game = new SinglePlayerGameWindow(settingsModel,mazeName,rows,cols);
            try
            {
                game.ShowDialog();
              
            }
            catch
            {
                this.Close();
            }
           
        }
    }
}
