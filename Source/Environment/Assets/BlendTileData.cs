

using SageCS.Core;
using SageCS.Environment.Utility;
using System;
using System.IO;

namespace SageCS.Environment.Assets
{
    class BlendTileData : MajorAsset
    {
        public int mapWidth;
        public int mapHeight;
        public ushort[,] tiles;
        private int blendsCount;
        public ushort[,] blends;
        public BlendInfo[] blendInfo;
        private ushort[,] singleEdgeBlends;
        private int cliffBlendsCount;
        private ushort[,] cliffBlends;
        public Passability[,] passability;
        private bool[,] passageWidth;
        public bool[,] visibility;
        private bool[,] buildability;
        private bool[,] tiberiumGrowability;
        private byte[,] dynamicShrubbery;
        private bool[,] taintability;
        private byte[,] flamability;
        private int textureCellCount;
        public Texture[] textures;
        private uint delim;

        public BlendTileData(int width, int height) : base()
        {
            this.mapHeight = height;
            this.mapWidth = width;
            this.Default();
        }

        public BlendTileData(BinaryReader br, int width, int height) : base(br)
        {
            this.mapWidth = width;
            this.mapHeight = height;
            if (br.ReadInt32() != this.mapWidth * this.mapHeight)
                Console.WriteLine("!\t BlendTileData area mismatch with mapWidth and mapHeight");
            this.tiles = IOUtility.ReadArray<ushort>(br, this.mapWidth, this.mapHeight);
            this.blends = IOUtility.ReadArray<ushort>(br, this.mapWidth, this.mapHeight);
            this.singleEdgeBlends = IOUtility.ReadArray<ushort>(br, this.mapWidth, this.mapHeight);
            this.cliffBlends = IOUtility.ReadArray<ushort>(br, this.mapWidth, this.mapHeight);
            for (int index1 = 0; index1 < this.mapHeight; ++index1)
            {
                for (int index2 = 0; index2 < this.mapWidth; ++index2)
                {
                    if ((int)this.cliffBlends[index2, index1] != 0)
                        Console.WriteLine("!\t BlendTileData.unknownMap[{0},{1}]={2}", (object)index2, (object)index1, (object)this.cliffBlends[index2, index1]);
                }
            }
            this.passability = new Passability[this.mapWidth, this.mapHeight];
            bool[,] flagArray1 = IOUtility.ReadArray<bool>(br, this.mapWidth, this.mapHeight);
            bool[,] flagArray2 = IOUtility.ReadArray<bool>(br, this.mapWidth, this.mapHeight);
            this.passageWidth = IOUtility.ReadArray<bool>(br, this.mapWidth, this.mapHeight);
            /*
            if (game == Game.CC3)
                this.taintability = IOUtility.ReadArray<bool>(br, this.mapWidth, this.mapHeight);
            */
            bool[,] flagArray3 = IOUtility.ReadArray<bool>(br, this.mapWidth, this.mapHeight);
            /*
            if (game == Game.CC3)
                this.flamability = IOUtility.ReadArray<byte>(br, this.mapWidth, this.mapHeight);
            */
            this.visibility = IOUtility.ReadArray<bool>(br, this.mapWidth, this.mapHeight);
            this.buildability = IOUtility.ReadArray<bool>(br, this.mapWidth, this.mapHeight);
            bool[,] flagArray4 = IOUtility.ReadArray<bool>(br, this.mapWidth, this.mapHeight);
            this.tiberiumGrowability = IOUtility.ReadArray<bool>(br, this.mapWidth, this.mapHeight);
            for (int index1 = 0; index1 < this.mapHeight; ++index1)
            {
                for (int index2 = 0; index2 < this.mapWidth; ++index2)
                    this.passability[index2, index1] = !flagArray1[index2, index1] ? (!flagArray2[index2, index1] ? (!flagArray4[index2, index1] ? (!flagArray3[index2, index1] ? Passability.Passable : Passability.ExtraPassable) : Passability.ImpassableToAirUnits) : Passability.ImpassableToPlayers) : Passability.Impassable;
            }
            /*
            if (game == Game.RA3)
                this.dynamicShrubbery = IOUtility.ReadArray<byte>(br, this.mapWidth, this.mapHeight);
            */
            this.textureCellCount = br.ReadInt32();
            this.blendsCount = br.ReadInt32() - 1;
            this.cliffBlendsCount = br.ReadInt32() - 1;
            if (this.cliffBlendsCount != 0)
                Console.WriteLine("!\t cliffBlendsCount={0}", (object)this.cliffBlendsCount);
            this.textures = new Texture[br.ReadInt32()];
            for (int index = 0; index < this.textures.Length; ++index)
                this.textures[index] = new Texture(br);
            this.delim = br.ReadUInt32();
            if (br.ReadInt32() != 0)
                Console.WriteLine("!\t expected 0 before blend info block");
            this.blendInfo = new BlendInfo[this.blendsCount];
            for (int index = 0; index < this.blendsCount; ++index)
                this.blendInfo[index] = new BlendInfo(br);
            if (this.cliffBlendsCount < 0)
            {
                Console.WriteLine("!\t Asset: BlendTileData.cliffBlendsCount = {0}, set to 0 as precaution, might be error in parsing", (object)this.cliffBlendsCount);
                this.cliffBlendsCount = 0;
            }
            byte[][] numArray = new byte[this.cliffBlendsCount][];
            for (int index = 0; index < this.cliffBlendsCount; ++index)
                numArray[index] = br.ReadBytes(38);
            this.CheckParsedSize(br);
        }

