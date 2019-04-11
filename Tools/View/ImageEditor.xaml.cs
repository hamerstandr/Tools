using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Telerik.Windows.Media.Imaging.Tools;

namespace Tools.View
{
    /// <summary>
    /// Interaction logic for ImageEditor.xaml
    /// </summary>
    public partial class ImageEditor : UserControl
    {
        public ImageEditor()
        {
            InitializeComponent();
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ImageExampleHelper.LoadSampleImage(this.ImageEditorUI, "hamed Logo.png");
            this.ImageEditorUI.ImageEditor.ExecuteTool(new ResizeTool());
        }
    }
}
