using SageCS.Core;
using SageCS.Environment.Utility;
using System.IO;

namespace SageCS.Environment.Assets
{
    class MPPositionInfo : MajorAsset
    {
        public bool isHuman;
        public bool isComputer;
        public bool loadAIScript;
        public uint team;
        public string[] sideRestriction;

        public MPPositionInfo() : base()
        {
            this.Default();
        }

        public MPPositionInfo(BinaryReader br) : base(br)
        {
            this.isHuman = br.ReadBoolean();
            this.isComputer = br.ReadBoolean();
            this.loadAIScript = br.ReadBoolean();
            this.team = br.ReadUInt32();
            this.sideRestriction = new string[br.ReadInt32()];
            for (int index = 0; index < this.sideRestriction.Length; ++index)
                this.sideRestriction[index] = IOUtility.ReadString(br);
            this.CheckParsedSize(br);
        }

        protected override void SaveData(BinaryWriter bw)
        {
            bw.Write(this.isHuman);
            bw.Write(this.isComputer);
            bw.Write(this.loadAIScript);
            bw.Write(this.team);
            bw.Write(this.sideRestriction.Length);
            for (int index = 0; index < this.sideRestriction.Length; ++index)
                IOUtility.WriteString(bw, this.sideRestriction[index]);
        }

        public override void Default()
        {
            this.isHuman = this.isComputer = this.loadAIScript = true;
            this.team = uint.MaxValue;
            this.sideRestriction = new string[0];
        }

        public override void Register(Map assetContainer)
        {
            this.RegisterSelf(assetContainer);
        }
    }
}
