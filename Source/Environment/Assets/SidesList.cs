

using SageCS.Core;
using System;
using System.IO;

namespace SageCS.Environment.Assets
{
    class SidesList : MajorAsset
    {
        private Player[] players;

        public SidesList() : base()
        {
            this.Default();
        }

        public SidesList(BinaryReader br) : base(br)
        {
            if ((int)br.ReadByte() != 1)
                Console.WriteLine("!\t Asset: SidesList expected 1");
            int length = br.ReadInt32();
            this.players = new Player[length];
            for (int index = 0; index < length; ++index)
                this.players[index] = new Player(br);
            this.CheckParsedSize(br);
        }

        protected override void SaveData(BinaryWriter bw)
        {
            bw.Write((byte)1);
            bw.Write(this.players.Length);
            foreach (Player player in this.players)
                player.Save(bw);
        }

        public override void Register(Map assetContainer)
        {
            this.RegisterSelf(assetContainer);
            foreach (Player player in this.players)
                player.Register(assetContainer);
        }

        public override void Default()
        {
            /*
            if (this.game == Game.RA3)
            {
                this.players = new Player[11];
                this.players[0] = new Player();
                this.players[1] = new Player("PlyrCivilian", "PlayerTemplate:FactionCivilian");
                this.players[2] = new Player("SkirmishNull", "PlayerTemplate:Null");
                this.players[3] = new Player("SkirmishObserver", "PlayerTemplate:Observer");
                this.players[4] = new Player("SkirmishJapan", "PlayerTemplate:Japan");
                this.players[5] = new Player("SkirmishCommentator", "PlayerTemplate:Commentator");
                this.players[6] = new Player("SkirmishAllies", "PlayerTemplate:Allies");
                this.players[7] = new Player("SkirmishNeutral", "PlayerTemplate:Neutral");
                this.players[8] = new Player("SkirmishCivilian", "PlayerTemplate:Civilian");
                this.players[9] = new Player("SkirmishRandom", "PlayerTemplate:Random");
                this.players[10] = new Player("SkirmishSoviet", "PlayerTemplate:Soviet");
            }
            else
            {
                if (this.game != Game.CC3)
                    throw new NotImplementedException("Asset: SidesList not implemented for Game." + (object)this.game);
                this.players = new Player[18];
                this.players[0] = new Player();
                this.players[1] = new Player("PlyrGDI", "FactionGDI");
                this.players[2] = new Player("PlyrCivilian", "FactionCivilian");
                this.players[3] = new Player("PlyrNeutral", "FactionNeutral");
                this.players[4] = new Player("PlyrCreeps", "FactionCivilian");
                this.players[5] = new Player("Skirmish", "");
                this.players[6] = new Player("SkirmishNull", "FactionRandom");
                this.players[7] = new Player("SkirmishObserver", "FactionObserver");
                this.players[8] = new Player("SkirmishCommentator", "FactionCommentator");
                this.players[9] = new Player("SkirmishCivilian", "FactionCivilian");
                this.players[10] = new Player("SkirmishNeutral", "FactionNeutral");
                this.players[11] = new Player("SkirmishGDI", "FactionGDI");
                this.players[12] = new Player("SkirmishNod", "FactionNOD");
                this.players[13] = new Player("SkirmishAlien", "FactionAlien");
                this.players[14] = new Player("Player_1", "FactionCivilian");
                this.players[15] = new Player("Player_2", "FactionCivilian");
                this.players[16] = new Player("Player_3", "FactionCivilian");
                this.players[17] = new Player("Player_4", "FactionCivilian");
            }
            */
        }

        public void AddPlayer(string name, string faction)
        {
            Player player = new Player(name, faction);
            Array.Resize<Player>(ref this.players, this.players.Length + 1);
            this.players[this.players.Length - 1] = player;
        }
    }
}
