using SageCS.Core;
using System.IO;

namespace SageCS.Environment.Assets
{
    class GlobalVersion : MajorAsset
    {
        public byte[] data;

        public GlobalVersion() : base()
        {
            this.Default();
        }

        public GlobalVersion(BinaryReader br) : base(br)
        {
            this.data = br.ReadBytes(this.dataSize);
        }

        protected override void SaveData(BinaryWriter bw)
        {
            bw.Write(this.data);
        }

        public override void Register(Map assetContainer)
        {
            this.RegisterSelf(assetContainer);
        }

        public override void Default()
        {
            this.data = new byte[0];
        }
    }
}

