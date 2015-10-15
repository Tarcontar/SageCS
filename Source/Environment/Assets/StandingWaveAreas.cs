using SageCS.Core;
using System.IO;

namespace SageCS.Environment.Assets
{
    internal class StandingWaveAreas : MajorAsset
    {
        public StandingWaveArea[] areas;

        public StandingWaveAreas() : base()
        {
            this.Default();
        }

        public StandingWaveAreas(BinaryReader br) : base(br)
        {
            this.areas = new StandingWaveArea[br.ReadInt32()];
            for (int index = 0; index < this.areas.Length; ++index)
                this.areas[index] = new StandingWaveArea(br);
            this.CheckParsedSize(br);
        }

        protected override void SaveData(BinaryWriter bw)
        {
            bw.Write(this.areas.Length);
            foreach (StandingWaveArea standingWaveArea in this.areas)
                standingWaveArea.Save(bw);
        }

        public override void Register(Map assetContainer)
        {
            this.RegisterSelf(assetContainer);
        }

        public override void Default()
        {
            this.areas = new StandingWaveArea[0];
        }
    }
}
