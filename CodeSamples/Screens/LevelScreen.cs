using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

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
    public class LevelScreen : GameScreen
    {
        KeyboardState keyState;
        List<Enemy> enemies; 
        World gameWorld;
        Layers backgroundLayer;
        SpriteFont font;
        Enemy enemy;
        Stairs stairs;
        List<Potion> potions;
        Potion potion;
        fileManager fileManager;
        

        public override void LoadContent(ContentManager Content, Player player, CollisionManager collisionManager)
        {
            base.LoadContent(Content, player, collisionManager);

            font = content.Load<SpriteFont>("font1");
            backgroundLayer = new Layers();
            backgroundLayer.LoadContent(content, "Layers", collisionManager);
            gameWorld = new World();
            enemies = new List<Enemy>();
            stairs = new Stairs();
            potions = new List<Potion>();
            fileManager = new fileManager();
           

            player.LoadContent(Content);
            stairs.LoadContent(Content);
            if (enemies.Count == 0)
            {
                for (int i = 0; i < backgroundLayer.m_enemyPos.Count; i++)
                {
                    enemy = new Enemy();
                    enemy.Initialise(backgroundLayer.m_enemyPos[i], collisionManager, player);
                    enemies.Add(enemy);
                    enemies[i].LoadContent(Content);
                    gameWorld.AddEnemy(enemies[i]);
                }
            }
            player.Initialise(backgroundLayer.m_playerPos, collisionManager);
            player.PlayerAnimation.millisecondsPerFrame = 200;
            stairs.Initialise(backgroundLayer.m_stairPos, collisionManager, fileManager);
            if(potions.Count == 0)
            {
                for(int i = 0; i <backgroundLayer.m_potionPos.Count; i++)
                {
                    potion = new Potion();
                    potion.Initialise(backgroundLayer.m_potionPos[i], collisionManager);
                    potions.Add(potion);
                    potions[i].LoadContent(Content);
                }
            }
            gameWorld.SetPlayer(player);

            player.Score = 0; 
            player.Health = 100;
        }

        public override void UnloadContent(ContentManager content, CollisionManager collisionManager, Player player)
        {
            base.UnloadContent(content, collisionManager, player);
            RemoveCollidables(collisionManager, player);
        }

        public override void AddCollidables(CollisionManager collisionManager, Player player)
        {
            base.AddCollidables(collisionManager,player);
        }

        public override void RemoveCollidables(CollisionManager collisionManager, Player player)
        {
            foreach(Enemy e in enemies)
            {
                collisionManager.RemoveCollidable(e);
            }
            foreach(Potion p in potions)
            {
                collisionManager.RemoveCollidable(p);
            }
            collisionManager.RemoveCollidable(stairs);
        }

        public override void Update(GameTime gameTime, Player player, CollisionManager collisionManager)
        {

            player.Update(gameTime);
            foreach (Enemy e in enemies)
                e.Update(gameTime);

            stairs.Update(gameTime);

            if(player.Health <= 0)
            {
                int TotalCollidables = collisionManager.GetCollidables().Count;
                for (int i = 0; i < TotalCollidables; i++)
                {
                    int count = collisionManager.GetCollidables().Count;
                    Collidable c = collisionManager.GetCollidables()[count - 1];
                    collisionManager.RemoveCollidable(c);
                }
                ScreenManager.Instance.AddScreen(new EndScreen(), player, eScreenState.END, collisionManager);
            }
        }

        public override void Draw(SpriteBatch spriteBatch, Rectangle titleSafeAreaIn, Player player)
        {
            
            backgroundLayer.Draw(spriteBatch);
            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].Draw(spriteBatch);
            }
            DrawHud(titleSafeAreaIn, player, font, spriteBatch);
            stairs.Draw(spriteBatch);
            for(int i = 0; i<potions.Count; i++)
            {
                if(potions[i].Active)
                potions[i].Draw(spriteBatch);
            }

        }
    }
}
