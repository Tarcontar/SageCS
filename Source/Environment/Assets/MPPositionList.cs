

using SageCS.Core;
using System;
using System.IO;

namespace SageCS.Environment.Assets
{
    class MPPositionList : MajorAsset
    {
        public MPPositionInfo[] positionInfo;

        public MPPositionList() : base()
        {
            this.Default();
        }

        public MPPositionList(BinaryReader br) : base(br)
        {
            this.positionInfo = new MPPositionInfo[0];
            while (br.BaseStream.Position < this.dataPos + (long)this.dataSize)
            {
                Array.Resize<MPPositionInfo>(ref this.positionInfo, this.positionInfo.Length + 1);
                this.positionInfo[this.positionInfo.Length - 1] = new MPPositionInfo(br);
            }
            if (this.positionInfo.Length != 6)
                Console.WriteLine("!\t Asset: MPPositionList has {0} MPPositionInfo(s)", (object)this.positionInfo.Length);
            this.CheckParsedSize(br);
        }

        protected override void SaveData(BinaryWriter bw)
        {
            for (int index = 0; index < this.positionInfo.Length; ++index)
                this.positionInfo[index].Save(bw);
        }

        public override void Register(Map assetContainer)
        {
            this.RegisterSelf(assetContainer);
            for (int index = 0; index < this.positionInfo.Length; ++index)
                this.positionInfo[index].Register(assetContainer);
        }

        public override void Default()
        {
            this.positionInfo = new MPPositionInfo[6];
            for (int index = 0; index < this.positionInfo.Length; ++index)
                this.positionInfo[index] = new MPPositionInfo();
        }
    }
}
