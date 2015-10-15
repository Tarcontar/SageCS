using OpenTK;
using SageCS.Environment.Utility;
using System;
using System.IO;

namespace SageCS.Environment.Assets
{
    internal class TriggerArea
    {
        public string name;
        public int id;
        public Vector2[] points;

        public TriggerArea(BinaryReader br)
        {
            this.name = IOUtility.ReadString(br);
            if ((int)br.ReadInt16() != 0)
                Console.WriteLine("!\t Asset: TriggerArea expected short 0");
            this.id = br.ReadInt32();
            this.points = new Vector2[br.ReadInt32()];
            for (int index = 0; index < this.points.Length; ++index)
                this.points[index] = new Vector2(br.ReadSingle(), br.ReadSingle());
            if (br.ReadInt32() == 0)
                return;
            Console.WriteLine("!\t Asset: TriggerArea expected int 0");
        }

        public void Save(BinaryWriter bw)
        {
            IOUtility.WriteString(bw, this.name);
            bw.Write((short)0);
            bw.Write(this.id);
            bw.Write(this.points.Length);
            foreach (Vector2 vec2 in this.points)
            {
                bw.Write(vec2[0]);
                bw.Write(vec2[1]);
            }
            bw.Write(0);
        }
    }
}
