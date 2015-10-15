using SageCS.Source.Environment.Assets;
using System;
using System.IO;

namespace SageCS.Environment.Assets
{
    struct BlendInfo
    {
        public int secondaryTextureTile;
        private uint i3;
        private uint i4;
        public BlendDirection blendDirection;

        public BlendInfo(int secondaryTextureTile, BlendDirection blendDirection)
        {
            this.secondaryTextureTile = secondaryTextureTile;
            this.blendDirection = blendDirection;
            this.i3 = uint.MaxValue;
            this.i4 = 2061107200U;
        }

        public BlendInfo(BinaryReader br)
        {
            this = new BlendInfo();
            this.secondaryTextureTile = br.ReadInt32();
            this.blendDirection = this.ToBlendDirection(br.ReadBytes(6));
            this.i3 = br.ReadUInt32();
            if ((int)this.i3 != -1)
                Console.WriteLine("!\t Asset: BlendTileData.BlendInfo expected 0xffffffff for i3 instead of 0x{0:x8}", (object)this.i3);
            this.i4 = br.ReadUInt32();
            if ((int)this.i4 == 2061107200)
                return;
            Console.WriteLine("!\t Asset: BlendTileData.BlendInfo expected 0x7ada0000 for i4 instead of 0x{0:x8}", (object)this.i4);
        }

        public void Save(BinaryWriter bw)
        {
            bw.Write(this.secondaryTextureTile);
            bw.Write(this.FromBlendDirection(this.blendDirection));
            bw.Write(this.i3);
            bw.Write(this.i4);
        }

        public BlendDirection ToBlendDirection(byte[] bytes)
        {
            int num = 0;
            for (int index = 0; index < bytes.Length; ++index)
            {
                if ((int)bytes[index] == 1)
                    num |= 1 << index;
            }
            return (BlendDirection)num;
        }

        public byte[] FromBlendDirection(BlendDirection bd)
        {
            byte[] numArray = new byte[6];
            for (int index = 0; index < numArray.Length; ++index)
            {
                if ((bd & (BlendDirection)(1 << index)) != (BlendDirection)0)
                    numArray[index] = (byte)1;
            }
            return numArray;
        }

    }
}
