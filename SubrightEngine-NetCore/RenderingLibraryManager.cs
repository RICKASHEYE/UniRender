using SharpDX.Direct2D1;
using SubrightEngine.DirectX;
using SubrightEngine.Types;
using SubrightEngine.VulkanBranch;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
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
        private static AppConfiguration _appConfiguration = new AppConfiguration("Subright Engine Game Window");
        public static Dimension currentDimension;
        public static Graphics gVulkan;

        public static AppConfiguration Config
        {
            get
            {
                return _appConfiguration;
            }
        }

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
                Debug.Log("3D is not supported at this time.");
            }
        }

        private void Draw(Graphics g)
        {
            Draw();
            if (modeRender == RenderMode.Vulkan)
            {
                if (gVulkan == null)
                {
                    gVulkan = g; 
                } 
            }
        }

        /// <summary>
        /// This function draws a pixel directly.
        /// </summary>
        public static void DirectDrawPixel(int x, int y, SubrightEngine.Types.Color32 color)
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
                if (gVulkan != null)
                {
                    SolidBrush brush = new SolidBrush(System.Drawing.Color.FromArgb((int)color.R, (int)color.G, (int)color.B));
                    Vector2 size = new Vector2(x + 1, y + 1);
                    Vector2 pos = new Vector2(x, y);
                    gVulkan.FillRectangle(brush, new RectangleF(pos.x, pos.y, size.x, size.y));
                    brush.Dispose(); 
                }
            }
        }

        //2D drawer for vulkan
        /*public static void DirectDrawPixel(int x, int y, Color color)
        {
            if(modeRender == RenderMode.Vulkan)
            {
                SolidBrush brush = new SolidBrush(System.Drawing.Color.FromArgb(color.R, color.G, color.B));
                gVulkan.FillRectangle(brush, new RectangleF(x, y, x + 1, y + 1));
                brush.Dispose();
            }
        }*/

        //static List<string> imagesPath = new List<string>();
        public virtual void Initialize(AppConfiguration config)
        {
            Debug.Log("General initialisation starting");
            string assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            //string assetsFolder = Path.Combine(assemblyFolder, "Assets");
            MiddleInit();
            Debug.Log("Finished initialisation cycle!");
            if (modeRender == RenderMode.DirectX)
            {
                if (currentDimension == Dimension.TWOD)
                {
                    Debug.Log("Initialising DirectX Render Mode 2D");
                    SDLBase baseDX = new SDLBase("SharpDX");
                    SDLBase.DrawEvent += delegate { Draw(); };
                    //Initialise the direct x renderer
                    libraries.Add(baseDX);
                    SDLBase renderLibrary = (SDLBase)libraryGet("SharpDX");
                    renderLibrary.Initialise(config); 
                }else if(currentDimension == Dimension.THREED)
                {
                    Debug.Log("Initialising DirectX Render Mode 3D");
                    Direct3D12Window window = new Direct3D12Window();
                    window.Initialize(new SharpDX.Windows.RenderForm());
                }
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
                VulkanBase baseVK = new VulkanBase("Vulkan");
                VulkanBase.DrawEvent += Draw;
                libraries.Add(baseVK);
                libraryGet("Vulkan").Initialise(config);
            }

            if (config != null)
            {
                _appConfiguration = config ?? new AppConfiguration();
            }
            else
            {
                AppConfiguration newConfig = new AppConfiguration("Subright Engine Window");
                _appConfiguration = newConfig ?? new AppConfiguration();
            }
        }

        public virtual void MiddleInit()
        {

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
