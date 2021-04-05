using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace ExampleMod.Util
{
    static class AssetBundleHelper
    {
        public static AssetBundle GetFromResources(string fileName)
        {
            Assembly execAssembly = Assembly.GetExecutingAssembly();
            string resourceName = execAssembly.GetManifestResourceNames().Single(str => str.EndsWith(fileName));
            using Stream stream = execAssembly.GetManifestResourceStream(resourceName);
            return AssetBundle.LoadFromStream(stream);
        }
    }
}
