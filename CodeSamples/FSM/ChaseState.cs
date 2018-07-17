using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

//Code based on Tutorial 5 in the INM379 module

namespace towerGame2
{
    public class ChaseState : State
    {
        

        public ChaseState()
        {
            Name = "Chase";
            
        }

        public override void Enter(object owner)
        {
            Enemy enemy = owner as Enemy;
            if(enemy != null)
            {
                enemy.VelocityScalar = 5.0f;
                
            }
        }

        public override void Exit(object owner)
        {
            Enemy enemy = owner as Enemy;
            if(enemy != null)
            {
                enemy.VelocityScalar = 5.0f;
                enemy.Target = null;
            }
        }

        public override void Execute(object owner, GameTime gameTime)
        {
            Enemy enemy = owner as Enemy;
            if (enemy == null) return; 
            if(enemy.Target == null)
            {
                enemy.Target = enemy.GameWorld.Player;
            }
        }

        
    }
}
