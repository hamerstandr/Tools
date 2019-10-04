using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools
{
    public class StorageFile
    {
        public string Name { get; set; }

        public string FileSystemInfoType { get; set; }

        public DateTime UploadedTime { get; set; }

        public StorageFile(string name)
        {
            this.Name = name;
            this.FileSystemInfoType = "File";
            this.UploadedTime = DateTime.Now;
        }
    }
}
