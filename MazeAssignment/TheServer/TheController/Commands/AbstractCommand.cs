using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TheServer.TheModel;
using TheServer.TheMazeGame;

namespace TheServer.TheController.Commands
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="TheServer.TheController.Commands.ICommand" />
    public abstract class AbstractCommand : ICommand
    {
        private IModel imodel;

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractCommand"/> class.
        /// </summary>
        /// <param name="imodel">The imodel.</param>
        public AbstractCommand(IModel imodel)
        {
            this.imodel = imodel;
        }

        /// <summary>
        /// Gets or sets the i model.
        /// </summary>
        /// <value>
        /// The i model.
        /// </value>
        public IModel IModel
        {
            get
            {
                return this.imodel;
            }
            set
            {
                this.imodel = value;
            }
        }

        /// <summary>
        /// Executes the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="player">The player.</param>
        /// <returns></returns>
        public abstract string Execute(string[] args, Player player);
    }
}
