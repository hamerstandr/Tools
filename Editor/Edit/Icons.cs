using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Editor
{
    class Icons
    {
        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, out SHFILEINFO psfi, uint cbFileInfo, uint uFlags);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool DestroyIcon(IntPtr hIcon);

        public const uint SHGFI_ICON = 0x000000100;
        public const uint SHGFI_USEFILEATTRIBUTES = 0x000000010;
        public const uint SHGFI_OPENICON = 0x000000002;
        public const uint SHGFI_SMALLICON = 0x000000001;
        public const uint SHGFI_LARGEICON = 0x000000000;
        public const uint FILE_ATTRIBUTE_DIRECTORY = 0x00000010;
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct SHFILEINFO
        {
            public IntPtr hIcon;
            public int iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        };
        public enum FolderType
        {
            Closed,
            Open,
            File
        }
        public enum FileAttribute : uint
        {
            Directory = 16,
            File = 256
        }
        public enum IconSize
        {
            Large,
            Small
        }
        public enum ShellAttribute : uint
        {
            LargeIcon = 0,              // 0x000000000
            SmallIcon = 1,              // 0x000000001
            OpenIcon = 2,               // 0x000000002
            ShellIconSize = 4,          // 0x000000004
            Pidl = 8,                   // 0x000000008
            UseFileAttributes = 16,     // 0x000000010
            AddOverlays = 32,           // 0x000000020
            OverlayIndex = 64,          // 0x000000040
            Others = 128,               // Not defined, really?
            Icon = 256,                 // 0x000000100  
            DisplayName = 512,          // 0x000000200
            TypeName = 1024,            // 0x000000400
            Attributes = 2048,          // 0x000000800
            IconLocation = 4096,        // 0x000001000
            ExeType = 8192,             // 0x000002000
            SystemIconIndex = 16384,    // 0x000004000
            LinkOverlay = 32768,        // 0x000008000 
            Selected = 65536,           // 0x000010000
            AttributeSpecified = 131072 // 0x000020000
        }
        public static Icon GetIcon(string Path = "C:\\Windows",IconSize size=IconSize.Large, FolderType folderType=FolderType.File)
        {
            // Need to add size check, although errors generated at present!    
            uint flags = SHGFI_ICON | SHGFI_USEFILEATTRIBUTES;
            FileAttribute fileAttribute = FileAttribute.Directory;
            if (FolderType.Open == folderType)
            {
                   flags += SHGFI_OPENICON;
            }
            if (IconSize.Small == size)
            {
                flags += SHGFI_SMALLICON;
            }
            if(FolderType.File == folderType)
            {
                fileAttribute = FileAttribute.File;
                flags += SHGFI_LARGEICON;
            }
            // Get the folder icon    
            var shfi = new SHFILEINFO();

            // if you change the string to point to just C: it will display a drive
            var res = SHGetFileInfo(Path,
                FILE_ATTRIBUTE_DIRECTORY,
                out shfi,
                (uint)Marshal.SizeOf(shfi),
                flags);

            if (res == IntPtr.Zero)
                throw Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error());

            // Load the icon from an HICON handle  
            System.Drawing.Icon.FromHandle(shfi.hIcon);

            // Now clone the icon, so that it can be successfully stored in an ImageList
            var icon = (System.Drawing.Icon)System.Drawing.Icon.FromHandle(shfi.hIcon).Clone();

            DestroyIcon(shfi.hIcon);        // Cleanup    

            return icon;
        }
        public static BitmapImage GetImage(string Path = "C:\\Windows", IconSize size = IconSize.Large, FolderType folderType = FolderType.Closed)
        {
            System.Drawing.Icon icon = GetIcon(Path,size, folderType);
            Bitmap bmp = icon.ToBitmap();
            MemoryStream strm = new MemoryStream();
            bmp.Save(strm, System.Drawing.Imaging.ImageFormat.Png);
            BitmapImage bmpImage = new BitmapImage();
            bmpImage.BeginInit();
            strm.Seek(0, SeekOrigin.Begin);
            bmpImage.StreamSource = strm;
            bmpImage.EndInit();
            return bmpImage;
        }

    }
    public class ShellManager
    {
        #region Include
        public static class Interop
        {
            [DllImport("shell32.dll", CharSet = CharSet.Auto)]
            public static extern IntPtr SHGetFileInfo(string path,
                uint attributes,
                out ShellFileInfo fileInfo,
                uint size,
                uint flags);

            [DllImport("user32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool DestroyIcon(IntPtr pointer);
        }
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct ShellFileInfo
        {
            public IntPtr hIcon;

            public int iIcon;

            public uint dwAttributes;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        }
        public enum FileAttribute : uint
        {
            Directory = 16,
            File = 256
        }
        [Flags]
        public enum ShellAttribute : uint
        {
            LargeIcon = 0,              // 0x000000000
            SmallIcon = 1,              // 0x000000001
            OpenIcon = 2,               // 0x000000002
            ShellIconSize = 4,          // 0x000000004
            Pidl = 8,                   // 0x000000008
            UseFileAttributes = 16,     // 0x000000010
            AddOverlays = 32,           // 0x000000020
            OverlayIndex = 64,          // 0x000000040
            Others = 128,               // Not defined, really?
            Icon = 256,                 // 0x000000100  
            DisplayName = 512,          // 0x000000200
            TypeName = 1024,            // 0x000000400
            Attributes = 2048,          // 0x000000800
            IconLocation = 4096,        // 0x000001000
            ExeType = 8192,             // 0x000002000
            SystemIconIndex = 16384,    // 0x000004000
            LinkOverlay = 32768,        // 0x000008000 
            Selected = 65536,           // 0x000010000
            AttributeSpecified = 131072 // 0x000020000
        }
        public enum IconSize : short
        {
            Small,
            Large
        }
        public enum ItemState : short
        {
            Undefined,
            Open,
            Close
        }
        public enum ItemType
        {
            Drive,
            Folder,
            File
        }
        #endregion
        public static Icon GetIcon(string path, ItemType type, IconSize iconSize, ItemState state)
        {
            var attributes = (uint)(type == ItemType.Folder ? FileAttribute.Directory : FileAttribute.File);
            var flags = (uint)(ShellAttribute.Icon | ShellAttribute.UseFileAttributes);

            if (type == ItemType.Folder && state == ItemState.Open)
            {
                flags = flags | (uint)ShellAttribute.OpenIcon;
            }
            if (iconSize == IconSize.Small)
            {
                flags = flags | (uint)ShellAttribute.SmallIcon;
            }
            else
            {
                flags = flags | (uint)ShellAttribute.LargeIcon;
            }

            var fileInfo = new ShellFileInfo();
            var size = (uint)Marshal.SizeOf(fileInfo);
            var result = Interop.SHGetFileInfo(path, attributes, out fileInfo, size, flags);

            if (result == IntPtr.Zero)
            {
                throw Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error());
            }

            try
            {
                return (Icon)Icon.FromHandle(fileInfo.hIcon).Clone();
            }
            catch
            {
                throw;
            }
            finally
            {
                Interop.DestroyIcon(fileInfo.hIcon);
            }
        }
    }
}
