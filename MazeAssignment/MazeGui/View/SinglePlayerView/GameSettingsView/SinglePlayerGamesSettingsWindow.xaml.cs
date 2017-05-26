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
    public partial class SinglePlayerGamesSettingsWindow : Window
    {
        private ISettingsModel settingsModel;

        public SinglePlayerGamesSettingsWindow(ISettingsModel settingsModel)
        {
            InitializeComponent();
            this.settingsModel = settingsModel;
            MazeSettingsUC.DataContext = settingsModel;
        }

        protected override void OnClosed(EventArgs e)
        {
            MainWindow window = new MainWindow();
            window.ShowDialog();
        }


        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            string mazeName = MazeSettingsUC.txtMazeName.Text;
            this.Hide();
            SinglePlayerGameWindow game = new SinglePlayerGameWindow(settingsModel,mazeName);
            try
            {
                game.ShowDialog();
                this.Close();
            }
            catch
            {

            }
           
        }
    }
}
