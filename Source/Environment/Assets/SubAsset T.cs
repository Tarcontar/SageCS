
using SageCS.Environment.Utility;
using System;
using System.Collections.Generic;
using System.IO;

namespace SageCS.Environment.Assets
{
    class SubAsset<T> : SubAsset
    {
        public T data;

        public SubAsset(string name, T data, Dictionary<string, SubAsset> owner)
        {
            this.data = data;
            this.name = name;
            this.id = 0;
            this.type = typeof(T);
            if (this.type == typeof(bool))
                this.subAssetType = SubAssetType.boolType;
            else if (this.type == typeof(int))
                this.subAssetType = SubAssetType.intType;
            else if (this.type == typeof(float))
                this.subAssetType = SubAssetType.floatType;
            else if (this.type == typeof(string))
            {
                if (name == "playerDisplayName")
                    this.subAssetType = SubAssetType.stringUnicodeType;
                else
                    this.subAssetType = SubAssetType.stringType;
            }
            else if (this.type == typeof(string[]))
            {
                this.subAssetType = SubAssetType.stringNameValueType;
            }
            else
            {
                if (this.type.BaseType != typeof(Enum))
                    throw new Exception("unknown sub-asset type: " + this.type.Name);
                this.subAssetType = SubAssetType.intType;
            }
            SubAsset.map.Register((IAsset)this);
            owner.Add(name, (SubAsset)this);
        }

        public SubAsset(BinaryReader br, string name, Dictionary<string, SubAsset> owner)
        {
            this.type = typeof(T);
            this.name = name;
            this.data = default(T);
            this.subAssetType = SubAssetType.unknownType;
            this.id = -1;
            if (br == null)
                return;
            this.id = br.ReadInt32();
            this.subAssetType = (SubAssetType)(this.id & (int)byte.MaxValue);
            this.id >>= 8;
            if (this.subAssetType != this.GetType(this.type) && (this.type != typeof(string) || this.subAssetType != SubAssetType.stringType && this.subAssetType != SubAssetType.stringUnicodeType && this.subAssetType != SubAssetType.stringNameValueType))
            {
                Console.WriteLine("!\t Sub-asset: {2} got wrong type, should be {0}, instead of {1}", (object)this.subAssetType, (object)this.GetType(this.type), (object)name);
                throw new Exception(string.Format("!\t Sub-asset: {2} got wrong type, should be {0}, instead of {1}", (object)this.subAssetType, (object)this.GetType(this.type), (object)name));
            }
            switch (this.subAssetType)
            {
                case SubAssetType.boolType:
                    this.data = (T)Convert.ChangeType((object)(br.ReadBoolean() ? true : false), this.type);
                    break;
                case SubAssetType.intType:
                    this.data = (T)Convert.ChangeType((object)br.ReadInt32(), this.type);
                    break;
                case SubAssetType.floatType:
                    this.data = (T)Convert.ChangeType((object)br.ReadSingle(), this.type);
                    break;
                case SubAssetType.stringType:
                    this.data = (T)Convert.ChangeType((object)IOUtility.ReadString(br), this.type);
                    break;
                case SubAssetType.stringUnicodeType:
                    this.data = (T)Convert.ChangeType((object)IOUtility.ReadStringUnicode(br), this.type);
                    break;
                case SubAssetType.stringNameValueType:
                    this.data = (T)Convert.ChangeType((object)IOUtility.ReadString(br), this.type);
                    break;
                default:
                    throw new Exception("unknown sub-asset type: " + (object)this.subAssetType);
            }
            if (name != assetNames[this.id - 1])
                Console.WriteLine("!\t Sub-asset got wrong name, should be {0}, instead of {1}", (object)assetNames[this.id - 1], (object)name);
            map.Register((IAsset)this);
            owner.Add(name, (SubAsset)this);
        }

        public override void Save(BinaryWriter bw)
        {
            if (this.IsEmpty() | this.id < 1)
                return;
            bw.Write((int)(this.subAssetType + (byte)(this.id << 8)));
            switch (this.subAssetType)
            {
                case SubAssetType.boolType:
                    bw.Write((bool)Convert.ChangeType((object)this.data, typeof(bool)));
                    break;
                case SubAssetType.intType:
                    bw.Write((int)Convert.ChangeType((object)this.data, typeof(int)));
                    break;
                case SubAssetType.floatType:
                    bw.Write((float)Convert.ChangeType((object)this.data, typeof(float)));
                    break;
                case SubAssetType.stringType:
                    IOUtility.WriteString(bw, (string)Convert.ChangeType((object)this.data, typeof(string)));
                    break;
                case SubAssetType.stringUnicodeType:
                    IOUtility.WriteStringUnicode(bw, (string)Convert.ChangeType((object)this.data, typeof(string)));
                    break;
                case SubAssetType.stringNameValueType:
                    IOUtility.WriteString(bw, (string)(object)this.data);
                    break;
                default:
                    throw new Exception("unknown sub-asset type: " + (object)this.subAssetType);
            }
        }

        public override void Default()
        {
            this.name = (string)null;
            this.data = default(T);
            this.id = -1;
            this.subAssetType = SubAssetType.unknownType;
        }

        public override void Clear()
        {
            this.name = (string)null;
            this.id = -1;
            this.data = default(T);
            this.subAssetType = SubAsset.SubAssetType.unknownType;
        }
    }
}
