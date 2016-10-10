using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PakBuilder
{
    public class PakEntry
    {
        public UInt32 ExtensionCrc { get; set; }
        public UInt32 Offset { get; set; }
        public UInt32 Length { get; set; }
        public UInt32 EmbeddedNameCrc { get; set; }
        public UInt32 FullNameCrc { get; set; }
        public UInt32 ShortNameCrc { get; set; }
        public UInt32 Unk01 { get; set; } // Keeping just for safety
        public UInt32 Flags { get; set; }

        public string EmbeddedName { get; set; }


        public static PakEntry Deserialize(BinaryReader br)
        {
            PakEntry result = new PakEntry();
            byte[] temp = br.ReadBytes(0x20);

            // As all the fields are big endian, reverse the entire header at once
            // then read backwards
            Array.Reverse(temp);
            result.ExtensionCrc = BitConverter.ToUInt32(temp, 28);
            result.Offset = BitConverter.ToUInt32(temp, 24);
            result.Length = BitConverter.ToUInt32(temp, 20);
            result.EmbeddedNameCrc = BitConverter.ToUInt32(temp, 16);
            result.FullNameCrc = BitConverter.ToUInt32(temp, 12);
            result.ShortNameCrc = BitConverter.ToUInt32(temp, 8);
            result.Unk01 = BitConverter.ToUInt32(temp, 4);
            result.Flags = BitConverter.ToUInt32(temp, 0);

            if ((result.Flags & 0x20) != 0)
            {
                temp = br.ReadBytes(0xA0);
                result.EmbeddedName = Encoding.ASCII.GetString(temp).TrimEnd();
            }

            return result;
        }
    }
}
