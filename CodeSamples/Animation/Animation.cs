using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace towerGame2
{
    public class Animation
    {
       public Texture2D Texture
       {
           get;
           set;
       }

       public Vector2 position;
    
       int timeSinceLastFrame = 0;
       public int millisecondsPerFrame = 100;

       public int Rows { get; set; }
       public int Columns { get; set; }
       private int currentFrame;
       public int CurrentFrame
       {
           get { return currentFrame; }
       }
       private int totalFrames;

       public int frameMin;
       public int frameMax;

       public bool IsLooping
       {
           get { return isLooping; }
       }
       bool isLooping;

       public Animation(Texture2D texture, int rows, int columns, bool looping)
       {
           Texture = texture;
           Rows = rows;
           Columns = columns;
           currentFrame = 0;
           totalFrames = Rows * Columns;
           position = new Vector2(100, 100);
           isLooping = looping;
       }

       public void setCurrentFrame(int frameMin, int frameMax)
       {
            if ((currentFrame < frameMin) || (currentFrame > frameMax))
            {
                this.frameMin = frameMin;
                this.frameMax = frameMax;
            }
       }


       public void Update(GameTime gameTime)
       {
            //use of timeSinceLastFrame taken from Xna animation 2D tutorial by James Ferry
            //Available on Youtube, URL: https://www.youtube.com/watch?v=tNdDRfxW87k
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
           if ((currentFrame < frameMin) || (currentFrame > frameMax))
           {
               currentFrame = frameMin;
           }

           if ((currentFrame < frameMax) && (timeSinceLastFrame > millisecondsPerFrame))
           {
               timeSinceLastFrame -= millisecondsPerFrame;
               currentFrame++;
           }
           if ((currentFrame == frameMax) && (isLooping))
               currentFrame = frameMin;
       }
        
       

       public void Draw(SpriteBatch spriteBatch, Vector2 location)
       {
           int width = Texture.Width / Columns;
           int height = Texture.Height / Rows;
           int row = (int)((float)currentFrame / (float)Columns);
           int column = currentFrame % Columns;
           Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
           Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, width, height);

           spriteBatch.Begin();
           spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
           spriteBatch.End();
       }
    }
}
