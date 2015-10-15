using OpenTK;
using SageCS.Core;
using System;
using System.IO;

namespace SageCS.Environment.Assets
{
    class StandingWaterAreas : MajorAsset
    {
        public StandingWaterArea[] areas;

        public StandingWaterAreas() : base()
        {
            this.Default();
        }

        public StandingWaterAreas(BinaryReader br) : base(br)
        {
            this.areas = new StandingWaterArea[br.ReadInt32()];
            for (int index = 0; index < this.areas.Length; ++index)
                this.areas[index] = new StandingWaterArea(br);
            this.CheckParsedSize(br);
        }

        protected override void SaveData(BinaryWriter bw)
        {
            bw.Write(this.areas.Length);
            foreach (StandingWaterArea standingWaterArea in this.areas)
                standingWaterArea.Save(bw);
        }

        public override void Register(Map assetContainer)
        {
            this.RegisterSelf(assetContainer);
        }

        public override void Default()
        {
            this.areas = new StandingWaterArea[0];
        }

        public void AddStandingWaterArea(int height, params Vector2[] points)
        {
            Array.Resize<StandingWaterArea>(ref this.areas, this.areas.Length + 1);
            this.areas[this.areas.Length - 1] = new StandingWaterArea(height, points);
        }

    }
}
