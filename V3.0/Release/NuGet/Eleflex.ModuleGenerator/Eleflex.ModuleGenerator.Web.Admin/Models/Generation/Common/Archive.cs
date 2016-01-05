using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;

namespace Eleflex.ModuleGenerator.Web.Admin.Models.Generation
{
    class Archive
    {
        public static byte[] CreateArchive(List<ArchiveItem> items)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    foreach(var item in items)
                    {
                        var entry = archive.CreateEntry(item.Filename);
                        using (var entryStream = entry.Open())
                        {
                            entryStream.Write(item.Data, 0, item.Data.Length);
                        }
                    }
                }
                memoryStream.Position = 0;
                return memoryStream.ToArray();
            }
        }
    }
}
