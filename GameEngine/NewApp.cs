﻿using System;
using System.Windows.Forms;
using SharpDX.Windows;

namespace SubrightEngine
{
    /// <summary>
    /// Root clas for a Demo Application
    /// </summary>
    public abstract class NewApp
    {
        private readonly AppTime clock = new AppTime();
        private FormWindowState _currentFormWindowState;
        private bool _disposed;
        public Form _form;
        private float _frameAccumulator;
        private int _frameCount;
        private static AppConfiguration _appConfiguration;

        /// <summary>
        ///   Performs object finalization.
        /// </summary>
        ~NewApp()
        {
            if (!_disposed)
            {
                Dispose(false);
                _disposed = true;
            }
        }

        /// <summary>
        ///   Disposes of object resources.
        /// </summary>
        public void Dispose()
        {
            if (!_disposed)
            {
                Dispose(true);
                _disposed = true;
            }
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///   Disposes of object resources.
        /// </summary>
        /// <param name = "disposeManagedResources">If true, managed resources should be
        ///   disposed of in addition to unmanaged resources.</param>
        protected virtual void Dispose(bool disposeManagedResources)
        {
            if (disposeManagedResources)
            {
                if (_form != null)
                    _form.Dispose();
            }
        }

        /// <summary>
        /// Return the Handle to display to.
        /// </summary>
        protected IntPtr DisplayHandle
        {
            get
            {
                return _form.Handle;
            }
        }

        /// <summary>
        /// Gets the config.
        /// </summary>
        /// <value>The config.</value>
        public static AppConfiguration Config
        {
            get
            {
                return _appConfiguration;
            }
        }

        /// <summary>
        ///   Gets the number of seconds passed since the last frame.
        /// </summary>
        public float FrameDelta { get; private set; }

        /// <summary>
        ///   Gets the number of seconds passed since the last frame.
        /// </summary>
        public float FramePerSecond { get; private set; }

        /// <summary>
        /// Create Form for this demo.
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        protected virtual Form CreateForm(AppConfiguration config)
        {
            return new RenderForm(config.Title)
            {
                ClientSize = new System.Drawing.Size(config.Width, config.Height)
            };
        }

        /// <summary>
        /// Runs the demo with default presentation
        /// </summary>
        public void Run()
        {
            Run(new AppConfiguration());
        }

        /// <summary>
        /// Runs the demo.
        /// </summary>
        public virtual void Run(AppConfiguration demoConfiguration)
        {
            _appConfiguration = demoConfiguration ?? new AppConfiguration();
            _form = CreateForm(_appConfiguration);
            Initialize(_appConfiguration);

            bool isFormClosed = false;
            bool formIsResizing = false;

            _form.MouseClick += HandleMouseClick;
            _form.KeyDown += HandleKeyDown;
            _form.KeyUp += HandleKeyUp;
            _form.Resize += (o, args) =>
            {
                if (_form.WindowState != _currentFormWindowState)
                {
                    HandleResize(o, args);
                }

                _currentFormWindowState = _form.WindowState;
            };

            _form.ResizeBegin += (o, args) => { formIsResizing = true; };
            _form.ResizeEnd += (o, args) =>
            {
                formIsResizing = false;
                HandleResize(o, args);
            };

            _form.Closed += (o, args) => { isFormClosed = true; };

            LoadContent();

            clock.Start();
            BeginRun();
            RenderLoop.Run(_form, () =>
            {
                if (isFormClosed)
                {
                    return;
                }

                OnUpdate();
                if (!formIsResizing)
                    Render();
            });

            UnloadContent();
            EndRun();

            // Dispose explicity
            Dispose();
        }

        /// <summary>
        ///   In a derived class, implements logic to initialize the sample.
        /// </summary>
        protected abstract void Initialize(AppConfiguration demoConfiguration);

        protected virtual void LoadContent()
        {
        }

        protected virtual void UnloadContent()
        {
        }

        /// <summary>
        ///   In a derived class, implements logic to update any relevant sample state.
        /// </summary>
        protected virtual void Update(AppTime time)
        {
        }

        /// <summary>
        ///   In a derived class, implements logic to render the sample.
        /// </summary>
        protected virtual void Draw(AppTime time)
        {
        }

        protected virtual void BeginRun()
        {
        }

        protected virtual void EndRun()
        {
        }

        /// <summary>
        ///   In a derived class, implements logic that should occur before all
        ///   other rendering.
        /// </summary>
        protected virtual void BeginDraw()
        {
        }

        /// <summary>
        ///   In a derived class, implements logic that should occur after all
        ///   other rendering.
        /// </summary>
        protected virtual void EndDraw()
        {
        }

        /// <summary>
        ///   Quits the sample.
        /// </summary>
        public void Exit()
        {
            _form.Close();
        }

        /// <summary>
        ///   Updates sample state.
        /// </summary>
        private void OnUpdate()
        {
            FrameDelta = (float)clock.Update();
            Update(clock);
        }

        protected System.Drawing.Size RenderingSize
        {
            get
            {
                return _form.ClientSize;
            }
        }

        /// <summary>
        ///   Renders the sample.
        /// </summary>
        private void Render()
        {
            _frameAccumulator += FrameDelta;
            ++_frameCount;
            if (_frameAccumulator >= 1.0f)
            {
                FramePerSecond = _frameCount / _frameAccumulator;

                _form.Text = _appConfiguration.Title + " - FPS: " + FramePerSecond;
                _frameAccumulator = 0.0f;
                _frameCount = 0;
            }

            BeginDraw();
            Draw(clock);
            EndDraw();
        }

        protected virtual void MouseClick(MouseEventArgs e)
        {
        }

        protected virtual void KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Exit();
        }

