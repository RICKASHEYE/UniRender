namespace GameEngineEditor
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gameObjectBox = new System.Windows.Forms.ListBox();
            this.gameView = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gameObjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.componentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rendererToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.animatorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.boxColliderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageRendererToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.componentBox = new System.Windows.Forms.ListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.componentInspector = new System.Windows.Forms.ListBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pixelToolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gameView)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.groupBox1.Controls.Add(this.gameObjectBox);
            this.groupBox1.Location = new System.Drawing.Point(570, 32);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(241, 448);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Game Objects";
            // 
            // gameObjectBox
            // 
            this.gameObjectBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gameObjectBox.FormattingEnabled = true;
            this.gameObjectBox.Location = new System.Drawing.Point(3, 16);
            this.gameObjectBox.Name = "gameObjectBox";
            this.gameObjectBox.Size = new System.Drawing.Size(235, 429);
            this.gameObjectBox.TabIndex = 0;
            this.gameObjectBox.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            this.gameObjectBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.gameObjectBox_MouseDoubleClick);
            // 
            // gameView
            // 
            this.gameView.Location = new System.Drawing.Point(10, 32);
            this.gameView.Name = "gameView";
            this.gameView.Size = new System.Drawing.Size(547, 448);
            this.gameView.TabIndex = 2;
            this.gameView.TabStop = false;
            this.gameView.Paint += new System.Windows.Forms.PaintEventHandler(this.gameView_Paint);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.createToolStripMenuItem,
            this.toolsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1349, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveAsProjectToolStripMenuItem,
            this.saveProjectToolStripMenuItem,
            this.loadProjectToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveAsProjectToolStripMenuItem
            // 
            this.saveAsProjectToolStripMenuItem.Name = "saveAsProjectToolStripMenuItem";
            this.saveAsProjectToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveAsProjectToolStripMenuItem.Text = "Save As Project";
            this.saveAsProjectToolStripMenuItem.Click += new System.EventHandler(this.saveAsProjectToolStripMenuItem_Click);
            // 
            // saveProjectToolStripMenuItem
            // 
            this.saveProjectToolStripMenuItem.Name = "saveProjectToolStripMenuItem";
            this.saveProjectToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveProjectToolStripMenuItem.Text = "Save Project";
            this.saveProjectToolStripMenuItem.Click += new System.EventHandler(this.saveProjectToolStripMenuItem_Click);
            // 
            // loadProjectToolStripMenuItem
            // 
            this.loadProjectToolStripMenuItem.Name = "loadProjectToolStripMenuItem";
            this.loadProjectToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.loadProjectToolStripMenuItem.Text = "Load Project";
            // 
            // createToolStripMenuItem
            // 
            this.createToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gameObjectToolStripMenuItem,
            this.componentsToolStripMenuItem});
            this.createToolStripMenuItem.Name = "createToolStripMenuItem";
            this.createToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.createToolStripMenuItem.Text = "Create";
            // 
            // gameObjectToolStripMenuItem
            // 
            this.gameObjectToolStripMenuItem.Name = "gameObjectToolStripMenuItem";
            this.gameObjectToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.gameObjectToolStripMenuItem.Text = "GameObject";
            this.gameObjectToolStripMenuItem.Click += new System.EventHandler(this.gameObjectToolStripMenuItem_Click);
            // 
            // componentsToolStripMenuItem
            // 
            this.componentsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rendererToolStripMenuItem,
            this.animatorToolStripMenuItem,
            this.boxColliderToolStripMenuItem,
            this.imageRendererToolStripMenuItem});
            this.componentsToolStripMenuItem.Name = "componentsToolStripMenuItem";
            this.componentsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.componentsToolStripMenuItem.Text = "Components";
            // 
            // rendererToolStripMenuItem
            // 
            this.rendererToolStripMenuItem.Name = "rendererToolStripMenuItem";
            this.rendererToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.rendererToolStripMenuItem.Text = "Renderer";
            this.rendererToolStripMenuItem.Click += new System.EventHandler(this.rendererToolStripMenuItem_Click);
            // 
            // animatorToolStripMenuItem
            // 
            this.animatorToolStripMenuItem.Name = "animatorToolStripMenuItem";
            this.animatorToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.animatorToolStripMenuItem.Text = "Animator";
            this.animatorToolStripMenuItem.Click += new System.EventHandler(this.animatorToolStripMenuItem_Click);
            // 
            // boxColliderToolStripMenuItem
            // 
            this.boxColliderToolStripMenuItem.Name = "boxColliderToolStripMenuItem";
            this.boxColliderToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.boxColliderToolStripMenuItem.Text = "Box Collider";
            this.boxColliderToolStripMenuItem.Click += new System.EventHandler(this.boxColliderToolStripMenuItem_Click);
            // 
            // imageRendererToolStripMenuItem
            // 
            this.imageRendererToolStripMenuItem.Name = "imageRendererToolStripMenuItem";
            this.imageRendererToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.imageRendererToolStripMenuItem.Text = "Image Renderer";
            this.imageRendererToolStripMenuItem.Click += new System.EventHandler(this.imageRendererToolStripMenuItem_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.groupBox2.Controls.Add(this.componentBox);
            this.groupBox2.Location = new System.Drawing.Point(817, 32);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(241, 451);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Component Editor";
            // 
            // componentBox
            // 
            this.componentBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.componentBox.FormattingEnabled = true;
            this.componentBox.Location = new System.Drawing.Point(3, 16);
            this.componentBox.Name = "componentBox";
            this.componentBox.Size = new System.Drawing.Size(235, 432);
            this.componentBox.TabIndex = 0;
            this.componentBox.SelectedIndexChanged += new System.EventHandler(this.componentBox_SelectedIndexChanged);
            this.componentBox.DoubleClick += new System.EventHandler(this.componentBox_DoubleClick);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.groupBox3.Controls.Add(this.componentInspector);
            this.groupBox3.Location = new System.Drawing.Point(1064, 32);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(241, 451);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Component Inspector";
            this.groupBox3.Enter += new System.EventHandler(this.groupBox3_Enter);
            // 
            // componentInspector
            // 
            this.componentInspector.Dock = System.Windows.Forms.DockStyle.Fill;
            this.componentInspector.FormattingEnabled = true;
            this.componentInspector.Location = new System.Drawing.Point(3, 16);
            this.componentInspector.Name = "componentInspector";
            this.componentInspector.Size = new System.Drawing.Size(235, 432);
            this.componentInspector.TabIndex = 0;
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pixelToolToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // pixelToolToolStripMenuItem
            // 
            this.pixelToolToolStripMenuItem.Name = "pixelToolToolStripMenuItem";
            this.pixelToolToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.pixelToolToolStripMenuItem.Text = "Pixel Tool";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.ClientSize = new System.Drawing.Size(1349, 495);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.gameView);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Main";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gameView)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox gameView;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gameObjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem componentsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rendererToolStripMenuItem;
        private System.Windows.Forms.ListBox gameObjectBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox componentBox;
        private System.Windows.Forms.ToolStripMenuItem animatorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem boxColliderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem imageRendererToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListBox componentInspector;
        private System.Windows.Forms.ToolStripMenuItem saveProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsProjectToolStripMenuItem;
        public System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pixelToolToolStripMenuItem;
    }
}

