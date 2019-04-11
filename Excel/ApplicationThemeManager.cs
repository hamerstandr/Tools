using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Excel
{
    public class ApplicationThemeManager
    {
        public const string DefaultThemeName = "Windows8";
        public const string DefaultThemeNameTouch = "Windows8Touch";

        public event EventHandler ThemeChanged;
        private static readonly ApplicationThemeManager instance = new ApplicationThemeManager();

        // contains items like <"/Telerik.Windows.Themes.Windows8;component/telerik.windows.controls.xaml", ResourceDictionary>
        private static readonly Dictionary<string, ResourceDictionary> cachedResourceDictionaries = new Dictionary<string, ResourceDictionary>();

        private static readonly string[] allThemes = new string[]
        {
            "Office_Black",
            "Office_Blue",
            "Office_Silver",
            "Summer",
            "Vista",
            "Windows8",
            "Windows8Touch",
            "Transparent",
            "Windows7",
            "Expression_Dark"
        };

        private static readonly string[] defaultReferencesNamesForApplication = new string[]
        {
            "Telerik.Windows.Controls",
            "Telerik.Windows.Controls.Input",
            "Telerik.Windows.Controls.Navigation",
            "Telerik.Windows.Documents",
            "System.Windows"
        };

        private string currentTheme;

        public string CurrentTheme
        {
            get
            {
                return this.currentTheme;
            }
        }

        private ApplicationThemeManager()
        {
        }

        public static ApplicationThemeManager GetInstance()
        {
            return instance;
        }

        public static string[] GetAllThemes()
        {
            return allThemes;
        }

        /// <summary>
        /// Adds ResourceDictionary items to application resources for certain theme using assembly names to form the ResourceDictionary's Source
        /// </summary>
        /// <param name="themeName">The theme for which resources to add</param>
        /// <param name="resourcesPaths">String array of assembly names used to form the Source of the ResourceDictionary items</param>
        public void EnsureResourcesForTheme(string themeName, string[] resourcesPaths = null)
        {
            if (resourcesPaths == null)
            {
                resourcesPaths = new string[] { };
            }

            // always include default resources
            resourcesPaths = resourcesPaths.Union(defaultReferencesNamesForApplication).ToArray();

            // temporarily save QSF's resources
            var telerikThemeDictionaries = from keyValuePair in cachedResourceDictionaries
                                           where keyValuePair.Key.Contains("Telerik.Windows.Themes.")
                                           select keyValuePair.Value;
            var qsfOnlyDictionaries = Application.Current.Resources.MergedDictionaries.Except(telerikThemeDictionaries).ToList();

            Action resetApplicationResources = () =>
            {
                Application.Current.Resources.MergedDictionaries.Clear();

                // add new resources
                foreach (string resourcePath in resourcesPaths)
                {
                    var xamlFile = resourcePath.Split(',')[0].ToLower(CultureInfo.InvariantCulture) + ".xaml";
                    var uriStringToAdd = "/Telerik.Windows.Themes." + themeName + ";component/themes/" + xamlFile;

                    // this call may cause "Collection was modified" exception
                    AddDictionaryToApplicationResources(uriStringToAdd);
                }

                //add back QSF's resources
                if (qsfOnlyDictionaries != null && qsfOnlyDictionaries.Count() > 0)
                {
                    foreach (var resourceDictionary in qsfOnlyDictionaries)
                    {
                        Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);
                    }
                }
            };

            // retry to reset resources several times (hides the exception with "Collection was modified")
            int retries = 0;

            while (retries < 5)
            {
                try
                {
                    resetApplicationResources();
                    break;
                }
                catch
                {
                    retries++;
                }
            }

            this.currentTheme = themeName;

            this.OnThemeChanged();
        }

        public static void EnsureFrameworkElementResourcesForTheme(FrameworkElement element, string themeName)
        {
            var resourcesPaths = new string[] { };

            // always include default resources
            resourcesPaths = defaultReferencesNamesForApplication.ToArray();

            // temporarily save QSF's resources
            var telerikThemeDictionaries = from keyValuePair in cachedResourceDictionaries
                                           where keyValuePair.Key.Contains("Telerik.Windows.Themes.")
                                           select keyValuePair.Value;
            var qsfOnlyDictionaries = Application.Current.Resources.MergedDictionaries.Except(telerikThemeDictionaries).ToList();

            element.Resources.MergedDictionaries.Clear();

            // add new resources
            foreach (string resourcePath in resourcesPaths)
            {
                var xamlFile = resourcePath.Split(',')[0].ToLower(CultureInfo.InvariantCulture) + ".xaml";
                var uriStringToAdd = "/Telerik.Windows.Themes." + themeName + ";component/themes/" + xamlFile;

                var uri = new Uri(uriStringToAdd, UriKind.RelativeOrAbsolute);
                var resourceDictionary = new ResourceDictionary() { Source = uri };
                element.Resources.MergedDictionaries.Add(resourceDictionary);
            }
        }

        private static void AddDictionaryToApplicationResources(string uriStringToAdd)
        {
            ResourceDictionary resourceDictionary = null;

            // load ResourceDictionary from cache, if exists there, otherwise try to create it and add it to cache
            if (cachedResourceDictionaries.ContainsKey(uriStringToAdd))
            {
                resourceDictionary = cachedResourceDictionaries[uriStringToAdd];
            }
            else
            {
                try
                {

                    var uri = new Uri(uriStringToAdd, UriKind.RelativeOrAbsolute);

                    resourceDictionary = new ResourceDictionary() { Source = uri };
                    cachedResourceDictionaries.Add(uriStringToAdd, resourceDictionary);
                }
                catch
                {
                    resourceDictionary = null;
                    cachedResourceDictionaries.Add(uriStringToAdd, resourceDictionary);
                }
            }

            if (resourceDictionary != null)
            {
                Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);
            }
        }

        private void OnThemeChanged()
        {
            if (this.ThemeChanged != null)
            {
                this.ThemeChanged(this, EventArgs.Empty);
            }
        }
    }
}
