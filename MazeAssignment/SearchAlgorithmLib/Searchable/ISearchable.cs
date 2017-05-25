using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISearchable<T>
    {
        /// <summary>
        /// Gets the initial state.
        /// </summary>
        /// <returns></returns>
        /// 
        State<T> GetInitialState();



        /// <summary>
        /// Gets the state of the goal.
        /// </summary>
        /// <returns></returns>
        /// 
        State<T> GetGoalState();
        /// <summary>
        /// Gets all possible states.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns></returns>
        /// 
        List<State<T>> GetAllPossibleStates(State<T> s);




        /// <summary>
        /// Gets the states cost.
        /// </summary>
        /// <param name="src">The source.</param>
        /// <param name="dst">The DST.</param>
        /// <returns></returns>
        double GetStatesCost(State<T> src, State<T> dst);
    }
}