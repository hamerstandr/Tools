using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Excel.Helper
{
    public static class FileHelper
    {
        public static Uri GetResourceUri(string resource, Type example)
        {
            AssemblyName assemblyName = new AssemblyName(example.Assembly.FullName);
            string resourcePath = "/" + assemblyName.Name + ";component/" + resource;
            Uri resourceUri = new Uri(resourcePath, UriKind.Relative);

            return resourceUri;
        }
    }
}
