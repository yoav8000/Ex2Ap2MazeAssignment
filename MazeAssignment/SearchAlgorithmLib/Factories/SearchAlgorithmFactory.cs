using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// a factory of algorithms.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SearchAlgorithmFactory<T>
    {

        /// <summary>
        /// Gets the search algorithm.
        /// </summary>
        /// <param name="algorithm">The algorithm.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">there is no such search algorithm in the factory!</exception>
        public ISearcher<T> GetSearchAlgorithm(string algorithm)
        {
            switch (algorithm.ToLower())
            {
                case "bfs":
                    {
                        return new BfsSearcher<T>(new CostComperator<T>());
                    }
                case "dfs":
                    {
                        return new DfsSearcher<T>();
                    }

                default:
                    {
                        throw new Exception("Error: there is no such search algorithm in the factory!");
                    }
            }



        }






    }
}