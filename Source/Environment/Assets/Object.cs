using OpenTK;
using SageCS.Core;
using SageCS.Environment.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageCS.Environment.Assets
{
    class Object : MajorAsset
    {
        public Vector3 position;
        public float angle;
        public Object.RoadOptions roadOptions;
        public string typeName;
        public SubAsset<int> objectInitialHealth;
        public SubAsset<bool> objectEnabled;
        public SubAsset<bool> objectIndestructible;
        public SubAsset<bool> objectUnsellable;
        public SubAsset<bool> objectPowered;
        public SubAsset<bool> objectRecruitableAI;
        public SubAsset<bool> objectTargetable;
        public SubAsset<bool> objectSleeping;
        public SubAsset<int> objectBasePriority;
        public SubAsset<int> objectBasePhase;
        public SubAsset<string> originalOwner;
        public SubAsset<string> uniqueID;
        public SubAsset<string> objectLayer;
        public SubAsset<string> objectName;
        public SubAsset<float> objectPrototypeScale;
        public SubAsset<bool> alignToTerrain;
        public SubAsset<int> objectTime;
        public SubAsset<int> objectWeather;
        public SubAsset<string> objectEventsList;
        public SubAsset<int> objectInitialStance;
        public SubAsset<bool> exportWithScript;
        public SubAsset<string> objectSoundAmbient;
        public SubAsset<int> scorchType;
        public SubAsset<float> objectRadius;
        public SubAsset<int> waypointID;
        public SubAsset<string> waypointName;
        public SubAsset<string> waypointTypeOption;
        public SubAsset<string> waypointPathLabel1;
        public SubAsset<string> waypointPathLabel2;
        public SubAsset<string> waypointPathLabel3;
        public SubAsset<bool> waypointPathBiDirectional;
        public SubAsset<Object.WaypointType> waypointType;

        public Object(string typeName, Vector3 pos, double angle) : base()
        {
            if ((Asset.map.majorAssets["StandingWaterAreas"] as StandingWaterAreas).areas.Length > 0)
            {
                float num = (float)(Asset.map.majorAssets["StandingWaterAreas"] as StandingWaterAreas).areas[0].waterHeight;
                int index1 = (int)((double)pos[0] / 10.0);
                int index2 = (int)((double)pos[1] / 10.0);
                if (index1 >= 0 && index1 < Asset.map.mapWidth && (index2 >= 0 && index2 < Asset.map.mapHeight) && (double)Asset.map.heightMap.elevations[index1, index2] < (double)num)
                    pos[2] = num - pos[2];
            }
            this.typeName = typeName;
            this.position = pos;
            this.angle = (float)angle;
            this.objectInitialHealth = new SubAsset<int>("objectInitialHealth", 100, this.namedSubAssets);
            this.objectEnabled = new SubAsset<bool>("objectEnabled", true, this.namedSubAssets);
            this.objectIndestructible = new SubAsset<bool>("objectIndestructible", false, this.namedSubAssets);
            this.objectUnsellable = new SubAsset<bool>("objectUnsellable", false, this.namedSubAssets);
            this.objectPowered = new SubAsset<bool>("objectPowered", true, this.namedSubAssets);
            this.objectRecruitableAI = new SubAsset<bool>("objectRecruitableAI", true, this.namedSubAssets);
            this.objectTargetable = new SubAsset<bool>("objectTargetable", false, this.namedSubAssets);
            this.objectSleeping = new SubAsset<bool>("objectSleeping", false, this.namedSubAssets);
            this.objectBasePriority = new SubAsset<int>("objectBasePriority", 40, this.namedSubAssets);
            this.objectBasePhase = new SubAsset<int>("objectBasePhase", 1, this.namedSubAssets);
            this.objectLayer = new SubAsset<string>("objectLayer", "", this.namedSubAssets);
        }

        public Object(BinaryReader br) : base(br)
        {
            this.position = new Vector3(br.ReadSingle(), br.ReadSingle(), br.ReadSingle());
            this.angle = (float)((double)br.ReadSingle() * 180.0 / 3.14159274101257);
            this.roadOptions = (Object.RoadOptions)br.ReadInt32();
            this.typeName = IOUtility.ReadString(br);
            short num1 = br.ReadInt16();
            uint num2 = 0U;
            for (int index = 0; index < (int)num1; ++index)
            {
                string name = SubAsset.PeekSubAssetName(br);
                switch (name)
                {
                    case "objectInitialHealth":
                        this.objectInitialHealth = new SubAsset<int>(br, name, this.namedSubAssets);
                        break;
                    case "objectEnabled":
                        this.objectEnabled = new SubAsset<bool>(br, name, this.namedSubAssets);
                        break;
                    case "objectIndestructible":
                        this.objectIndestructible = new SubAsset<bool>(br, name, this.namedSubAssets);
                        break;
                    case "objectUnsellable":
                        this.objectUnsellable = new SubAsset<bool>(br, name, this.namedSubAssets);
                        break;
                    case "objectPowered":
                        this.objectPowered = new SubAsset<bool>(br, name, this.namedSubAssets);
                        break;
                    case "objectRecruitableAI":
                        this.objectRecruitableAI = new SubAsset<bool>(br, name, this.namedSubAssets);
                        break;
                    case "objectTargetable":
                        this.objectTargetable = new SubAsset<bool>(br, name, this.namedSubAssets);
                        break;
                    case "objectSleeping":
                        this.objectSleeping = new SubAsset<bool>(br, name, this.namedSubAssets);
                        break;
                    case "objectBasePriority":
                        this.objectBasePriority = new SubAsset<int>(br, name, this.namedSubAssets);
                        break;
                    case "objectBasePhase":
                        this.objectBasePhase = new SubAsset<int>(br, name, this.namedSubAssets);
                        break;
                    case "originalOwner":
                        this.originalOwner = new SubAsset<string>(br, name, this.namedSubAssets);
                        break;
                    case "uniqueID":
                        this.uniqueID = new SubAsset<string>(br, name, this.namedSubAssets);
                        break;
                    case "objectLayer":
                        this.objectLayer = new SubAsset<string>(br, name, this.namedSubAssets);
                        break;
                    case "objectName":
                        this.objectName = new SubAsset<string>(br, name, this.namedSubAssets);
                        break;
                    case "objectPrototypeScale":
                        this.objectPrototypeScale = new SubAsset<float>(br, name, this.namedSubAssets);
                        break;
                    case "alignToTerrain":
                        this.alignToTerrain = new SubAsset<bool>(br, name, this.namedSubAssets);
                        break;
                    case "objectTime":
                        this.objectTime = new SubAsset<int>(br, name, this.namedSubAssets);
                        break;
                    case "objectWeather":
                        this.objectWeather = new SubAsset<int>(br, name, this.namedSubAssets);
                        break;
                    case "objectEventsList":
                        this.objectEventsList = new SubAsset<string>(br, name, this.namedSubAssets);
                        break;
                    case "objectInitialStance":
                        this.objectInitialStance = new SubAsset<int>(br, name, this.namedSubAssets);
                        break;
                    case "exportWithScript":
                        this.exportWithScript = new SubAsset<bool>(br, name, this.namedSubAssets);
                        break;
                    case "objectSoundAmbient":
                        this.objectSoundAmbient = new SubAsset<string>(br, name, this.namedSubAssets);
                        break;
                    case "scorchType":
                        this.scorchType = new SubAsset<int>(br, name, this.namedSubAssets);
                        break;
                    case "objectRadius":
                        this.objectRadius = new SubAsset<float>(br, name, this.namedSubAssets);
                        break;
                    case "objectSoundAmbientEnabled":
                        SubAsset<bool> subAsset1 = new SubAsset<bool>(br, name, this.namedSubAssets);
                        break;
                    case "objectSoundAmbientCustomized":
                        SubAsset<bool> subAsset2 = new SubAsset<bool>(br, name, this.namedSubAssets);
                        break;
                    case "objectSoundAmbientVolume":
                        SubAsset<float> subAsset3 = new SubAsset<float>(br, name, this.namedSubAssets);
                        break;
                    case "objectSoundAmbientMaxRange":
                        SubAsset<float> subAsset4 = new SubAsset<float>(br, name, this.namedSubAssets);
                        break;
                    case "objectSoundAmbientMinRange":
                        SubAsset<float> subAsset5 = new SubAsset<float>(br, name, this.namedSubAssets);
                        break;
                    case "waypointID":
                        this.waypointID = new SubAsset<int>(br, name, this.namedSubAssets);
                        break;
                    case "waypointName":
                        this.waypointName = new SubAsset<string>(br, name, this.namedSubAssets);
                        break;
                    case "waypointTypeOption":
                        this.waypointTypeOption = new SubAsset<string>(br, name, this.namedSubAssets);
                        break;
                    case "waypointPathLabel1":
                        this.waypointPathLabel1 = new SubAsset<string>(br, name, this.namedSubAssets);
                        break;
                    case "waypointPathLabel2":
                        this.waypointPathLabel2 = new SubAsset<string>(br, name, this.namedSubAssets);
                        break;
                    case "waypointPathLabel3":
                        this.waypointPathLabel3 = new SubAsset<string>(br, name, this.namedSubAssets);
                        break;
                    case "waypointPathBiDirectional":
                        this.waypointPathBiDirectional = new SubAsset<bool>(br, name, this.namedSubAssets);
                        break;
                    case "waypointType":
                        this.waypointType = new SubAsset<Object.WaypointType>(br, name, this.namedSubAssets);
                        break;
                    default:
                        SubAsset.ReadAsBytes(br, name);
                        ++num2;
                        break;
                }
            }
            if (num2 > 0U)
                Console.WriteLine("!\t Asset: {0} skipped parsing {1} SubAssets", (object)this.GetType().Name, (object)num2);
            if (this.roadOptions == (Object.RoadOptions)0)
                return;
            Console.WriteLine("!\t Asset: Object - {0} roadOptions = {1}", (object)this.uniqueID.data, (object)this.roadOptions);
        }

        protected override void SaveData(BinaryWriter bw)
        {
            bw.Write(this.position[0]);
            bw.Write(this.position[1]);
            bw.Write(this.position[2]);
            bw.Write((float)((double)this.angle * 3.14159274101257 / 180.0));
            bw.Write((int)this.roadOptions);
            IOUtility.WriteString(bw, this.typeName);
            bw.Write((short)this.namedSubAssets.Count);
            foreach (SubAsset subAsset in this.namedSubAssets.Values)
                subAsset.Save(bw);
        }

        public override void Register(Map assetContainer)
        {
            this.RegisterSelf(assetContainer);
            this.objectInitialHealth.Register(assetContainer);
            this.objectEnabled.Register(assetContainer);
            this.objectIndestructible.Register(assetContainer);
            this.objectUnsellable.Register(assetContainer);
            this.objectPowered.Register(assetContainer);
            this.objectRecruitableAI.Register(assetContainer);
            this.objectTargetable.Register(assetContainer);
            this.objectSleeping.Register(assetContainer);
            this.objectBasePriority.Register(assetContainer);
            this.objectBasePhase.Register(assetContainer);
            this.originalOwner.Register(assetContainer);
            this.uniqueID.Register(assetContainer);
            this.objectLayer.Register(assetContainer);
            this.objectName.Register(assetContainer);
            this.objectPrototypeScale.Register(assetContainer);
            this.alignToTerrain.Register(assetContainer);
            this.objectTime.Register(assetContainer);
            this.objectWeather.Register(assetContainer);
            this.objectEventsList.Register(assetContainer);
            this.objectInitialStance.Register(assetContainer);
            this.exportWithScript.Register(assetContainer);
            this.objectSoundAmbient.Register(assetContainer);
            this.scorchType.Register(assetContainer);
            this.objectRadius.Register(assetContainer);
            this.waypointID.Register(assetContainer);
            this.waypointName.Register(assetContainer);
            this.waypointTypeOption.Register(assetContainer);
            this.waypointPathLabel1.Register(assetContainer);
            this.waypointPathLabel2.Register(assetContainer);
            this.waypointPathLabel3.Register(assetContainer);
            this.waypointPathBiDirectional.Register(assetContainer);
        }

        public override string ToString()
        {
            return this.typeName;
        }

        public override void Default()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        [Flags]
        public enum RoadOptions
        {
            Start = 2,
            End = 4,
            Angled = 8,
            TightCurve = 64,
            EndCapOrJoin = 128,
        }

        public enum WaypointType
        {
            Normal,
            Portal,
            WalkPortal,
            ClimbPortal,
            PreClimbPortal,
            Beacon,
            Spline,
            FakePathfindPortal,
            MineshaftPortal,
        }
    }
}
