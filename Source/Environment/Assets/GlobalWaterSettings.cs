using SageCS.Core;
using System.IO;

namespace SageCS.Environment.Assets
{
    class GlobalWaterSettings : MajorAsset
    {
        private bool reflection;
        private float reflectionPlaneZ;

        public bool Reflection
        {
            get
            {
                return this.reflection;
            }
            set
            {
                this.reflection = value;
            }
        }

        public float ReflectionPlaneZ
        {
            get
            {
                return this.reflectionPlaneZ;
            }
            set
            {
                this.reflectionPlaneZ = value;
            }
        }

        public GlobalWaterSettings() : base()
        {
            this.Default();
        }

        public GlobalWaterSettings(BinaryReader br) : base(br)
        {
            this.reflection = br.ReadInt32() == 1;
            this.reflectionPlaneZ = br.ReadSingle();
            this.CheckParsedSize(br);
        }

        protected override void SaveData(BinaryWriter bw)
        {
            bw.Write(this.reflection ? 1 : 0);
            bw.Write(this.reflectionPlaneZ);
        }

        public override void Register(Map assetContainer)
        {
            this.RegisterSelf(assetContainer);
        }

        public override void Default()
        {
            this.reflection = false;
            this.reflectionPlaneZ = 0.0f;
        }
    }
}
