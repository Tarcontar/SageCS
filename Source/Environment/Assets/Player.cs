using SageCS.Core;
using System;
using System.Collections.Generic;
using System.IO;

namespace SageCS.Environment.Assets
{
    class Player
    {
        private Dictionary<string, SubAsset> namedSubAssets;
        private int trailer;
        private SubAsset<string> playerName;
        private SubAsset<bool> playerIsHuman;
        private SubAsset<string> playerDisplayName;
        public SubAsset<string> playerFaction;
        private SubAsset<string> playerAllies;
        private SubAsset<string> playerEnemies;
        private SubAsset<Player.AIDifficulty> aiBaseBuilder;
        private SubAsset<Player.AIDifficulty> aiUnitBuilder;
        private SubAsset<Player.AIDifficulty> aiTeamBuilder;
        private SubAsset<Player.AIDifficulty> aiEconomyBuilder;
        private SubAsset<Player.AIDifficulty> aiWallBuilder;
        private SubAsset<Player.AIDifficulty> aiUnitUpgrader;
        private SubAsset<Player.AIDifficulty> aiScienceUpgrader;
        private SubAsset<Player.AIDifficulty> aiTactical;
        private SubAsset<Player.AIDifficulty> aiOpeningMover;
        private SubAsset<string> aiPersonality;
        private SubAsset<int> playerColor;
        private SubAsset<int> playerRadarColor;

        public Player()
        {
            this.namedSubAssets = new Dictionary<string, SubAsset>();
            this.playerName = new SubAsset<string>("playerName", "", this.namedSubAssets);
            this.playerIsHuman = new SubAsset<bool>("playerIsHuman", false, this.namedSubAssets);
            this.playerDisplayName = new SubAsset<string>("playerDisplayName", "Neutral", this.namedSubAssets);
            this.playerFaction = new SubAsset<string>("playerFaction", "", this.namedSubAssets);
            this.playerAllies = new SubAsset<string>("playerAllies", "", this.namedSubAssets);
            this.playerEnemies = new SubAsset<string>("playerEnemies", "", this.namedSubAssets);
            this.trailer = 0;
        }

        public Player(string name, string faction)
        {
            this.namedSubAssets = new Dictionary<string, SubAsset>();
            Player.AIDifficulty data = Player.AIDifficulty.Easy | Player.AIDifficulty.Normal | Player.AIDifficulty.Hard | Player.AIDifficulty.Brutal;
            this.playerName = new SubAsset<string>("playerName", name, this.namedSubAssets);
            this.playerIsHuman = new SubAsset<bool>("playerIsHuman", false, this.namedSubAssets);
            this.playerDisplayName = new SubAsset<string>("playerDisplayName", name, this.namedSubAssets);
            this.playerFaction = new SubAsset<string>("playerFaction", faction, this.namedSubAssets);
            this.playerAllies = new SubAsset<string>("playerAllies", "", this.namedSubAssets);
            this.playerEnemies = new SubAsset<string>("playerEnemies", "", this.namedSubAssets);
            this.aiBaseBuilder = new SubAsset<Player.AIDifficulty>("aiBaseBuilder", data, this.namedSubAssets);
            this.aiUnitBuilder = new SubAsset<Player.AIDifficulty>("aiUnitBuilder", data, this.namedSubAssets);
            this.aiTeamBuilder = new SubAsset<Player.AIDifficulty>("aiTeamBuilder", data, this.namedSubAssets);
            this.aiEconomyBuilder = new SubAsset<Player.AIDifficulty>("aiEconomyBuilder", data, this.namedSubAssets);
            this.aiWallBuilder = new SubAsset<Player.AIDifficulty>("aiWallBuilder", data, this.namedSubAssets);
            this.aiUnitUpgrader = new SubAsset<Player.AIDifficulty>("aiUnitUpgrader", data, this.namedSubAssets);
            this.aiScienceUpgrader = new SubAsset<Player.AIDifficulty>("aiScienceUpgrader", data, this.namedSubAssets);
            this.aiTactical = new SubAsset<Player.AIDifficulty>("aiTactical", data, this.namedSubAssets);
            this.aiOpeningMover = new SubAsset<Player.AIDifficulty>("aiOpeningMover", data, this.namedSubAssets);
            this.trailer = 0;
        }

