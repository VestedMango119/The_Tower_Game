using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

//Code based on the enemy class in Tutorial 3 in the INM379 module - City University of London

namespace towerGame2
{
    public class Enemy : GameCharacter
    {
        public float VelocityScalar = 0.0f;
        FSM stateMachine;
        public Animation EnemyAnimation;
        public Vector2 position;
        Texture2D texture;
        Player target;
        

        int health;
        public int Health
        {
            set { health = value; }
            get { return health; }
        }
        
        public void Initialise(Vector2 position, CollisionManager collisionManager, Player player)
        {
            Active = true;
            target = player;
            
            this.position = position;
            stateMachine = new FSM(this);

            IdleState idle = new IdleState();
            ChaseState chase = new ChaseState();

            idle.AddTransition(new Transition(chase, () => TargetSeen));
            chase.AddTransition(new Transition(idle, () => !TargetSeen));

            stateMachine.AddState(idle);
            stateMachine.AddState(chase);

            stateMachine.Initialise("Idle");

            health = 50;

            
            collisionManager.AddCollidable(this);
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("hell-hound-idle");
            EnemyAnimation = new Animation(texture, 1, 6, true);
            EnemyAnimation.setCurrentFrame(0, 5);
            boundingRect = new Rectangle((int)(position.X), (int)(position.Y), (int)EnemyAnimation.Texture.Width / (EnemyAnimation.Columns * 2), (int)EnemyAnimation.Texture.Height / (EnemyAnimation.Rows*2));
        }

        public void SetRandomDirection()
        {
            float randX = RandomHelper.RandFloat();
            float randY = RandomHelper.RandFloat();

            Direction.X += randX * 100.0f;
            Direction.Y += randY * 100.0f;
            Direction.Normalize();
        }


        public override void Update(GameTime gameTime)
        {
            Sense();
            Think(gameTime);

            if (TargetSeen)
            {
                Direction = target.playerPosition - this.position;
                Direction.Normalize();
            }
            else
            {
                SetRandomDirection();
            }
            
            position += Direction;
            boundingRect = new Rectangle((int)position.X, (int)position.Y, (int)texture.Width/2, (int)texture.Height/2);
            getRectCenter(boundingRect);

            EnemyAnimation.setCurrentFrame(0, 4);
            EnemyAnimation.Update(gameTime);      
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(Active)
                EnemyAnimation.Draw(spriteBatch, position);
        }

        private void Sense()
        {
                if (position.Length() - target.playerPosition.Length() <50)
                {
                    targetSeen = true;
                }else
                {
                    targetSeen = false;
                }
        }

        private void Think(GameTime gameTime)
        {
            stateMachine.Update(gameTime);
        }

        public override void OnCollision(Player obj, CollisionManager collisionManager)
        {
            if (CollisionTest(obj))
            {
                if(obj.CurrentPlayerState == ePlayerState.ATTACKLEFT || obj.CurrentPlayerState == ePlayerState.ATTACKRIGHT || obj.CurrentPlayerState == ePlayerState.ATTACKUP || obj.CurrentPlayerState == ePlayerState.ATTACKDOWN)
                {
                    health-=4;
                    if (health <= 0)
                        Active = false;
                    if (Active)
                        obj.Score += 10;
                } else
                {
                    if(Active)
                    obj.Health--;
                }
            }
        }

      
    }
}
