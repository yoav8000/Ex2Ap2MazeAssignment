using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Academy.Collections;
using Academy.Collections.Generic;
namespace SearchAlgorithmsLib
{
    /// <summary>
    /// priority queue based searcher.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="SearchAlgorithmsLib.Searcher{T}" />
    public abstract class PrioritySearcher<T> : Searcher<T>
    {
        private PriorityQueue<State<T>> openList;


        /// <summary>
        /// Initializes a new instance of the <see cref="PrioritySearcher{T}"/> class.
        /// </summary>
        /// <param name="capacity">The capacity.</param>
        /// <param name="comparer">The comparer.</param>
        public PrioritySearcher(int capacity, IComparer<State<T>> comparer)
        {
            this.openList = new PriorityQueue<State<T>>(capacity, comparer);
        }


        /// <summary>
        /// Gets the open list.
        /// </summary>
        /// <value>
        /// The open list.
        /// </value>
        public PriorityQueue<State<T>> OpenList
        {
            get
            {
                return this.openList;
            }

        }

        /// <summary>
        /// Gets the size of the open list.
        /// </summary>
        /// <value>
        /// The size of the open list.
        /// </value>
        public int OpenListSize
        {
            get
            {
                return this.openList.Count;
            }
        }

        /// <summary>
        /// Pops the open list.
        /// </summary>
        /// <returns></returns>
        protected State<T> PopOpenList()
        {
            IncreaseNodesEvaluated();
            return openList.Dequeue();
        }

        /// <summary>
        /// Adjusts the state of the priority for.
        /// </summary>
        /// adjust the priority if there is a better path to the vertex.
        /// <param name="currentSuccessor">The current successor.</param>
        /// <param name="currentState">State of the current.</param>
        /// <param name="newCost">The new cost.</param>
        protected void AdjustPriorityForState(State<T> currentSuccessor, State<T> currentState, double newCost)///check if works and fix if not.
        {

            State<T>[] array = EmptyQueueIntoArray();
            int size = array.Length;
            for (int i = size - 1; i >= 0; --i)
            {
                if (!array[i].Equals(currentSuccessor))
                {
                    openList.Enqueue(array[i]);
                    continue;

                }
                else
                {
                    if (array[i].Cost > newCost)
                    {
                        array[i].CameFrom = currentState;
                        array[i].Cost = newCost;
                    }
                    openList.Enqueue(array[i]);
                }

            }


        }

        /// <summary>
        /// Empties the queue into array.
        /// </summary>
        /// empties the queue to an array.
        /// <returns></returns>
        private State<T>[] EmptyQueueIntoArray()
        {
            int size = OpenListSize;
            State<T>[] array = new State<T>[size];
            for (int i = 0; i < size; ++i)
            {
                array[i] = OpenList.Dequeue();
            }
            return array;
        }
    }
}