        protected virtual void KeyUp(KeyEventArgs e)
        {
        }

        /// <summary>
        ///   Handles a mouse click event.
        /// </summary>
        /// <param name = "sender">The sender.</param>
        /// <param name = "e">The <see cref = "System.Windows.Forms.MouseEventArgs" /> instance containing the event data.</param>
        private void HandleMouseClick(object sender, MouseEventArgs e)
        {
            MouseClick(e);
        }

        /// <summary>
        ///   Handles a key down event.
        /// </summary>
        /// <param name = "sender">The sender.</param>
        /// <param name = "e">The <see cref = "System.Windows.Forms.KeyEventArgs" /> instance containing the event data.</param>
        private void HandleKeyDown(object sender, KeyEventArgs e)
        {
            KeyDown(e);
        }

        /// <summary>
        ///   Handles a key up event.
        /// </summary>
        /// <param name = "sender">The sender.</param>
        /// <param name = "e">The <see cref = "System.Windows.Forms.KeyEventArgs" /> instance containing the event data.</param>
        private void HandleKeyUp(object sender, KeyEventArgs e)
        {
            KeyUp(e);
        }

        private void HandleResize(object sender, EventArgs e)
        {
            if (_form.WindowState == FormWindowState.Minimized)
            {
                return;
            }

            // UnloadContent();

            //_configuration.WindowWidth = _form.ClientSize.Width;
            //_configuration.WindowHeight = _form.ClientSize.Height;

            //if( Context9 != null ) {
            //    userInterfaceRenderer.Dispose();

            //    Context9.PresentParameters.BackBufferWidth = _configuration.WindowWidth;
            //    Context9.PresentParameters.BackBufferHeight = _configuration.WindowHeight;

            //    Context9.Device.Reset( Context9.PresentParameters );

            //    userInterfaceRenderer = new UserInterfaceRenderer9( Context9.Device, _form.ClientSize.Width, _form.ClientSize.Height );
            //} else if( Context10 != null ) {
            //    userInterfaceRenderer.Dispose();

            //    Context10.SwapChain.ResizeBuffers( 1, WindowWidth, WindowHeight, Context10.SwapChain.Description.ModeDescription.Format, Context10.SwapChain.Description.Flags );


            //    userInterfaceRenderer = new UserInterfaceRenderer10( Context10.Device, _form.ClientSize.Width, _form.ClientSize.Height );
            //}

            // LoadContent();
        }
    }
}