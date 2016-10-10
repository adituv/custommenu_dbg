using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Crc32 = DamienG.Security.Cryptography.Crc32;

namespace PakBuilder
{
    static class Program
    {
        static int Main(string[] args)
        {
            Options options = new Options();

            for (int i = 0; i < args.Length - 1; )
            {
                if (args[i] == "-c")
                {
                    options.SourceFolderPath = args[i + 1];
                    i += 2;
                }
                else if (args[i] == "-o")
                {
                    options.DestPath = args[i + 1];
                    i += 2;
                }
                else if (args[i] == "-a")
                {
                    options.Update = true;
                    i++;
                }
                else
                {
                    Console.WriteLine("Unrecognised argument: \"{0}\"", args[i]);
                }
            }

            if (options.SourceFolderPath == null || options.DestPath == null)
            {
                Console.WriteLine("Usage: PakBuilder.exe -c SourceFolderPath -o DestPath [-a]");
                Console.WriteLine("\tThe -a flag signifies adding to an existing destination file.");
                Console.WriteLine("\tIf a source is already present in the destination it will be");
                Console.WriteLine("\toverwritten *without warning*.");
                return 1;
            }

            Dictionary<string, UInt32> crcs = CrcFileNamesInDirectory(options.SourceFolderPath);

            using (var pf = PakFile.Open(options.DestPath))
            {
                // TODO
            }
            
            return 0;
        }

        private static Dictionary<string, uint> CrcFileNamesInDirectory(object sourceFolderPath) {
            throw new NotImplementedException();
        }

        private struct Options
        {
            public string SourceFolderPath { get; set; }
            public string DestPath { get; set; }
            public bool Update { get; set; }
        }
    }
}
