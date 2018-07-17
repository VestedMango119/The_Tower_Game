using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;

//Based on the tutorials made by CodingMadeEasy on Youtube
//Available at URLs:
// fileManager part 1 : https://www.youtube.com/watch?v=ybVSBcfKyQw&list=PLE500D63CA505443B&index=8
//fileManager part 2 : https://www.youtube.com/watch?v=2r2u_rcwC00&list=PLE500D63CA505443B&index=9

namespace towerGame2
{
    public class fileManager
    {
        enum LoadType { Attributes, Contents};

        LoadType type;
            
        List<string> tempContents;
        List<string> tempAttributes;

        bool identifierFound = false;

        public void LoadContent(List<List<string>> contents, List<List<string>> attributes, string filename)
        {
            using (StreamReader reader = new StreamReader(filename))
            {
                while (!reader.EndOfStream)
                {
                    //tempContents = new List<string>();
                    string line = reader.ReadLine();

                    if (line.Contains("Load="))
                    {
                        tempAttributes = new List<string>();
                        line = line.Remove(0, line.IndexOf("=") + 1);
                        type = LoadType.Attributes;
                    }
                    else
                    {
                        tempContents = new List<string>();
                        type = LoadType.Contents;
                    }

                    string[] lineArray = line.Split(']');

                    foreach (string li in lineArray)
                    {
                        string newLine = li.Trim('[', ' ', ']');

                        if (newLine != string.Empty)
                        {
                            if (type == LoadType.Contents)
                            {
                                tempContents.Add(newLine);
                            }
                            else
                            {
                                tempAttributes.Add(newLine);
                            }
                        }
                    }
                    if (type == LoadType.Contents && tempContents.Count > 0)
                    {
                        contents.Add(tempContents);
                        attributes.Add(tempAttributes);
                    }

                }
            }
        }

        public void LoadContent(List<List<string>> contents, List<List<string>> attributes, string filename, string identifier)
        {
            using (StreamReader reader = new StreamReader(filename))
            {
                while (!reader.EndOfStream)
                {
                    tempContents = new List<string>();
                    string line = reader.ReadLine();

                    if (line.Contains("EndLoad=") && line.Contains(identifier))
                    {
                        identifierFound = false;
                        break;
                    }
                    else if ((line.Contains("Load="))&&(line.Contains(identifier))){
                        identifierFound = true;
                        continue;
                    }

                    if (identifierFound)
                    {
                        if (line.Contains("Load="))
                        {
                            tempAttributes = new List<string>();
                            line.Remove(0, line.IndexOf("=") + 1);
                            type = LoadType.Attributes;
                        }
                        else
                        {
                            tempContents = new List<string>();
                            type = LoadType.Contents;
                        }

                        string[] lineArray = line.Split(' ');

                        foreach (string li in lineArray)
                        {

                            string newLine = li.Trim('[', ' ', ']');

                            if (newLine != string.Empty)
                            {
                                if (type == LoadType.Contents)
                                {
                                    tempContents.Add(newLine);
                                }
                                else
                                {
                                    tempAttributes.Add(newLine);
                                }

                            }
                        }
                        if (type == LoadType.Contents && tempContents.Count > 0)
                        {
                            contents.Add(tempContents);
                            attributes.Add(tempAttributes);
                        }

                    }
                }
            }
        }
    }
}
