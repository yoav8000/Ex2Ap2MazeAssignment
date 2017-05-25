using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// bfs searcher
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="SearchAlgorithmsLib.PrioritySearcher{T}" />
    public class BfsSearcher<T> : PrioritySearcher<T>
    {
        private IComparer<State<T>> comparer;
        private int searchableCapacity;

        /// <summary>
        /// Initializes a new instance of the <see cref="BfsSearcher{T}"/> class.
        /// </summary>
        /// <param name="comparer1">The comparer1.</param>
        /// <param name="capacity1">The capacity1.</param>
        public BfsSearcher(IComparer<State<T>> comparer1, int capacity1) : base(capacity1, comparer1)
        {
            this.comparer = comparer1;
            this.searchableCapacity = capacity1;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="BfsSearcher{T}"/> class.
        /// </summary>
        /// <param name="comparer1">The comparer1.</param>
        public BfsSearcher(IComparer<State<T>> comparer1) : base(10000, comparer1)
        {
            this.comparer = comparer1;
            this.searchableCapacity = 10000;
        }

        /// <summary>
        /// Searches the specified searchable.
        /// </summary>
        /// <param name="searchable">The searchable.</param>
        /// <returns></returns>
        public override Solution<T> Search(ISearchable<T> searchable)// Searcher's abstract method overriding
        {
            State<T> initialState = searchable.GetInitialState();
            OpenList.Enqueue(initialState);

            HashSet<State<T>> closed = new HashSet<State<T>>();
            while (OpenListSize > 0)
            {
                State<T> currentState = PopOpenList(); // inherited from Searcher, removes the best state
                closed.Add(currentState);
                if (currentState.Equals(searchable.GetGoalState()))
                {
                    return Backtrace(searchable.GetGoalState(), GetNumberOfNodesEvaluated()); // private method, back traces through the parents
                }
                // calling the delegated method, returns a list of state 
                List<State<T>> succerssors = searchable.GetAllPossibleStates(currentState);
                foreach (State<T> currentSuccessor in succerssors)
                {
                    if (!closed.Contains(currentSuccessor) && !OpenList.Contains(currentSuccessor))
                    {
                        currentSuccessor.CameFrom = currentState;
                        currentSuccessor.Cost = currentState.Cost + searchable.GetStatesCost(currentState, currentSuccessor);
                        OpenList.Enqueue(currentSuccessor);
                    }
                    else
                    {
                        if (!closed.Contains(currentSuccessor))
                        {
                            AdjustPriorityForState(currentSuccessor, currentState,
                                currentState.Cost + searchable.GetStatesCost(currentState, currentSuccessor));
                        }
                    }


                }
            }
            return null;
        }
    }
}