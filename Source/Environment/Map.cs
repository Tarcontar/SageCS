
using SageCS.Environment.Assets;
using System.Collections.Generic;

namespace SageCS.Core
{
    class Map
    {
        internal int mapWidth = 600;
        internal int mapHeight = 400;
        private int playerCount = 6;
        internal Dictionary<string, int> nameIDs;
        public Dictionary<string, MajorAsset> majorAssets;
        internal int mapBorder;
        public AssetList assetList;
        public HeightMapData heightMap;
        public BlendTileData blendTile;
        public WorldInfo worldInfo;
        public ObjectsList objectList;
        public MPPositionList mpPositionList;
        public SidesList sidesList;
        //public static Dictionary<string, Bitmap> textureBitmaps;

        public int MapWidth
        {
            get
            {
                return this.mapWidth;
            }
            set
            {
                this.mapWidth = value;
            }
        }

        public int MapHeight
        {
            get
            {
                return this.mapHeight;
            }
            set
            {
                this.mapHeight = value;
            }
        }

        public int PlayerCount
        {
            get
            {
                return this.playerCount;
            }
            set
            {
                this.playerCount = value;
            }
        }

        public bool IsScenarioMultiplayer
        {
            get
            {
                if (this.worldInfo != null)
                    return this.worldInfo.IsScenarioMultiplayer;
                return false;
            }
            set
            {
                if (this.worldInfo == null)
                    return;
                this.worldInfo.IsScenarioMultiplayer = value;
            }
        }

        public Map()
        {
        }

        public void Create()
        {
            this.nameIDs = new Dictionary<string, int>();
            this.majorAssets = new Dictionary<string, MajorAsset>();
            SubAsset.map = this;
            Asset.map = this;
            this.assetList = new AssetList();
            this.AddMajorAsset((MajorAsset)this.assetList);
            this.AddMajorAsset((MajorAsset)new GlobalVersion());
            this.heightMap = new HeightMapData(this.mapWidth, this.mapHeight, this.mapBorder);
            this.AddMajorAsset((MajorAsset)this.heightMap);
            this.mapWidth = this.heightMap.mapWidth;
            this.mapHeight = this.heightMap.mapHeight;
            this.mapBorder = this.heightMap.borderWidth;
            this.blendTile = new BlendTileData(this.mapWidth, this.mapHeight);
            this.AddMajorAsset((MajorAsset)this.blendTile);
            this.worldInfo = new WorldInfo();
            this.AddMajorAsset((MajorAsset)this.worldInfo);
            this.mpPositionList = new MPPositionList();
            this.AddMajorAsset((MajorAsset)this.mpPositionList);
            this.sidesList = new SidesList();
            this.AddMajorAsset((MajorAsset)this.sidesList);
            this.AddMajorAsset((MajorAsset)new LibraryMapLists());
            this.AddMajorAsset((MajorAsset)new Teams());
            this.AddMajorAsset((MajorAsset)new PlayerScriptsList());
            this.AddMajorAsset((MajorAsset)new BuildLists());
            this.objectList = new ObjectsList();
            this.AddMajorAsset((MajorAsset)this.objectList);
            /*
            this.AddMajorAsset((MajorAsset)new TriggerAreas(this.game));
            this.AddMajorAsset((MajorAsset)new GlobalWaterSettings(this.game));
            this.AddMajorAsset((MajorAsset)new FogSettings(this.game));
            this.AddMajorAsset((MajorAsset)new MissionHotSpots(this.game));
            this.AddMajorAsset((MajorAsset)new MissionObjectives(this.game));
            this.AddMajorAsset((MajorAsset)new StandingWaterAreas(this.game));
            this.AddMajorAsset((MajorAsset)new RiverAreas(this.game));
            this.AddMajorAsset((MajorAsset)new StandingWaveAreas(this.game));
            this.AddMajorAsset((MajorAsset)new GlobalLighting(this.game));
            this.AddMajorAsset((MajorAsset)new PostEffectsChunk(this.game));
            this.AddMajorAsset((MajorAsset)new EnvironmentData(this.game));
            this.AddMajorAsset((MajorAsset)new NamedCameras(this.game));
            this.AddMajorAsset((MajorAsset)new CameraAnimationList(this.game));
            this.AddMajorAsset((MajorAsset)new WaypointsList(this.game));
            */
        }

        internal void AddMajorAsset(MajorAsset majorAsset)
        {
            this.majorAssets.Add(majorAsset.Name, majorAsset);
        }

        internal int Register(IAsset asset)
        {
            int num = 0;
            if (string.IsNullOrEmpty(asset.Name))
                num = -1;
            else if (!this.nameIDs.TryGetValue(asset.Name, out num))
            {
                num = this.nameIDs.Count + 1;
                this.nameIDs.Add(asset.Name, num);
            }
            asset.ID = num;
            return num;
        }
    }
}
