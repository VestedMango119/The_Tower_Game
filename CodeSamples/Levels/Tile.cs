using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

//based on code in the Platformer example provided in lab 2 of the INM379 module - City University of London

namespace towerGame2
{
    public enum TileCollision
    {
        Passable = 0, 
        Impassable = 1, 
        Platform =2,
    }

    public class Tile : Collidable
    {
        public Texture2D Texture;
        TileCollision Collision
        {
            get { return collision; }
        } public TileCollision collision;

        public Vector2 position;

        public const int Width = 40;
        public const int Height = 32;

        

        public static readonly Vector2 Size = new Vector2(Width, Height);

        public Tile(Texture2D texture, TileCollision collision, Vector2 Position, CollisionManager collisionManager)
        {
            Texture = texture;
            this.collision = collision;
            position = Position;
            Active = true;
            
        }

        public override void OnCollision(Player obj, CollisionManager collisionManager)
        {
            if(CollisionTest(obj))
              base.OnCollision(obj, collisionManager);
        }
    }
}
