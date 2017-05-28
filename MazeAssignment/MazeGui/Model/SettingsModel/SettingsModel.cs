using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGui.Model.SettingsModel
{
    /// <summary>
    /// SettingsModel implements ISettingsModel
    /// </summary> 
    /// <seealso cref="MazeGui.Model.SettingsModel.ISettingsModel" />
    public class SettingsModel : ISettingsModel
    {
        //members.
        private string oldServerIp;
        private int oldServerPort;
        private int oldMazeRows;
        private int oldMazeCols;
        private int oldSearchAlgo;



        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsModel"/> class.
        /// </summary>
        public SettingsModel()
        {
            oldServerIp = ServerIp;
            oldServerPort = ServerPort;
            oldMazeRows = MazeRows;
            oldMazeCols = MazeCols;
            oldSearchAlgo = SearchAlgorithm;
        }

        /// <summary>
        /// Gets or sets the server ip.
        /// </summary>
        /// <value>
        /// The server ip.
        /// </value>
        public string ServerIp
        {
            get { return Properties.Settings.Default.ServerIp; }
            set {
                if (value != oldServerIp)
                {
                    this.oldServerIp = ServerIp;
                }
                Properties.Settings.Default.ServerIp = value; }
        }
        /// <summary>
        /// Gets or sets the server port.
        /// </summary>
        /// <value>
        /// The server port.
        /// </value>
        public int ServerPort
        {
            get { return Properties.Settings.Default.ServerPort; }
            set {
                if (value != oldServerPort)
                {
                    this.oldServerPort = ServerPort;
                }
                Properties.Settings.Default.ServerPort = value; }
        }

        /// <summary>
        /// Gets or sets the maze rows.
        /// </summary>
        /// <value>
        /// The maze rows.
        /// </value>
        public int MazeRows
        {
            get { return Properties.Settings.Default.MazeRows; }
            set {
                if (value != oldMazeRows)
                {
                    this.oldMazeRows = MazeRows;
                }
                Properties.Settings.Default.MazeRows = value;
           

            }
        }

        /// <summary>
        /// Gets or sets the maze cols.
        /// </summary>
        /// <value>
        /// The maze cols.
        /// </value>
        public int MazeCols
        {
            get { return Properties.Settings.Default.MazeCols; }
            set {
                if (value != oldMazeCols)
                {
                    this.oldMazeCols = MazeCols;
                }
                Properties.Settings.Default.MazeCols = value; }
        }

        /// <summary>
        /// Gets or sets the search algorithm.
        /// </summary>
        /// <value>
        /// The search algorithm.
        /// </value>
        public int SearchAlgorithm
        {
            get { return Properties.Settings.Default.SearchAlgorithm; }
            set {
                if (value != oldSearchAlgo)
                {
                    this.oldSearchAlgo = SearchAlgorithm;
                }
                Properties.Settings.Default.SearchAlgorithm = value; }
        }

        /// <summary>
        /// Saves the settings.
        /// </summary>
        public void SaveSettings()
        {
            this.oldServerIp = ServerIp;
            this.oldServerPort = ServerPort;
            this.oldMazeRows = MazeRows;
            this.oldMazeCols = MazeCols;
            this.oldSearchAlgo = SearchAlgorithm;
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// Cancels the settings.
        /// </summary>
        public void CancelSettings()
        {
            if (oldServerIp != ServerIp)
            {
                this.ServerIp = oldServerIp;
            }
            if (oldServerPort != ServerPort)
            {
                this.ServerPort = oldServerPort;
            }
            if (oldMazeRows != MazeRows)
            {
                this.MazeRows = oldMazeRows;
            }
            if (oldMazeCols != MazeCols)
            {
                this.MazeCols = oldMazeCols;
            }
            if (oldSearchAlgo != SearchAlgorithm)
            {
                this.SearchAlgorithm = oldSearchAlgo;
            }
          
        }

    }
}
