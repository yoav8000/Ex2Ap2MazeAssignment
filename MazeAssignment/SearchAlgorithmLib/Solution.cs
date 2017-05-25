using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    public class Solution<T>
    {
        private List<State<T>> pathToDestination;
        private int nodesEvaluated;


        public Solution(List<State<T>> path, int nodesEvaluated1)
        {
            this.pathToDestination = path;
            this.nodesEvaluated = nodesEvaluated1;
        }

        public List<State<T>> PathToDestination
        {
            get
            {
                return this.pathToDestination;
            }
        }

        public int NodesEvaluated
        {
            get
            {
                return this.nodesEvaluated;
            }
            set
            {
                if (value < 0)
                {
                    throw new Exception("the amount of evaluated nodes has to be positive");
                }
                this.nodesEvaluated = 0;
            }
        }

    }
}