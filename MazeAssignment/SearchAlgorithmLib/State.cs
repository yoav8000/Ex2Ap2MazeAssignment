using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    public class State<T> : IComparable
    {
        private double cost;
        private State<T> cameFrom;
        private T stateIdentifier;
        public double Cost
        {
            get
            {
                return cost;
            }
            set
            {
                if (value < -1)
                {
                    throw new ArgumentOutOfRangeException("Value can not be negative");
                }
                this.cost = value;
            }
        }

        public State<T> CameFrom
        {
            get
            {
                return cameFrom;
            }
            set
            {
                this.cameFrom = value;
            }
        }

        public T StateIdentifier
        {
            get
            {
                return this.stateIdentifier;
            }
        }



        public State(double cost, State<T> cameFrom)
        {
            this.cost = cost;
            this.CameFrom = cameFrom;
        }

        public State()
        {
            this.cost = 0;
            this.CameFrom = null;
        }

        public State(T identifier)
        {
            this.stateIdentifier = identifier;
            this.CameFrom = null;
        }
        public override int GetHashCode()
        {
            return String.Intern(this.stateIdentifier.ToString()).GetHashCode();
        }


        public bool Equals(State<T> other)
        {
            return this.stateIdentifier.Equals(other.StateIdentifier);

        }

        public override bool Equals(object obj)////check if works fine.
        {
            if ((obj != null) && (this != null))
            {
                if (obj.GetType() == typeof(State<T>))
                {
                    return this.Equals(obj as State<T>);
                }
            }
            return false;
        }


        public int CompareTo(Object obj)
        {
            if (obj == null)
            {
                return -1;
            }
            else
            {
                return this.CompareTo(obj as State<T>);
            }
        }

        public static class StatePool
        {
            private static Dictionary<T, State<T>> statePool = new Dictionary<T, State<T>>();

            public static State<T> GetState(T identifier)
            {
                if (!statePool.ContainsKey(identifier))
                {
                    statePool.Add(identifier, new State<T>(identifier));
                }
                return statePool[identifier];
            }


        }

    }
}