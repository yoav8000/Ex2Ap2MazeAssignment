using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using System.IO;
using System.Net.Sockets;

namespace TheServer.TheMazeGame
{
    /// <summary>
    /// MazeGame class.
    /// </summary>
    public class MazeGame
    {
        /// <summary>
        /// The members.
        /// </summary>
        private int gameCapacity;
        private List<Player> players;
        private string mazeName;
        private Maze maze;



        /// <summary>
        /// Initializes a new instance of the <see cref="MazeGame"/> class.
        /// </summary>
        /// <param name="mazeName">Name of the maze.</param>
        /// <param name="maze">The maze.</param>
        /// <param name="gameCapacity">The game capacity.</param>
        public MazeGame(string mazeName,Maze maze, int gameCapacity)
        {
            this.players = new List<Player>();
            this.maze = maze;
            this.gameCapacity = gameCapacity;
            this.mazeName = mazeName;
        }

        /// <summary>
        /// Gets or sets the game capacity.
        /// </summary>
        /// <value>
        /// The game capacity.
        /// </value>
        public int GameCapacity//the minimum is 2 players
        {
            get
            {
                return this.gameCapacity;
            }
            set
            {
                if (value < 2)
                {
                    this.gameCapacity = 2;
                }
                else
                {
                    this.gameCapacity = value;
                }
            }
        }
        /// <summary>
        /// Gets the maze.
        /// </summary>
        /// <value>
        /// The maze.
        /// </value>
        public Maze Maze
        {
            get
            {
                return this.maze;
            }
            
        }
        public string MazeName
        {
            get
            {
                return this.mazeName;
            }
        }

        /// <summary>
        /// Gets the players.
        /// </summary>
        /// <value>
        /// The players.
        /// </value>
        public List<Player> Players
        {
            get
            {
                return this.players;
            }
        }

        /// <summary>
        /// Adds the player.
        /// </summary>
        /// <param name="player">The player.</param>
        public void AddPlayer(Player player)
        {
            players.Add(player);
            
        }

        /// <summary>
        /// Removes the player.
        /// </summary>
        /// <param name="player">The player.</param>
        public void RemovePlayer(Player player)//need to override the equal method.
        {
            players.Remove(player);
        }





        /// <summary>
        /// Notifies all players.
        /// </summary>
        /// <param name="message">The message.</param>
        public void NotifyAllPlayers(string message)
        {
         foreach(Player p in players)
            {

                p.NotifyClient(message);
            }  
        }

        /// <summary>
        /// Closes all clients.
        /// </summary>
        public void CloseAllClients()
        {
            foreach (Player p in players)
            {
                p.Communicate = false;
                p.Client.Close();
            }
        }



        /// <summary>
        /// Notifies the other players.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="player">The player.</param>
        public void NotifyOtherPlayers(string message, Player player)
        {
            foreach (Player p in players)
            {
                if (!p.Equals(player))
                {
                    p.NotifyClient(message);
                }
                
            }
        }
    }
}
