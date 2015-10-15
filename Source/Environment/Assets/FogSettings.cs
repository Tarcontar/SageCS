using SageCS.Core;
using System.IO;

namespace SageCS.Environment.Assets
{
    internal class FogSettings : MajorAsset
    {
        public bool enabled;
        public float start;
        public float end;
        public float r;
        public float g;
        public float b;

        public FogSettings() : base()
        {
            this.Default();
        }

        public FogSettings(BinaryReader br) : base(br)
        {
            this.enabled = br.ReadInt32() == 1;
            this.start = br.ReadSingle();
            this.end = br.ReadSingle();
            this.r = br.ReadSingle();
            this.g = br.ReadSingle();
            this.b = br.ReadSingle();
            this.CheckParsedSize(br);
        }

        protected override void SaveData(BinaryWriter bw)
        {
            bw.Write(this.enabled ? 1 : 0);
            bw.Write(this.start);
            bw.Write(this.end);
            bw.Write(this.r);
            bw.Write(this.g);
            bw.Write(this.b);
        }

        public override void Register(Map assetContainer)
        {
            this.RegisterSelf(assetContainer);
        }

        public override void Default()
        {
            this.enabled = true;
            this.start = 2000f;
            this.end = 5000f;
            this.r = this.g = this.b = 0.5f;
        }
    }
}
