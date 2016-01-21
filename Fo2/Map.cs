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
        public LinkedList<FRM> _sceneryObjects;

        public LinkedList<MapObject> _mapObjects;

        public Map(Hex[] hexes)
        {
            byte[] bytes = File.ReadAllBytes(_repo + "/maps/artemple.map");
            InitTiles(bytes);
            InitSceneryObjects(bytes, hexes);

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
            _sceneryObjects = new LinkedList<FRM>();
            _mapObjects = new LinkedList<MapObject>();

            string[] objNames = (File.ReadAllLines(_repo + "art/scenery/scenery.lst")).Select(l => l.Trim()).ToArray();
            string[] criNames = (File.ReadAllLines(_repo + "art/critters/critters.lst")).Select(l => l.Trim()).ToArray();

            int cnt = 0;
            for (int i = 42448; i < bytes.Length && cnt < 566;)
            {

                int x1 = bytes[i + 6];
                int x2 = bytes[i + 7];
                int onTheMap = (x1 * 16 * 16 + x2) + 0;

                int y1 = bytes[i + 34];
                int y2 = bytes[i + 35];
                int positionInTheList = (y1 * 16 * 16 + y2) + 0;
                if (bytes[i + 32] == 2)
                {
                    _sceneryObjects.AddLast(new FRM(_repo + "art/scenery/" + objNames[positionInTheList], (int)hexes[onTheMap]._vertexes[0].X, (int)hexes[onTheMap]._vertexes[0].Y));
                }
                if (bytes[i + 32] == 1)
                {
                    _mapObjects.AddLast(MapObjectFactory.GetMapObject(i, bytes, hexes, out i));
                    //_scentaryObjects.AddFirst(new FRM(repo + "art/critters/" + criNames[positionInTheList], (int)_hexes[onTheMap]._vertexes[0].X, (int)_hexes[onTheMap]._vertexes[0].Y));
                    //12798 - critter
                }
                //if (i == 75800) i -= 4; //adjustment for inventory item 
                switch (bytes[i + 32])
                {
                    case 0:
                        i += 104;
                        break;
                    case 1:
                        //i += 128;
                        break;
                    case 2:
                        i += 88;
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
                    default:
                        i += 88;
                        break;
                }
                cnt++;
            }
        }


        private void InitSceneryObjectsAlt(byte[] bytes, Hex[] hexes)
        {
            _sceneryObjects = new LinkedList<FRM>();

            string[] objNames = (File.ReadAllLines(_repo + "art/scenary/scenery.lst")).Select(l => l.Trim()).ToArray();
            string[] criNames = (File.ReadAllLines(_repo + "art/critters/critters.lst")).Select(l => l.Trim()).ToArray();
            string[] protoCritterNames = (File.ReadAllLines(_repo + "art/critters/critters.lst")).Select(l => l.Trim()).ToArray();


            string subFolder = "";
            int cnt = 0;
            for (int i = 42448; i < bytes.Length && cnt < 567;)
            {
                var objectType = bytes[i + 32];
                switch (objectType)
                {
                    case 0:
                        subFolder = "items";
                        break;
                    case 1:
                        subFolder = "critters";
                        break;
                    case 2:
                        subFolder = "scenery";
                        break;
                    case 3:
                        subFolder = "walls";
                        break;
                    default:
                        subFolder = "misc";
                        break;
                }
                byte[] protoFile = (File.ReadAllBytes(_repo + "proto/" + subFolder +"/"+ protoCritterNames[bytes[i + 44]]));
                int objectSubType = protoFile[32];
                
                int offset = -1;
                switch (objectSubType)
                {
                    case 0:
                        offset = 88;
                        break;
                    case 1:
                        offset = 88;
                        break;
                    case 2:
                        offset = 88;
                        break;
                    case 3:
                        offset = 96;
                        break;
                    case 4:
                        offset = 92;
                        break;
                    case 5:
                        offset = 92;
                        break;
                    case 6:
                        offset = 92;
                        break;
                }





                int x1 = bytes[i + 6];
                int x2 = bytes[i + 7];
                int onTheMap = (x1 * 16 * 16 + x2) + 0;

                int y1 = bytes[i + 34];
                int y2 = bytes[i + 35];
                int positionInTheList = (y1 * 16 * 16 + y2) + 0;
                if (bytes[i + 32] == 2)
                {
                    _sceneryObjects.AddLast(new FRM(_repo + "art/scenery/" + objNames[positionInTheList], (int)hexes[onTheMap]._vertexes[0].X, (int)hexes[onTheMap]._vertexes[0].Y));
                }
                if (bytes[i + 32] == 1)
                {
                    //_scentaryObjects.AddFirst(new FRM(repo + "art/critters/" + criNames[positionInTheList], (int)_hexes[onTheMap]._vertexes[0].X, (int)_hexes[onTheMap]._vertexes[0].Y));
                    //12798 - critter
                }
                if (i == 75800) i -= 4; //adjustment for inventory item 
                switch (objectType)
                {
                    case 0:
                        i += 104;
                        break;
                    case 1:
                        i += 128;
                        break;
                    case 2:
                        i += 88;
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
                    default:
                        i += 88;
                        break;
                }
                cnt++;
            }
        }

    }
}
