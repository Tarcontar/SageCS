using SageCS.Core;
using SageCS.Environment.Utility;
using System.IO;

namespace SageCS.Environment.Assets
{
    internal class LibraryMaps : MajorAsset
    {
        public string[] libraryMaps;

        public LibraryMaps() : base()
        {
            this.Default();
        }

        public LibraryMaps(BinaryReader br) : base(br)
        {
            this.libraryMaps = new string[br.ReadInt32()];
            for (int index = 0; index < this.libraryMaps.Length; ++index)
                this.libraryMaps[index] = IOUtility.ReadString(br);
        }

        protected override void SaveData(BinaryWriter bw)
        {
            bw.Write(this.libraryMaps.Length);
            foreach (string s in this.libraryMaps)
                IOUtility.WriteString(bw, s);
        }

        public override void Register(Map assetContainer)
        {
            this.RegisterSelf(assetContainer);
        }

        public override void Default()
        {
            this.libraryMaps = new string[0];
        }
    }
}
