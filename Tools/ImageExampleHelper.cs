using System;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Telerik.Windows.Controls;
using Telerik.Windows.Media.Imaging;
using Telerik.Windows.Media.Imaging.FormatProviders;

namespace Tools
{
    public class ImageExampleHelper
    {
        private static string SampleImageFolder = "Images/RadialMenu/";

        public static void LoadSampleImage(RadImageEditorUI imageEditor, string image)
        {
            //SampleImageFolder +
            using (Stream stream = Application.GetResourceStream(GetResourceUri( image)).Stream)
            {
                imageEditor.Image = new RadBitmap(stream);
                imageEditor.ApplyTemplate();
                imageEditor.ImageEditor.ScaleFactor = 0;
            }
        }
        public static BitmapSource LoadImage( string image)
        {
            //SampleImageFolder +
            BitmapSource bitmapSource= new BitmapImage(GetResourceUri(image));
            return bitmapSource;
        }
        public static void LoadSampleImage(RadImageEditor imageEditor, string image)
        {
            using (Stream stream = Application.GetResourceStream(GetResourceUri(SampleImageFolder + image)).Stream)
            {
                imageEditor.Image = new RadBitmap(stream);
                imageEditor.ScaleFactor = 0;
                imageEditor.ApplyTemplate();
            }
        }

        public static RadBitmap GetRadBitmap(string resource, IImageFormatProvider provider)
        {
            using (Stream stream = Application.GetResourceStream(new Uri(resource, UriKind.RelativeOrAbsolute)).Stream)
            {
                return provider.Import(stream);
            }
        }

        public static Uri GetResourceUri(string resource)
        {
            AssemblyName assemblyName = new AssemblyName(typeof(ImageExampleHelper).Assembly.FullName);
            string resourcePath = "/" + assemblyName.Name + ";component/" + resource;
            Uri resourceUri = new Uri(resourcePath, UriKind.Relative);

            return resourceUri;
        }
    }
}
