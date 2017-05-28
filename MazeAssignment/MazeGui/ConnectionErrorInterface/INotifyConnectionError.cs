using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGui.ConnectionErrorInterface
{
    /// <summary>
    /// interface that contains the connection error.
    /// </summary>
    public interface INotifyConnectionError
    {
        event CriticalErrorHandler ConnectionErrorOccurred;
        void NotifyConnectionError(string message);

    }


    /// <summary>
    /// a delegate from the critical error handler type.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
    public delegate void CriticalErrorHandler(object sender, PropertyChangedEventArgs e);
}

