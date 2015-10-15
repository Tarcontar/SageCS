using SageCS.Environment.Assets;
using SageCS.Environment.Utility;
using System;
using System.Collections.Generic;
using System.IO;

namespace SageCS.Core.Loaders
{
    class MapLoader
    {

        public MapLoader(Stream file)
        {
            Console.WriteLine("### Opening map: " + ((BigStream)file).Name);
            BinaryReader binaryReader = new BinaryReader(file);
            uint flag = binaryReader.ReadUInt32();

            switch (flag)
            {
                case 1884121923U:
                    Console.WriteLine("### Map is in uncompressed format");
                    break;
                case 5390661U:
                    Console.WriteLine("### Map is in compressed RefPack format, decompressing...");

                    BinaryWriter output = new BinaryWriter((Stream)new MemoryStream((int)binaryReader.BaseStream.Length));
                    binaryReader.BaseStream.Position = 8L;
                    IOUtility.DecompressData(binaryReader, output);
                    binaryReader = new BinaryReader(output.BaseStream);
                    byte[] numArray = new byte[(int)output.BaseStream.Length];
                    output.BaseStream.Position = 0L;
                    output.BaseStream.Read(numArray, 0, (int)output.BaseStream.Length);
                    File.WriteAllBytes("decompressed.map", numArray);
                    binaryReader.BaseStream.Position = 4L;

                    break;
                default:
                    Console.WriteLine("### Unknow map format, not supported");
                    return;
            }

            Map map = new Map();
            SubAsset.map = map;
            Asset.map = map;

            binaryReader.BaseStream.Position = 4L;
            string[] assetStrings = new string[binaryReader.ReadInt32()];
            map.nameIDs = new Dictionary<string, int>(assetStrings.Length);
            map.majorAssets = new Dictionary<string, MajorAsset>();
            StreamWriter streamWriter1 = new StreamWriter("assetStrings.txt");
            for (int index = assetStrings.Length - 1; index >= 0; --index)
            {
                assetStrings[index] = binaryReader.ReadString();
                streamWriter1.WriteLine("{0,-20}\t{1}", (object)assetStrings[index], (object)(index + 1));
                //Console.WriteLine(assetStrings[index]);
                int num = binaryReader.ReadInt32();
                if (num != index + 1)
                    Console.WriteLine("!\t string suffix {0} mismatch with index {1}", (object)num, (object)(index + 1));
            }
            streamWriter1.Close();
            SubAsset.assetNames = assetStrings;
            StreamWriter streamWriter2 = new StreamWriter("majorAssetStrings.txt");
            long position = binaryReader.BaseStream.Position;
            while (binaryReader.BaseStream.Position < binaryReader.BaseStream.Length)
            {
                int num1 = binaryReader.ReadInt32();
                short num2 = binaryReader.ReadInt16();
                int num3 = binaryReader.ReadInt32();
                string key = assetStrings[num1 - 1];
                binaryReader.BaseStream.Position -= 10L;
                streamWriter2.WriteLine("{0,-20}\t{1,-6}\t{2,-3}\t{3}", (object)key, (object)num3, (object)num1, (object)num2);
                File.WriteAllBytes(assetStrings[num1 - 1] + ".bin", binaryReader.ReadBytes(num3 + 10));
                binaryReader.BaseStream.Position -= (long)(num3 + 10);
                switch (key)
                {
                    
                    case "AssetList":
                        Console.WriteLine("AssetList");
                        map.majorAssets.Add(key, (MajorAsset)(map.assetList = new AssetList(binaryReader)));
                        continue;
                    case "GlobalVersion":
                        Console.WriteLine("GlobalVersion");
                        map.majorAssets.Add(key, (MajorAsset)new GlobalVersion(binaryReader));
                        continue;
                    case "HeightMapData":
                        Console.WriteLine("HeigthMapData");
                        map.heightMap = new HeightMapData(binaryReader);
                        map.majorAssets.Add(key, (MajorAsset)map.heightMap);
                        map.mapWidth = map.heightMap.mapWidth;
                        map.mapHeight = map.heightMap.mapHeight;
                        continue;
                    /*
                    case "BlendTileData":
                        Console.WriteLine("BlendTileData");
                        map.blendTile = new BlendTileData(binaryReader, map.mapWidth, map.mapHeight);
                        map.majorAssets.Add(key, (MajorAsset)map.blendTile);
                        continue;
                    case "WorldInfo":
                        Console.WriteLine("WorldInfo");
                        map.worldInfo = new WorldInfo(binaryReader, assetStrings);
                        map.majorAssets.Add(key, (MajorAsset)map.worldInfo);
                        continue;
                    case "MPPositionList":
                        Console.WriteLine("MPPositionList");
                        map.mpPositionList = new MPPositionList(binaryReader);
                        map.majorAssets.Add(key, (MajorAsset)map.mpPositionList);
                        continue;
                    case "SidesList":
                        Console.WriteLine("SidesList");
                        map.sidesList = new SidesList(binaryReader);
                        map.majorAssets.Add(key, (MajorAsset)map.sidesList);
                        continue;
                    case "LibraryMapLists":
                        Console.WriteLine("LibraryMapLists");
                        map.majorAssets.Add(key, (MajorAsset)new LibraryMapLists(binaryReader));
                        continue;
                    case "Teams":
                        Console.WriteLine("Teams");
                        map.majorAssets.Add(key, (MajorAsset)new Teams(binaryReader));
                        continue;
                    case "PlayerScriptsList":
                        Console.WriteLine("PlayerScriptsList");
                        map.majorAssets.Add(key, (MajorAsset)new PlayerScriptsList(binaryReader));
                        continue;
                    case "BuildLists":
                        Console.WriteLine("BuildLists");
                        map.majorAssets.Add(key, (MajorAsset)new BuildLists(binaryReader));
                        continue;
                    case "ObjectsList":
                        Console.WriteLine("ObjectList");
                        map.objectList = new ObjectsList(binaryReader);
                        map.majorAssets.Add(key, (MajorAsset)map.objectList);
                        continue;
                    case "TriggerAreas":
                        Console.WriteLine("TriggerAreas");
                        map.majorAssets.Add(key, (MajorAsset)new TriggerAreas(binaryReader));
                        continue;
                    case "GlobalWaterSettings":
                        Console.WriteLine("GlobalWaterSettings");
                        map.majorAssets.Add(key, (MajorAsset)new GlobalWaterSettings(binaryReader));
                        continue;
                    case "FogSettings":
                        Console.WriteLine("FogSettings");
                        map.majorAssets.Add(key, (MajorAsset)new FogSettings(binaryReader));
                        continue;
                    case "MissionHotSpots":
                        Console.WriteLine("MissionHotSpots");
                        map.majorAssets.Add(key, (MajorAsset)new MissionHotSpots(binaryReader));
                        continue;
                    case "MissionObjectives":
                        Console.WriteLine("MissionObjectives");
                        map.majorAssets.Add(key, (MajorAsset)new MissionObjectives(binaryReader));
                        continue;
                    case "StandingWaterAreas":
                        Console.WriteLine("StandingWaterAreas");
                        map.majorAssets.Add(key, (MajorAsset)new StandingWaterAreas(binaryReader));
                        continue;
                    case "RiverAreas":
                        Console.WriteLine("RiverAreas");
                        map.majorAssets.Add(key, (MajorAsset)new RiverAreas(binaryReader));
                        continue;
                    case "StandingWaveAreas":
                        Console.WriteLine("StandingWaveAreas");
                        map.majorAssets.Add(key, (MajorAsset)new StandingWaveAreas(binaryReader));
                        continue;
                    case "GlobalLighting":
                        Console.WriteLine("GlobalLighting");
                        map.majorAssets.Add(key, (MajorAsset)new GlobalLighting(binaryReader));
                        continue;
                    case "PostEffectsChunk":
                        Console.WriteLine("PostEffectsChunk");
                        map.majorAssets.Add(key, (MajorAsset)new PostEffectsChunk(binaryReader));
                        continue;
                    case "EnvironmentData":
                        Console.WriteLine("EnvironmentData");
                        map.majorAssets.Add(key, (MajorAsset)new EnvironmentData(binaryReader));
                        continue;
                    case "NamedCameras":
                        Console.WriteLine("NamedCameras");
                        map.majorAssets.Add(key, (MajorAsset)new NamedCameras(binaryReader));
                        continue;
                    case "CameraAnimationList":
                        Console.WriteLine("CameraAnimationList");
                        map.majorAssets.Add(key, (MajorAsset)new CameraAnimationList(binaryReader));
                        continue;
                    case "WaypointsList":
                        Console.WriteLine("WaypointsList");
                        map.majorAssets.Add(key, (MajorAsset)new WaypointsList(binaryReader));
                        continue;
                    */
                    default:
                        Console.WriteLine("*\t Asset: {0} not parsed, error occurred, ending parsing", (object)key);
                        binaryReader.BaseStream.Position = binaryReader.BaseStream.Length;
                        continue;
                }
            }
            streamWriter2.Close();
            StreamWriter streamWriter3 = new StreamWriter("parsed assetStrings.txt");
            foreach (KeyValuePair<string, int> keyValuePair in map.nameIDs)
                streamWriter3.WriteLine("{0,-20}\t{1}", (object)keyValuePair.Key, (object)keyValuePair.Value);
            streamWriter3.Close();
            //map.mapWidth = map.heightMap.mapWidth;
            //map.mapHeight = map.heightMap.mapHeight;
            //map.mapBorder = map.heightMap.borderWidth;
            //map.PlayerCount = map.objectList.StartingLocationsCount;
            binaryReader.Close();
            file.Close();
            //Console.WriteLine("\t Map exported to \"{0}.bin\"", (object)Path.GetFileNameWithoutExtension(file));
            Console.WriteLine("*\t Done parsing \"{0}\"", (object)file);
        }
    }
}
