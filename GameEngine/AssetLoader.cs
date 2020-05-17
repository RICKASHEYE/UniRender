using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using SharpDX.DirectWrite;
using System.Drawing.Text;
using SharpVPK;

namespace SubrightEngine
{
    public static class AssetLoader
    {
        public static List<Type> types_ = new List<Type>();

        public static void Main(string[] args)
        {
            //Load all assets into the game.
            //Do a general load of the main engine
            Debug.Log("Loading General Stuff");
            if (!Directory.Exists("bin"))
            {
                Directory.CreateDirectory("bin");
            }

            Debug.Log("Finished Loading General Stuff");
            Debug.Log("Loading main stuff");
            string game = "";
            Debug.Log("" + args.Length);
            if (args != null || args.Length != 0 || args[1] == "-game")
            {
                if (args != null && args.Length > 1 && args[2] != null && !args[2].Contains(""))
                {
                    game = args[2];
                }
                else if(args.Length > 1 && args[2] != null && !args[2].Contains(""))
                {
                    //Nothing here!
                    MessageBox.Show("Game Not Found! Please retry arguments and try again");
                    Debug.Error("Game Not Found!");
                    Environment.Exit(0);
                }
            }

            if (game == null || game == "")
            {
                //Search for the closest game and run it!
                Debug.Log("Finding avaliable games inside " + Application.StartupPath);
                string[] directories = Directory.GetDirectories(Application.StartupPath);
                foreach (string m in directories)
                {
                    //check if the path includes a file called 'gameinfo'
                    Debug.Log("At path: " + m);
                    if (File.Exists(Path.Combine(m, "gameinfo.txt")))
                    {
                        //Has a game info choose this game and quit
                        game = new DirectoryInfo(m).Name;
                        break;
                    }
                }
            }

            //When found the game info load the game otherwise pull a message box saying no game found!
            if (game == "")
            {
                MessageBox.Show("No Game Found... please create a directory with a valid gameinfo.txt");
                Debug.Error("No Game Found");
                Environment.Exit(0);
            }
            else
            {
                Debug.Log("Game Found: " + game + " Loading game...");
                //load the gameinfo.txt file from directory
                string gameDirectory = Path.Combine(Application.StartupPath, game);
                string locationGameInfo = Path.Combine(gameDirectory, "gameinfo.txt");
                if(!File.Exists(locationGameInfo))
                {
                    MessageBox.Show("No gameinfo.txt found!");
                    Debug.Error("No GameInfo Found!");
                    Environment.Exit(0);
                }
                else
                {
                    Debug.Log("GameInfo Found!!! Reading...");
                    List<string> lines = File.ReadLines(locationGameInfo).ToList();
                    string gameName = "";
                    foreach(string m in lines)
                    {
                        string[] arguments = m.Split(' ');
                        switch (arguments[0].ToLower())
                        {
                            case "gamename":
                                if(arguments[1] != null && !arguments[1].Contains(""))
                                {
                                    gameName = arguments[1];
                                }
                                break;
                        }
                    }

                    if(gameName == null || gameName.Contains(""))
                    {
                        gameName = "Subright Engine Window";
                    }
                    Debug.Log("Checking for thirdparty assets..");
                    List<string> directories = Directory.GetFiles(gameDirectory, "*.vpk*", SearchOption.AllDirectories).ToList();
                    //load the vpks and extract all files into a format inside the game folder
                    foreach(string m in directories)
                    {
                        var archive = new VpkArchive();
                        archive.Load(m);
                        foreach(var directory in archive.Directories)
                        {
                            if (!Directory.Exists(Path.Combine(gameDirectory, directory.Path)))
                            {
                                Directory.CreateDirectory(Path.Combine(gameDirectory, directory.Path));
                            }

                            foreach(var entry in directory.Entries)
                            {
                                Console.WriteLine(entry.ToString());
                                File.WriteAllBytes(Path.Combine(gameDirectory, directory.Path, entry.Filename + "." + entry.Extension),entry.Data);
                            }
                        }
                        Console.WriteLine("Found and converted VPK: " + m);
                        File.Delete(m);
                    }
                    //Find and decompile the vtf's

                    Debug.Log("Finished Checking for thirdparty assets");
                    Debug.Log("Loading Assemblies");
                    string binFolder = Path.Combine(Application.StartupPath, "bin");
                    string binFolderLocal = Path.Combine(gameDirectory, "bin");
                    if(Directory.GetFiles(binFolder).Length > 0) { LoadAddinClasses(binFolder); } else { Debug.Log("No Assemblies found in global bin folder"); MessageBox.Show("No Assemblies found in global bin folder"); Environment.Exit(0); }
                    if(Directory.GetFiles(binFolder).Length > 0) { LoadAddinClasses(binFolderLocal); } else { Debug.Log("No Assemblies found in the local bin folder"); MessageBox.Show("Now Assemblies found in the local bin folder /n Which would result in a black window or a default game!"); }
                    Debug.Log("Finished Loading...");
                }
            }
        }

        private static void LoadAddinClasses(string assemblyFile)
        {
            string[] files = Directory.GetFiles(assemblyFile);
            foreach (string dll in files)
            {
                if (Path.GetExtension(dll) == "dll")
                {
                    Assembly asm = null;
                    Type[] types = null;

                    try
                    {
                        asm = Assembly.LoadFrom(dll);
                        types = asm.GetTypes();
                    }
                    catch (Exception ex)
                    {
                        var msg = $"Unable to load add-in assembly: {Path.GetFileNameWithoutExtension(assemblyFile)}";
                        MessageBox.Show(msg + ex);
                        Debug.Error(msg + ex);
                        return;
                    } 
                }
            }
        }
    }
}
