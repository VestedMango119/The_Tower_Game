using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

//Based on the tutorials made by CodingMadeEasy on Youtube
//Available at URLs:
//Screen states part 1 : https://www.youtube.com/watch?v=FR7crO2xq8A&list=PLE500D63CA505443B&index=1
//screen states part 2 : https://www.youtube.com/watch?v=JzI82Yrj96I&index=2&list=PLE500D63CA505443B
//screen states part 3 : https://www.youtube.com/watch?v=kLs7uMHECIc&list=PLE500D63CA505443B&index=3

namespace towerGame2
{
    public class GameScreen
    {
        protected ContentManager content;
        protected List<List<string>> attributes;
        protected List<List<string>> contents;
       
        public GameScreen()
        {
            attributes = new List<List<string>>();
            contents = new List<List<string>>();
        }

        public virtual void LoadContent(ContentManager Content)
        {
            content = new ContentManager(Content.ServiceProvider, "Content");
            attributes = new List<List<string>>();
            contents = new List<List<string>>();
        }

        public virtual void LoadContent(ContentManager Content, Player Player, CollisionManager collisionManager)
        {
            content = new ContentManager(Content.ServiceProvider, "Content");
            attributes = new List<List<string>>();
            contents = new List<List<string>>();
        }

        public virtual void AddCollidables(CollisionManager collisionManager, Player player)
        {
            
        }

        public virtual void RemoveCollidables(CollisionManager collisionManager, Player player)
        {

        }

        public virtual void UnloadContent(ContentManager content, CollisionManager collisionManager,Player player)
        {
            content.Unload();
            
        }

        public virtual void Update(GameTime gameTime, Player player, CollisionManager collsionManager){}
        public virtual void Draw(SpriteBatch spriteBatch, Rectangle titleSafeAreaIn, Player player) {}


        public void DrawHud(Rectangle titleSafeAreaIn, Player player, SpriteFont font, SpriteBatch spriteBatch)
        {
            Rectangle titleSafeArea = titleSafeAreaIn;
            Vector2 hudLocation = new Vector2(titleSafeArea.X, titleSafeArea.Y);
            Vector2 center = new Vector2(titleSafeArea.X + titleSafeArea.Width / 2.0f,
                                         titleSafeArea.Y + titleSafeArea.Height / 2.0f);

            SpriteFont hudFont = font;

            DrawShadowedString(hudFont, "Score: " + player.Score.ToString(), hudLocation, Color.Maroon, spriteBatch);
            DrawShadowedString(hudFont, "Health: " + player.Health.ToString(), hudLocation + new Vector2(titleSafeArea.Width - font.MeasureString("Health: " + player.Health.ToString()).X, 0), Color.Maroon, spriteBatch);

            // Determine the status overlay message to show.
        }

        private void DrawShadowedString(SpriteFont font, string value, Vector2 position, Color color, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(font, value, position + new Vector2(1.0f, 1.0f), Color.Black);
            spriteBatch.DrawString(font, value, position, color);
            spriteBatch.End();
        }
    }
}
