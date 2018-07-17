using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

//Based on the Ship class in Lab 5 in the INM379 module - City University of London
//Designed to be a base class for all NPC characters in the game
namespace towerGame2
{
    public class GameCharacter :Collidable
    {
        private Vector2 position;
        public Vector2 Position
        {
            get { return position; }
            set { position = value;}
        }

        public Vector2 Direction;

        public Vector2 Velocity;

        private const float RotationRate = 1.5f;

       

        public Vector2 WorldBounds
        {
            get;
            set;
        }

        //collision box for character
        private Rectangle sensor;
        public Rectangle Sensor
        {
            get { return sensor; }
        }
        private const float sensorLength =500.0f;

        private World gameWorld;
        public World GameWorld
        {
            get { return gameWorld; }
            set { gameWorld = value; }
        }

        public bool TargetSeen
        {
            get { return targetSeen; }
        }
        protected bool targetSeen = false;

        GameCharacter target;
        public GameCharacter Target
        {
            get { return target; }
            set { target = value; }
        }

        public void Reset()
        {
            Position = new Vector2(0, 0);
            Direction = -Vector2.UnitX;
            
            //reset collision rectangle
            boundingRect = new Rectangle();
            sensor = new Rectangle((int)Position.X, (int)Position.Y, (int)sensorLength/2, (int)sensorLength/2);
            
        }

        //Update character transformations and rectangles
        public virtual void Update(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Matrix rotationMatrix = Matrix.CreateRotationY(RotationRate);

            Direction = Vector2.TransformNormal(Direction, rotationMatrix);
            Direction.Normalize();

            Velocity += Direction *elapsed;
            Position += Velocity * elapsed;

            getRectCenter(boundingRect);

            getRectCenter(sensor);
            
        }

        //For when the character is moving position on screen.
        public void AddPosition(Vector2 p)
        {
            position += p;
            boundingRect.X = (int)position.X;
            boundingRect.Y = (int)position.Y;

            sensor.X = (int)position.X;
            sensor.Y = (int)position.Y;
        }

        public override bool CollisionTest(Collidable obj)
        {
            if(obj!= null)
            {
                return boundingRect.Intersects(obj.BoundingRect);
            }
            return false;
        }

        public override void OnCollision(Player obj, CollisionManager collisionManager)
        {
            Player other = obj as Player;
            if(other != null)
            {
                Vector2 collisionNormal = Vector2.Normalize(other.getRectCenter(boundingRect) - getRectCenter(boundingRect));

                float distance = Vector2.Distance(other.getRectCenter(other.boundingRect), getRectCenter(boundingRect));
                Vector2 penetrationDepth = GetIntersectionDepth(obj, other);

                if (!penetrationDepth.Equals(Vector2.Zero))
                {
                    AddPosition(-collisionNormal * penetrationDepth);
                }
            }
        }

        public override void OnCollision(Collidable obj, CollisionManager collisionManager)
        {
           GameCharacter other = obj as Enemy;
            if (other != null)
            {
                Vector2 collisionNormal = Vector2.Normalize(other.getRectCenter(boundingRect) - getRectCenter(boundingRect));

                float distance = Vector2.Distance(other.getRectCenter(other.boundingRect), getRectCenter(boundingRect));
                Vector2 penetrationDepth = GetIntersectionDepth(obj, other);

                if (!penetrationDepth.Equals(Vector2.Zero))
                {
                    AddPosition(-collisionNormal * penetrationDepth);
                }
            }
        }
    }
}
