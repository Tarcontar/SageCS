using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SageCS.Core.Loaders;

namespace SageCS.Core
{
    class FileSystem                                   
    {
        private static SortedDictionary<string, Stream> entries;

        public static void Init()
        {
            entries = new SortedDictionary<string, Stream>();
            string[] archives = Directory.GetFiles(Directory.GetCurrentDirectory(),"*.big",SearchOption.AllDirectories);
                               
            foreach (var archive in archives)
            {
                var content = Loaders.BigArchive.GetEntries(archive);
                foreach (var c in content)
                {
                    if (!entries.ContainsKey(c.Key))
                        entries.Add(c.Key, c.Value);
                }  
            }

            string[] files = Directory.GetFiles(Directory.GetCurrentDirectory(), "*", SearchOption.AllDirectories);
            foreach (var f in files)
            {
                if (Path.GetExtension(f)!=".big")
                {
                    var relPath = f.Replace(Directory.GetCurrentDirectory(), "").TrimStart('\\');
                    if (!entries.ContainsKey(relPath))
                        entries.Add(relPath, null);
                }
            }     
        }

        public static Stream Open(string name)
        {
            return entries[name.ToLower()];
        }

        public static List<Stream> OpenAll(string path, List<string> excluded)
        {
            List<Stream> streams = new List<Stream>();
            foreach (KeyValuePair<string, Stream> entry in entries)
            {
                if (entry.Key.StartsWith(path))
                    streams.Add(entry.Value);
                foreach (String s in excluded)
                {
                    if (entry.Key.StartsWith(s))
                        streams.Remove(entry.Value);
                }
            }
            return streams;
        }
    }
}
