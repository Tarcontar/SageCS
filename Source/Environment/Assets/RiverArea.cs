using OpenTK;
using SageCS.Environment.Utility;
using System;
using System.IO;

namespace SageCS.Environment.Assets
{
    class RiverArea
    {
        public int id;
        public string name;
        public string riverTexture;
        public string normalMap;
        public string lowLODNoiseTexture;
        public string lowLODSparkleTexture;
        public uint color;
        public float alpha;
        public bool additiveBlending;
        public int waterHeight;
        public float uvScrollSpeed;
        public string riverType;
        public string minimumWaterLOD;
        public Vector2[] points;

        public RiverArea(BinaryReader br)
        {
            this.id = br.ReadInt32();
            this.name = IOUtility.ReadString(br);
            if ((int)br.ReadInt16() != 0)
                Console.WriteLine("!\t Asset: RiverArea expected short 0");
            this.uvScrollSpeed = br.ReadSingle();
            this.additiveBlending = br.ReadBoolean();
            this.riverTexture = IOUtility.ReadString(br);
            this.normalMap = IOUtility.ReadString(br);
            this.lowLODNoiseTexture = IOUtility.ReadString(br);
            this.lowLODSparkleTexture = IOUtility.ReadString(br);
            this.color = br.ReadUInt32();
            this.alpha = br.ReadSingle() * (float)byte.MaxValue;
            this.waterHeight = br.ReadInt32();
            this.riverType = IOUtility.ReadString(br);
            this.minimumWaterLOD = IOUtility.ReadString(br);
            this.points = new Vector2[br.ReadInt32() * 2];
            for (int index = 0; index < this.points.Length; ++index)
                this.points[index] = new Vector2(br.ReadSingle(), br.ReadSingle());
        }

        public RiverArea(int Id, int WaterHeight, Vector2[] Points)
        {
            this.additiveBlending = false;
            this.alpha = (float)byte.MaxValue;
            this.color = 16777215U;
            this.id = Id;
            this.lowLODNoiseTexture = "Noise0000";
            this.lowLODSparkleTexture = "WaterSurfaceBubbles";
            this.minimumWaterLOD = "";
            this.name = "New River";
            this.normalMap = "FXRiverA_Nrm";
            this.points = Points;
            this.riverTexture = "FXRiverA_Diffuse";
            this.riverType = "Standard River";
            this.uvScrollSpeed = 0.06f;
            this.waterHeight = WaterHeight;
        }

        public void Save(BinaryWriter bw)
        {
            bw.Write(this.id);
            IOUtility.WriteString(bw, this.name);
            bw.Write((short)0);
            bw.Write(this.uvScrollSpeed);
            bw.Write(this.additiveBlending);
            IOUtility.WriteString(bw, this.riverTexture);
            IOUtility.WriteString(bw, this.normalMap);
            IOUtility.WriteString(bw, this.lowLODNoiseTexture);
            IOUtility.WriteString(bw, this.lowLODSparkleTexture);
            bw.Write(this.color);
            bw.Write(this.alpha / (float)byte.MaxValue);
            bw.Write(this.waterHeight);
            IOUtility.WriteString(bw, this.riverType);
            IOUtility.WriteString(bw, this.minimumWaterLOD);
            bw.Write(this.points.Length / 2);
            foreach (Vector2 vec2 in this.points)
            {
                bw.Write(vec2[0]);
                bw.Write(vec2[1]);
            }
        }
    }
}
