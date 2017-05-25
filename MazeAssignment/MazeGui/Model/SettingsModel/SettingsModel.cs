using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGui.Model.SettingsModel
{
    public class SettingsModel : ISettingsModel
    {
        public SettingsModel()
        {

        }

        public string ServerIp
        {
            get { return Properties.Settings.Default.ServerIp; }
            set { Properties.Settings.Default.ServerIp = value; }
        }
        public int ServerPort
        {
            get { return Properties.Settings.Default.ServerPort; }
            set { Properties.Settings.Default.ServerPort = value; }
        }

        public int MazeRows
        {
            get { return Properties.Settings.Default.MazeRows; }
            set { Properties.Settings.Default.MazeRows = value; }
        }

        public int MazeCols
        {
            get { return Properties.Settings.Default.MazeCols; }
            set { Properties.Settings.Default.MazeCols = value; }
        }

        public int SearchAlgorithm
        {
            get { return Properties.Settings.Default.SearchAlgorithm; }
            set { Properties.Settings.Default.SearchAlgorithm = value; }
        }

        public void SaveSettings()
        {
            Properties.Settings.Default.Save();
        }
    }
}
