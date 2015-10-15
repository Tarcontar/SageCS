using SageCS.Core;
using System;
using System.Collections.Generic;
using System.IO;

namespace SageCS.Environment.Assets
{
    internal class PlayerScriptsList : MajorAsset
    {
        private ScriptList[] scriptList;

        public PlayerScriptsList() : base()
        {
            this.Default();
        }

        public PlayerScriptsList(BinaryReader br) : base(br)
        {
            List<ScriptList> list = new List<ScriptList>();
            while (br.BaseStream.Position - this.dataPos < (long)this.dataSize)
                list.Add(new ScriptList(br));
            this.scriptList = list.ToArray();
            if (br.BaseStream.Position - this.dataPos == (long)this.dataSize)
                return;
            Console.WriteLine("!\t Asset: PlayerScriptsList parsed wrong number of bytes");
        }

        protected override void SaveData(BinaryWriter bw)
        {
            for (int index = 0; index < this.scriptList.Length; ++index)
                this.scriptList[index].Save(bw);
        }

        public override void Register(Map assetContainer)
        {
            this.RegisterSelf(assetContainer);
            for (int index = 0; index < this.scriptList.Length; ++index)
                this.scriptList[index].Register(assetContainer);
        }

        public override void Default()
        {
            this.scriptList = new ScriptList[11];
            for (int index = 0; index < this.scriptList.Length; ++index)
                this.scriptList[index] = new ScriptList();
        }

        public void AddScript()
        {
            Array.Resize<ScriptList>(ref this.scriptList, this.scriptList.Length + 1);
            this.scriptList[this.scriptList.Length - 1] = new ScriptList();
        }
    }
}
