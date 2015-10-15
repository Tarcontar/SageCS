using OpenTK;
using SageCS.Environment.Utility;
using System;
using System.IO;

namespace SageCS.Environment.Assets
{
    class StandingWaveArea
    {
        public int id;
        public string name;
        public string particleEffect;
        public float uvScrollSpeed;
        public bool additiveBlending;
        public Vector2[] points;

        public StandingWaveArea(BinaryReader br)
        {
            this.id = br.ReadInt32();
            this.name = IOUtility.ReadString(br);
            if ((int)br.ReadInt16() != 0)
                Console.WriteLine("!\t Asset: StandingWaveArea expected short 0");
            this.uvScrollSpeed = br.ReadSingle();
            this.additiveBlending = br.ReadBoolean();
            this.points = new Vector2[br.ReadInt32()];
            for (int index = 0; index < this.points.Length; ++index)
                this.points[index] = new Vector2(br.ReadSingle(), br.ReadSingle());
            if (br.ReadInt32() != 0)
                Console.WriteLine("!\t Asset: StandingWaveArea expected int 0");
            this.particleEffect = IOUtility.ReadString(br);
        }

        public void Save(BinaryWriter bw)
        {
            bw.Write(this.id);
            IOUtility.WriteString(bw, this.name);
            bw.Write((short)0);
            bw.Write(this.uvScrollSpeed);
            bw.Write(this.additiveBlending);
            bw.Write(this.points.Length);
            foreach (Vector2 vec2 in this.points)
            {
                bw.Write(vec2[0]);
                bw.Write(vec2[1]);
            }
            bw.Write(0);
            IOUtility.WriteString(bw, this.particleEffect);
        }
    }
}
