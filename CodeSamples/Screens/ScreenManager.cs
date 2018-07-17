using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

//Based on the tutorials made by CodingMadeEasy on Youtube
//Available at URLs:
//Screen states part 1 : https://www.youtube.com/watch?v=FR7crO2xq8A&list=PLE500D63CA505443B&index=1
//screen states part 2 : https://www.youtube.com/watch?v=JzI82Yrj96I&index=2&list=PLE500D63CA505443B
//screen states part 3 : https://www.youtube.com/watch?v=kLs7uMHECIc&list=PLE500D63CA505443B&index=3

namespace towerGame2
{

    public enum eScreenState
    {
        TITLE,
        GAME, 
        EXIT, 
        END,
        CONTROLS
    }

    public class ScreenManager
    {

        #region variables

        GameScreen currentScreen;
        GameScreen newScreen;

        public eScreenState CurrentScreenState;

       
        ContentManager content;

        //ScreenManager instance
        private static ScreenManager instance;

        // lets us know which screens appear in which order
        Stack<GameScreen> screenStack = new Stack<GameScreen>();

        //screen dimensions
        Vector2 dimensions;

      

        #endregion

        #region Properties

        public static ScreenManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new ScreenManager();
                return instance;
            }
        }

        public Vector2 Dimensions
        {
            get { return dimensions; }
            set { dimensions = value; }
        }

        #endregion

        #region Main Methods

        public void AddScreen(GameScreen screen, Player player, eScreenState state, CollisionManager collisionManager)
        {
            currentScreen.UnloadContent(content, collisionManager, player);
            newScreen = screen;
            screenStack.Push(screen);
            
            currentScreen = newScreen;
            CurrentScreenState = state;
            LoadContent(content, player, collisionManager);
        }

        public void Initialize()
        {
            currentScreen = new TitleScreen();   
        }

        public void LoadContent(ContentManager Content, Player player, CollisionManager collisionManager)
        {
            content = new ContentManager(Content.ServiceProvider, "Content");
            currentScreen.LoadContent(content, player, collisionManager);
        }

        public void Update(GameTime gameTime, Player player, CollisionManager collisionManager)
        {
            currentScreen.Update(gameTime, player, collisionManager);
        }

        public void Draw(SpriteBatch spriteBatch, Rectangle titleSafeAreaIn, Player player)
        {
            currentScreen.Draw(spriteBatch, titleSafeAreaIn, player);
        }

        #endregion

    }
}
