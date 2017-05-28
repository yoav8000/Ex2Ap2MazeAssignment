using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGui.Model.SettingsModel
{
    /// <summary>
    /// an interface called ISettingsModel.
    /// </summary>
    public interface ISettingsModel
    {
        //properties.
        string ServerIp { get; set; }
        int ServerPort { get; set; }
        int MazeRows { get; set; }
        int MazeCols { get; set; }
        int SearchAlgorithm { get; set; }
        void SaveSettings();
        void CancelSettings();
    }
}