using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
namespace Tools
{
    public  class ShellLink
	{
        public static void CreateShortcut(string path, string LinkName, string PathLink, string Arguments, Icon Icon = new Icon())
        {
            if (Icon.Path == "")
            { Icon.Path = PathLink; Icon.Index = 0; }
            ShellLink shortcut = new ShellLink();
            //string deskDir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            shortcut.Arguments = Arguments;
            shortcut.Path = PathLink;// Application.ExecutablePath.ToString();
            shortcut.WorkingDirectory = Directory.GetDirectoryRoot(PathLink);//Application.ExecutablePath
            shortcut.Description = "H.m Soft";
            shortcut.IconIndex =Icon.Index;
            shortcut.IconPath = Icon.Path;// Application.ExecutablePath.ToString();
            shortcut.Save(path + @"\" + LinkName + ".lnk");


        }
        public struct Icon
        {
            public string Path;
            public int Index ;
        }
        /// <summary>
        /// Shell link object's identifier.
        /// </summary>
        private static readonly Guid CLSID_ShellLink = new Guid(0x21401, 0, 0, 0xc0, 0, 0, 0, 0, 0, 0, 0x46);

		/// <summary>
		/// Represents SW_xxx constants that apply to shell links.
		/// </summary>
		/// <remarks>
		/// See Platform SDK for full list of possible SW_xxx values.
		/// </remarks>
		public enum ShowCommand: int
		{
			/// <summary>
			/// Maps to SW_SHOWNORMAL. The main application window opens in
			/// normal state.
			/// </summary>
			ShowNormal = 1,

			/// <summary>
			/// Maps to SW_SHOWMAXIMIZED. The main application windows opens
			/// maximized.
			/// </summary>
			ShowMaximized = 3,

			/// <summary>
			/// Maps to SW_SHOWMINNOACTIVE. The main application window opens
			/// minimized and doesn't get activated. Usefull for running
			/// background processes.
			/// </summary>
			ShowMinimized = 7
		}

		private static string Str(string str)
		{
			return (null == str) ? String.Empty : str;
		}

		private string arguments = String.Empty;

		/// <summary>
		/// The command-line arguments associated with a shell link object.
		/// </summary>
		/// <value>
		/// The command-line arguments string.
		/// </value>
		public string Arguments
		{
			get
			{
				return arguments;
			}

			set
			{
				arguments = Str(value);
			}
		}

		private string description = String.Empty;

		/// <summary>
		/// The description for a shell link object. The description can be
		/// any user-defined string.
		/// </summary>
		/// <value>
		/// The description string.
		/// </value>
		public string Description
		{
			get
			{
				return description;
			}

			set
			{
				description = Str(value);
			}
		}

		private short hotkey;

		/// <summary>
		/// The hot key for a shell link object.
		/// </summary>
		/// <value>
		/// The hot key value.
		/// </value>
		/// <remarks>
		/// <para>The virtual key code is in the low-order byte, and the
		/// modifier flags are in the high-order byte.</para>
		/// <para>For more information see Platform SDK information on subject
		/// <c>IShellLink::GetHotKey</c>.</para>
		/// </remarks>
		public short Hotkey
		{
			get
			{
				return hotkey;
			}

			set
			{
				hotkey = value;
			}
		}

		private string iconPath = String.Empty;

		/// <summary>
		/// The path of the file containing the icon for a shell link object.
		/// </summary>
		/// <value>
		/// The path to the icon file.
		/// </value>
		public string IconPath
		{
			get
			{
				return iconPath;
			}

			set
			{
				iconPath = Str(value);
			}
		}

		private int iconIndex;

		/// <summary>
		/// The index of the icon for the shell object.
		/// </summary>
		/// <value>
		/// The index of the icon.
		/// </value>
		public int IconIndex
		{
			get
			{
				return iconIndex;
			}

			set
			{
				iconIndex = value;
			}
		}

		private string path = String.Empty;

		/// <summary>
		/// The path and file name of a shell link object.
		/// </summary>
		/// <value>
		/// The path and file name of the shell link object.
		/// </value>
		public string Path
		{
			get
			{
				return path;
			}

			set
			{
				path = Str(value);
			}
		}

		private ShowCommand showCmd = ShowCommand.ShowNormal;

		/// <summary>
		/// The show command for a shell link object.
		/// </summary>
		/// <value>
		/// A <see cref="ShowCommand"/> value.
		/// </value>
		public ShowCommand ShowCmd
		{
			get
			{
				return showCmd;
			}

			set
			{
				showCmd = value;
			}
		}

		private string workingDirectory = String.Empty;

		/// <summary>
		/// The name of the working directory for a shell link object.
		/// </summary>
		/// <value>
		/// The name of the working directory.
		/// </value>
		public string WorkingDirectory
		{
			get
			{
				return workingDirectory;
			}

			set
			{
				workingDirectory = Str(value);
			}
		}

		private static void ThrowInvalidComObjectException()
		{
			throw new InvalidComObjectException("Not shell link interfaces supported by the shell link object");
		}

		/// <summary>
		/// Loads a shell link from the specified file and initializes
		/// the object from the file contents.
		/// </summary>
		/// <param name="linkFileName">
		/// The absolute path of the file to open.
		/// </param>
		/// <exception cref="FileNotFoundException">
		/// No link file found.
		/// </exception>
		/// <exception cref="InvalidComObjectException">
		/// The shell link COM object does not support required interfaces;
		/// Windows corrupt?
		/// </exception>
		/// <exception cref="SecurityException">
		/// Either the calls to unmanaged code are restricted or the shell link
		/// file cannot be read.
		/// </exception>
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode=true)]
		public void Load(string linkFileName)
		{
			if(null == linkFileName)
				throw new ArgumentNullException("linkFileName", "A name of the link file cannot be null");
			if(!File.Exists(linkFileName))
				throw new FileNotFoundException("Link not found", linkFileName);
			new FileIOPermission(FileIOPermissionAccess.Read | FileIOPermissionAccess.PathDiscovery, linkFileName).Demand();

			object sl = null;
			try
			{
				Type slType = Type.GetTypeFromCLSID(CLSID_ShellLink);
				sl = Activator.CreateInstance(slType);
				IPersistFile pf = sl as IPersistFile;
				pf.Load(linkFileName, 0);

				int showCmd;
				StringBuilder builder = new StringBuilder(INFOTIPSIZE);
				IShellLinkW shellLinkW = sl as IShellLinkW;
				if(null == shellLinkW)
				{
					IShellLinkA shellLinkA = sl as IShellLinkA;
					if(null == shellLinkA)
						ThrowInvalidComObjectException();
					shellLinkA.GetArguments(builder, builder.Capacity);
					this.arguments = builder.ToString();
					shellLinkA.GetDescription(builder, builder.Capacity);
					this.description = builder.ToString();
					shellLinkA.GetHotkey(out this.hotkey);
					shellLinkA.GetIconLocation(builder, builder.Capacity, out this.iconIndex);
					this.iconPath = builder.ToString();
					Win32FindDataA wfd;
					shellLinkA.GetPath(builder, builder.Capacity, out wfd, SLGP_UNCPRIORITY);
					this.path = builder.ToString();
					shellLinkA.GetShowCmd(out showCmd);
					shellLinkA.GetWorkingDirectory(builder, builder.Capacity);
					this.workingDirectory = builder.ToString();
				}
				else
				{
					shellLinkW.GetArguments(builder, builder.Capacity);
					this.arguments = builder.ToString();
					shellLinkW.GetDescription(builder, builder.Capacity);
					this.description = builder.ToString();
					shellLinkW.GetHotkey(out this.hotkey);
					shellLinkW.GetIconLocation(builder, builder.Capacity, out this.iconIndex);
					this.iconPath = builder.ToString();
					Win32FindDataW wfd;
					shellLinkW.GetPath(builder, builder.Capacity, out wfd, SLGP_UNCPRIORITY);
					this.path = builder.ToString();
					shellLinkW.GetShowCmd(out showCmd);
					shellLinkW.GetWorkingDirectory(builder, builder.Capacity);
					this.workingDirectory = builder.ToString();
				}
				this.showCmd = (ShowCommand)showCmd;
			}
			finally
			{
				if(null != sl)
					Marshal.ReleaseComObject(sl);
			}
			// This object is not eligible for the garbage collection during this method call
			GC.KeepAlive(this);
		}

