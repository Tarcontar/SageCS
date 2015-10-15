using OpenTK;
using SageCS.Core;
using System;
using System.IO;

namespace SageCS.Environment.Assets
{
    internal class GlobalLighting : MajorAsset
    {
        private GlobalLighting.Time time;
        private Vector3 ambientColor;
        private Vector3 noCloudColor;
        private Light[] lights;
        private uint shadowColor;
        private byte[] unknown;

        public GlobalLighting() : base()
        {
            this.Default();
        }

        public GlobalLighting(BinaryReader br) : base(br)
        {
            this.time = (GlobalLighting.Time)br.ReadInt32();
            this.ambientColor = new Vector3(br.ReadSingle(), br.ReadSingle(), br.ReadSingle());
            this.lights = new Light[3];
            this.lights[0] = new Light(br);
            if (!(new Vector3(br.ReadSingle(), br.ReadSingle(), br.ReadSingle()).Length == 0))
                Console.WriteLine("!\t Asset: GlobalLighting expected 0s");
            this.lights[1] = new Light(br);
            if (!(new Vector3(br.ReadSingle(), br.ReadSingle(), br.ReadSingle()).Length == 0))
                Console.WriteLine("!\t Asset: GlobalLighting expected 0s");
            this.lights[2] = new Light(br);
            this.unknown = br.ReadBytes(324);
            this.shadowColor = br.ReadUInt32();
            this.noCloudColor = new Vector3(br.ReadSingle(), br.ReadSingle(), br.ReadSingle());
            this.CheckParsedSize(br);
        }

        protected override void SaveData(BinaryWriter bw)
        {
            bw.Write((int)this.time);
            bw.Write(this.ambientColor.X);
            bw.Write(this.ambientColor.Y);
            bw.Write(this.ambientColor.Z);
            this.lights[0].Save(bw);
            bw.Write(0.0);
            bw.Write(0.0);
            bw.Write(0.0);
            this.lights[1].Save(bw);
            bw.Write(0.0);
            bw.Write(0.0);
            bw.Write(0.0);
            this.lights[2].Save(bw);
            //bw.Write(Resource.ra3_default_GlobalLighting_chunk, 0, 324);
            bw.Write(this.shadowColor);
            bw.Write(this.noCloudColor.X);
            bw.Write(this.noCloudColor.Y);
            bw.Write(this.noCloudColor.Z);
        }

        public override void Register(Map assetContainer)
        {
            this.RegisterSelf(assetContainer);
        }

        public override void Default()
        {
            /*
            if (this.game == Game.RA3)
            {
                this.unknown = Resource.ra3_default_GlobalLighting_chunk;
                Array.Resize<byte>(ref this.unknown, 324);
            }
            else
            {
                if (this.game != Game.CC3)
                    throw new NotImplementedException("Asset: GlobalLighting.Default() not implemented for Game." + (object)this.game);
                this.unknown = Resource.cc3_default_GlobalLighting;
            }
            */
            this.time = GlobalLighting.Time.Morning;
            this.ambientColor = new Vector3(0.14f, 0.13f, 0.13f);
            this.lights = new Light[3];
            this.lights[0] = new Light(-50f, -30f, new Vector3(1.25f, 1.21f, 1.04f));
            this.lights[1] = new Light(-6f, 36f, new Vector3(0.69f, 0.67f, 0.69f));
            this.lights[2] = new Light(-67f, -70f, new Vector3(0.75f, 0.83f, 0.89f));
            this.shadowColor = uint.MaxValue;
            this.noCloudColor = new Vector3(1f, 1f, 1f);
        }

        public enum Time
        {
            Morning = 1,
            Afternoon = 2,
            Evening = 3,
            Night = 4,
            Interpolate = 5,
        }
    }
}
