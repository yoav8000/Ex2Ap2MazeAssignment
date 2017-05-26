using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientDll
{
  public class Client
    {
        /// <summary>
        /// The members
        /// </summary>
        private bool communicate;
        private NetworkStream stream;
        private StreamReader streamReader;
        private StreamWriter streamWriter;
        private TcpClient theClient;
        private IPEndPoint ep;
        private int portNumber;


        /// <summary>
        /// Initializes a new instance of the <see cref="Client"/> class.
        /// </summary>
        /// <param name="ep">The ep.</param>
        /// <param name="portNumber">The port number.</param>
        public Client(string ip, int portNumber)
        {
            this.ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), portNumber);
            this.portNumber = portNumber;
            string result = CreateANewConnection();
            if (!result.Contains("Connection Error"))
            {
                communicate = true;
                streamWriter.AutoFlush = true;
            }
        }


        /// <summary>
        /// Gets or sets the client.
        /// </summary>
        /// <value>
        /// The client.
        /// </value>
        public TcpClient TheClient
        {
            get
            {
                return this.theClient;
            }

            set
            {
                this.theClient = value;
            }
        }

        /// <summary>
        /// Gets or sets the stream reader.
        /// </summary>
        /// <value>
        /// The stream reader.
        /// </value>
        public StreamReader StreamReader
        {
            get
            {
                return this.streamReader;
            }
            set
            {
                this.streamReader = value;
            }
        }

        /// <summary>
        /// Gets or sets the stream writer.
        /// </summary>
        /// <value>
        /// The stream writer.
        /// </value>
        public StreamWriter StreamWriter
        {
            get
            {
                return this.streamWriter;
            }
            set
            {
                this.streamWriter = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Client"/> is communicate.
        /// </summary>
        /// <value>
        ///   <c>true</c> if communicate; otherwise, <c>false</c>.
        /// </value>
        public bool Communicate
        {
            get
            {
                return this.communicate;
            }
            set
            {
                this.communicate = value;
            }
        }
        public string ReadMessage()
        {
            try
            {
                string result = StreamReader.ReadLine();
                if (result != null)
                {

                    //  Console.WriteLine(result);
                    string[] arr;
                    arr = result.Split(' ');
                    if (arr[0].StartsWith("Error"))
                    {
                        //  Console.WriteLine("there was an error please type another command ");
                        return "Input Error";
                    }
                    if (arr[0].Contains("Closed"))
                    {
                        //  Console.WriteLine("other player closed connection ");
                        return "The other player closed the game";
                    }
                    return result;
                }
                return null;
            }

            catch
            {
                communicate = false;
                return "Communication with server ended";

            }
        }




        /// <summary>
        /// Writes the message.
        /// </summary>
        public string WriteMessage(string command)
        {

            // Console.WriteLine("Please enter a command: ");

            //string command = Console.ReadLine();
            if (command != null && command != " ")
            {
                //  Console.WriteLine($"the command is: {command} ");
                if (!communicate)
                {
                    CreateANewConnection();
                    communicate = true;

                }
                try
                {
                    StreamWriter.WriteLine(command);
                    StreamWriter.Flush();
                    return "";
                }
                catch
                {
                    communicate = false;
                    return "Communication Error";
                }
            }
            return null;
        }


        /// <summary>
        /// Closes the connection.
        /// </summary>
        public void CloseConnection()
        {
            TheClient.Close();
        }

        /// <summary>
        /// Creates a new connection.
        /// </summary>
        public string CreateANewConnection()
        {
            TheClient = new TcpClient();
            try
            {


                TheClient.Connect(this.ep);//connect to the server
                this.stream = TheClient.GetStream();
                StreamReader = new StreamReader(TheClient.GetStream());
                StreamWriter = new StreamWriter(TheClient.GetStream());
                Console.WriteLine("you are connected ");
                communicate = true;
                return "";
            }
            catch
            {
                return "Connection Error";
            }
        }


    }

}


