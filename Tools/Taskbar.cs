using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Telerik.Windows.Controls;
using Tools.View;

namespace Tools
{
    public class Taskbar
    {
        public static void ShowInTaskbar(RadWindow control, string title, string icon)
        {
            control.IconTemplate = Application.Current.Resources[icon] as DataTemplate;
            //Image image = new Image();
            //image.Source = ImageExampleHelper.LoadImage("PDF.ico");
            //spFactory.AppendChild(image);

            control.Show();
            var window = control.ParentOfType<Window>();
            window.ShowInTaskbar = true;
            window.Title = title;
            var uri = new Uri("pack://application:,,,/Tools;component/Image/" + icon);
            window.Icon =BitmapFrame.Create(uri);
        }
        public static string ExePath; public static string Path;
        public static void CreateShortcut(string Name, string NameIcon)
        {
            try{
                CreateIcon(NameIcon);
                if (!File.Exists(Path + @"\" + Name + ".lnk"))
                    ShellLink.CreateShortcut(Path, Name, ExePath, NameIcon, new ShellLink.Icon() { Path = Path + @"\Image\" + NameIcon + ".ico", Index = 0 });
            } catch { }
            
        }
        public static void CreateIcon(string Name)
        {
            using (Stream stream = Application.GetResourceStream(new Uri("pack://application:,,,/Tools;component/Image/" + Name + ".ico")).Stream)
            {
                System.Drawing.Icon myIcon = new System.Drawing.Icon(stream);
                if(!File.Exists(Path + @"\Image\" + Name + ".ico"))
                {
                    if (!Directory.Exists(Path + @"\Image\"))
                        Directory.CreateDirectory(Path + @"\Image\");
                    using (Stream stream1 = new StreamWriter(Path + @"\Image\" + Name + ".ico", true).BaseStream)
                        myIcon.Save(stream1);
                }
                
            }
            

        }
        public static System.Drawing.Bitmap GetBitmap(BitmapSource source)
        {
            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(
              source.PixelWidth,
              source.PixelHeight,
              System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            System.Drawing.Imaging.BitmapData data = bmp.LockBits(
              new System.Drawing.Rectangle(System.Drawing.Point.Empty, bmp.Size),
              System.Drawing.Imaging.ImageLockMode.WriteOnly,
              System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            source.CopyPixels(
              Int32Rect.Empty,
              data.Scan0,
              data.Height * data.Stride,
              data.Stride);
            bmp.UnlockBits(data);
            return bmp;
        }
    }
}
