

using SageCS.Core;
using System;
using System.IO;

namespace SageCS.Environment.Assets
{
    class WorldInfo : MajorAsset
    {
        private SubAsset<string> musicZone;
        private SubAsset<WorldInfo.WeatherType> weather;
        private SubAsset<string> terrainTextureStrings;
        private SubAsset<string> mapName;
        private SubAsset<string> mapDescription;
        private SubAsset<WorldInfo.CompressionType> compression;
        private SubAsset<float> cameraGroundMinHeight;
        private SubAsset<float> cameraGroundMaxHeight;
        private SubAsset<float> cameraMinHeight;
        private SubAsset<float> cameraMaxHeight;
        private SubAsset<float> cameraPitchAngle;
        private SubAsset<float> cameraYawAngle;
        private SubAsset<float> cameraScrollSpeedScalar;
        private SubAsset<float> cameraMapHeightSmoothnessScalar;
        private SubAsset<bool> isScenarioMultiplayer;

        public string MusicZone
        {
            get
            {
                return this.musicZone.data.Replace("MusicPalette:", "");
            }
            set
            {
                this.musicZone.data = "MusicPalette:" + value;
            }
        }

        public WorldInfo.WeatherType Weather
        {
            get
            {
                return this.weather.data;
            }
            set
            {
                this.weather.data = value;
            }
        }

        public string TerrainTextureStrings
        {
            get
            {
                return this.terrainTextureStrings.data;
            }
            set
            {
                this.terrainTextureStrings.data = value;
            }
        }

        public string MapName
        {
            get
            {
                return this.mapName.data;
            }
            set
            {
                this.mapName.data = value;
            }
        }

        public string MapDescription
        {
            get
            {
                return this.mapDescription.data;
            }
            set
            {
                this.mapDescription.data = value;
            }
        }

        public WorldInfo.CompressionType Compression
        {
            get
            {
                return this.compression.data;
            }
            set
            {
                this.compression.data = value;
            }
        }

        public float CameraGroundMinHeight
        {
            get
            {
                return this.cameraGroundMinHeight.data;
            }
            set
            {
                this.cameraGroundMinHeight.data = value;
            }
        }

        public float CameraGroundMaxHeight
        {
            get
            {
                return this.cameraGroundMaxHeight.data;
            }
            set
            {
                this.cameraGroundMaxHeight.data = value;
            }
        }

        public float CameraMinHeight
        {
            get
            {
                return this.cameraMinHeight.data;
            }
            set
            {
                this.cameraMinHeight.data = value;
            }
        }

        public float CameraMaxHeight
        {
            get
            {
                return this.cameraMaxHeight.data;
            }
            set
            {
                this.cameraMaxHeight.data = value;
            }
        }

        public bool IsScenarioMultiplayer
        {
            get
            {
                return this.isScenarioMultiplayer.data;
            }
            set
            {
                this.isScenarioMultiplayer.data = value;
            }
        }

        public WorldInfo() : base()
        {
            this.Default();
        }