		/// <summary>
		/// Saves a shell link into the specified file.
		/// </summary>
		/// <param name="linkFileName">
		/// The absolute path of the file to which the object should be saved.
		/// Note that the file should have the extension .LNK or else the shell
		/// would not recognize it as a link.
		/// </param>
		/// <exception cref="InvalidComObjectException">
		/// The shell link COM object does not support required interfaces;
		/// Windows corrupt?
		/// </exception>
		/// <exception cref="SecurityException">
		/// Either the calls to unmanaged code are restricted or the shell link
		/// file cannot be read.
		/// </exception>
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode=true)]
		public void Save(string linkFileName)
		{
			if(null == linkFileName)
				throw new ArgumentNullException("linkFileName", "A name of the link file cannot be null");
			new FileIOPermission(FileIOPermissionAccess.Write, linkFileName).Demand();

			int showCmd = (int)this.showCmd;
			object sl = null;
			try
			{
				Type slType = Type.GetTypeFromCLSID(CLSID_ShellLink);
				sl = Activator.CreateInstance(slType);
				IShellLinkW shellLinkW = sl as IShellLinkW;
				if(null == shellLinkW)
				{
					IShellLinkA shellLinkA = sl as IShellLinkA;
					if(null == shellLinkA)
						ThrowInvalidComObjectException();
					shellLinkA.SetArguments(Str(this.arguments));
					shellLinkA.SetDescription(Str(this.description));
					shellLinkA.SetHotkey(this.hotkey);
					shellLinkA.SetIconLocation(Str(this.iconPath), this.iconIndex);
					shellLinkA.SetPath(Str(this.path));
					shellLinkA.SetShowCmd(showCmd);
					shellLinkA.SetWorkingDirectory(Str(this.workingDirectory));
				}
				else
				{
					shellLinkW.SetArguments(Str(this.arguments));
					shellLinkW.SetDescription(Str(this.description));
					shellLinkW.SetHotkey(this.hotkey);
					shellLinkW.SetIconLocation(Str(this.iconPath), this.iconIndex);
					shellLinkW.SetPath(Str(this.path));
					shellLinkW.SetShowCmd(showCmd);
					shellLinkW.SetWorkingDirectory(Str(this.workingDirectory));
				}

				IPersistFile pf = sl as IPersistFile;
				pf.Save(linkFileName, true);
			}
			finally
			{
				if(null != sl)
					Marshal.ReleaseComObject(sl);
			}
			// This object is not eligible for the garbage collection during this method call
			GC.KeepAlive(this);
		}

		/// <summary>
		/// Checks if a file is a shell link.
		/// </summary>
		/// <param name="filePath">The name of the file to check.</param>
		/// <returns><c>true</c> if file is a shell link; <c>false</c>
		/// otherwise.</returns>
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode=true)]
		public static bool IsShellLink(string filePath)
		{
			if(null == filePath)
				throw new ArgumentNullException("filePath", "A name of the file cannot be null");
			if(!File.Exists(filePath))
				throw new FileNotFoundException("Cannot check file that doesn't exist", filePath);

			new FileIOPermission(FileIOPermissionAccess.Read | FileIOPermissionAccess.PathDiscovery, filePath).Demand();

			SHFileInfo sfi = new SHFileInfo();
			SHGetFileInfo(filePath, 0, out sfi, Marshal.SizeOf(sfi), SHGFI_ATTRIBUTES);
			return SFGAO_LINK == (sfi.Attributes & SFGAO_LINK);
		}

		#region Native declarations
		private const int INFOTIPSIZE = 1024;

		private const int SLGP_UNCPRIORITY = 0x2;

		private const int SLR_INVOKE_MSI = 0x80;

		private const int SLR_UPDATE = 0x8;

		private const int SFGAO_LINK = 0x00010000;

		private const int SHGFI_ATTRIBUTES = 0x000000800;

		[StructLayoutAttribute(LayoutKind.Sequential)]
		private struct SHFileInfo
		{
			private IntPtr hIcon;
			private IntPtr iIcon;
			private int dwAttributes;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
			private string szDisplayName;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
			private string szTypeName;

			internal int Attributes
			{
				get
				{
					return dwAttributes;
				}
			}
		}

		[StructLayoutAttribute(LayoutKind.Sequential, CharSet=CharSet.Ansi)]
		private struct Win32FindDataA
		{
			private uint dwFileAttributes;
            //private FILETIME ftCreationTime;
			//private FILETIME ftLastAccessTime;
			//private FILETIME ftLastWriteTime;
			private uint nFileSizeHigh;
			private uint nFileSizeLow;
			private uint dwReserved0;
			private uint dwReserved1;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst=260)]
			private string cFileName;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst=14)]
			private string cAlternateFileName;
		}

		[StructLayoutAttribute(LayoutKind.Sequential, CharSet=CharSet.Unicode)]
		private struct Win32FindDataW
		{
			private uint dwFileAttributes;
			//private FILETIME ftCreationTime;
			//private FILETIME ftLastAccessTime;
			//private FILETIME ftLastWriteTime;
			private uint nFileSizeHigh;
			private uint nFileSizeLow;
			private uint dwReserved0;
			private uint dwReserved1;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst=260)]
			private string cFileName;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst=14)]
			private string cAlternateFileName;
		}

		[ComImport]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[Guid("0000010B-0000-0000-C000-000000000046")]
		private interface IPersistFile
		{
			[PreserveSig]
			int GetClassID(out Guid pClassID);

			[PreserveSig()]
			int IsDirty();

			void Load([MarshalAs(UnmanagedType.LPWStr)] string pszFileName, int dwMode);

			void Save([MarshalAs(UnmanagedType.LPWStr)] string pszFileName, bool fRemember);

			void SaveCompleted([MarshalAs(UnmanagedType.LPWStr)] string pszFileName);

			void GetCurFile([MarshalAs(UnmanagedType.LPWStr)] out string ppszFileName);

		}

		[ComImport]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[Guid("000214EE-0000-0000-C000-000000000046")]
		private interface IShellLinkA
		{
			void GetPath([Out(), MarshalAs(UnmanagedType.LPStr)] StringBuilder pszFile, int cchMaxPath, out Win32FindDataA pfd, int fFlags);

			void GetIDList(out IntPtr ppidl);

			void SetIDList(IntPtr pidl);

			void GetDescription([Out(), MarshalAs(UnmanagedType.LPStr)] StringBuilder pszName, int cchMaxName);

			void SetDescription([MarshalAs(UnmanagedType.LPStr)] string pszName);

			void GetWorkingDirectory([Out(), MarshalAs(UnmanagedType.LPStr)] StringBuilder pszDir, int cchMaxPath);

			void SetWorkingDirectory([MarshalAs(UnmanagedType.LPStr)] string pszDir);

			void GetArguments([Out(), MarshalAs(UnmanagedType.LPStr)] StringBuilder pszArgs, int cchMaxPath);

			void SetArguments([MarshalAs(UnmanagedType.LPStr)] string pszArgs);

			void GetHotkey(out short pwHotkey);

			void SetHotkey(short wHotkey);

			void GetShowCmd(out int piShowCmd);

			void SetShowCmd(int iShowCmd);

			void GetIconLocation([Out(), MarshalAs(UnmanagedType.LPStr)] StringBuilder pszIconPath, int cchIconPath, out int piIcon);

			void SetIconLocation([MarshalAs(UnmanagedType.LPStr)] string pszIconPath, int iIcon);

			void SetRelativePath([MarshalAs(UnmanagedType.LPStr)] string pszPathRel, int dwReserved);

			void Resolve(IntPtr hwnd, int fFlags);

			void SetPath([MarshalAs(UnmanagedType.LPStr)] string pszFile);

		}

		[ComImport]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[Guid("000214F9-0000-0000-C000-000000000046")]
		private interface IShellLinkW
		{
			void GetPath([Out(), MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszFile, int cchMaxPath, out Win32FindDataW pfd, int fFlags);

			void GetIDList(out IntPtr ppidl);

			void SetIDList(IntPtr pidl);

			void GetDescription([Out(), MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszName, int cchMaxName);

			void SetDescription([MarshalAs(UnmanagedType.LPWStr)] string pszName);

			void GetWorkingDirectory([Out(), MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszDir, int cchMaxPath);

			void SetWorkingDirectory([MarshalAs(UnmanagedType.LPWStr)] string pszDir);

			void GetArguments([Out(), MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszArgs, int cchMaxPath);

			void SetArguments([MarshalAs(UnmanagedType.LPWStr)] string pszArgs);

			void GetHotkey(out short pwHotkey);

			void SetHotkey(short wHotkey);

			void GetShowCmd(out int piShowCmd);

			void SetShowCmd(int iShowCmd);

			void GetIconLocation([Out(), MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszIconPath, int cchIconPath, out int piIcon);

			void SetIconLocation([MarshalAs(UnmanagedType.LPWStr)] string pszIconPath, int iIcon);

			void SetRelativePath([MarshalAs(UnmanagedType.LPWStr)] string pszPathRel, int dwReserved);

			void Resolve(IntPtr hwnd, int fFlags);

			void SetPath([MarshalAs(UnmanagedType.LPWStr)] string pszFile);
		}

		[DllImport("shell32.dll", SetLastError=true)]
		private static extern IntPtr SHGetFileInfo(string pszPath, int dwFileAttributes, out SHFileInfo psfi, int cbFileInfo, int uFlags);
		#endregion

    }
}
