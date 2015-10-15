using SageCS.Core;
using System;
using System.Collections.Generic;
using System.IO;

namespace SageCS.Environment.Assets
{
    abstract class MajorAsset : Asset
    {
        protected int dataSize;
        protected long dataPos;
        public Dictionary<string, SubAsset> namedSubAssets;

        protected MajorAsset() : base()
        {
            this.dataSize = -1;
            this.dataPos = -1L;
            this.namedSubAssets = new Dictionary<string, SubAsset>();
        }


        protected MajorAsset(BinaryReader br) : base(br)
        {
            this.dataSize = br.ReadInt32();
            this.dataPos = br.BaseStream.Position;
            this.namedSubAssets = new Dictionary<string, SubAsset>();
        }

        protected bool CheckParsedSize(BinaryReader br)
        {
            if (br.BaseStream.Position - this.dataPos == (long)this.dataSize)
                return true;
            Console.WriteLine("!\t Asset: {2} parsed wrong number of bytes, should be 0x{0:X6} instead of 0x{1:X6}", (object)this.dataSize, (object)(br.BaseStream.Position - this.dataPos), (object)this.GetType().Name);
            return false;
        }

        protected override void RegisterSelf(Map assetContainer)
        {
            base.RegisterSelf(assetContainer);
            foreach (SubAsset subAsset in this.namedSubAssets.Values)
                subAsset.Register(assetContainer);
        }

        public abstract override void Register(Map assetContainer);

        public abstract override void Default();
    }
}