        public Player(BinaryReader br)
        {
            short num1 = br.ReadInt16();
            this.namedSubAssets = new Dictionary<string, SubAsset>((int)num1);
            uint num2 = 0U;
            for (int index = 0; index < (int)num1; ++index)
            {
                string name = SubAsset.PeekSubAssetName(br);
                switch (name)
                {
                    case "playerName":
                        this.playerName = new SubAsset<string>(br, name, this.namedSubAssets);
                        break;
                    case "playerIsHuman":
                        this.playerIsHuman = new SubAsset<bool>(br, name, this.namedSubAssets);
                        break;
                    case "playerDisplayName":
                        this.playerDisplayName = new SubAsset<string>(br, name, this.namedSubAssets);
                        break;
                    case "playerFaction":
                        this.playerFaction = new SubAsset<string>(br, name, this.namedSubAssets);
                        break;
                    case "playerAllies":
                        this.playerAllies = new SubAsset<string>(br, name, this.namedSubAssets);
                        break;
                    case "playerEnemies":
                        this.playerEnemies = new SubAsset<string>(br, name, this.namedSubAssets);
                        break;
                    case "aiBaseBuilder":
                        this.aiBaseBuilder = new SubAsset<Player.AIDifficulty>(br, name, this.namedSubAssets);
                        break;
                    case "aiUnitBuilder":
                        this.aiUnitBuilder = new SubAsset<Player.AIDifficulty>(br, name, this.namedSubAssets);
                        break;
                    case "aiTeamBuilder":
                        this.aiTeamBuilder = new SubAsset<Player.AIDifficulty>(br, name, this.namedSubAssets);
                        break;
                    case "aiEconomyBuilder":
                        this.aiEconomyBuilder = new SubAsset<Player.AIDifficulty>(br, name, this.namedSubAssets);
                        break;
                    case "aiWallBuilder":
                        this.aiWallBuilder = new SubAsset<Player.AIDifficulty>(br, name, this.namedSubAssets);
                        break;
                    case "aiUnitUpgrader":
                        this.aiUnitUpgrader = new SubAsset<Player.AIDifficulty>(br, name, this.namedSubAssets);
                        break;
                    case "aiScienceUpgrader":
                        this.aiScienceUpgrader = new SubAsset<Player.AIDifficulty>(br, name, this.namedSubAssets);
                        break;
                    case "aiTactical":
                        this.aiTactical = new SubAsset<Player.AIDifficulty>(br, name, this.namedSubAssets);
                        break;
                    case "aiOpeningMover":
                        this.aiOpeningMover = new SubAsset<Player.AIDifficulty>(br, name, this.namedSubAssets);
                        break;
                    case "aiPersonality":
                        this.aiPersonality = new SubAsset<string>(br, name, this.namedSubAssets);
                        break;
                    case "playerColor":
                        this.playerColor = new SubAsset<int>(br, name, this.namedSubAssets);
                        break;
                    case "playerRadarColor":
                        this.playerRadarColor = new SubAsset<int>(br, name, this.namedSubAssets);
                        break;
                    default:
                        SubAsset.ReadAsBytes(br, name);
                        ++num2;
                        break;
                }
            }
            if (num2 > 0U)
                Console.WriteLine("!\t Asset: {0} skipped parsing {1} SubAssets", (object)this.GetType().Name, (object)num2);
            this.trailer = br.ReadInt32();
            if (this.trailer == 0)
                return;
            Console.WriteLine("!\t Asset: Player expected int 0 for trailer");
        }

        public void Save(BinaryWriter bw)
        {
            bw.Write((short)this.namedSubAssets.Count);
            foreach (SubAsset subAsset in this.namedSubAssets.Values)
                subAsset.Save(bw);
            bw.Write(this.trailer);
        }

        public void Register(Map assetContainer)
        {
            this.playerName.Register(assetContainer);
            this.playerIsHuman.Register(assetContainer);
            this.playerDisplayName.Register(assetContainer);
            this.playerFaction.Register(assetContainer);
            this.playerAllies.Register(assetContainer);
            this.playerEnemies.Register(assetContainer);
            this.aiBaseBuilder.Register(assetContainer);
            this.aiUnitBuilder.Register(assetContainer);
            this.aiTeamBuilder.Register(assetContainer);
            this.aiEconomyBuilder.Register(assetContainer);
            this.aiWallBuilder.Register(assetContainer);
            this.aiUnitUpgrader.Register(assetContainer);
            this.aiScienceUpgrader.Register(assetContainer);
            this.aiTactical.Register(assetContainer);
            this.aiOpeningMover.Register(assetContainer);
            this.aiPersonality.Register(assetContainer);
        }

        public void SaveText(string fileName)
        {
            StreamWriter streamWriter = new StreamWriter(fileName + ".txt", true);
            streamWriter.WriteLine("player[]=new Player(\"{0}\"", (object)this.playerName.data);
            foreach (SubAsset subAsset in this.namedSubAssets.Values)
                streamWriter.WriteLine(subAsset.Name + "=" + subAsset.ToString());
            streamWriter.WriteLine();
            streamWriter.Close();
        }

        [Flags]
        public enum AIDifficulty
        {
            Easy = 1,
            Normal = 2,
            Hard = 4,
            Brutal = 8,
            Unused0 = 16,
            Unused1 = 32,
            Unused2 = 64,
            Unused3 = 128,
        }
    }
}
