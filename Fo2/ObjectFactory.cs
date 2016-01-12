using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Fo2
{
    class ObjectFactory
    {

        public static ScenaryObject GetScenaryObject(int onTheMap, int positionInTheList, string[] objNames, Hex[] hexes, ContentManager content)
        {
            Texture2D texture;
            try
            {
                texture = content.Load<Texture2D>("art/scenary/" + objNames[positionInTheList].Substring(0, objNames[positionInTheList].Length - 4));
            }
            catch (Exception)
            {
                //then load animation file 000
                texture = content.Load<Texture2D>("art/scenary/" + objNames[positionInTheList].Substring(0, objNames[positionInTheList].Length - 4)+ "_000");
            }
            
            Vector2 position = new Vector2(hexes[onTheMap]._vertexes[0].X, hexes[onTheMap]._vertexes[0].Y);
            //if (onTheMap == 14276)
            //{
            //    position = new Vector2(hexes[onTheMap]._vertexes[2].X, hexes[onTheMap]._vertexes[2].Y);
            //}
            return new ScenaryObject(position, texture);
        }

       
    }
}
