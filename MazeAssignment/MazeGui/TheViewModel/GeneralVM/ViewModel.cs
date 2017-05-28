using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeGui.ConnectionErrorInterface;
using System.ComponentModel;

namespace MazeGui.ViewModel.GeneralVM
{
    /// <summary>
    /// ViewModel implements INotifyConnectionError and INotifyPropertyChanged.
    /// </summary>
    /// <seealso cref="MazeGui.ConnectionErrorInterface.INotifyConnectionError" />
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public abstract class ViewModel : INotifyConnectionError , INotifyPropertyChanged
    {
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Occurs when [connection error occurred].
        /// </summary>
        public event CriticalErrorHandler ConnectionErrorOccurred;

        /// <summary>
        /// Notifies the connection error.
        /// </summary>
        /// <param name="message">The message.</param>
        public void NotifyConnectionError(string message)
        {
            this.ConnectionErrorOccurred?.Invoke(this, new PropertyChangedEventArgs(message));
        }

        /// <summary>
        /// Notifies the property changed.
        /// </summary>
        /// <param name="propName">Name of the property.</param>
        public void NotifyPropertyChanged(string propName)
          {
              this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
          }
          
        
    }
}