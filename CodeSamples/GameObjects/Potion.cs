using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace towerGame2
{
    public class Potion : GameCharacter
    {
        public Vector2 potionPosition;
        Texture2D texture;
        int potionAmount;

        public void Initialise(Vector2 position, CollisionManager collisionManager)
        {
            potionPosition = position;
            
            collisionManager.AddCollidable(this);
            Active = true;
            potionAmount = 50;
        }

        public void LoadContent(ContentManager content) {
            texture = content.Load<Texture2D>("potion2");
            boundingRect = new Rectangle((int)potionPosition.X, (int)potionPosition.Y, (int)texture.Width, (int)texture.Height);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(texture, potionPosition, Color.White);
            spriteBatch.End();
        }

        public override void OnCollision(Player obj, CollisionManager collisionManager)
        {
            if (CollisionTest(obj))
            {
                //check player health and add bonus points if any extra
                if (Active)
                {
                    if (obj.Health + potionAmount < 100)
                    {
                        obj.Health += potionAmount;
                    }else
                    {
                        
                        int total = obj.Health + potionAmount;
                        obj.Health = 100;

                        obj.Score += total - 100;
                        Active = false;
                    }
                    Active = false;
                }
            }
              
        }

    }
}
