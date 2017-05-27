using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGui.NoifyGameClosedInterface
{

    public delegate void GameWasClosedEventHandler(string laseCommand);

    public interface INotifyGameWasClosed
    {
        event GameWasClosedEventHandler GameWasClosed;
    }

}
