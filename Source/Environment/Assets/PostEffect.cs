using SageCS.Environment.Utility;
using System;
using System.IO;

namespace SageCS.Environment.Assets
{
    internal struct PostEffect
    {
        public string name;
        public PostEffect.Parameter[] parameters;

        public PostEffect(BinaryReader br)
        {
            this.name = IOUtility.ReadString(br);
            this.parameters = new PostEffect.Parameter[br.ReadInt32()];
            for (int index = 0; index < this.parameters.Length; ++index)
                this.parameters[index] = new PostEffect.Parameter(br);
        }

        public void Save(BinaryWriter bw)
        {
            IOUtility.WriteString(bw, this.name);
            bw.Write(this.parameters.Length);
            foreach (PostEffect.Parameter parameter in this.parameters)
                parameter.Save(bw);
        }

        public struct Parameter
        {
            private object data;
            private string name;
            private string type;

            public Parameter(string name, string type, object data)
            {
                this.name = name;
                this.type = type;
                this.data = data;
            }

            public Parameter(BinaryReader br)
            {
                this.data = (object)null;
                this.name = IOUtility.ReadString(br);
                this.type = IOUtility.ReadString(br);
                switch (this.type)
                {
                    case "Float":
                        this.data = (object)br.ReadSingle();
                        break;
                    case "Float4":
                        this.data = (object)new float[4];
                        for (int index = 0; index < 4; ++index)
                            ((float[])this.data)[index] = br.ReadSingle();
                        break;
                    case "Texture":
                        this.data = (object)IOUtility.ReadString(br);
                        break;
                    case "Int":
                        this.data = (object)br.ReadInt32();
                        Console.WriteLine("!\t PostEffect Parameter type Int unsure");
                        break;
                    default:
                        Console.WriteLine("!\t Sub-asset: Parameter does not implement type: {0}", (object)this.type);
                        break;
                }
            }

            public void Save(BinaryWriter bw)
            {
                IOUtility.WriteString(bw, this.name);
                IOUtility.WriteString(bw, this.type);
                switch (this.type)
                {
                    case "Float":
                        bw.Write((float)this.data);
                        break;
                    case "Float4":
                        for (int index = 0; index < 4; ++index)
                            bw.Write(((float[])this.data)[index]);
                        break;
                    case "Texture":
                        IOUtility.WriteString(bw, (string)this.data);
                        break;
                    default:
                        Console.WriteLine("!\t Sub-asset: Parameter does not implement type: {0}", (object)this.type);
                        break;
                }
            }
        }
    }
}
