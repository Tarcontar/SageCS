using OpenTK;
using SageCS.Core;
using System;
using System.IO;

namespace SageCS.Environment.Assets
{
    class RiverAreas : MajorAsset
    {
        private static int riverAreaUniqueId;
        public RiverArea[] areas;

        public RiverAreas() : base()
        {
            this.Default();
        }

        public RiverAreas(BinaryReader br) : base(br)
        {
            this.areas = new RiverArea[br.ReadInt32()];
            for (int index = 0; index < this.areas.Length; ++index)
                this.areas[index] = new RiverArea(br);
            this.CheckParsedSize(br);
        }

        protected override void SaveData(BinaryWriter bw)
        {
            bw.Write(this.areas.Length);
            foreach (RiverArea riverArea in this.areas)
                riverArea.Save(bw);
        }

        public override void Register(Map assetContainer)
        {
            this.RegisterSelf(assetContainer);
        }

        public override void Default()
        {
            this.areas = new RiverArea[0];
        }

        public void AddRiverArea(int height, Vector2[] points)
        {
            Array.Resize<RiverArea>(ref this.areas, this.areas.Length + 1);
            this.areas[this.areas.Length - 1] = new RiverArea(RiverAreas.riverAreaUniqueId++, height, points);
        }
    }
}
