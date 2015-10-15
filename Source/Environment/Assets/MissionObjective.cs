using SageCS.Environment.Utility;
using System.IO;

namespace SageCS.Environment.Assets
{
    internal class MissionObjective
    {
        public string id;
        public string text;
        public string description;
        public bool bonusObjective;
        public MissionObjective.ObjectiveType objectiveType;

        public MissionObjective(BinaryReader br)
        {
            this.id = IOUtility.ReadString(br);
            this.text = IOUtility.ReadString(br);
            this.description = IOUtility.ReadString(br);
            this.bonusObjective = br.ReadBoolean();
            this.objectiveType = (MissionObjective.ObjectiveType)br.ReadInt32();
        }

        public void Save(BinaryWriter bw)
        {
            IOUtility.WriteString(bw, this.id);
            IOUtility.WriteString(bw, this.text);
            IOUtility.WriteString(bw, this.description);
            bw.Write(this.bonusObjective);
            bw.Write((int)this.objectiveType);
        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3} {4}", (object)this.id, (object)this.text, (object)this.description, (object)(this.bonusObjective ? true : false), (object)this.objectiveType);
        }

        public enum ObjectiveType
        {
            Attack = 0,
            Build = 3,
            Capture = 4,
            Move = 5,
            Protect = 6,
        }
    }
}
