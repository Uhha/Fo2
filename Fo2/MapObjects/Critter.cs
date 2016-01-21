using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.IO;

namespace Fo2.MapObjects
{
    class Critter : MapObject
    {
        public MapObjectType MapObjectType = MapObjectType.Critter;

        private FRM _texture;
        private int _frameNumer;
        private int _objDirection;
        private int _protoId;
        private int _numberOfSubItems;
        private int _hitPoints;
        private string _repo = HelperFuncts.Repo;


        public Critter(int start, byte[] bytes, Hex[] hexes, string[] criNames, string[] itemsProtoNames, out int newStart)
        {
            int hexNumber = HelperFuncts.SumTwoBytes(bytes[start + 6], bytes[start + 7]);
            _objDirection = bytes[start + 31];

            string frmName = _repo + "art/critters/" + criNames[bytes[start + 35]].Split(',')[0] + "GA.frm";
            _texture = new FRM(frmName, (int)hexes[hexNumber]._vertexes[0].X, (int)hexes[hexNumber]._vertexes[0].Y);
            _texture.CurrentDirection = _objDirection;
            _frameNumer = bytes[start + 27];
            _protoId = HelperFuncts.SumTwoBytes(bytes[start + 46], bytes[start + 47]); ;
            _numberOfSubItems = bytes[start + 75];
            _hitPoints = bytes[start + 119];
            
            if (_numberOfSubItems > 0) { GetSubItems(_numberOfSubItems, start, bytes, itemsProtoNames, out start); }
            else { start += 128; }

            newStart = start;

        }

        private void GetSubItems(int numberOfSubs, int start, byte[] bytes, string[] itemsProtoNames, out int startOut)
        {
            start += 128;
            for (int i = 0; i < numberOfSubs; i++)
            {
                byte[] protoFile = File.ReadAllBytes(_repo + "proto/items/" + itemsProtoNames[HelperFuncts.SumTwoBytes(bytes[start + 42], bytes[start + 43])]);
                //int subType = protoFile[35];
                ItemSubType subT = (ItemSubType)((int)protoFile[35]);
                //TODO: READ AND LOAD ITEMS OBJECT
                switch (subT)
                {
                    case ItemSubType.Armor:
                        start += 88;
                        break;
                    case ItemSubType.Container:
                        start += 88;
                        break;
                    case ItemSubType.Drug:
                        start += 88;
                        break;
                    case ItemSubType.Weapon:
                        start += 96;
                        break;
                    case ItemSubType.Ammo:
                        start += 92;
                        break;
                    case ItemSubType.Misc:
                        start += 92;
                        break;
                    case ItemSubType.Key:
                        start += 92;
                        break;
                    default:
                        break;
                }
                start += 4;
            }
            startOut = start;
        }


        public override void Update(double gameTime)
        {
            _texture.Update(gameTime);
        }

        public override void Draw(SpriteBatch sb)
        {
            _texture.Draw(sb);
        }
        
    }
}
