using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

//Code based on Tutorial 4 in the INM379 module
namespace towerGame2
{
    public abstract class State
    {
        public abstract void Enter(object owner); //functionality on entry of new state
        public abstract void Exit(object owner); //functionality on exit of current state
        public abstract void Execute(object owner, GameTime gameTime); //functionality during current state

        public string Name
        {
            get;
            set;
        }

        //list of possible state transitions
        private List<Transition> m_Transitions = new List<Transition>();
        public List<Transition> Transitions
        {
            get { return m_Transitions;  }
        }

        public void AddTransition(Transition transition)
        {
            m_Transitions.Add(transition);
        }
    }

    public class Transition
    {
        public readonly State NextState;
        public readonly Func<bool> Condition;

        public Transition(State nextState, Func<bool> condition)
        {
            NextState = nextState;
            Condition = condition; 
        }
    }
}
