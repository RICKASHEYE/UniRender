using SharpDX.Direct2D1;
using SubrightEngine.DirectX;
using SubrightEngine.VulkanBranch;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SubrightEngine
{
    public class RenderingLibraryManager
    {
        public static List<IRenderingLibrary> libraries = new List<IRenderingLibrary>();
        //public RenderingLibrary currentLibrary;
        //We will store the keys here
        public static List<Pixel> ScreenRender = new List<Pixel>();
        public static List<KeyCode> codes = new List<KeyCode>();
        //Rendering methods lmao
        public static RenderMode modeRender = RenderMode.DirectX;
        public static AppConfiguration Config;
        public static Dimension currentDimension;

        public virtual void Draw()
        {
            if (currentDimension == Dimension.TWOD)
            {
                foreach (Pixel m in ScreenRender)
                {
                    m.DrawPixel();
                }
            }
            else
            {
                Debug.Error("3D support isnt added yet!!!");
            }
        }

        /// <summary>
        /// This function draws a pixel directly.
        /// </summary>
        public static void DirectDrawPixel(int x, int y, Color color)
        {
            if (modeRender == RenderMode.DirectX)
            {
                var solidColorBrush = new SolidColorBrush(Direct2D1Handler.RenderTarget2D, new SharpDX.Mathematics.Interop.RawColor4(color.R, color.G, color.B, 1));
                libraryGet("SharpDX").getRenderTarget().FillRectangle(new SharpDX.Mathematics.Interop.RawRectangleF(x, y, x + 1, y + 1), solidColorBrush);
                solidColorBrush.Dispose();
            }
            else if (modeRender == RenderMode.Software)
            {
                Debug.Log("Cannot draw pixel as software mode is deprecated.");
                //renderSoftware.DrawPixel(x, y, color);
            }else if(modeRender == RenderMode.Vulkan)
            {
                //3D support isnt in this yet!
                Debug.Error("Vulkan only renders in 3D");
            }
        }

        static List<string> imagesPath = new List<string>();
        public virtual void Initialize(AppConfiguration config)
        {
            Debug.Log("General initialisation starting");
            string assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string assetsFolder = Path.Combine(assemblyFolder, "Assets");

            if (!Directory.Exists(assetsFolder))
            {
                //Load an assembly assets folder
                Directory.CreateDirectory(assetsFolder);
                Initialize(config);
            }
            else
            {
                //Now initialise the assets into a registry and allow them to be loaded into the game.
                string[] files = Directory.GetFiles(assetsFolder);
                imagesPath.AddRange(files);
                Debug.Log("Loaded " + files.Length + " to image array");
            }
            Debug.Log("Finished initialisation cycle!");
            libraries.Add(new SharpDXBase("SharpDX"));
            libraries.Add(new VulkanBase("Vulkan"));
            Config = config;
            BeforeRender();
            if (modeRender == RenderMode.DirectX)
            {
                Debug.Log("Initialising DirectX Render Mode");
                //Initialise the direct x renderer
                libraryGet("SharpDX").Initialise(config);
            }
            else if (modeRender == RenderMode.Software)
            {
                Debug.Log("Initialising Software Render Mode");
                modeRender = RenderMode.DirectX;
                Initialize(config);
            }
            else if (modeRender == RenderMode.Vulkan)
            {
                Debug.Log("Initialising Vulkan Render Mode only supporting 3D for the time being which is not implemented!");
                //Initialise the vulkan renderer
                libraryGet("Vulkan").Initialise(config);
            }
            AfterRender();
        }

        public virtual void BeforeRender()
        {

        }

        public virtual void AfterRender()
        {

        }

        // <summary>
        // Gets an image loaded from the Assets folder.
        // </summary>
        public static string GetImage(string fileName)
        {
            string returnedPath = null;
            foreach (string path in imagesPath)
            {
                string filename = Path.GetFileName(path);
                if (fileName == filename)
                {
                    returnedPath = path;
                }
                else if (fileName != filename)
                {
                    string filepath = Path.GetFileNameWithoutExtension(path);
                    returnedPath = filepath;
                }
            }
            return returnedPath;
        }

        public static IRenderingLibrary libraryGet(string name)
        {
            IRenderingLibrary renderLibrary = null;
            foreach(IRenderingLibrary renderLib in libraries)
            {
                if(renderLib.getName() == name)
                {
                    renderLibrary = renderLib;
                }
            }

            if(renderLibrary == null)
            {
                Debug.Error("Failed to get the library");
            }
            return renderLibrary;
        }

        public virtual void Run(AppConfiguration config)
        {
            Initialize(config);
        }

        public virtual void Run()
        {
            Initialize(new AppConfiguration("Subright Engine Window"));
        }

        public static void AssignNewKey(KeyCode code)
        {
            //Assign a new key and check if the existing exists!
            if (checkKeyExists(code))
            {
                //Key exists break
                Console.WriteLine("Key already exists!");
                return;
            }
            else
            {
                //Key doesnt exist so create one
                Console.WriteLine("Created key or registered key!");
                //Register key here
                codes.Add(code);
            }
        }

        public static void AssignNewKey(string codeName, Keys keyPos, Keys keyNeg)
        {
            KeyCode code = new KeyCode(codeName, keyPos, keyNeg);
            AssignNewKey(code);
        }

        public static void RemoveOldKey(string name)
        {
            if (checkKeyExists(name))
            {
                Console.WriteLine("Key exists removing...");
                foreach (KeyCode code_ in codes)
                {
                    if (code_.name == name)
                    {
                        codes.Remove(code_);
                        Console.WriteLine("Deregistering, " + code_.name + " or well deleting...");
                    }
                }
            }
            else
            {
                Console.WriteLine("Nothing to remove so not doing anything!!!");
                return;
            }
        }

        public static bool checkKeyExists(string name)
        {
            bool keyExists = false;
            if (getCodeFromName(name) == null)
            {
                keyExists = false;
            }
            else
            {
                keyExists = true;
            }
            return keyExists;
        }

        public static bool checkKeyExists(KeyCode code)
        {
            return checkKeyExists(code.name);
        }

        public static KeyCode getCodeFromName(string name)
        {
            KeyCode returnKey = null;
            foreach (KeyCode code in codes)
            {
                if (code.name == name)
                {
                    returnKey = code;
                }
            }

            if (returnKey == null)
            {
                //Returned key but theres no key here
                Console.WriteLine("No key here!");
            }

            return returnKey;
        }

        public static KeyCode getCodeFromName(KeyCode codeGet)
        {
            KeyCode returnKey = null;
            foreach (KeyCode code in codes)
            {
                if (codeGet != null)
                {
                    if (code.name == codeGet.name)
                    {
                        returnKey = code;
                    }
                }
                else
                {
                    Console.WriteLine("The code is not avaliable!");
                }
            }

            if (returnKey == null)
            {
                //Returned key but theres no key here
                Console.WriteLine("No key avail");
            }

            return returnKey;
        }
    }
}
