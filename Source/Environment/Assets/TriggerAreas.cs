using SageCS.Core;
using System.IO;

namespace SageCS.Environment.Assets
{
    internal class TriggerAreas : MajorAsset
    {
        public TriggerArea[] areas;

        public TriggerAreas() : base()
        {
            this.Default();
        }

        public TriggerAreas(BinaryReader br) : base(br)
        {
            this.areas = new TriggerArea[br.ReadInt32()];
            for (int index = 0; index < this.areas.Length; ++index)
                this.areas[index] = new TriggerArea(br);
            this.CheckParsedSize(br);
        }

        protected override void SaveData(BinaryWriter bw)
        {
            bw.Write(this.areas.Length);
            foreach (TriggerArea triggerArea in this.areas)
                triggerArea.Save(bw);
        }

        public override void Register(Map assetContainer)
        {
            this.RegisterSelf(assetContainer);
        }

        public override void Default()
        {
            this.areas = new TriggerArea[0];
        }
    }
}
