using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

//based on code Lab 3 of the INM379 module - City University of London

namespace towerGame2
{
    public class World
    {
        private GameCharacter player;   
        public GameCharacter Player
        {
            get { return player; }
        }

        private List<GameCharacter> enemies;
        public List<GameCharacter> Enemies
        {
            get { return enemies; }
        }

        private List<GameCharacter> allCharacters;
        public List<GameCharacter> AllCharacters
        {
            get { return allCharacters; }
        }

        public World()
        {
            player = null;
            enemies = new List<GameCharacter>();
            allCharacters = new List<GameCharacter>();
        }

        public void UpdateWorld(GameTime gameTime)
        {
            player.Update(gameTime);

            foreach(Enemy enemy in enemies)
            {
                enemy.Update(gameTime);
            }
        }

        public void SetPlayer(GameCharacter p)
        {
            player = p;
            player.GameWorld = this;
            if (!allCharacters.Contains(player))
            {
                allCharacters.Add(player);
            }
        }

        public void AddEnemy(GameCharacter e)
        {
            e.GameWorld = this;
            enemies.Add(e);
        }



    }
}
