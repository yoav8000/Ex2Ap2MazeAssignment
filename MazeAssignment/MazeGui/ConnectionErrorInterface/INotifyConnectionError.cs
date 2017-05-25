using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGui.ConnectionErrorInterface
{
    public interface INotifyConnectionError
    {
        event CriticalErrorHandler ConnectionErrorOccurred;
        void NotifyConnectionError(string message);

    }


    public delegate void CriticalErrorHandler(object sender, PropertyChangedEventArgs e);
}

