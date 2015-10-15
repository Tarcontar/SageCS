using System;
using System.IO;
using System.Text;

namespace SageCS.Environment.Utility
{
    class IOUtility
    {
        public static string ReadString(BinaryReader br)
        {
            ushort num = br.ReadUInt16();
            return Encoding.ASCII.GetString(br.ReadBytes((int)num));
        }

        public static string ReadStringUnicode(BinaryReader br)
        {
            ushort num = br.ReadUInt16();
            return Encoding.Unicode.GetString(br.ReadBytes((int)num * 2));
        }

        public static void WriteString(BinaryWriter bw, string s)
        {
            bw.Write((ushort)s.Length);
            bw.Write(Encoding.ASCII.GetBytes(s));
        }

        public static void WriteStringUnicode(BinaryWriter bw, string s)
        {
            bw.Write((ushort)s.Length);
            bw.Write(Encoding.Unicode.GetBytes(s));
        }


        public static T[,] ReadArray<T>(BinaryReader br, int width, int height) where T : struct
        {
            T[,] objArray = new T[width, height];
            Type type = typeof(T);
            if (type == typeof(bool))
            {
                byte num = (byte)0;
                for (int index1 = 0; index1 < height; ++index1)
                {
                    for (int index2 = 0; index2 < width; ++index2)
                    {
                        if (index2 % 8 == 0)
                            num = br.ReadByte();
                        objArray[index2, index1] = (T)(ValueType)((num & 1 << index2 % 8) > 0 ? true : false);
                    }
                }
            }
            else
            {
                for (int index1 = 0; index1 < height; ++index1)
                {
                    for (int index2 = 0; index2 < width; ++index2)
                    {
                        if (type == typeof(int))
                            objArray[index2, index1] = (T)(ValueType)br.ReadInt32();
                        else if (type == typeof(short))
                            objArray[index2, index1] = (T)(ValueType)br.ReadInt16();
                        else if (type == typeof(ushort))
                        {
                            objArray[index2, index1] = (T)(ValueType)br.ReadUInt16();
                        }
                        else
                        {
                            if (type != typeof(byte))
                                throw new Exception(string.Format("Type: {0} is not supported for method ReadArray", (object)type.Name));
                            objArray[index2, index1] = (T)(ValueType)br.ReadByte();
                        }
                    }
                }
            }
            return objArray;
        }

        public static void WriteArray<T>(BinaryWriter bw, T[,] array) where T : struct
        {
            int length1 = array.GetLength(0);
            int length2 = array.GetLength(1);
            Type type = typeof(T);
            if (type == typeof(bool))
            {
                byte num = (byte)0;
                for (int index1 = 0; index1 < length2; ++index1)
                {
                    int index2;
                    for (index2 = 0; index2 < length1; ++index2)
                    {
                        num |= (byte)(((bool)(ValueType)array[index2, index1] ? 1 : 0) << index2 % 8);
                        if (index2 % 8 == 7)
                        {
                            bw.Write(num);
                            num = (byte)0;
                        }
                    }
                    if ((index2 - 1) % 8 != 7)
                        bw.Write(num);
                }
            }
            else
            {
                for (int index1 = 0; index1 < length2; ++index1)
                {
                    for (int index2 = 0; index2 < length1; ++index2)
                    {
                        if (type == typeof(int))
                            bw.Write((int)(ValueType)array[index2, index1]);
                        else if (type == typeof(short))
                            bw.Write((short)(ValueType)array[index2, index1]);
                        else if (type == typeof(ushort))
                        {
                            bw.Write((ushort)(ValueType)array[index2, index1]);
                        }
                        else
                        {
                            if (type != typeof(byte))
                                throw new Exception(string.Format("Type: {0} is not supported for method WriteArray", (object)type.Name));
                            bw.Write((byte)(ValueType)array[index2, index1]);
                        }
                    }
                }
            }
        }

        public static float FromSageFloat16(ushort v)
        {
            return (float)((double)(byte)((uint)v >> 8) * 10.0 + (double)(byte)((uint)v & (uint)byte.MaxValue) * 9.96000003814697 / 256.0);
        }

        public static ushort ToSageFloat16(float v)
        {
            return (ushort)((uint)(byte)(((double)v - (double)v % 10.0) / 10.0) << 8 | (uint)(byte)((double)v % 10.0 * 256.0 / 9.96));
        }

        private static int GetUncompressedSize(BinaryReader br)
        {
            int num1 = 0;
            ushort num2 = (ushort)(((uint)br.ReadByte() << 8) + (uint)br.ReadByte());
            if (((int)num2 & 16127) == 4347)
            {
                int num3 = (int)num2 & 32768;
                if (num3 > 0)
                    num3 = 1;
                int num4 = num3 + 3;
                for (int index = 0; index < num4; ++index)
                    num1 = (num1 << 8) + (int)br.ReadByte();
            }
            return num1;
        }

