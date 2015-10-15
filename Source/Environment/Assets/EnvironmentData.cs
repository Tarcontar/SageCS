using SageCS.Core;
using SageCS.Environment.Utility;
using System.IO;

namespace SageCS.Environment.Assets
{
    internal class EnvironmentData : MajorAsset
    {
        private float waterMaxAlphaDepth;
        private float waterMaxAlpha;
        private string macroTexture;
        private string cloudTexture;
        private string environmentMap;

        public EnvironmentData() : base()
        {
            this.Default();
        }

        public EnvironmentData(BinaryReader br) : base(br)
        {
            this.waterMaxAlphaDepth = br.ReadSingle();
            this.waterMaxAlpha = br.ReadSingle() * (float)byte.MaxValue;
            /*
            if (game == Game.CC3 && (int)br.ReadByte() != 0)
                OutputConsole.WriteLine("!\t EnvironmentData expected byte 0 between waterMaxAlpha and macroTexture");
            */
            this.macroTexture = IOUtility.ReadString(br);
            this.cloudTexture = IOUtility.ReadString(br);
            this.environmentMap = IOUtility.ReadString(br);
            this.CheckParsedSize(br);
        }

        protected override void SaveData(BinaryWriter bw)
        {
            bw.Write(this.waterMaxAlphaDepth);
            bw.Write(this.waterMaxAlpha / (float)byte.MaxValue);
            /*
            if (this.game == Game.CC3)
                bw.Write((byte)0);
            */
            IOUtility.WriteString(bw, this.macroTexture);
            IOUtility.WriteString(bw, this.cloudTexture);
            IOUtility.WriteString(bw, this.environmentMap);
        }

        public override void Register(Map assetContainer)
        {
            this.RegisterSelf(assetContainer);
        }

        public override void Default()
        {
            this.waterMaxAlpha = (float)byte.MaxValue;
            this.waterMaxAlphaDepth = 20f;
            this.macroTexture = "TSNoiseUrb";
            this.cloudTexture = "TSCloudMed";
            this.environmentMap = "EVDefault";
        }
    }
}
