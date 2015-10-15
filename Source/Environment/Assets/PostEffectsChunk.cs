using SageCS.Core;
using System.IO;

namespace SageCS.Environment.Assets
{
    internal class PostEffectsChunk : MajorAsset
    {
        private PostEffect[] effects;

        public PostEffectsChunk() : base()
        {
            this.Default();
        }

        public PostEffectsChunk(BinaryReader br) : base(br)
        {
            this.effects = new PostEffect[br.ReadInt32()];
            for (int index = 0; index < this.effects.Length; ++index)
                this.effects[index] = new PostEffect(br);
            this.CheckParsedSize(br);
        }

        protected override void SaveData(BinaryWriter bw)
        {
            bw.Write(this.effects.Length);
            foreach (PostEffect postEffect in this.effects)
                postEffect.Save(bw);
        }

        public override void Register(Map assetContainer)
        {
            this.RegisterSelf(assetContainer);
        }

        public override void Default()
        {
            this.effects = new PostEffect[1];
            this.effects[0] = new PostEffect();
            this.effects[0].name = "Distortion";
            this.effects[0].parameters = new PostEffect.Parameter[0];
        }
    }
}
