using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO.IsolatedStorage;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

//code based on blog on Pacific Simplicity
//Available at URL: https://www.pacificsimplicity.ca/blog/loading-and-save-highscore-xmlisolatedstorage-tutorial

namespace towerGame2
{
    //stuff for HighScore
    public struct HighScoreData
    {
        public List<string> PlayerName;
        public List<int> Score;

        public int Count;

        public HighScoreData(int count)
        {
            PlayerName = new List<string>();
            Score = new List<int>();

            Count = count;
        }
        

    }

    public class HighScoreDataClass
    {
        HighScoreData data;
        public string HighScoresFilename;
        
        Player player;      

        public void Initialise(Player player)
        {
            
            HighScoresFilename = "Content/Load/End/highScoreFile";

            this.player = player;
            //Check to see if the save exists
            if (!File.Exists(HighScoresFilename))
            {
                
                // Create the data to save

                data = new HighScoreData(10);

                for(int i = 0; i < data.Count; i++)
                {
                    data.PlayerName[i] = (i+1).ToString();
                    data.Score[i] = 0;
                }
               
                SaveHighScores(data, HighScoresFilename);
            }
        }

        /*Load high scores */
        public HighScoreData LoadHighScores(string filename)
        {
            
            data = new HighScoreData(10);

            //Get the path of the save game
            

            //OPen the file
            FileStream stream = File.Open(filename, FileMode.OpenOrCreate, FileAccess.Read);
            try
            {
                //Read the data from the file
                XmlSerializer serializer = new XmlSerializer(typeof(HighScoreData));
                data = (HighScoreData)serializer.Deserialize(stream);
            }
            finally
            {
                //Close the file
                stream.Close();
            }
            return data;
        }

        public static void SaveHighScores(HighScoreData data, string filename)
        {
            //get the pull path of the save game
            StreamWriter writer = new StreamWriter(filename);
            
            //Convert the object to XML data and put it in the stream
            XmlSerializer serializer = new XmlSerializer(typeof(HighScoreData));
            serializer.Serialize(writer, data);
            writer.Close();
        }


        /*Save player High score when game ends */
        public void SaveHighScore(ContentManager content)
        {
            //Create the data to saved
            HighScoreData data = LoadHighScores(HighScoresFilename);

            int scoreIndex = -1;
            for(int i = data.Count -1; i > -1; i--)
            {
                if(player.Score > data.Score[i])
                {
                    scoreIndex = i;
                    if (i != data.Count - 1)
                        data.Score[i + 1] = data.Score[i];
                }
                else
                {
                    break;
                }
            }

            if(scoreIndex > -1)
            {
                //New high score found
                //Do swaps
                for(int i = data.Count-1; i<scoreIndex; i--)
                {
                    //data.PlayerName[i] = data.PlayerName[i - 1];
                    data.Score[i] = data.Score[i - 1];
                }
                //data.PlayerName[scoreIndex] = PlayerName;
                data.Score[scoreIndex] = player.Score;
               
                SaveHighScores(data, HighScoresFilename);
            }
        }

        public string makehighScoreString()
        {
            //Create the data to save
            HighScoreData data2 = LoadHighScores(HighScoresFilename);

            //Create scoreBoardString
            string scoreBoardString = "High Scores: \n\n";

            for(int i = 0; i < 10; i++)
            {
                if (i < 9)
                {
                    scoreBoardString = scoreBoardString + data2.PlayerName[i] + "       " + data2.Score[i] + "\n";
                }else
                {
                    //one less space for double digit player numbers so that scores are aligned
                    scoreBoardString = scoreBoardString + data2.PlayerName[i] + "      " + data2.Score[i] + "\n";
                }
            }
            return scoreBoardString;
        }
    }

    
}
