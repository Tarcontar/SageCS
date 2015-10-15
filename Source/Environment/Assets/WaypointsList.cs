using SageCS.Core;
using System;
using System.IO;

namespace SageCS.Environment.Assets
{
    internal class WaypointsList : MajorAsset
    {
        private int count;

        public WaypointsList() : base()
        {
            this.Default();
        }

        public WaypointsList(BinaryReader br) : base(br)
        {
            this.count = br.ReadInt32();
            br.BaseStream.Position += (long)(this.dataSize - 4);
            Console.WriteLine("!\t Asset: WaypointsList not implemented, skipping data");
            this.CheckParsedSize(br);
        }

        protected override void SaveData(BinaryWriter bw)
        {
            bw.Write(this.count);
            Console.WriteLine("!\t Asset: WaypointsList not implemented, writing default data");
        }

        public override void Register(Map assetContainer)
        {
            this.RegisterSelf(assetContainer);
        }

        public override void Default()
        {
            this.count = 0;
        }
    }
}
