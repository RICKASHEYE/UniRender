using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    public static class PluginLoader
    {
        public static void LoadPlugins()
        {
            Debug.Log("Plugin loader in progress of getting built.");
        }

        public static void UnloadPlugins()
        {
            Debug.Log("Unloading plugins is also in progress of getting built.");
        }
    }
}
