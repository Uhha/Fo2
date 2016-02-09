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
    static class MapObjectFactory
    {
        private static string repo = HelperFuncts.Repo;

        private static string[] criNames;
        private static string[] sceNames;
        private static string[] miscNames;
        private static string[] wallNames;
        private static string[] itemsProtoNames;


        public static void Initialize()
        {
            sceNames = (File.ReadAllLines(repo + "art/scenery/scenery.lst")).Select(l => l.Trim()).ToArray();
            criNames = (File.ReadAllLines(repo + "art/critters/critters.lst")).Select(l => l.Trim()).ToArray();
            miscNames = (File.ReadAllLines(repo + "art/misc/misc.lst")).Select(l => l.Trim()).ToArray();
            wallNames = (File.ReadAllLines(repo + "art/walls/walls.lst")).Select(l => l.Trim()).ToArray();
            itemsProtoNames = (File.ReadAllLines(repo + "proto/items/items.lst")).Select(l => l.Trim()).ToArray();
        }

    public static MapObject GetMapObject(int start, MapObjectType objType, byte[] bytes, Hex[] hexes, out int newStart)
        {
            switch (objType)
            {
                //case MapObjectType.Item:
                //    break;
                case MapObjectType.Critter:
                    return new Critter(start, bytes, hexes, criNames, itemsProtoNames, out newStart);
                case MapObjectType.Scenery:
                    return new Scenery(start, bytes, hexes, sceNames, itemsProtoNames, out newStart);
                case MapObjectType.Wall:
                    return new Wall(start, bytes, hexes, wallNames, itemsProtoNames, out newStart);
                //case MapObjectType.Tiles:
                //    break;
                case MapObjectType.Misc:
                    return new Misc(start, bytes, hexes, miscNames, itemsProtoNames, out newStart);
                //case MapObjectType.Interface:
                //    break;
                //case MapObjectType.Invent:
                //    break;
                //case MapObjectType.Head:
                //    break;
                //case MapObjectType.Backgrnd:
                //    break;
                //case MapObjectType.Skilldex:
                //    break;
                //default:
                //    break;
            }
            newStart = start + 88;
            return null;

        }

        public static MapObject GetMapObject(string name, MapObjectType objType, Hex[] hexes, int position)
        {
            return new GenericMapObject(name, objType, (int)hexes[position]._vertexes[0].X, (int)hexes[position]._vertexes[0].Y);
        }


        public static void DeInitialize()
        {
            sceNames = null;
            criNames = null;
            miscNames = null;
            wallNames = null;
            itemsProtoNames = null;
        }
    }
}
