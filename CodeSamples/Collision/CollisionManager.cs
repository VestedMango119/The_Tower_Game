using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Code based on the CollisonManager class in Tutorial 4 in the INM379 module - City University of London

namespace towerGame2
{
    public class CollisionManager
    {
        private List<Collidable> m_collidables = new List<Collidable>();

        private HashSet<Collision> m_collisions = new HashSet<Collision>(new CollisionComparer());

        public void AddCollidable(Collidable c)
        {
            m_collidables.Add(c);
        }

        //extra function to add player directly to the list of collidables
        public void AddCollidable(Player p)
        {
            m_collidables.Add(p);
        }


        public bool RemoveCollidable(Collidable c)
        {
            return m_collidables.Remove(c);
        }

        //returns a list of collidable objects currently in game.
        public List<Collidable> GetCollidables()
        {
            return m_collidables;
        }


        public void Update()
        {
            UpdateCollisions();
            ResolveCollisions();
        }

        //check collidables at each frame to see if they are overlapping.
        private void UpdateCollisions()
        {
            if(m_collisions.Count > 0)
            {
                m_collisions.Clear();
            }

            for (int i = 0; i < m_collidables.Count; i++)
            {
                for (int j = 0; j < m_collidables.Count; j++)
                {
                    Collidable collidable1 = m_collidables[i];
                    Collidable collidable2 = m_collidables[j];

                    if (!collidable1.Equals(collidable2))
                    {
                        if (collidable1.CollisionTest(collidable2))
                        {
                            m_collisions.Add(new Collision(collidable1, collidable2));
                        }
                    }
                }
            }
        }

        //resolve any collisions that were detected in the UpdateCollisions function
        private void ResolveCollisions()
        {
            foreach(Collision collision in m_collisions)
            {
                collision.Resolve();
            }
        }
    }
}
