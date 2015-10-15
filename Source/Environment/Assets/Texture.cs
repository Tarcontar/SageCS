using SageCS.Environment.Utility;
using System;
using System.IO;

namespace SageCS.Environment.Assets
{
    struct Texture
    {
        public int cellStart;
        public int cellCount;
        public int unknown1;
        public int unknown2;
        public string name;

        public Texture(int start, string Name)
        {
            this.cellStart = start;
            this.cellCount = 16;
            this.unknown1 = 4;
            this.unknown2 = 0;
            this.name = Name;
        }

        public Texture(BinaryReader br)
        {
            this.cellStart = br.ReadInt32();
            this.cellCount = br.ReadInt32();
            this.unknown1 = br.ReadInt32();
            this.unknown2 = br.ReadInt32();
            this.name = IOUtility.ReadString(br);
            if (this.cellCount != 16)
                throw new Exception("cellCount!=16");
            if (this.unknown1 != 4)
                throw new Exception("unknown1!=4");
            if (this.unknown2 != 0)
                throw new Exception("unknown2!=0");
        }

        public void Save(BinaryWriter bw)
        {
            bw.Write(this.cellStart);
            bw.Write(this.cellCount);
            bw.Write(this.unknown1);
            bw.Write(this.unknown2);
            IOUtility.WriteString(bw, this.name);
        }
    }
}
