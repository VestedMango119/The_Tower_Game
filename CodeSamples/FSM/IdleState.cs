using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

//Code based on Tutorial 4 in the INM379 module
namespace towerGame2
{
    public class IdleState : State
    {
        private const double directionChangeTime = 10;
        private double curTime = 0.0; 

        public IdleState()
        {
            Name = "Idle";
        }

        public override void Enter(object owner)
        {
            Enemy enemy = owner as Enemy;
            if (enemy != null) enemy.VelocityScalar = 0.0f;
            curTime = 0.0;
        }

        public override void Exit(object owner)
        {
            Enemy enemy = owner as Enemy;
            if (enemy != null) enemy.VelocityScalar = 0.0f;
            curTime = 0.0;
        }

        public override void Execute(object owner, GameTime gameTime)
        {
            Enemy enemy = owner as Enemy;
            if (enemy == null) return; 

            if(curTime >= directionChangeTime)
            {
                curTime = 0.0;
                enemy.SetRandomDirection();
            }
            else
            {
                curTime += gameTime.ElapsedGameTime.TotalSeconds;
            }
        }
    }
}
