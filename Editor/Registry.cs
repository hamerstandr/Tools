using Microsoft.Win32;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace Editor
{
    static class Registr
    {
        public static void DefaultProgram()
        {
            try {
                if (!FileAssociation.IsAssociated(".code-proge"))
                    FileAssociation.Associate(".code-proge", "Editor.Edit", "Code Folder", Assembly.GetEntryAssembly().Location, Assembly.GetEntryAssembly().Location);
                if (!FileAssociation.IsAssociated(".code-proge\\ShellNew"))
                {
                    var Key = Registry.ClassesRoot.OpenSubKey(".code-proge\\ShellNew");
                    Key.SetValue("NullFile", "", RegistryValueKind.DWord);
                }
            } catch { return; }
            

            //var imgKey = Registry.ClassesRoot.OpenSubKey(".code-proge");
            //try {
               
            //} catch { return; }

            //if (imgKey == null)
            //    imgKey = Registry.ClassesRoot.CreateSubKey(".code-proge");
            //imgKey.SetValue("", Assembly.GetEntryAssembly().Location);

            //String myExecutable = Assembly.GetEntryAssembly().Location;
            //String command = "\"" + myExecutable + "\"" + " \"%1\"";
            //String keyName = imgKey + @"\shell\Open\command";
            //using (var key = Registry.ClassesRoot.CreateSubKey(keyName))
            //{
            //    key.SetValue("", command);
            //}
        }
        private const string MenuName = "Folder\\shell\\HEditor";
        private const string Command = "Folder\\shell\\HEditor\\command";
        public static void AddMenu()
        {
            RegistryKey regmenu = null;
            RegistryKey regcmd = null;
            try
            {
                regmenu = Registry.ClassesRoot.CreateSubKey(MenuName);
                if (regmenu != null)
                {
                    regmenu.SetValue("Icon", Assembly.GetEntryAssembly().Location);
                    regmenu.SetValue("", "Open in HEditor");
                }
                    
                regcmd = Registry.ClassesRoot.CreateSubKey(Command);
                if (regcmd != null)
                    regcmd.SetValue("", Assembly.GetEntryAssembly().Location);
            }
            catch //(Exception ex)
            {
                //MessageBox.Show(this, ex.ToString());
            }
            finally
            {
                if (regmenu != null)
                    regmenu.Close();
                if (regcmd != null)
                    regcmd.Close();
            }
        }
        public static void RemoveMenu()
        {
            try
            {
                RegistryKey reg = Registry.ClassesRoot.OpenSubKey(Command);
                if (reg != null)
                {
                    reg.Close();
                    Registry.ClassesRoot.DeleteSubKey(Command);
                }
                reg = Registry.ClassesRoot.OpenSubKey(MenuName);
                if (reg != null)
                {
                    reg.Close();
                    Registry.ClassesRoot.DeleteSubKey(MenuName);
                }
            }
            catch //(Exception ex)
            {
                //MessageBox.Show(this, ex.ToString());
            }
        }
    }
    public class FileAssociation
    {
        // Associate file extension with progID, description, icon and application
        public static void Associate(string extension,
               string progID, string description, string icon, string application)
        {
            Registry.ClassesRoot.CreateSubKey(extension).SetValue("", progID);
            if (progID != null && progID.Length > 0)
                using (RegistryKey key = Registry.ClassesRoot.CreateSubKey(progID))
                {
                    if (description != null)
                        key.SetValue("", description);
                    if (icon != null)
                        key.CreateSubKey("DefaultIcon").SetValue("", ToShortPathName(icon));
                    if (application != null)
                        key.CreateSubKey(@"Shell\Open\Command").SetValue("",
                                    ToShortPathName(application) + " \"%1\"");
                }
        }

        // Return true if extension already associated in registry
        public static bool IsAssociated(string extension)
        {
            return (Registry.ClassesRoot.OpenSubKey(extension, false) != null);
        }

        [DllImport("Kernel32.dll")]
        private static extern uint GetShortPathName(string lpszLongPath,
            [Out] StringBuilder lpszShortPath, uint cchBuffer);

        // Return short path format of a file name
        private static string ToShortPathName(string longName)
        {
            StringBuilder s = new StringBuilder(1000);
            uint iSize = (uint)s.Capacity;
            uint iRet = GetShortPathName(longName, s, iSize);
            return s.ToString();
        }
    }
}