using SharpDX.Direct2D1;
using SharpDX.IO;
using SharpDX.MediaFoundation;
using SharpDX.XAudio2;
using System;
using System.IO;
using System.Windows.Forms;
using System.Windows.Threading;
using SubrightEngine.Types;

namespace SubrightEngine.DirectX
{
    public class SharpDXBase : Direct2D1Handler, IRenderingLibrary
    {
        protected override void Draw(AppTime time)
        {
            base.Draw(time);
            DrawEvent?.Invoke(time);
        }

        public delegate void DrawEventHandler(AppTime time);
        public static event DrawEventHandler DrawEvent;

        public string renderName = "SharpDX";
        public string getName()
        {
            return renderName;
        }

        public SharpDXBase(string name)
        {
            renderName = name;
        }

        public void Initialise(AppConfiguration configuration)
        {
            Debug.Log("Starting Rendering");

            SharpDXBase baseScript = new SharpDXBase(renderName);
            baseScript.Run(configuration);
        }

        private XAudio2 xaudio2;
        private MasteringVoice masteringVoice;
        private Stream fileStream;
        private SharpDXAudio audioPlayer;

        private void InitializeXAudio2()
        {
            // This is mandatory when using any of SharpDX.MediaFoundation classes
            MediaManager.Startup();

            // Starts The XAudio2 engine
            xaudio2 = new XAudio2();
            xaudio2.StartEngine();
            masteringVoice = new MasteringVoice(xaudio2);
        }

        public void OpenAudio(string pathway)
        {
            if (File.Exists(pathway))
            {
                if (xaudio2 == null)
                {
                    Debug.Log("Initialising Audio!");
                    InitializeXAudio2();
                }

                if (audioPlayer != null)
                {
                    audioPlayer.Close();
                    //audioPlayer = null;
                }

                if (fileStream != null)
                {
                    fileStream.Close();
                }

                // Ask the user for a video or audio file to play
                fileStream = new NativeFileStream(pathway, NativeFileMode.Open, NativeFileAccess.Read);
                audioPlayer = new SharpDXAudio(xaudio2, fileStream);
            }
            else
            {
                Console.WriteLine("File doesnt exist!");
            }
        }

        public void PlayAudio()
        {
            if (audioPlayer != null)
            {
                audioPlayer.Play(); 
            }
        }

        public void StopAudio()
        {
            audioPlayer.Stop();
        }

        public void Intialise(AppConfiguration config)
        {
            Initialize(config);
        }

        protected override void KeyDown(KeyEventArgs e)
        {
            base.KeyDown(e);
            foreach (KeyCode coedes in RenderingLibraryManager.codes)
            {
                if (coedes.KeyUsePositive != null)
                {
                    if (e.KeyCode == coedes.KeyUsePositive)
                    {
                        coedes.keyAxis = 1;
                        // Debug.Log("Positive");
                        coedes.PositiveDown = true;
                    }
                }

                if (coedes.KeyUseNegative != null)
                {
                    if (e.KeyCode == coedes.KeyUseNegative)
                    {
                        coedes.keyAxis = -1;
                        //Debug.Log("Negative");
                        coedes.NegativeDown = true;
                    }
                }
            }
        }

        protected override void KeyUp(KeyEventArgs e)
        {
            base.KeyUp(e);
            foreach (KeyCode coedes in RenderingLibraryManager.codes)
            {
                if(coedes.KeyUseNegative != null)
                {
                    if(e.KeyCode == coedes.KeyUseNegative)
                    {
                        coedes.NegativeDown = false;
                    }
                }

                if(coedes.KeyUsePositive != null)
                {
                    if(e.KeyCode == coedes.KeyUsePositive)
                    {
                        coedes.PositiveDown = false;
                    }
                }

                if(coedes.PositiveDown == false && coedes.NegativeDown == false)
                {
                    coedes.keyAxis = 0;
                }
            }
        }

        protected override void Initialize(AppConfiguration demoConfiguration)
        {
            base.Initialize(demoConfiguration);
        }

        public RenderTarget getRenderTarget()
        {
            return RenderTarget2D;
        }

        public void Draw()
        {
            //throw new NotImplementedException();
        }
    }
}
