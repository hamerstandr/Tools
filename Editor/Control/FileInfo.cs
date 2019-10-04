using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Windows.Controls;

namespace Editor.Control
{
    public class FileInfo: RadTreeViewItem
    {
        public FileInfo(string fullPath, string Header)
        {
            this.FullPath = fullPath;
            this.Header = Header;
            Foreground = System.Windows.Media.Brushes.White;
        }

        public string FullPath
        {
            get;
            set;
        }
        public RadPaneGroup Root;
    }
}
