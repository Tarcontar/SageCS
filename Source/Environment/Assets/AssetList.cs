using SageCS.Core;
using System;
using System.Collections.Generic;
using System.IO;

namespace SageCS.Environment.Assets
{
    class AssetList : MajorAsset
    {

        public static Dictionary<string, AssetBlock> assetIds = new Dictionary<string, AssetBlock>();
        public AssetList.AssetBlock[] assets;

        public AssetList() : base()
        {
            this.Default();
        }

        public AssetList(BinaryReader br) : base(br)
        {
            this.assets = new AssetList.AssetBlock[br.ReadInt32()];
            for (int index = 0; index < this.assets.Length; ++index)
                this.assets[index] = new AssetList.AssetBlock(br);
            this.CheckParsedSize(br);
        }

        public void AddAsset(string typeName)
        {
            AssetList.AssetBlock assetBlock1;
            if (AssetList.assetIds.TryGetValue(typeName.ToLowerInvariant(), out assetBlock1))
            {
                foreach (AssetList.AssetBlock assetBlock2 in this.assets)
                {
                    if ((int)assetBlock1.id == (int)assetBlock2.id && (int)assetBlock1.type == (int)assetBlock2.type)
                        return;
                }
                Array.Resize<AssetList.AssetBlock>(ref this.assets, this.assets.Length + 1);
                this.assets[this.assets.Length - 1] = assetBlock1;
            }
            else
                Console.WriteLine("!\t AssetList unknown asset typename: {0}", (object)typeName);
        }

        protected override void SaveData(BinaryWriter bw)
        {
            bw.Write(this.assets.Length);
            foreach (AssetList.AssetBlock assetBlock in this.assets)
                assetBlock.Save(bw);
        }

        public override void Register(Map assetContainer)
        {
            this.RegisterSelf(assetContainer);
        }

        public override void Default()
        {
            this.assets = new AssetList.AssetBlock[17];
            this.assets[0] = new AssetList.AssetBlock(568797146U, 864929218U);
            this.assets[1] = new AssetList.AssetBlock(568797146U, 2206724476U);
            this.assets[2] = new AssetList.AssetBlock(568797146U, 2782672656U);
        }

        public struct AssetBlock
        {
            public uint type;
            public uint id;

            public AssetBlock(uint Type, uint Id)
            {
                this.type = Type;
                this.id = Id;
            }

            public AssetBlock(BinaryReader br)
            {
                this.type = br.ReadUInt32();
                this.id = br.ReadUInt32();
            }

            public void Save(BinaryWriter bw)
            {
                bw.Write(this.type);
                bw.Write(this.id);
            }

            public override string ToString()
            {
                return string.Format("0x{0:X8}, 0x{1:X8}", (object)this.type, (object)this.id);
            }
        }
    }
}
