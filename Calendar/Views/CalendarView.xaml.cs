using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Xml;
using System.Xml.Serialization;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.ScheduleView;

namespace Calendar
{
    /// <summary>
    /// Interaction logic for CalendarView.xaml
    /// </summary>
    public partial class CalendarView : UserControl
    {
        public CalendarView()
        {
            InitializeComponent();
            IconSources.ChangeIconsSet(IconsSet.Modern);
            try
            {
                StreamReader R = new StreamReader(Application.Current.StartupUri + @"\Data1.dat");//Directory.GetCurrentDirectory()
                string s = R.ReadToEnd();
                R.Close();
                Model.Appointments = CreateObject(s, typeof(ObservableCollection<Appointment>)) as ObservableCollection<Appointment>;
            }
            catch { }
            
            StyleManager.SetTheme(CalendarScheduleView, new Office2016Theme());
            
            Unloaded += CalendarView_Unloaded;

        }

        private void CalendarView_Unloaded(object sender, RoutedEventArgs e)
        {
            var x = CreateXML(Model.Appointments, typeof(ObservableCollection<Appointment>));
            StreamWriter W = new StreamWriter(Application.Current.StartupUri + @"\Data1.dat", false);
            W.Write(x);
            W.Close();
        }

        /// <summary>
        /// Convert class To Object
        /// </summary>
        /// <param name="YourClassObject">Stting</param>
        /// <returns></returns>
        public static string CreateXML(object YourClassObject, Type Type1)
        {
            // Represents an XML document, Initializes a new instance of the XmlDocument class.
            XmlDocument xmlDoc = new XmlDocument();
            XmlSerializer xmlSerializer = new XmlSerializer(Type1);

            // Creates a stream whose backing store is memory. Initializes a new instance of the MemoryStream class with an expandable capacity initialized to zero.
            using (MemoryStream xmlStream = new MemoryStream())
            {
                if (xmlStream.CanWrite)
                {
                    xmlSerializer.Serialize(xmlStream, YourClassObject);
                    xmlStream.Position = 0;
                    using (StreamReader r = new StreamReader(xmlStream))
                    {
                        string s = r.ReadToEnd();
                        xmlDoc.LoadXml(s);
                    }
                }



                //Loads the XML document from the specified string.

                return xmlDoc.InnerXml;
            }
        }
        /// <summary>
        /// convert xml To class
        /// </summary>
        /// <param name="XMLString"></param>
        /// <param name="YourClassObject">Stting</param>
        /// <returns></returns>
        public static object CreateObject(string XMLString, Type Type1)
        {
            try
            {
                object YourClassObject;
                //if (((List<Virus>)YourClassObject).Count == 0)
                //    return null;
                if (XMLString == "")
                    return null;
                XmlSerializer oXmlSerializer = new XmlSerializer(Type1);

                //The StringReader will be the stream holder for the existing XML file

                YourClassObject = oXmlSerializer.Deserialize(new StringReader(XMLString));

                //initially deserialized, the data is represented by an object without a defined type
                return YourClassObject;
            }
            catch (Exception ex)
            {
                var e = ex.Message;
                return null;
            }


        }

        private void RadRibbonButton_Click(object sender, RoutedEventArgs e)
        {
            var x = CreateXML(Model.Appointments, typeof(ObservableCollection<Appointment>));
            StreamWriter W = new StreamWriter(Application.Current.StartupUri + @"\Data1.dat", false);
            W.Write(x);
            W.Close();
        }
    }
}
