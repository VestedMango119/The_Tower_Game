using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using System.IO;

namespace towerGame2
{

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        CollisionManager collisionManager;
        CommandManager commandManager;

        Rectangle titleSafeAreaIn;

        World gameWorld;

        Player player;

        //initialise the graphics device manager and the content directory on construction
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

        }

        //initialise game area and managers
        protected override void Initialize()
        {
            player = new Player();
            gameWorld = new World();
            commandManager = new CommandManager();
            ScreenManager.Instance.Initialize();          

            titleSafeAreaIn = new Rectangle();
          
            collisionManager = new CollisionManager();
            InitialiseBindings();
            base.Initialize();
        }


        private void InitialiseBindings()
        {
            //Initialise the bindings between keyboard inputs and in-game functionality
            commandManager.AddKeyboardBindings(Keys.Escape, StopGame);
            commandManager.AddKeyboardBindings(Keys.Left, player.MoveLeft);
            commandManager.AddKeyboardBindings(Keys.Right, player.MoveRight);
            commandManager.AddKeyboardBindings(Keys.Up, player.MoveUp);
            commandManager.AddKeyboardBindings(Keys.Down, player.MoveDown);
            commandManager.AddKeyboardBindings(Keys.A, player.Attack);

        }



        
        private const int numberOfLevels = 1;

        //Called once per game to load all content and save memory/performance during gameplay
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            player.LoadContent(Content);
            

            titleSafeAreaIn = GraphicsDevice.Viewport.TitleSafeArea;

            Vector2 playerPosition = new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X, GraphicsDevice.Viewport.TitleSafeArea.Y + GraphicsDevice.Viewport.TitleSafeArea.Height / 2);
            player.Initialise(playerPosition, collisionManager);
            

            ScreenManager.Instance.LoadContent(Content, player, collisionManager);
            ScreenManager.Instance.Dimensions = new Vector2(800, 600);
            graphics.PreferredBackBufferWidth = (int)ScreenManager.Instance.Dimensions.X;
            graphics.PreferredBackBufferHeight = (int)ScreenManager.Instance.Dimensions.Y;
            graphics.ApplyChanges();

            gameWorld.SetPlayer(player);

        }

        protected override void UnloadContent()
        {
            
        }

        //Updates the managers, player properties and current collisions during gameplay.
        protected override void Update(GameTime gameTime)
        {
            
            commandManager.Update();
            ScreenManager.Instance.Update(gameTime, player, collisionManager);
            player.Update(gameTime);
            collisionManager.Update();
            base.Update(gameTime);

            //This function watches for the screenstate to become EXIT.
            StopGame();
        }
        
        //This is used to draw the game on screen at each frame based on the current gameTime
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            ScreenManager.Instance.Draw(spriteBatch, titleSafeAreaIn, player);
            player.Draw(spriteBatch);
            base.Draw(gameTime);
        }

        #region GameActions
        public void StopGame(eButtonState buttonState, Vector2 amount)
        {
            Exit();            
        }

        public void StopGame()
        {
            if (ScreenManager.Instance.CurrentScreenState == eScreenState.EXIT)
            {
                Exit();
            }
        }

        #endregion
    }
}
