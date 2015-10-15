using SageCS.Core;
using System;
using System.IO;

namespace SageCS.Environment.Assets
{
    internal class BuildLists : MajorAsset
    {
        private BuildList[] buildList;

        public BuildLists() : base()
        {
            this.Default();
        }

        public BuildLists(BinaryReader br) : base(br)
        {
            this.buildList = new BuildList[br.ReadInt32()];
            for (int index = 0; index < this.buildList.Length; ++index)
                this.buildList[index] = new BuildList(br);
            this.CheckParsedSize(br);
        }

        public override void Default()
        {
            this.buildList = new BuildList[11];
            this.buildList[0] = new BuildList("PlayerTemplate:Null", 0);
            this.buildList[1] = new BuildList("PlayerTemplate:Civilian", 0);
            this.buildList[2] = new BuildList("PlayerTemplate:Null", 0);
            this.buildList[3] = new BuildList("PlayerTemplate:Observer", 0);
            this.buildList[4] = new BuildList("PlayerTemplate:Japan", 0);
            this.buildList[5] = new BuildList("PlayerTemplate:Commentator", 0);
            this.buildList[6] = new BuildList("PlayerTemplate:Allies", 0);
            this.buildList[7] = new BuildList("PlayerTemplate:Neutral", 0);
            this.buildList[8] = new BuildList("PlayerTemplate:Civilian", 0);
            this.buildList[9] = new BuildList("PlayerTemplate:Random", 0);
            this.buildList[10] = new BuildList("PlayerTemplate:Soviet", 0);
        }

        public void AddBuildList(Player player)
        {
            Array.Resize<BuildList>(ref this.buildList, this.buildList.Length + 1);
            this.buildList[this.buildList.Length - 1] = new BuildList(player.playerFaction.data, 0);
        }

        protected override void SaveData(BinaryWriter bw)
        {
            bw.Write(this.buildList.Length);
            for (int index = 0; index < this.buildList.Length; ++index)
                this.buildList[index].Save(bw);
        }

        public override void Register(Map assetContainer)
        {
            this.RegisterSelf(assetContainer);
        }
    }
}
