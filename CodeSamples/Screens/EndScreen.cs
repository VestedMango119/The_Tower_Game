using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

//Based on the tutorials made by CodingMadeEasy on Youtube
//Available at URLs:
//Screen states part 1 : https://www.youtube.com/watch?v=FR7crO2xq8A&list=PLE500D63CA505443B&index=1
//screen states part 2 : https://www.youtube.com/watch?v=JzI82Yrj96I&index=2&list=PLE500D63CA505443B
//screen states part 3 : https://www.youtube.com/watch?v=kLs7uMHECIc&list=PLE500D63CA505443B&index=3

namespace towerGame2
{
    public class EndScreen : GameScreen
    {
        KeyboardState keyState;
        SpriteFont font;
        fileManager fileManager;

        GameButton restart;
        GameButton exit;

        List<Texture2D> images;
        
        CollisionManager mycollisionManager;
        World  gameWorld;
        HighScoreDataClass highscores;

        public override void LoadContent(ContentManager Content, Player player, CollisionManager collisionManager)
        {
            base.LoadContent(Content);
            font = content.Load<SpriteFont>("font1");
            fileManager = new fileManager();
            highscores = new HighScoreDataClass();
            gameWorld = new World();
            images = new List<Texture2D>();
            restart = new GameButton();
            restart.Initialise("Start", font, new Vector2(100, 520), Color.DimGray);
           
            exit = new GameButton();
            exit.Initialise("Exit", font, new Vector2(650, 520), Color.DimGray);
            mycollisionManager = collisionManager;
            Vector2 playerPosition = new Vector2(300, 500);
            player.LoadContent(content);
            player.Initialise(playerPosition, collisionManager);
            highscores.Initialise(player);
            
            AddCollidables(collisionManager, player);

            fileManager.LoadContent(contents, attributes, "Content/Load/End/Content.txt");
            highscores.SaveHighScore(content);

            for (int i = 0; i < attributes.Count; i++)
            {
                for (int j = 0; j < attributes[i].Count; j++)
                {
                    switch (attributes[i][j])
                    {
                        case "EndBG":
                            images.Add(content.Load<Texture2D>(contents[i][j]));
                            break;
                    }
                }
            }
        }
        public override void AddCollidables(CollisionManager collisionManager, Player player)
        {
            mycollisionManager.AddCollidable(player);
            mycollisionManager.AddCollidable(restart);
            mycollisionManager.AddCollidable(exit);
        }

        public override void RemoveCollidables(CollisionManager collisionManager,Player player)
        {
            
            mycollisionManager.RemoveCollidable(restart);
            mycollisionManager.RemoveCollidable(exit);
        }

        public override void UnloadContent(ContentManager content, CollisionManager collisionManager, Player player)
        {
            base.UnloadContent(content, collisionManager, player);
            fileManager = null;
            RemoveCollidables(collisionManager,player);
            restart.UnloadContent();
            exit.UnloadContent();
            restart = null;
            exit = null;
        }

        public override void Draw(SpriteBatch spriteBatch, Rectangle titleSafeAreaIn, Player player)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(images[0], Vector2.Zero, Color.Maroon);
            spriteBatch.DrawString(font, "GAME OVER", new Vector2(300, 100), Color.DimGray);
            spriteBatch.DrawString(font, highscores.makehighScoreString(), new Vector2(300, 200), Color.White, 0.0f, Vector2.Zero, 0.7f, SpriteEffects.None, 1);
            spriteBatch.End();

            restart.Draw(spriteBatch);
            exit.Draw(spriteBatch);
            
            
        }

    }
}