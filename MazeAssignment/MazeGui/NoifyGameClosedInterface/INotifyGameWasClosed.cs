using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGui.NoifyGameClosedInterface
{


    /// <summary>
    /// interface called INotifyGameWasClosed holds an event of the type GameWasClosedEventHandler.
    /// </summary>
    public interface INotifyGameWasClosed
    {
        event GameWasClosedEventHandler GameWasClosed;
    }

    /// <summary>
    /// delegate called  GameWasClosedEventHandler.
    /// </summary>
    /// <param name="laseCommand">The lase command.</param>
    public delegate void GameWasClosedEventHandler(string laseCommand);
}
