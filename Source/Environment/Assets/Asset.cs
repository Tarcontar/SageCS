using SageCS.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageCS.Environment.Assets
{
    abstract class Asset : IAsset
    {
        public static Map map;
        protected int id;
        protected short header;

        public string Name
        {
            get
            {
                return this.GetType().Name;
            }
        }

        public int ID
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }

        protected Asset()
        {
            this.id = -1;
            this.header = (short)-1;
            Asset.map.Register((IAsset)this);
            switch (this.GetType().Name)
            {
                case "AssetList":
                    this.header = (short)1;
                    break;
                case "GlobalVersion":
                    this.header = (short)1;
                    break;
                case "HeightMapData":
                    this.header = (short)6;
                    break;
                case "BlendTileData":
                    this.header = (short)27;
                    break;
                case "WorldInfo":
                    this.header = (short)1;
                    break;
                case "MPPositionList":
                    this.header = (short)0;
                    break;
                case "MPPositionInfo":
                    this.header = (short)1;
                    break;
                case "SidesList":
                    this.header = (short)6;
                    break;
                case "LibraryMapLists":
                    this.header = (short)1;
                    break;
                case "LibraryMaps":
                    this.header = (short)1;
                    break;
                case "Teams":
                    this.header = (short)1;
                    break;
                case "PlayerScriptsList":
                    this.header = (short)1;
                    break;
                case "ScriptList":
                    this.header = (short)1;
                    break;
                case "BuildLists":
                    this.header = (short)1;
                    break;
                case "ObjectsList":
                    this.header = (short)3;
                    break;
                case "Object":
                    this.header = (short)3;
                    break;
                case "TriggerAreas":
                    this.header = (short)1;
                    break;
                case "GlobalWaterSettings":
                    this.header = (short)1;
                    break;
                case "FogSettings":
                    this.header = (short)1;
                    break;
                case "MissionHotSpots":
                    this.header = (short)1;
                    break;
                case "MissionObjectives":
                    this.header = (short)3;
                    break;
                case "StandingWaterAreas":
                    this.header = (short)2;
                    break;
                case "RiverAreas":
                    this.header = (short)3;
                    break;
                case "StandingWaveAreas":
                    this.header = (short)4;
                    break;
                case "GlobalLighting":
                    this.header = (short)11;
                    break;
                case "PostEffectsChunk":
                    this.header = (short)2;
                    break;
                case "EnvironmentData":
                    this.header = (short)5;
                    break;
                case "NamedCameras":
                    this.header = (short)2;
                    break;
                case "CameraAnimationList":
                    this.header = (short)3;
                    break;
                case "WaypointsList":
                    this.header = (short)1;
                    break;
                default:
                    throw new Exception(string.Format("!\t Asset: {0} does not know its header", (object)this.GetType().Name));
            }
        }

        protected Asset(BinaryReader br)
        {
            this.id = br.ReadInt32();
            this.header = br.ReadInt16();
            map.Register((IAsset)this);
        }

        protected virtual void RegisterSelf(Map assetContainer)
        {
            if (assetContainer.nameIDs.ContainsKey(this.GetType().Name))
                this.id = assetContainer.nameIDs[this.GetType().Name];
            else
                assetContainer.nameIDs.Add(this.GetType().Name, assetContainer.nameIDs.Count + 1);
        }

        public abstract void Register(Map assetContainer);

        public virtual void Save(BinaryWriter bw)
        {
            bw.Write(this.id);
            bw.Write(this.header);
            this.SaveData(bw);
        }

        protected abstract void SaveData(BinaryWriter bw);

        public abstract void Default();
    }
}
