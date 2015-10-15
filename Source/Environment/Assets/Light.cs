using OpenTK;
using System;
using System.IO;

namespace SageCS.Environment.Assets
{
    internal struct Light
    {
        private float pitch;
        private float yaw;
        private Vector3 color;

        public Light(float Pitch, float Yaw, Vector3 Color)
        {
            this.pitch = Pitch;
            this.yaw = Yaw;
            this.color = Color;
        }

        public Light(BinaryReader br)
        {
            this.color = new Vector3(br.ReadSingle(), br.ReadSingle(), br.ReadSingle());
            Light.VectorToAngles(new Vector3(br.ReadSingle(), br.ReadSingle(), br.ReadSingle()), out this.yaw, out this.pitch);
        }

        private static float ToDeg(double rad)
        {
            return (float)(rad * 180.0 / Math.PI);
        }

        private static float ToRad(double deg)
        {
            return (float)(deg * Math.PI / 180.0);
        }

        private static Vector3 AnglesToVector(float yaw, float pitch)
        {
            double num1 = Math.Sin((double)Light.ToRad((double)pitch));
            double num2 = Math.Cos((double)Light.ToRad((double)pitch));
            return new Vector3((float)(num2 * Math.Cos((double)Light.ToRad((double)yaw))), (float)(num2 * Math.Sin((double)Light.ToRad((double)yaw))), (float)num1);
        }

        private static void VectorToAngles(Vector3 v, out float yaw, out float pitch)
        {
            yaw = Light.ToDeg(Math.Atan2((double)v.Y, (double)v.X));
            pitch = Light.ToDeg(Math.Atan2((double)v.Z, Math.Sqrt((double)v.X * (double)v.X + (double)v.Y * (double)v.Y)));
        }

        public void Save(BinaryWriter bw)
        {
            bw.Write(this.color.X);
            bw.Write(this.color.Y);
            bw.Write(this.color.Z);
            Vector3 v = Light.AnglesToVector(this.yaw, this.pitch);
            bw.Write(v.X);
            bw.Write(v.Y);
            bw.Write(v.Z);
        }
    }
}
