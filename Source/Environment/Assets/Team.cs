using System;
using System.Collections.Generic;
using System.IO;

namespace SageCS.Environment.Assets
{
    internal class Team 
    {
        private Dictionary<string, SubAsset> namedSubAssets;
        private SubAsset<string> teamName;
        private SubAsset<string> teamOwner;
        private SubAsset<bool> teamIsSingleton;

        public Team(string name, string owner, bool singleton)
        {
            this.namedSubAssets = new Dictionary<string, SubAsset>();
            this.teamName = new SubAsset<string>("teamName", name, this.namedSubAssets);
            this.teamOwner = new SubAsset<string>("teamOwner", owner, this.namedSubAssets);
            this.teamIsSingleton = new SubAsset<bool>("teamIsSingleton", singleton, this.namedSubAssets);
        }

        public Team(BinaryReader br)
        {
            short num1 = br.ReadInt16();
            this.namedSubAssets = new Dictionary<string, SubAsset>((int)num1);
            uint num2 = 0U;
            for (int index = 0; index < (int)num1; ++index)
            {
                string name = SubAsset.PeekSubAssetName(br);
                if (name.StartsWith("teamUnitType"))
                {
                    SubAsset<string> subAsset1 = new SubAsset<string>(br, name, this.namedSubAssets);
                }
                else if (name.StartsWith("teamUnitMaxCount"))
                {
                    SubAsset<int> subAsset2 = new SubAsset<int>(br, name, this.namedSubAssets);
                }
                else if (name.StartsWith("teamUnitMinCount"))
                {
                    SubAsset<int> subAsset3 = new SubAsset<int>(br, name, this.namedSubAssets);
                }
                else
                {
                    switch (name)
                    {
                        case "teamName":
                            this.teamName = new SubAsset<string>(br, name, this.namedSubAssets);
                            continue;
                        case "teamOwner":
                            this.teamOwner = new SubAsset<string>(br, name, this.namedSubAssets);
                            continue;
                        case "teamIsSingleton":
                            this.teamIsSingleton = new SubAsset<bool>(br, name, this.namedSubAssets);
                            continue;
                        case "exportWithScript":
                            SubAsset<bool> subAsset4 = new SubAsset<bool>(br, name, this.namedSubAssets);
                            continue;
                        default:
                            SubAsset.ReadAsBytes(br, name);
                            ++num2;
                            continue;
                    }
                }
            }
            if (num2 <= 0U)
                return;
            Console.WriteLine("!\t Asset: {0} skipped parsing {1} SubAssets", (object)this, (object)num2);
        }

        public void Save(BinaryWriter bw)
        {
            bw.Write((short)this.namedSubAssets.Count);
            foreach (SubAsset subAsset in this.namedSubAssets.Values)
                subAsset.Save(bw);
        }

        public override string ToString()
        {
            return string.Format("\"{0}\" \"{1}\" \"{2}\"", (object)this.teamName, (object)this.teamOwner, (object)this.teamIsSingleton);
        }
    }
}
