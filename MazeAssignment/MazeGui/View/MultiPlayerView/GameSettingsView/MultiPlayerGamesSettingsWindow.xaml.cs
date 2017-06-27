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
    /// <seealso cref="System.Windows.Window" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class MultiPlayerGamesSettingsWindow : Window
    {
        //members.
        private ISettingsModel settingsModel;
        private MultiPlayerViewModel vm;


        /// <summary>
        /// Initializes a new instance of the <see cref="MultiPlayerGamesSettingsWindow"/> class.
        /// </summary>
        /// <param name="settingsModel">The settings model.</param>
        public MultiPlayerGamesSettingsWindow(ISettingsModel settingsModel)
        {
            vm = new MultiPlayerViewModel(new MultiPlayerModel(settingsModel,""));
            this.DataContext = vm;
            InitializeComponent();
            cboListOfGames.SelectedIndex = 0;
            this.settingsModel = settingsModel;
            MazeSettingsUC.DataContext = settingsModel;
        }

        /// <summary>
        /// Handles the Click event of the StartGameButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void StartGameButton_Click(object sender, RoutedEventArgs e)
        {
            string mazeName = MazeSettingsUC.txtMazeName.Text;
            int rows = int.Parse(MazeSettingsUC.txtMazeRows.Text);
            int cols = int.Parse(MazeSettingsUC.txtMazeRows.Text);
            waitingImage.Visibility = Visibility.Visible;
            Window win = new BreakPointWindow();
            win.Show();
            win.Close();

            this.waitingImage.Visibility = Visibility.Visible;

            MultiPlayerGameWindow game = new MultiPlayerGameWindow(settingsModel,vm, mazeName,rows,cols, "Start");
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


        /// <summary>
        /// Raises the <see cref="E:System.Windows.Window.Closed" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnClosed(EventArgs e)
        {
            MainWindow window = new MainWindow();
            window.ShowDialog();
        }


        void OnDropDownOpened(object sender, EventArgs e)
        {
            vm.VM_ListGames();
        }

        /// <summary>
        /// Handles the Click event of the JoinGameButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void JoinGameButton_Click(object sender, RoutedEventArgs e)
        {
            if (cboListOfGames.SelectedValue != null)
            {
                string mazeName = cboListOfGames.SelectedValue.ToString();

                MultiPlayerGameWindow game = new MultiPlayerGameWindow(settingsModel, vm, mazeName,0,0, "Join");
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
}
