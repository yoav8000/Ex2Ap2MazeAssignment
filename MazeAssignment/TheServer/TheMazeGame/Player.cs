using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TheServer.TheMazeGame
{
    /// <summary>
    /// Player class.
    /// </summary>
    public class Player
    {
        bool communicate;
        private bool needToWait;
        private string mazeName;
        private TcpClient client;
        private string message;
       

     

        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        /// <param name="client">The client.</param>
        public Player(TcpClient client = null)
        {
            this.needToWait = false;
            this.communicate = true;
            this.mazeName = null;
            this.client = client;
          
        }
       


        /// <summary>
        /// Gets or sets a value indicating whether [need to wait].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [need to wait]; otherwise, <c>false</c>.
        /// </value>
        public bool NeedToWait
        {
            get
            {
                return this.needToWait;
            }
            set
            {
                this.needToWait = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Player"/> is communicate.
        /// </summary>
        /// <value>
        ///   <c>true</c> if communicate; otherwise, <c>false</c>.
        /// </value>
        public bool Communicate
        {
            get
            {
                return this.communicate; ;
            }
            set
            {
                this.communicate = value;
            }
        }

        /// <summary>
        /// Gets or sets the name of the maze.
        /// </summary>
        /// <value>
        /// The name of the maze.
        /// </value>
        public string MazeName
        {
            get
            {
                return this.mazeName;
            }

            set
            {
                this.mazeName = value;  
            }
        }

        /// <summary>
        /// Gets or sets the client.
        /// </summary>
        /// <value>
        /// The client.
        /// </value>
        public TcpClient Client
        {
            get
            {
                return this.client;
            }

            set
            {
                this.client = value;
            }
        }

        
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message
        {
            get
            {
                return this.message;
            }
            set
            {
                this.message = value;
            }
        }

        /// <summary>
        /// Notifies the client.
        /// </summary>
        /// <param name="message">The message.</param>
        public void NotifyClient(string message)
        {
            NetworkStream stream = Client.GetStream();
            StreamWriter streamWriter = new StreamWriter(stream);
            streamWriter.WriteLine(message.Replace(Environment.NewLine, ""));
            streamWriter.Flush();
        }



      

        /// <summary>
        /// Equalses the specified other.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns></returns>
        public bool Equals(Player other)
        {
            return this.Client.Equals(other.Client);
        }



    }
}
