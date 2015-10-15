using SageCS.Core;
using System.IO;

namespace SageCS.Environment.Assets
{
    internal class MissionHotSpots : MajorAsset
    {
        public MissionHotSpot[] spots;

        public MissionHotSpots() : base()
        {
            this.Default();
        }

        public MissionHotSpots(BinaryReader br) : base(br)
        {
            this.spots = new MissionHotSpot[br.ReadInt32()];
            for (int index = 0; index < this.spots.Length; ++index)
                this.spots[index] = new MissionHotSpot(br);
            this.CheckParsedSize(br);
        }

        protected override void SaveData(BinaryWriter bw)
        {
            bw.Write(this.spots.Length);
            foreach (MissionHotSpot missionHotSpot in this.spots)
                missionHotSpot.Save(bw);
        }

        public override void Register(Map assetContainer)
        {
            this.RegisterSelf(assetContainer);
        }

        public override void Default()
        {
            this.spots = new MissionHotSpot[0];
        }
    }
}
