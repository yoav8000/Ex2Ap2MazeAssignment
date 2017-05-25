using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchAlgorithmsLib;
using MazeLib;
using MazeGeneratorLib;


/// <summary>
/// dll
/// </summary>
namespace SearchAlgorithmsLib
{
    /// <summary>
    /// maze adapter
    /// </summary>
    /// <seealso cref="SearchAlgorithmsLib.ISearchable{MazeLib.Position}" />
    public class MazeAdapter : ISearchable<Position>
    {
        //members
        private Maze mazeGame;
        public Maze MazeGame
        {
            get;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MazeAdapter"/> class.
        /// </summary>
        /// <param name="maze">The maze.</param>
        public MazeAdapter(Maze maze)
        {
            this.mazeGame = maze;
        }

        /// <summary>
        /// Gets the initial state.
        /// </summary>
        /// <returns></returns>
        public State<Position> GetInitialState()
        {
            State<Position> state = State<Position>.StatePool.GetState(mazeGame.InitialPos);
            state.Cost = 0;
            return state;
        }

        /// <summary>
        /// Gets the state of the goal.
        /// </summary>
        /// <returns></returns>
        public State<Position> GetGoalState()
        {
            State<Position> state = State<Position>.StatePool.GetState(mazeGame.GoalPos);
            return state;
        }

        /// <summary>
        /// Gets the states cost.
        /// </summary>
        /// <param name="src">The source.</param>
        /// <param name="dst">The DST.</param>
        /// <returns></returns>
        public double GetStatesCost(State<Position> src, State<Position> dst)
        {
            return 1;
        }


        /// <summary>
        /// Gets all possible states.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <returns></returns>
        public List<State<Position>> GetAllPossibleStates(State<Position> state)
        {
            List<State<Position>> successors = new List<State<Position>>();
            int row = state.StateIdentifier.Row;
            int col = state.StateIdentifier.Col;

            ///left neighbor
            if ((col > 0) && (col < mazeGame.Cols) && (mazeGame[row, col - 1] == CellType.Free))
            {
                State<Position> currentSuccesor = State<Position>.StatePool.GetState(new Position(row, col - 1));
                successors.Add(currentSuccesor);
            }



            ///upper neighbor
            if ((row > 0) && (row < mazeGame.Rows) && (mazeGame[row - 1, col] == CellType.Free))
            {
                State<Position> currentSuccesor = State<Position>.StatePool.GetState(new Position(row - 1, col));
                successors.Add(currentSuccesor);
            }

            ///right neighbor
            if ((col >= 0) && (col < mazeGame.Cols - 1) && (mazeGame[row, col + 1] == CellType.Free))
            {
                State<Position> currentSuccesor = State<Position>.StatePool.GetState(new Position(row, col + 1));
                successors.Add(currentSuccesor);
            }

            ///lower neighbor
            if ((row >= 0) && (row < mazeGame.Rows - 1) && (mazeGame[row + 1, col] == CellType.Free))
            {
                State<Position> currentSuccesor = State<Position>.StatePool.GetState(new Position(row + 1, col));
                successors.Add(currentSuccesor);
            }

            return successors;

        }


    }
}
