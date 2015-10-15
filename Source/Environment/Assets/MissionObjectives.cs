using SageCS.Core;
using System.IO;

namespace SageCS.Environment.Assets
{
    internal class MissionObjectives : MajorAsset
    {
        public MissionObjective[] objectives;

        public MissionObjectives() : base()
        {
            this.Default();
        }

        public MissionObjectives(BinaryReader br) : base(br)
        {
            this.objectives = new MissionObjective[br.ReadInt32()];
            for (int index = 0; index < this.objectives.Length; ++index)
                this.objectives[index] = new MissionObjective(br);
            this.CheckParsedSize(br);
        }

        protected override void SaveData(BinaryWriter bw)
        {
            bw.Write(this.objectives.Length);
            for (int index = 0; index < this.objectives.Length; ++index)
                this.objectives[index].Save(bw);
        }

        public override void Register(Map assetContainer)
        {
            this.RegisterSelf(assetContainer);
        }

        public override void Default()
        {
            this.objectives = new MissionObjective[0];
        }
    }
}
