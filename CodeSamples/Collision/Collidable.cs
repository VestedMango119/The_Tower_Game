using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

//Code based on the collidable class Tutorial 4 in the INM379 module - City University of London

namespace towerGame2
{
    public class Collidable
    {
        public Collidable(GraphicsDevice device)
        {

        }

        public Collidable()
        {

        }

        protected Rectangle boundingRect; 
        public Rectangle BoundingRect
        {
            get { return boundingRect; }
            set { boundingRect = value; }
        }

        public bool Active;

        public Vector2 getRectCenter(Rectangle rect)
        {
            int x = rect.X / 2;
            int y = rect.Y / 2;
            return new Vector2(x, y);
        }

        public virtual bool CollisionTest(Collidable obj)
        {
            if (GetIntersectionDepth(this, obj).X > 0 || GetIntersectionDepth(this, obj).Y > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public virtual bool CollisionTest(Player player)
        {
            if (GetIntersectionDepth(this, player).X > 0 || GetIntersectionDepth(this, player).Y > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

       public virtual void OnCollision(Collidable obj)
        {
            
        }

        public virtual void OnCollision(Collidable obj, CollisionManager collisionManager)
        {
            
        }

        public virtual void OnCollision(Player player, CollisionManager collisionManager) {

            //code based on solution given on Game Dev Stack Exchange forum
            //Avialable at URL: https://gamedev.stackexchange.com/questions/12982/stuck-in-wall-after-rectangle-bounding-box-collision

            Vector2 velocity = new Vector2(player.playerPosition.X-player.playerPrevPos.X, player.playerPosition.Y-player.playerPrevPos.Y);
            ePlayerState direction = player.CurrentPlayerState;

            if (player.boundingRect.Intersects(boundingRect))
            {
                Vector2 newPos = player.playerPosition;

                if(velocity.X > 0)
                {
                    newPos.X = boundingRect.Left - player.PlayerAnimation.Texture.Width/player.PlayerAnimation.Columns;     
                }else if(velocity.X < 0)
                {
                    newPos.X = boundingRect.Right;
                }
                if(velocity.Y > 0)
                {
                    newPos.Y = boundingRect.Top - player.PlayerAnimation.Texture.Height/player.PlayerAnimation.Rows;
                }else if(velocity.Y < 0)
                {
                    newPos.Y = boundingRect.Bottom;
                }

                player.playerPosition = newPos;
            }
        }
       

        public static Vector2 GetIntersectionDepth(Collidable a, Collidable b)
        {
            float halfWidthA = a.BoundingRect.Width / 2.0f;
            float halfHeightA = a.BoundingRect.Height / 2.0f;
            float halfWidthB = b.BoundingRect.Width / 2.0f;
            float halfHeightB = b.BoundingRect.Height / 2.0f;

            Vector2 centerA = new Vector2(a.BoundingRect.Left + halfWidthA, a.BoundingRect.Top + halfHeightA);
            Vector2 centerB = new Vector2(b.BoundingRect.Left + halfWidthB, b.BoundingRect.Top + halfHeightB);

            float distanceX = centerA.X - centerB.X;
            float distanceY = centerA.Y - centerB.Y;
            float minDistanceX = halfWidthA + halfWidthB;
            float minDistanceY = halfHeightA + halfHeightB;

            if (Math.Abs(distanceX) >= minDistanceX || Math.Abs(distanceY) >= minDistanceY)
                return Vector2.Zero;

            float depthX = distanceX > 0 ? minDistanceX - distanceX : -minDistanceX - distanceX;
            float depthY = distanceY > 0 ? minDistanceY - distanceY : -minDistanceY - distanceY;
            return new Vector2(depthX, depthY);

        }
    }
}
