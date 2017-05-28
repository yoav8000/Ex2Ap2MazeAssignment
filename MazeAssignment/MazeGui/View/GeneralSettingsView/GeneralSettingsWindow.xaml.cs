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
using MazeGui.ViewModel;
using MazeGui.Model.SettingsModel;
using System.Threading;
using MazeGui.ViewModel.SettingsVM;
using MazeGui.View.breakPointWindow;

namespace MazeGui.View.GeneralSettingsView
{
    /// <summary>
    /// Interaction logic for GeneralSettingsWindow.xaml implemented as singleton.
    /// </summary>
    /// <seealso cref="System.Windows.Window" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class GeneralSettingsWindow : Window
    {
        //members.
        private SettingsViewModel vm;
        private static GeneralSettingsWindow instance;
        public static Mutex MuTexLock = new Mutex();

        /// <summary>
        /// Prevents a default instance of the <see cref="GeneralSettingsWindow"/> class from being created.
        /// </summary>
        private GeneralSettingsWindow()
        {
            InitializeComponent();
            vm = new SettingsViewModel(new SettingsModel());
            this.DataContext = vm;
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <returns></returns>
        public static GeneralSettingsWindow GetInstance()
        {
            if (MuTexLock == null)
            {
                MuTexLock = new Mutex();
            }

            MuTexLock.WaitOne();
            if (instance == null)
            {
                instance = new GeneralSettingsWindow();
            }
            MuTexLock.ReleaseMutex();
            return instance;

        }

        /// <summary>
        /// Handles the Click event of the BtnOK control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            vm.SaveSettings();
            Hide();

        }

        /// <summary>
        /// Handles the Click event of the BtnCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            vm.CancelSettings();
            this.Close();
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Window.Closed" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnClosed(EventArgs e)
        {
            instance = null;
        }
    }
}

