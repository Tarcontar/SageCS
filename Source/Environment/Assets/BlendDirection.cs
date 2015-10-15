using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageCS.Source.Environment.Assets
{
    [Flags]
    public enum BlendDirection : byte
    {
        BottomRight = (byte)40,
        Bottom = (byte)2,
        BottomLeft = (byte)36,
        Right = (byte)17,
        Left = (byte)1,
        TopRight = (byte)56,
        Top = (byte)18,
        TopLeft = (byte)52,
        ExceptBottomRight = (byte)20,
        ExceptBottomLeft = (byte)24,
        ExceptTopRight = (byte)4,
        ExceptTopLeft = (byte)8,
    }
}
