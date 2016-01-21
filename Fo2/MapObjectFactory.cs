using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.IO;
using Fo2.MapObjects;

namespace Fo2
{
    class MapObjectFactory
    {
        private static string repo = "C:/!tmp/f2/data/";
        private static string[] objNames = (File.ReadAllLines(repo + "art/scenery/scenery.lst")).Select(l => l.Trim()).ToArray();
        private static string[] criNames = (File.ReadAllLines(repo + "art/critters/critters.lst")).Select(l => l.Trim()).ToArray();
        private static string[] itemsProtoNames = (File.ReadAllLines(repo + "proto/items/items.lst")).Select(l => l.Trim()).ToArray();


        public static MapObject GetMapObject(int start, byte[] bytes, Hex[] hexes, out int newStart)
        {
            return new Critter(start, bytes, hexes, criNames, itemsProtoNames, out newStart);

        }

       
    }
}
