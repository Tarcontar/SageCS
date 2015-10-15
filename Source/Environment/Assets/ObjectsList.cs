using OpenTK;
using SageCS.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageCS.Environment.Assets
{
    class ObjectsList : MajorAsset
    {
        private static int objectUniqueID;
        public List<Object> objectList;

        public int StartingLocationsCount
        {
            get
            {
                int num = 0;
                foreach (Object @object in this.objectList)
                {
                    if (@object.typeName == "*Waypoints/Waypoint")
                        ++num;
                }
                return num;
            }
        }

        public ObjectsList() : base()
        {
            this.Default();
        }

        public ObjectsList(BinaryReader br) : base(br)
        {
            this.objectList = new List<Object>();
            while (br.BaseStream.Position - this.dataPos < (long)this.dataSize)
                this.objectList.Add(new Object(br));
            this.CheckParsedSize(br);
        }

        protected override void SaveData(BinaryWriter bw)
        {
            foreach (Asset asset in this.objectList)
                asset.Save(bw);
        }

        public override void Register(Map assetContainer)
        {
            this.RegisterSelf(assetContainer);
            foreach (Asset asset in this.objectList)
                asset.Register(assetContainer);
        }

        public Object AddObject(string typeName, Vector3 pos, double angle)
        {
            Object @object = new Object(typeName, pos, angle);
            @object.originalOwner = new SubAsset<string>("originalOwner", "SkirmishNeutral/teamSkirmishNeutral", @object.namedSubAssets);
            @object.uniqueID = new SubAsset<string>("uniqueID", @object.typeName + (object)" " + (string)(object)ObjectsList.objectUniqueID++, @object.namedSubAssets);
            this.objectList.Add(@object);
            Asset.map.assetList.AddAsset(@object.typeName);
            return @object;
        }

        public Object AddTechStructure(string typeName, Vector3 pos, double angle)
        {
            Object @object = new Object(typeName, pos, angle);
            @object.originalOwner = new SubAsset<string>("originalOwner", "PlyrNeutral/teamPlyrNeutral", @object.namedSubAssets);
            @object.uniqueID = new SubAsset<string>("uniqueID", @object.typeName + (object)" " + (string)(object)ObjectsList.objectUniqueID++, @object.namedSubAssets);
            this.objectList.Add(@object);
            Asset.map.assetList.AddAsset(@object.typeName);
            return @object;
        }

        public void AddPlayerStart(Vector3 pos, int playerNumber)
        {
            Object @object = new Object("*Waypoints/Waypoint", pos, 0.0);
            @object.originalOwner = new SubAsset<string>("originalOwner", "/team", @object.namedSubAssets);
            @object.uniqueID = new SubAsset<string>("uniqueID", string.Format("Player_{0}_Start", (object)playerNumber), @object.namedSubAssets);
            @object.waypointID = new SubAsset<int>("waypointID", playerNumber, @object.namedSubAssets);
            @object.waypointName = new SubAsset<string>("waypointName", string.Format("Player_{0}_Start", (object)playerNumber), @object.namedSubAssets);
            @object.waypointTypeOption = new SubAsset<string>("waypointTypeOption", "", @object.namedSubAssets);
            this.objectList.Add(@object);
        }

        public override void Default()
        {
            this.objectList = new List<Object>();
        }
    }
}
