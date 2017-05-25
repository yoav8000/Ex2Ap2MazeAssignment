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

namespace MazeGui.View.GeneralSettingsView
{
    /// <summary>
    /// Interaction logic for GeneralSettingsWindow.xaml
    /// </summary>
    public partial class GeneralSettingsWindow : Window
    {

        private SettingsViewModel vm;
        private static GeneralSettingsWindow instance;
        public static Mutex MuTexLock = new Mutex();

        private GeneralSettingsWindow()
        {
            InitializeComponent();
            vm = new SettingsViewModel(new SettingsModel());
            this.DataContext = vm;
        }

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

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            vm.SaveSettings();
            Hide();
            MainWindow win = (MainWindow)Application.Current.MainWindow;
            win.Show();

        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow win = (MainWindow)Application.Current.MainWindow;
            win.Show();
            this.Hide();
        }

        protected override void OnClosed(EventArgs e)
        {
            instance = null;
        }
    }
}

