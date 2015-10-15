using SageCS.Core;
using System;
using System.IO;

namespace SageCS.Environment.Assets
{
    internal class ScriptList : Asset
    {
        private int len;
        private byte[] data;

        public ScriptList() : base()
        {
            this.Default();
        }

        public ScriptList(BinaryReader br) : base(br)
        {
            this.len = br.ReadInt32();
            this.data = br.ReadBytes(this.len);
            if (this.len == 0)
                return;
            Console.WriteLine("!\t Asset: ScriptList has data, len = {0}", (object)this.len);
        }

        protected override void SaveData(BinaryWriter bw)
        {
            bw.Write(this.len);
            Console.WriteLine("!\t Asset: ScriptList not implemented, writing default data");
        }

        public override void Register(Map assetContainer)
        {
            this.RegisterSelf(assetContainer);
        }

        public override void Default()
        {
            this.len = 0;
        }
    }
}