        protected override void SaveData(BinaryWriter bw)
        {
            bw.Write(this.mapHeight * this.mapWidth);
            IOUtility.WriteArray<ushort>(bw, this.tiles);
            IOUtility.WriteArray<ushort>(bw, this.blends);
            IOUtility.WriteArray<ushort>(bw, this.singleEdgeBlends);
            IOUtility.WriteArray<ushort>(bw, this.cliffBlends);
            bool[,] array1 = new bool[this.mapWidth, this.mapHeight];
            bool[,] array2 = new bool[this.mapWidth, this.mapHeight];
            bool[,] array3 = new bool[this.mapWidth, this.mapHeight];
            bool[,] array4 = new bool[this.mapWidth, this.mapHeight];
            for (int index1 = 0; index1 < this.mapHeight; ++index1)
            {
                for (int index2 = 0; index2 < this.mapWidth; ++index2)
                {
                    switch (this.passability[index2, index1])
                    {
                        case Passability.Impassable:
                            array1[index2, index1] = true;
                            break;
                        case Passability.ImpassableToPlayers:
                            array2[index2, index1] = true;
                            break;
                        case Passability.ImpassableToAirUnits:
                            array3[index2, index1] = true;
                            break;
                        case Passability.ExtraPassable:
                            array4[index2, index1] = true;
                            break;
                    }
                }
            }
            IOUtility.WriteArray<bool>(bw, array1);
            IOUtility.WriteArray<bool>(bw, array2);
            /*
            if (this.game == Game.RA3)
            {
                IOUtility.WriteArray<bool>(bw, array4);
                IOUtility.WriteArray<bool>(bw, this.passageWidth);
            }
            else if (this.game == Game.CC3)
            {
                IOUtility.WriteArray<bool>(bw, this.passageWidth);
                IOUtility.WriteArray<bool>(bw, this.taintability);
                IOUtility.WriteArray<bool>(bw, array4);
                IOUtility.WriteArray<byte>(bw, this.flamability);
            }
            */
            IOUtility.WriteArray<bool>(bw, this.visibility);
            IOUtility.WriteArray<bool>(bw, this.buildability);
            IOUtility.WriteArray<bool>(bw, array3);
            IOUtility.WriteArray<bool>(bw, this.tiberiumGrowability);
            /*
            if (this.game == Game.RA3)
                IOUtility.WriteArray<byte>(bw, this.dynamicShrubbery);
            */
            bw.Write(this.textureCellCount);
            bw.Write(this.blendsCount + 1);
            bw.Write(this.cliffBlendsCount + 1);
            bw.Write(this.textures.Length);
            foreach (Texture texture in this.textures)
                texture.Save(bw);
            bw.Write(this.delim);
            bw.Write(0);
            for (int index = 0; index < this.blendsCount; ++index)
                this.blendInfo[index].Save(bw);
        }

        public override void Default()
        {
            this.textures = new Texture[0];
            this.textureCellCount = 0;
            /*
            if (this.game == Game.RA3)
                this.AddTexture("RA3Grid1");
            else if (this.game == Game.CC3)
                this.AddTexture("CnC3Default");
            */
            this.tiles = new ushort[this.mapWidth, this.mapHeight];
            this.blends = new ushort[this.mapWidth, this.mapHeight];
            this.singleEdgeBlends = new ushort[this.mapWidth, this.mapHeight];
            this.cliffBlends = new ushort[this.mapWidth, this.mapHeight];
            this.passability = new Passability[this.mapWidth, this.mapHeight];
            this.visibility = new bool[this.mapWidth, this.mapHeight];
            this.passageWidth = new bool[this.mapWidth, this.mapHeight];
            this.buildability = new bool[this.mapWidth, this.mapHeight];
            this.tiberiumGrowability = new bool[this.mapWidth, this.mapHeight];
            /*
            if (this.game == Game.RA3)
                this.dynamicShrubbery = new byte[this.mapWidth, this.mapHeight];
            else if (this.game == Game.CC3)
            {
                this.taintability = new bool[this.mapWidth, this.mapHeight];
                this.flamability = new byte[this.mapWidth, this.mapHeight];
            }
            else
                OutputConsole.WriteLine("!\t Asset: BlendTileData does not know how to create default data for Game." + (object)this.game);
            */
            for (int y = 0; y < this.mapHeight; ++y)
            {
                for (int x = 0; x < this.mapWidth; ++x)
                {
                    this.visibility[x, y] = true;
                    this.tiles[x, y] = this.GetTile(x, y, 0);
                }
            }
            this.blendsCount = 0;
            this.cliffBlendsCount = 0;
            this.delim = 3452816845U;
            this.blendInfo = new BlendInfo[0];
        }

        public ushort GetTile(int x, int y, int texture)
        {
            int num = y % 8 / 2 * 16 + y % 2 * 2;
            return (ushort)(x % 8 / 2 * 4 + x % 2 + num + 64 * texture);
        }

        public override void Register(Map assetContainer)
        {
            this.RegisterSelf(assetContainer);
        }
    }
}
