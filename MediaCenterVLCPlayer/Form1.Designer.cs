namespace MediaCenterVLCPlayer
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.durationPanel = new System.Windows.Forms.Panel();
            this.Options = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.soundDevicesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.audioChannelsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.monoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stereoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FivePointOnetoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SixPointOnetoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SevenPointOnetoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SPDIFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.audioTracksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.subtitlesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aspectRatioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsDefaultToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.Options.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.AutoSize = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.Controls.Add(this.durationPanel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(292, 273);
            this.panel1.TabIndex = 0;
            // 
            // durationPanel
            // 
            this.durationPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.durationPanel.BackColor = System.Drawing.Color.Maroon;
            this.durationPanel.BackgroundImage = global::MediaCenterVLCPlayer.Properties.Resources.LocationBackground;
            this.durationPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.durationPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.durationPanel.Location = new System.Drawing.Point(0, 253);
            this.durationPanel.Margin = new System.Windows.Forms.Padding(0);
            this.durationPanel.Name = "durationPanel";
            this.durationPanel.Size = new System.Drawing.Size(292, 20);
            this.durationPanel.TabIndex = 0;
            this.durationPanel.Visible = false;
            // 
            // Options
            // 
            this.Options.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Options.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.soundDevicesToolStripMenuItem,
            this.audioChannelsToolStripMenuItem,
            this.audioTracksToolStripMenuItem,
            this.subtitlesToolStripMenuItem,
            this.aspectRatioToolStripMenuItem,
            this.saveAsDefaultToolStripMenuItem});
            this.Options.Name = "Options";
            this.Options.Size = new System.Drawing.Size(276, 232);
            // 
            // soundDevicesToolStripMenuItem
            // 
            this.soundDevicesToolStripMenuItem.Name = "soundDevicesToolStripMenuItem";
            this.soundDevicesToolStripMenuItem.Size = new System.Drawing.Size(275, 38);
            this.soundDevicesToolStripMenuItem.Text = "Sound Devices";
            // 
            // audioChannelsToolStripMenuItem
            // 
            this.audioChannelsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.monoToolStripMenuItem,
            this.stereoToolStripMenuItem,
            this.FivePointOnetoolStripMenuItem,
            this.SixPointOnetoolStripMenuItem,
            this.SevenPointOnetoolStripMenuItem,
            this.SPDIFToolStripMenuItem});
            this.audioChannelsToolStripMenuItem.Name = "audioChannelsToolStripMenuItem";
            this.audioChannelsToolStripMenuItem.Size = new System.Drawing.Size(275, 38);
            this.audioChannelsToolStripMenuItem.Text = "Audio Channels";
            // 
            // monoToolStripMenuItem
            // 
            this.monoToolStripMenuItem.CheckOnClick = true;
            this.monoToolStripMenuItem.Name = "monoToolStripMenuItem";
            this.monoToolStripMenuItem.Size = new System.Drawing.Size(172, 38);
            this.monoToolStripMenuItem.Text = "Mono";
            // 
            // stereoToolStripMenuItem
            // 
            this.stereoToolStripMenuItem.CheckOnClick = true;
            this.stereoToolStripMenuItem.Name = "stereoToolStripMenuItem";
            this.stereoToolStripMenuItem.Size = new System.Drawing.Size(172, 38);
            this.stereoToolStripMenuItem.Text = "Stereo";
            // 
            // FivePointOnetoolStripMenuItem
            // 
            this.FivePointOnetoolStripMenuItem.CheckOnClick = true;
            this.FivePointOnetoolStripMenuItem.Name = "FivePointOnetoolStripMenuItem";
            this.FivePointOnetoolStripMenuItem.Size = new System.Drawing.Size(172, 38);
            this.FivePointOnetoolStripMenuItem.Text = "5.1";
            // 
            // SixPointOnetoolStripMenuItem
            // 
            this.SixPointOnetoolStripMenuItem.CheckOnClick = true;
            this.SixPointOnetoolStripMenuItem.Name = "SixPointOnetoolStripMenuItem";
            this.SixPointOnetoolStripMenuItem.Size = new System.Drawing.Size(172, 38);
            this.SixPointOnetoolStripMenuItem.Text = "6.1";
            // 
            // SevenPointOnetoolStripMenuItem
            // 
            this.SevenPointOnetoolStripMenuItem.CheckOnClick = true;
            this.SevenPointOnetoolStripMenuItem.Name = "SevenPointOnetoolStripMenuItem";
            this.SevenPointOnetoolStripMenuItem.Size = new System.Drawing.Size(172, 38);
            this.SevenPointOnetoolStripMenuItem.Text = "7.1";
            // 
            // SPDIFToolStripMenuItem
            // 
            this.SPDIFToolStripMenuItem.CheckOnClick = true;
            this.SPDIFToolStripMenuItem.Name = "SPDIFToolStripMenuItem";
            this.SPDIFToolStripMenuItem.Size = new System.Drawing.Size(172, 38);
            this.SPDIFToolStripMenuItem.Text = "S/PDIF";
            // 
            // audioTracksToolStripMenuItem
            // 
            this.audioTracksToolStripMenuItem.Name = "audioTracksToolStripMenuItem";
            this.audioTracksToolStripMenuItem.Size = new System.Drawing.Size(275, 38);
            this.audioTracksToolStripMenuItem.Text = "Audio Tracks";
            // 
            // subtitlesToolStripMenuItem
            // 
            this.subtitlesToolStripMenuItem.Name = "subtitlesToolStripMenuItem";
            this.subtitlesToolStripMenuItem.Size = new System.Drawing.Size(275, 38);
            this.subtitlesToolStripMenuItem.Text = "Subtitles";
            // 
            // aspectRatioToolStripMenuItem
            // 
            this.aspectRatioToolStripMenuItem.Name = "aspectRatioToolStripMenuItem";
            this.aspectRatioToolStripMenuItem.Size = new System.Drawing.Size(275, 38);
            this.aspectRatioToolStripMenuItem.Text = "Aspect Ratio";
            // 
            // saveAsDefaultToolStripMenuItem
            // 
            this.saveAsDefaultToolStripMenuItem.Name = "saveAsDefaultToolStripMenuItem";
            this.saveAsDefaultToolStripMenuItem.Size = new System.Drawing.Size(275, 38);
            this.saveAsDefaultToolStripMenuItem.Text = "Save As Default";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel1.ResumeLayout(false);
            this.Options.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Panel durationPanel;
        public System.Windows.Forms.ToolStripMenuItem audioChannelsToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem subtitlesToolStripMenuItem;
        public System.Windows.Forms.ContextMenuStrip Options;
        public System.Windows.Forms.ToolStripMenuItem soundDevicesToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem saveAsDefaultToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem audioTracksToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem aspectRatioToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem monoToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem stereoToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem FivePointOnetoolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem SixPointOnetoolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem SevenPointOnetoolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem SPDIFToolStripMenuItem;
    }
}

