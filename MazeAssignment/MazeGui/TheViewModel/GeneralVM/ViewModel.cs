using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeGui.ConnectionErrorInterface;
using System.ComponentModel;

namespace MazeGui.ViewModel.GeneralVM
{
    public abstract class ViewModel : INotifyConnectionError
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public event CriticalErrorHandler ConnectionErrorOccurred;

        public void NotifyConnectionError(string message)
        {
            this.ConnectionErrorOccurred?.Invoke(this, new PropertyChangedEventArgs(message));
        }

        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}