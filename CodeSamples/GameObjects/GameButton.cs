using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace towerGame2
{
    class GameButton : Collidable
    {
        SpriteFont font;
        Vector2 fontSize;
        string Name;
        Vector2 Position;
        Color fontColor;


        public void Initialise(string name, SpriteFont font, Vector2 position, Color color)
        {
            Active = true;
            this.Name = name;
            this.font = font;
            this.Position = position;
            this.fontColor = color;
            
            boundingRect = new Rectangle((int)Position.X, (int)Position.Y, (int)font.MeasureString(Name).X, (int)font.MeasureString(Name).Y);

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(font, Name, Position, fontColor);
            spriteBatch.End();
        }

        public void UnloadContent()
        {
            //deactivate when the button is not in use (i.e not drawn on screen)
            Active = false;
        }

        //check button type and change screen as appropriate
        public override void OnCollision(Player obj, CollisionManager collisionManager)
        {     
            if (CollisionTest(obj) && Active)
            {
                if ((this.Name == "Start") && (obj.CurrentPlayerState == ePlayerState.ATTACKLEFT || obj.CurrentPlayerState == ePlayerState.ATTACKUP || obj.CurrentPlayerState == ePlayerState.ATTACKRIGHT || obj.CurrentPlayerState == ePlayerState.ATTACKDOWN))
                {
                    ScreenManager.Instance.AddScreen(new LevelScreen(), obj, eScreenState.GAME, collisionManager);
                                        
                }else if((this.Name == "Exit") && (obj.CurrentPlayerState == ePlayerState.ATTACKLEFT || obj.CurrentPlayerState == ePlayerState.ATTACKUP || obj.CurrentPlayerState == ePlayerState.ATTACKRIGHT || obj.CurrentPlayerState == ePlayerState.ATTACKDOWN))
                {
                   ScreenManager.Instance.CurrentScreenState = eScreenState.EXIT;
                   
                }   
                else if ((Name == "Controls") && (obj.CurrentPlayerState == ePlayerState.ATTACKLEFT || obj.CurrentPlayerState == ePlayerState.ATTACKUP || obj.CurrentPlayerState == ePlayerState.ATTACKRIGHT || obj.CurrentPlayerState == ePlayerState.ATTACKDOWN))
                {
                    ScreenManager.Instance.AddScreen(new ControlsScreen(), obj, eScreenState.CONTROLS, collisionManager);
                } else if((Name =="Back") && (obj.CurrentPlayerState == ePlayerState.ATTACKLEFT || obj.CurrentPlayerState == ePlayerState.ATTACKUP || obj.CurrentPlayerState == ePlayerState.ATTACKRIGHT || obj.CurrentPlayerState == ePlayerState.ATTACKDOWN))
                {
                    ScreenManager.Instance.AddScreen(new TitleScreen(), obj, eScreenState.TITLE, collisionManager);
                }
            }
        }

    }
}
