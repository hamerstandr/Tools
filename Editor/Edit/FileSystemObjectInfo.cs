using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor
{
    public class FileSystemObjectInfo// : BaseObject
    {
        public FileSystemObjectInfo(DriveInfo drive)
            : this(drive.RootDirectory)
        {
        }

        public FileSystemObjectInfo(FileSystemInfo info)
        {
        }
    }
}
