using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

//Based on the tutorials made by CodingMadeEasy on Youtube
//Available at URLs:
// Creating the Map Class https://www.youtube.com/watch?v=q7wcVEhXFHA&list=PLE500D63CA505443B&index=27
//Drawing the map https://www.youtube.com/watch?v=q7wcVEhXFHA&list=PLE500D63CA505443B&index=27

namespace towerGame2
{
    public class Layers
    {
        List<List<List<Vector2>>> tileMap;
        List<List<Vector2>> layer;
        List<Vector2> tile;
        
        CollisionManager myCollisionManager;

        fileManager fileManager;

        Texture2D tileset;
        Vector2 tileDimensions;
        List<TileCollision> m_collisionType;

        Collidable collidableTile;

        public List<Vector2> m_enemyPos;
        public Vector2 m_playerPos;

        public Vector2 m_stairPos;
        public List<Vector2> m_potionPos;

        int layerNumber;
        public int LayerNumber
        {
            set { layerNumber = value;
            }
        }

        List<List<string>> contents;
        List<List<string>> attributes;

        public void LoadContent(ContentManager content, string mapID, CollisionManager collisionManager)
        {
            content = new ContentManager(content.ServiceProvider, "Content");
            fileManager = new fileManager();

            tile = new List<Vector2>();
            layer = new List<List<Vector2>>();
            tileMap = new List<List<List<Vector2>>>();
            
            myCollisionManager = collisionManager;

            contents = new List<List<string>>();
            attributes = new List<List<string>>();

            m_enemyPos = new List<Vector2>();
            m_potionPos = new List<Vector2>();

            m_collisionType = new List<TileCollision>() ;

            fileManager.LoadContent(contents, attributes, "Content/Load/Maps/" + mapID + ".txt");
            
            for(int i =0; i < attributes.Count; i++)
            {
                for(int j = 0; j < attributes[i].Count; j++)
                {
                    switch(attributes[i][j])
                    {
                        case "TileSet":
                            tileset = content.Load<Texture2D>("Load/TileSets/" + contents[i][j]);
                            break;
                        case "TileDimensions":
                            string[] split = contents[i][j].Split(' ');
                            tileDimensions = new Vector2(int.Parse(split[0]), int.Parse(split[1]));
                            break;
                        case "StartLayer":
                            //iterate through the tileMap to get the tiles at each position
                            for(int k = 0; k < contents[i].Count; k++)
                            {
                                split = contents[i][k].Split(' ');
                                tile.Add(new Vector2(int.Parse(split[0]), int.Parse(split[1])));
                                
                            }
                            if (tile.Count > 0)
                            {
                                layer.Add(tile);
                            }
                            tile = new List<Vector2>();
                            break;
                        case "EndLayer":
                            if (layer.Count > 0)
                            {
                                tileMap.Add(layer);
                            }
                            layer = new List<List<Vector2>>();
                            break;
                        case "positionLayer":
                            split = null;
                            split = contents[i][j].Split(' ');
                            for (int k = 0; k < split.Count(); k++) {
                                switch (split[k])
                                {
                                    case "-":
                                        break;
                                    case "P":
                                        m_playerPos = new Vector2(k*tileDimensions.X - tileDimensions.X/2, (i-20) * tileDimensions.Y - tileDimensions.Y /2);
                                        break;
                                    case "E":
                                        m_enemyPos.Add(new Vector2(k * tileDimensions.X - tileDimensions.X / 2, (i - 20) * tileDimensions.Y - tileDimensions.Y / 2));
                                        break;
                                    case "x":
                                        collidableTile = new Tile(tileset, TileCollision.Impassable, new Vector2(k-1*(int)tileDimensions.X, (i - 20)-1 * (int)tileDimensions.Y),  myCollisionManager);
                                        collidableTile.BoundingRect = new Rectangle(k*(int)tileDimensions.X + (int)tileDimensions.X/3, (i - 20)*(int)tileDimensions.Y - (int)tileDimensions.Y/3, (int)tileDimensions.X/3, (int)tileDimensions.Y/3);
                                        collisionManager.AddCollidable(collidableTile);
                                        break;
                                    case "S":
                                        m_stairPos = new Vector2(k * tileDimensions.X - tileDimensions.X / 2, (i - 20) * tileDimensions.Y - tileDimensions.Y / 2);

                                        break;
                                    case "p":
                                        m_potionPos.Add(new Vector2(k * tileDimensions.X - tileDimensions.X / 2, (i - 20) * tileDimensions.Y - tileDimensions.Y / 2));
                                        break;
                                   }
                            }
                        break;
                    }
                }
            }
        }

      

        public void Draw(SpriteBatch spriteBatch)
        {
            for(int i =0; i < tileMap[layerNumber].Count; i++)
            {
                for(int j=0; j < tileMap[layerNumber][i].Count; j++)
                {
                    spriteBatch.Begin();
                    spriteBatch.Draw(tileset, new Vector2(j * tileDimensions.X, i * tileDimensions.Y), new Rectangle((int)tileMap[layerNumber][i][j].X * (int)tileDimensions.X, (int)tileMap[layerNumber][i][j].Y * (int)tileDimensions.Y, (int)tileDimensions.X, (int)tileDimensions.Y), Color.White);
                    spriteBatch.End();
                }
            }
        }

    }
}