        public WorldInfo(BinaryReader br, string[] assetStrings)
          : base(br)
        {
            short num1 = br.ReadInt16();
            uint num2 = 0U;
            for (int index = 0; index < (int)num1; ++index)
            {
                string name = SubAsset.PeekSubAssetName(br);
                switch (name)
                {
                    case "musicZone":
                        this.musicZone = new SubAsset<string>(br, name, this.namedSubAssets);
                        break;
                    case "terrainTextureStrings":
                        this.terrainTextureStrings = new SubAsset<string>(br, name, this.namedSubAssets);
                        break;
                    case "weather":
                        this.weather = new SubAsset<WorldInfo.WeatherType>(br, name, this.namedSubAssets);
                        break;
                    case "mapName":
                        this.mapName = new SubAsset<string>(br, name, this.namedSubAssets);
                        break;
                    case "mapDescription":
                        this.mapDescription = new SubAsset<string>(br, name, this.namedSubAssets);
                        break;
                    case "compression":
                        this.compression = new SubAsset<WorldInfo.CompressionType>(br, name, this.namedSubAssets);
                        break;
                    case "cameraGroundMinHeight":
                        this.cameraGroundMinHeight = new SubAsset<float>(br, name, this.namedSubAssets);
                        break;
                    case "cameraGroundMaxHeight":
                        this.cameraGroundMaxHeight = new SubAsset<float>(br, name, this.namedSubAssets);
                        break;
                    case "cameraMinHeight":
                        this.cameraMinHeight = new SubAsset<float>(br, name, this.namedSubAssets);
                        break;
                    case "cameraMaxHeight":
                        this.cameraMaxHeight = new SubAsset<float>(br, name, this.namedSubAssets);
                        break;
                    case "isScenarioMultiplayer":
                        this.isScenarioMultiplayer = new SubAsset<bool>(br, name, this.namedSubAssets);
                        break;
                    case "cameraPitchAngle":
                        this.cameraPitchAngle = new SubAsset<float>(br, name, this.namedSubAssets);
                        break;
                    case "cameraYawAngle":
                        this.cameraYawAngle = new SubAsset<float>(br, name, this.namedSubAssets);
                        break;
                    case "cameraScrollSpeedScalar":
                        this.cameraScrollSpeedScalar = new SubAsset<float>(br, name, this.namedSubAssets);
                        break;
                    case "cameraMapHeightSmoothnessScalar":
                        this.cameraMapHeightSmoothnessScalar = new SubAsset<float>(br, name, this.namedSubAssets);
                        break;
                    case "isLivingWorldScriptHolder":
                        SubAsset<bool> subAsset = new SubAsset<bool>(br, name, this.namedSubAssets);
                        break;
                    default:
                        SubAsset.ReadAsBytes(br, name);
                        ++num2;
                        break;
                }
            }
            if (num2 > 0U)
                Console.WriteLine("!\t Asset: {0} skipped parsing {1} SubAssets", (object)this.GetType().Name, (object)num2);
            this.CheckParsedSize(br);
            if (this.mapName != null)
                return;
            this.mapName = new SubAsset<string>("mapName", "", this.namedSubAssets);
        }

        protected override void SaveData(BinaryWriter bw)
        {
            bw.Write((short)this.namedSubAssets.Count);
            foreach (SubAsset subAsset in this.namedSubAssets.Values)
                subAsset.Save(bw);
        }

        public override void Register(Map assetContainer)
        {
            this.RegisterSelf(assetContainer);
        }

        public override void Default()
        {
            foreach (SubAsset subAsset in this.namedSubAssets.Values)
                subAsset.Clear();
            this.namedSubAssets.Clear();
            /*
            switch (this.game)
            {
                case Game.RA3:
                    this.musicZone = new SubAsset<string>("musicZone", "MusicPalette:MusicPalette_NotSet", this.namedSubAssets);
                    this.cameraMinHeight = new SubAsset<float>("cameraMinHeight", 40f, this.namedSubAssets);
                    this.cameraMaxHeight = new SubAsset<float>("cameraMaxHeight", 650f, this.namedSubAssets);
                    this.terrainTextureStrings = new SubAsset<string>("terrainTextureStrings", "RA3Grid1;RA3Grid1_NRM;", this.namedSubAssets);
                    break;
                case Game.CC3:
                    this.musicZone = new SubAsset<string>("musicZone", "MusicPalette_NotSet", this.namedSubAssets);
                    this.cameraMinHeight = new SubAsset<float>("cameraMinHeight", 40f, this.namedSubAssets);
                    this.cameraMaxHeight = new SubAsset<float>("cameraMaxHeight", 500f, this.namedSubAssets);
                    break;
                default:
                    Console.WriteLine("!\t Asset: WorldInfo does not know how to create default data for Game." + (object)this.game);
                    break;
            }
            */
            this.weather = new SubAsset<WorldInfo.WeatherType>("weather", WorldInfo.WeatherType.Normal, this.namedSubAssets);
            this.mapName = new SubAsset<string>("mapName", "random", this.namedSubAssets);
            this.mapDescription = new SubAsset<string>("mapDescription", "map created with map generator by KOS4U2C", this.namedSubAssets);
            this.compression = new SubAsset<WorldInfo.CompressionType>("compression", WorldInfo.CompressionType.None, this.namedSubAssets);
            this.cameraGroundMinHeight = new SubAsset<float>("cameraGroundMinHeight", 0.0f, this.namedSubAssets);
            this.cameraGroundMaxHeight = new SubAsset<float>("cameraGroundMaxHeight", 2560f, this.namedSubAssets);
            this.isScenarioMultiplayer = new SubAsset<bool>("isScenarioMultiplayer", false, this.namedSubAssets);
        }

        public enum WeatherType
        {
            Normal,
            Cloudy,
            Rainy,
            CloudyRainy,
            Sunny,
            Snowy,
            Invalid,
        }

        public enum CompressionType
        {
            None,
            RefPack,
        }
    }
}
