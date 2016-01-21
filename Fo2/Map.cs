using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Fo2
{
    class Map
    {
        //private string repo = "D:/Games/Fallout 2/data/";
        private string _repo = HelperFuncts.Repo;
        public Dictionary<int, FRM> _tiles;

        public LinkedList<MapObject> _mapObjects;

        public Map(Hex[] hexes)
        {
            byte[] bytes = File.ReadAllBytes(_repo + "/maps/artemple.map");
            InitTiles(bytes);
            MapObjectFactory.Initialize();
            InitSceneryObjects(bytes, hexes);

            MapObjectFactory.DeInitialize();
        }

        
        public void InitTiles(byte[] bytes)
        {
            _tiles = new Dictionary<int, FRM>();

            string[] tilesNames = File.ReadAllLines(_repo + "art/tiles/tiles.lst");
            for (int i = 236; i < 40236 - 3; i++)
            {
                int Y1 = bytes[i];
                int Y2 = bytes[++i];
                int X1 = bytes[++i];
                int X2 = bytes[++i];

                int positionInTheList = (X1 * 16 * 16 + X2) + 0;
                Vector2 pos = HelperFuncts.NextTilePos();

                if (!_tiles.ContainsKey(positionInTheList))
                {
                    _tiles.Add(positionInTheList, new FRM(_repo + "art/tiles/" + tilesNames[positionInTheList], (int)pos.X, (int)pos.Y));
                }
                else
                {
                    _tiles[positionInTheList].AddDrawElement(pos);
                }
            }
        }

        private void InitSceneryObjects(byte[] bytes, Hex[] hexes)
        {
            _mapObjects = new LinkedList<MapObject>();
            
            int cnt = 0;
            for (int i = 42448; i < bytes.Length - 88 || cnt < 566;)
            {
                
                if (bytes[i + 32] == 1 || bytes[i + 32] == 2)
                {
                    _mapObjects.AddLast(MapObjectFactory.GetMapObject(i, (MapObjectType)bytes[i + 32], bytes, hexes, out i));
                    //12798 - critter
                    cnt++;
                    continue;
                }
                //if (i == 75800) i -= 4; //adjustment for inventory item 
                var asd = bytes[i + 32];
                switch (bytes[i + 32])
                {
                    case 0:
                        i += 104;
                        break;
                    case 3:
                        i += 88;
                        break;
                    case 4:
                        i += 88;
                        break;
                    case 5:
                        i += 88;
                        break;
                    case 6:
                        i += 88;
                        break;
                    case 7:
                        i += 88;
                        break;
                    case 8:
                        i += 88;
                        break;
                    case 9:
                        i += 88;
                        break;
                    case 255:
                        i += 88;
                        break;
                }
                cnt++;
            }
        }
    }
}
