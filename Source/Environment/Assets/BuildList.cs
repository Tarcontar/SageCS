using SageCS.Environment.Utility;
using System;
using System.IO;

namespace SageCS.Environment.Assets
{
    internal class BuildList
    {
        private string faction;
        private int count;

        public BuildList(string faction, int count)
        {
            this.faction = faction;
            this.count = count;
        }

        public BuildList(BinaryReader br)
        {
            this.faction = IOUtility.ReadString(br);
            this.count = br.ReadInt32();
            if (this.count == 0)
                return;
            Console.WriteLine("!\t Asset: BuildList expected 0");
        }

        public void Save(BinaryWriter bw)
        {
            IOUtility.WriteString(bw, this.faction);
            bw.Write(this.count);
        }

        public override string ToString()
        {
            return string.Format("{0} count = {1}", (object)this.faction, (object)this.count);
        }
    }
}
