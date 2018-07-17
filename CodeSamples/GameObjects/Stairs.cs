using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics; 

namespace towerGame2
{
    public class Stairs : GameCharacter
    {
        public Vector2 stairPosition;
        Texture2D texture;
        fileManager fileManager; 

        public void Initialise(Vector2 position, CollisionManager collisionManager, fileManager fileManager)
        {
            stairPosition = position;
            BoundingRect = new Rectangle((int)(stairPosition.X), (int)stairPosition.Y, (int)texture.Width, (int)texture.Height);
            collisionManager.AddCollidable(this);
            this.fileManager = fileManager;
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("stairs");
            boundingRect = new Rectangle((int)(stairPosition.X), (int)stairPosition.Y, (int)texture.Width, (int)texture.Height);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(texture, stairPosition, Color.White);
            spriteBatch.End();
        }

        public override void OnCollision(Player obj, CollisionManager collisionManager)
        {
            if (CollisionTest(obj))
            {
                int TotalCollidables = collisionManager.GetCollidables().Count;
                for (int i = 0; i < TotalCollidables; i++)
                {
                    int count = collisionManager.GetCollidables().Count;
                    Collidable c = collisionManager.GetCollidables()[count - 1];
                    collisionManager.RemoveCollidable(c);
                }
                ScreenManager.Instance.AddScreen(new EndScreen(), obj, eScreenState.END, collisionManager);
            }
        }

    }
}
