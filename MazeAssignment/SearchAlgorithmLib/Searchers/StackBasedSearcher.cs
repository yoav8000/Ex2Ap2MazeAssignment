using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    public abstract class StackBasedSearcher<T> : Searcher<T>
    {
        private int stacksCapacity;

        public StackBasedSearcher(int stacksCap)
        {
            MyStack = new Stack<State<T>>(stacksCap);
            this.stacksCapacity = stacksCap;
        }

        public StackBasedSearcher()
        {
            MyStack = new Stack<State<T>>();
        }

        public Stack<State<T>> MyStack
        {
            get;
        }

        public State<T> PopStack()
        {
            IncreaseNodesEvaluated();
            return MyStack.Pop();
        }

        public int MyStackSize
        {
            get
            {
                return MyStack.Count;
            }
        }


    }
}