        public static void DecompressData(BinaryReader input, BinaryWriter output)
        {
            int uncompressedSize = GetUncompressedSize(input);
            long num1 = 0L;
            Console.Write("*\t decompressing... ");
            byte num2;
            while (true)
            {
                long position1 = 0;
                long newPos1 = 0;
                int repeatAvailable1 = 0;
                int count1 = 0;
                do
                {
                    if (output.BaseStream.Length * 100L / (long)uncompressedSize > num1)
                    {
                        num1 = output.BaseStream.Length * 100L / (long)uncompressedSize;
                    }
                    num2 = input.ReadByte();
                    if (((int)num2 & 128) == 0)
                    {
                        byte num3 = input.ReadByte();
                        int count2 = (int)num2 & 3;
                        output.Write(input.ReadBytes(count2));
                        long position2 = output.BaseStream.Position;
                        long newPos2 = position2 - 1L - (long)((int)num3 + ((int)num2 & 96) * 8);
                        int repeatAvailable2 = (int)(position2 - newPos2);
                        int count3 = ((int)num2 & 28) / 4 + 3;
                        output.BaseStream.Seek((long)-repeatAvailable2, SeekOrigin.Current);
                        byte[] buffer = new BinaryReader(output.BaseStream).ReadBytes(count3);
                        output.BaseStream.Seek(0L, SeekOrigin.End);
                        output.Write(buffer);
                        if (count3 > repeatAvailable2)
                            CopyRepeat(input, output, position2, newPos2, count3, repeatAvailable2);
                    }
                    else if (((int)num2 & 64) == 0)
                    {
                        byte num3 = input.ReadByte();
                        byte num4 = input.ReadByte();
                        int count2 = (int)num3 >> 6;
                        output.Write(input.ReadBytes(count2));
                        long position2 = output.BaseStream.Position;
                        long newPos2 = position2 - 1L - (long)((((int)num3 & 63) << 8) + (int)num4);
                        int repeatAvailable2 = (int)(position2 - newPos2);
                        int count3 = ((int)num2 & 63) + 4;
                        output.BaseStream.Seek((long)-repeatAvailable2, SeekOrigin.Current);
                        byte[] buffer = new BinaryReader(output.BaseStream).ReadBytes(count3);
                        output.BaseStream.Seek(0L, SeekOrigin.End);
                        output.Write(buffer);
                        if (count3 > repeatAvailable2)
                            CopyRepeat(input, output, position2, newPos2, count3, repeatAvailable2);
                    }
                    else if (((int)num2 & 32) == 0)
                    {
                        byte num3 = input.ReadByte();
                        byte num4 = input.ReadByte();
                        byte num5 = input.ReadByte();
                        int count2 = (int)num2 & 3;
                        output.Write(input.ReadBytes(count2));
                        position1 = output.BaseStream.Position;
                        newPos1 = position1 - 1L - (long)((((int)num2 & 16) >> 4 << 16) + ((int)num3 << 8) + (int)num4);
                        repeatAvailable1 = (int)(position1 - newPos1);
                        count1 = (((int)num2 & 12) >> 2 << 8) + (int)num5 + 5;
                        output.BaseStream.Seek((long)-repeatAvailable1, SeekOrigin.Current);
                        byte[] buffer = new BinaryReader(output.BaseStream).ReadBytes(count1);
                        output.BaseStream.Seek(0L, SeekOrigin.End);
                        output.Write(buffer);
                    }
                    else
                        goto label_12;
                }
                while (count1 <= repeatAvailable1);
                CopyRepeat(input, output, position1, newPos1, count1, repeatAvailable1);
                continue;
            label_12:
                int count4 = ((int)num2 & 31) * 4 + 4;
                if (count4 <= 112)
                    output.Write(input.ReadBytes(count4));
                else
                    break;
            }
            int count = (int)num2 & 3;
            output.Write(input.ReadBytes(count));
            output.BaseStream.Seek(0L, SeekOrigin.Begin);
            Console.WriteLine("\r\n*\t done decompressing, total size = {0}KB", (object)(output.BaseStream.Length / 1024L));
        }

        private static void CopyRepeat(BinaryReader input, BinaryWriter output, long oldPos, long newPos, int count, int repeatAvailable)
        {
            int num1 = count - repeatAvailable;
            int num2 = 0;
            while (num2 < num1)
            {
                output.BaseStream.Seek(oldPos, SeekOrigin.Begin);
                byte[] buffer = new BinaryReader(output.BaseStream).ReadBytes(num1 - num2);
                oldPos += (long)buffer.Length;
                num2 += buffer.Length;
                output.BaseStream.Seek(0L, SeekOrigin.End);
                output.Write(buffer);
            }
        }
    }
}

