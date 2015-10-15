using SageCS.Environment.Utility;
using System.IO;

namespace SageCS.Environment.Assets
{
    class MissionHotSpot
    {
        public string id;
        public string title;
        public string description;

        public MissionHotSpot(BinaryReader br)
        {
            this.id = IOUtility.ReadString(br);
            this.title = IOUtility.ReadString(br);
            this.description = IOUtility.ReadString(br);
        }

        public void Save(BinaryWriter bw)
        {
            IOUtility.WriteString(bw, this.id);
            IOUtility.WriteString(bw, this.title);
            IOUtility.WriteString(bw, this.description);
        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2}", (object)this.id, (object)this.title, (object)this.description);
        }
    }
}
