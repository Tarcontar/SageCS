
using OpenTK;
using SageCS.Core;
using SageCS.Environment.Utility;
using System;
using System.IO;

namespace SageCS.Environment.Assets
{
    class HeightMapData : MajorAsset
    {
        public float[,] elevations;
        private byte[] unknownBlock;
        public int mapWidth;
        public int mapHeight;
        public int borderWidth;
        public int playableWidth;
        public int playableHeight;
        public int area;

        public float this[int x, int y]
        {
            get
            {
                return this.elevations[x, y];
            }
            set
            {
                this.elevations[x, y] = value;
            }
        }

        public float this[Vector2 p]
        {
            get
            {
                return this.elevations[(int)p[0], (int)p[1]];
            }
            set
            {
                this.elevations[(int)p[0], (int)p[1]] = value;
            }
        }

        public HeightMapData(BinaryReader br) : base(br)
        {
            this.mapWidth = br.ReadInt32();
            if (this.mapWidth == 2)
            {
                Console.WriteLine("!\t read map width of 2, possibily start of unmarked GlobalVersion; skipping 6 bytes");
                br.BaseStream.Position += 6L;
                this.mapWidth = br.ReadInt32();
            }
            this.mapHeight = br.ReadInt32();
            if (this.mapWidth > 1000 || this.mapHeight > 1000)
                Console.WriteLine("!\t map size seems too big");
            else if (this.mapWidth < 50 || this.mapHeight < 50)
                Console.WriteLine("!\t map size seems too small");
            this.borderWidth = br.ReadInt32();
            int num = br.ReadInt32();
            this.unknownBlock = br.ReadBytes((num - 1) * 16 + 8);
            this.playableWidth = br.ReadInt32();
            this.playableHeight = br.ReadInt32();
            this.area = br.ReadInt32();
            if (this.mapHeight * this.mapWidth != this.area)
                Console.WriteLine("!\t read incorrect map size, expected {0}, read {1}", (object)(this.mapHeight * this.mapWidth), (object)this.area);
            else
                Console.WriteLine("map width = {0}, map height = {1}, border width = {2}, playable width = {3}, playable height = {4}, area = {5}", (object)this.mapWidth, (object)this.mapHeight, (object)this.borderWidth, (object)this.playableWidth, (object)this.playableHeight, (object)this.area);
            this.elevations = new float[this.mapWidth, this.mapHeight];
            for (int index1 = 0; index1 < this.mapHeight; ++index1)
            {
                for (int index2 = 0; index2 < this.mapWidth; ++index2)
                    this.elevations[index2, index1] = IOUtility.FromSageFloat16(br.ReadUInt16());
            }
            this.CheckParsedSize(br);
        }

        public HeightMapData(int width, int height, int border) : base()
        {
            this.mapWidth = width;
            this.mapHeight = height;
            this.borderWidth = border;
            this.playableHeight = height - 2 * border;
            this.playableWidth = width - 2 * border;
            this.area = width * height;
            this.Default();
        }

        protected override void SaveData(BinaryWriter bw)
        {
            bw.Write(this.mapWidth);
            bw.Write(this.mapHeight);
            bw.Write(this.borderWidth);
            bw.Write((this.unknownBlock.Length - 8) / 16 + 1);
            bw.Write(this.unknownBlock);
            bw.Write(this.playableWidth);
            bw.Write(this.playableHeight);
            bw.Write(this.area);
            for (int index1 = 0; index1 < this.mapHeight; ++index1)
            {
                for (int index2 = 0; index2 < this.mapWidth; ++index2)
                    bw.Write(IOUtility.ToSageFloat16(this.elevations[index2, index1]));
            }
        }

        public override void Register(Map assetContainer)
        {
            this.RegisterSelf(assetContainer);
        }

        public override void Default()
        {
            this.elevations = new float[this.mapWidth, this.mapHeight];
            this.unknownBlock = new byte[8];
        }
    }
}

