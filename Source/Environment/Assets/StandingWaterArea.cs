using OpenTK;
using SageCS.Environment.Utility;
using System;
using System.IO;

namespace SageCS.Environment.Assets
{
    class StandingWaterArea
    {
        public int id;
        public string name;
        public float UVScrollSpeed;
        public bool additiveBlending;
        public string bumpmapTexture;
        public string skyTexture;
        public Vector2[] points;
        public int waterHeight;
        public string fxShader;
        public string depthColors;

        public StandingWaterArea(int height, params Vector2[] Points)
        {
            this.id = 1;
            this.name = "";
            this.UVScrollSpeed = 0.06f;
            this.additiveBlending = false;
            this.bumpmapTexture = "WaterRippleBump";
            this.skyTexture = "SkyEnv";
            this.points = Points;
            this.waterHeight = height;
            /*
            if (Game.CC3 == game)
            {
                this.fxShader = "CNC3_OceanA";
            }
            else
            {
                if (game != Game.RA3)
                    throw new NotImplementedException("StandingWaterArea fxShader unknown for " + (object)game);
                this.fxShader = "FXOceanRA3";
            }
            */
            this.depthColors = "LUTDepthTint.tga";
        }

        public StandingWaterArea(BinaryReader br)
        {
            this.id = br.ReadInt32();
            this.name = IOUtility.ReadString(br);
            int num = (int)br.ReadInt16();
            this.UVScrollSpeed = br.ReadSingle();
            this.additiveBlending = br.ReadBoolean();
            this.bumpmapTexture = IOUtility.ReadString(br);
            this.skyTexture = IOUtility.ReadString(br);
            this.points = new Vector2[br.ReadInt32()];
            for (int index = 0; index < this.points.Length; ++index)
                this.points[index] = new Vector2(br.ReadSingle(), br.ReadSingle());
            this.waterHeight = br.ReadInt32();
            this.fxShader = IOUtility.ReadString(br);
            this.depthColors = IOUtility.ReadString(br);
        }

        public void Save(BinaryWriter bw)
        {
            bw.Write(this.id);
            IOUtility.WriteString(bw, this.name);
            bw.Write((short)0);
            bw.Write(this.UVScrollSpeed);
            bw.Write(this.additiveBlending);
            IOUtility.WriteString(bw, this.bumpmapTexture);
            IOUtility.WriteString(bw, this.skyTexture);
            bw.Write(this.points.Length);
            foreach (Vector2 vec2 in this.points)
            {
                bw.Write(vec2[0]);
                bw.Write(vec2[1]);
            }
            bw.Write(this.waterHeight);
            IOUtility.WriteString(bw, this.fxShader);
            IOUtility.WriteString(bw, this.depthColors);
        }

        internal int CalculateDataSize()
        {
            throw new NotImplementedException();
        }
    }
}
