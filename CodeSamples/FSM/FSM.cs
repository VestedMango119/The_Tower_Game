using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

//Code based on Tutorial 4 in the INM379 module - City university of London
namespace towerGame2
{
    public class FSM
    {
        private object m_Owner;
        private List<State> m_States;

        private State m_CurrentState;

        public FSM() : this(null)
        {
        }

        public FSM(object owner)
        {
            m_Owner = owner;
            m_States = new List<State>();
            m_CurrentState = null;
        }

        public void Initialise(string stateName)
        {
            m_CurrentState = m_States.Find(state => state.Name.Equals(stateName));
            if(m_CurrentState != null)
            {
                m_CurrentState.Enter(m_Owner);
            }
        }

        public void AddState(State state)
        {
            m_States.Add(state);
        }

        public void Update(GameTime gameTime)
        {
            if (m_CurrentState == null) return; 

            foreach(Transition t in m_CurrentState.Transitions)
            {
                if (t.Condition())
                {
                    m_CurrentState.Exit(m_Owner);
                    m_CurrentState = t.NextState;
                    m_CurrentState.Enter(m_Owner);
                    break;
                }
            }

            m_CurrentState.Execute(m_Owner, gameTime);
        }
    }
}
