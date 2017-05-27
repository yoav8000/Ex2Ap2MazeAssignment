using MazeGui.Model.MultiPlayerModel;
using MazeGui.Model.SettingsModel;
using MazeGui.TheViewModel.MultiPlayerVM;
using MazeGui.View.MultiPlayerView.GameView;
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
using MazeGui.View.breakPointWindow;

namespace MazeGui.View.GeneralSettingsView.GameSettingsView
{
    /// <summary>
    /// Interaction logic for MultiPlayerGamesSettingsWindow.xaml
    /// </summary>
    public partial class MultiPlayerGamesSettingsWindow : Window
    {

        private ISettingsModel settingsModel;
        private MultiPlayerViewModel vm;


        public MultiPlayerGamesSettingsWindow(ISettingsModel settingsModel)
        {
            vm = new MultiPlayerViewModel(new MultiPlayerModel(settingsModel,""));
            this.DataContext = vm;
            InitializeComponent();
            cboListOfGames.SelectedIndex = 0;
            this.settingsModel = settingsModel;
            MazeSettingsUC.DataContext = settingsModel;
        }

        private void StartGameButton_Click(object sender, RoutedEventArgs e)
        {
            string mazeName = MazeSettingsUC.txtMazeName.Text;
            waitingImage.Visibility = Visibility.Visible;
            Window win = new BreakPointWindow();
            win.Show();
            win.Close();

            this.waitingImage.Visibility = Visibility.Visible;


            MultiPlayerGameWindow game = new MultiPlayerGameWindow(settingsModel,vm, mazeName, "Start");
            try
            {
                this.Hide();
                if (vm.VM_Maze != null)
                {
                    game.ShowDialog();
                }
                this.Close();
            }
            catch
            {

            }
        }


        protected override void OnClosed(EventArgs e)
        {
            MainWindow window = new MainWindow();
            window.ShowDialog();
        }


        private void JoinGameButton_Click(object sender, RoutedEventArgs e)
        {
            string mazeName = cboListOfGames.SelectedValue.ToString();

            MultiPlayerGameWindow game = new MultiPlayerGameWindow(settingsModel,vm, mazeName,"Join");
            try
            {
                this.Hide();
                game.ShowDialog();
                this.Close();
            }
            catch
            {

            }
        }
    }
}
