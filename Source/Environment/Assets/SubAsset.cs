

using SageCS.Core;
using System;
using System.IO;

namespace SageCS.Environment.Assets
{
    abstract class SubAsset : IAsset
    {
        protected string name;
        protected int id;
        protected SubAsset.SubAssetType subAssetType;
        protected Type type;
        public static string[] assetNames;
        public static Map map;

        public string Name
        {
            get
            {
                return this.name;
            }
        }

        public int ID
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }

        public bool IsEmpty()
        {
            return string.IsNullOrEmpty(this.name);
        }

        public abstract void Clear();

        public static string PeekSubAssetName(BinaryReader br)
        {
            int num = br.ReadInt32();
            br.BaseStream.Position -= 4L;
            return SubAsset.assetNames[(num >> 8) - 1];
        }

        public static byte[] ReadAsBytes(BinaryReader br, string name)
        {
            int num1 = -1;
            if (br == null)
                return (byte[])null;
            int num2 = br.ReadInt32();
            SubAssetType subAssetType = (SubAssetType)(num2 & (int)byte.MaxValue);
            num1 = num2 >> 8;
            Console.WriteLine("!\t Skipping not implemented SubAsset: \"{0}\" of type: {1}", (object)name, (object)subAssetType);
            switch (subAssetType)
            {
                case SubAssetType.boolType:
                    return br.ReadBytes(1);
                case SubAssetType.intType:
                    return br.ReadBytes(4);
                case SubAssetType.floatType:
                    return br.ReadBytes(4);
                case SubAssetType.stringType:
                    return br.ReadBytes((int)br.ReadUInt16());
                case SubAssetType.stringUnicodeType:
                    return br.ReadBytes((int)br.ReadUInt16() * 2);
                case SubAssetType.stringNameValueType:
                    return br.ReadBytes((int)br.ReadUInt16());
                default:
                    throw new Exception("unknown sub-asset type: " + (object)subAssetType);
            }
        }

        public void Register(Map assetContainer)
        {
            if (this.IsEmpty())
                return;
            if (assetContainer.nameIDs.ContainsKey(this.name))
                this.id = assetContainer.nameIDs[this.name];
            else
                assetContainer.nameIDs.Add(this.name, assetContainer.nameIDs.Count + 1);
        }

        protected SubAssetType GetType(Type t)
        {
            if (t == typeof(bool))
                return SubAssetType.boolType;
            if (t == typeof(int))
                return SubAssetType.intType;
            if (t == typeof(float))
                return SubAssetType.floatType;
            if (t == typeof(string))
                return this.name == "playerDisplayName" ? SubAssetType.stringUnicodeType : SubAssetType.stringType;
            if (t == typeof(string[]))
                return SubAssetType.stringNameValueType;
            return t.BaseType == typeof(Enum) ? SubAssetType.intType : SubAssetType.unknownType;
        }

        public abstract void Save(BinaryWriter bw);

        public abstract void Default();

        public enum SubAssetType : byte
        {
            boolType = (byte)0,
            intType = (byte)1,
            floatType = (byte)2,
            stringType = (byte)3,
            stringUnicodeType = (byte)4,
            stringNameValueType = (byte)5,
            unknownType = (byte)255,
        }
    }
}
