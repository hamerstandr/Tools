using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using Telerik.Windows;
using Telerik.Windows.Controls;

namespace Editor.Control
{
    //public class DirectoryInfo : FileInfo
    //{
    //    public DirectoryInfo(string fullPath, string name) : base(fullPath, name)
    //    {
    //        this.Children = new ObservableCollection<object>();
    //    }

    //    public ObservableCollection<object> Children
    //    {
    //        get;
    //        private set;
    //    }

    //    public void LoadChildren()
    //    {
    //        try
    //        {
    //            foreach (string directory in Directory.GetDirectories(this.FullPath))
    //            {
    //                System.IO.DirectoryInfo directoryInfo = new System.IO.DirectoryInfo(directory);
    //                this.Children.Add(new DirectoryInfo(directory, directoryInfo.Name));
    //            }

    //            var files = Directory.GetFiles(this.FullPath);

    //            foreach (string file in files)
    //            {
    //                System.IO.FileInfo fileInfo = new System.IO.FileInfo(file);
    //                this.Children.Add(new FileInfo(file, fileInfo.Name));
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            MessageBox.Show(ex.Message);
    //        }
    //    }
    //}
    public class DirectoryInfo : FileInfo
    {
        public DirectoryInfo(string fullPath, string Header) : base(fullPath, Header)
        {
            StyleManager.SetTheme(this, new FluentTheme());
            //this.Children = new ObservableCollection<object>();
        }
        
        //public ObservableCollection<object> Children
        //{
        //    get;
        //    private set;
        //}

        public void LoadChildren()
        {
            try
            {
                foreach (string directory in Directory.GetDirectories(this.FullPath))
                {
                    System.IO.DirectoryInfo directoryInfo = new System.IO.DirectoryInfo(directory);
                    DirectoryInfo i = new DirectoryInfo(directory, directoryInfo.Name) {
                        DefaultImageSrc = FolderManager.GetImageSource(directory,ShellManager.ItemState.Close),//Icons.GetImage(directory,Icons.IconSize.Large,Icons.FolderType.Closed ),
                        ExpandedImageSrc = FolderManager.GetImageSource(directory, ShellManager.ItemState.Open),//Icons.GetImage(directory,Icons.IconSize.Large, Icons.FolderType.Open),
                        Root = Root,
                        
                    };
                    StyleManager.SetTheme(i, new FluentTheme());
                    i.Click += Folder_Click;
                    i.Expanded += Folder_Expanded;
                    //if (Directory.GetDirectories(directory).Length > 0)
                    //    i.IsExpanded = true;
                    Items.Add(i);
                }

                var files = Directory.GetFiles(this.FullPath);

                foreach (string file in files)
                {
                    System.IO.FileInfo fileInfo = new System.IO.FileInfo(file);
                    FileInfo i = new FileInfo(file, fileInfo.Name)
                    {
                        DefaultImageSrc = FileManager.GetImageSource(file),//Icons.GetImage(file, Icons.IconSize.Large),
                        Root = Root
                    };
                    StyleManager.SetTheme(i, new FluentTheme());
                    i.DoubleClick += File_DoubleClick;
                    Items.Add(i); 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Folder_Expanded(object sender, RadRoutedEventArgs e)
        {
            DirectoryInfo dir = sender as DirectoryInfo;
            if (!dir.IsExpanded)
                if (Directory.GetDirectories(dir.FullPath).Length + Directory.GetFiles(this.FullPath).Length != dir.Items.Count)
                {
                    dir.Items.Clear();
                    dir.LoadChildren();
                    dir.IsExpanded = true;
                }
        }

        private void Folder_Click(object sender, RadRoutedEventArgs e)
        {
            DirectoryInfo dir = sender as DirectoryInfo;
            if (Directory.GetDirectories(dir.FullPath).Length + Directory.GetFiles(dir.FullPath).Length != dir.Items.Count)
            {
                dir.Items.Clear();
                dir.LoadChildren();
                dir.IsExpanded = true;
            }
            
        }

        private void File_DoubleClick(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            Tab tab;
            FileInfo file = sender as FileInfo;
            if (file != null)
            {
                tab = new Tab();
                var Header = new HeaderPane() { Header = file.Header };
                Header.Click += () => {
                    if(tab.Save())
                        Root.Items.Remove(tab); };
                tab.Header = Header;
                tab.LoadFile(file.FullPath);
                Root.Items.Add(tab);
            }
        }
        public RadPaneGroup GetParent(RadTreeViewItem item)
        {
            return Root.FindChildByType<RadPaneGroup>();
            //return (item.Parent as RadTreeView).Parent as RadPaneGroup;
        }
    }
}
