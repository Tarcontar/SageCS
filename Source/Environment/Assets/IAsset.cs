using System.IO;

namespace SageCS.Environment.Assets
{
    interface IAsset
    {
        string Name { get; }

        int ID { get; set; }

        void Default();

        void Save(BinaryWriter bw);
    }
}
