using SageCS.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageCS.Environment.Assets
{
    internal class Teams : MajorAsset
    {
        public Team[] teams;

        public Teams() : base()
        {
            this.Default();
        }

        public Teams(BinaryReader br) : base(br)
        {
            this.teams = new Team[br.ReadInt32()];
            for (int index = 0; index < this.teams.Length; ++index)
                this.teams[index] = new Team(br);
            this.CheckParsedSize(br);
        }

        protected override void SaveData(BinaryWriter bw)
        {
            bw.Write(this.teams.Length);
            foreach (Team team in this.teams)
                team.Save(bw);
        }

        public override void Register(Map assetContainer)
        {
            this.RegisterSelf(assetContainer);
        }

        public void AddTeam(string name, string owner, bool singleton)
        {
            Team team = new Team(name, owner, singleton);
            Array.Resize<Team>(ref this.teams, this.teams.Length + 1);
            this.teams[this.teams.Length - 1] = team;
        }

        public override void Default()
        {
            this.teams = new Team[11];
            this.teams[0] = new Team("team", "", true);
            this.teams[1] = new Team("teamPlyrCivilian", "PlyrCivilian", true);
            this.teams[2] = new Team("teamSkirmishNull", "SkirmishNull", true);
            this.teams[3] = new Team("teamSkirmishObserver", "SkirmishObserver", true);
            this.teams[4] = new Team("teamSkirmishJapan", "SkirmishJapan", true);
            this.teams[5] = new Team("teamSkirmishCommentator", "SkirmishCommentator", true);
            this.teams[6] = new Team("teamSkirmishAllies", "SkirmishAllies", true);
            this.teams[7] = new Team("teamSkirmishNeutral", "SkirmishNeutral", true);
            this.teams[8] = new Team("teamSkirmishCivilian", "SkirmishCivilian", true);
            this.teams[9] = new Team("teamSkirmishRandom", "SkirmishRandom", true);
            this.teams[10] = new Team("teamSkirmishSoviet", "SkirmishSoviet", true);
        }
    }
}
