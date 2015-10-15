using SageCS.Core;
using System;
using System.Collections.Generic;
using System.IO;

namespace SageCS.Environment.Assets
{
    internal class LibraryMapLists : MajorAsset
    {
        public LibraryMaps[] libraryMaps;

        public LibraryMapLists() : base()
        {
            this.Default();
        }

        public LibraryMapLists(BinaryReader br) : base(br)
        {
            List<LibraryMaps> list = new List<LibraryMaps>();
            while (br.BaseStream.Position - this.dataPos < (long)this.dataSize)
                list.Add(new LibraryMaps(br));
            this.libraryMaps = list.ToArray();
            this.CheckParsedSize(br);
        }

        protected override void SaveData(BinaryWriter bw)
        {
            foreach (Asset asset in this.libraryMaps)
                asset.Save(bw);
        }

        public override void Register(Map assetContainer)
        {
            this.RegisterSelf(assetContainer);
        }

        public override void Default()
        {
            this.libraryMaps = new LibraryMaps[11];
            for (int index = 0; index < this.libraryMaps.Length; ++index)
                this.libraryMaps[index] = new LibraryMaps();
        }

        public void AddLibraryMaps()
        {
            Array.Resize<LibraryMaps>(ref this.libraryMaps, this.libraryMaps.Length + 1);
            this.libraryMaps[this.libraryMaps.Length - 1] = new LibraryMaps();
        }
    }
}
