using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DamienG.Security.Cryptography;

namespace PakBuilder
{
    public class PakFile : IDisposable
    {
        private readonly FileStream _FileStream;

        public Dictionary<UInt32, PakEntry> Entries { get; }

        public static PakFile Open(string PakName)
        {
            PakFile result = new PakFile(PakName);
            UInt32 lastKey = Crc32.Compute(Encoding.ASCII.GetBytes(".last"));
            
            // Intentionally not using "using" so the file stream won't be disposed
            var br = new BinaryReader(result._FileStream);

            PakEntry entry;
            do
            {
                entry = PakEntry.Deserialize(br);
                result.Entries.Add(entry.FullNameCrc, entry);
            }
            while (entry.ExtensionCrc != lastKey);

            return result;
        }

        private PakFile(string filename)
        {
            Entries = new Dictionary<uint, PakEntry>();
            _FileStream = File.Open(filename, FileMode.OpenOrCreate, FileAccess.ReadWrite);
        }

        public void Dispose() {
            ((IDisposable) _FileStream).Dispose();
        }
    }
}
