namespace SofwareEngineeringProj
{
    partial class FaceDetectingPlayer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FaceDetectingPlayer));
            this.mediaPlayer = new AxWMPLib.AxWindowsMediaPlayer();
            this.gbMediaControls = new System.Windows.Forms.GroupBox();
            this.tbMediaPath = new System.Windows.Forms.TextBox();
            this.btnSelectMedia = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.ofdFileSelectDialog = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.mediaPlayer)).BeginInit();
            this.gbMediaControls.SuspendLayout();
            this.SuspendLayout();
            // 
            // mediaPlayer
            // 
            this.mediaPlayer.Dock = System.Windows.Forms.DockStyle.Top;
            this.mediaPlayer.Enabled = true;
            this.mediaPlayer.Location = new System.Drawing.Point(0, 0);
            this.mediaPlayer.Name = "mediaPlayer";
            this.mediaPlayer.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("mediaPlayer.OcxState")));
            this.mediaPlayer.Size = new System.Drawing.Size(565, 300);
            this.mediaPlayer.TabIndex = 0;
            this.mediaPlayer.MediaError += new AxWMPLib._WMPOCXEvents_MediaErrorEventHandler(this.mediaPlayer_MediaError);
            // 
            // gbMediaControls
            // 
            this.gbMediaControls.BackColor = System.Drawing.SystemColors.ControlDark;
            this.gbMediaControls.Controls.Add(this.tbMediaPath);
            this.gbMediaControls.Controls.Add(this.btnSelectMedia);
            this.gbMediaControls.Controls.Add(this.btnStop);
            this.gbMediaControls.Controls.Add(this.btnPause);
            this.gbMediaControls.Controls.Add(this.btnPlay);
            this.gbMediaControls.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbMediaControls.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gbMediaControls.Location = new System.Drawing.Point(0, 300);
            this.gbMediaControls.Name = "gbMediaControls";
            this.gbMediaControls.Size = new System.Drawing.Size(565, 80);
            this.gbMediaControls.TabIndex = 1;
            this.gbMediaControls.TabStop = false;
            this.gbMediaControls.Text = "Controls";
            // 
            // tbMediaPath
            // 
            this.tbMediaPath.Location = new System.Drawing.Point(343, 34);
            this.tbMediaPath.Name = "tbMediaPath";
            this.tbMediaPath.ReadOnly = true;
            this.tbMediaPath.Size = new System.Drawing.Size(210, 20);
            this.tbMediaPath.TabIndex = 4;
            this.tbMediaPath.TabStop = false;
            // 
            // btnSelectMedia
            // 
            this.btnSelectMedia.Location = new System.Drawing.Point(380, 6);
            this.btnSelectMedia.Name = "btnSelectMedia";
            this.btnSelectMedia.Size = new System.Drawing.Size(135, 23);
            this.btnSelectMedia.TabIndex = 3;
            this.btnSelectMedia.TabStop = false;
            this.btnSelectMedia.Text = "Select Media";
            this.btnSelectMedia.UseVisualStyleBackColor = true;
            this.btnSelectMedia.Click += new System.EventHandler(this.btnSelectMedia_Click);
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(240, 19);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 38);
            this.btnStop.TabIndex = 2;
            this.btnStop.TabStop = false;
            this.btnStop.Text = "STOP";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnPause
            // 
            this.btnPause.Enabled = false;
            this.btnPause.Location = new System.Drawing.Point(127, 19);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(75, 38);
            this.btnPause.TabIndex = 1;
            this.btnPause.TabStop = false;
            this.btnPause.Text = "PAUSE";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnPlay.Enabled = false;
            this.btnPlay.Location = new System.Drawing.Point(12, 21);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(82, 36);
            this.btnPlay.TabIndex = 0;
            this.btnPlay.TabStop = false;
            this.btnPlay.Text = "Play";
            this.btnPlay.UseVisualStyleBackColor = false;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // ofdFileSelectDialog
            // 
            this.ofdFileSelectDialog.Filter = "All Media|*.wma;*.wav;*.mp3;*.m4a;*.m4v;*.mov;*.mp4;*.mpg;*.wmv|Music Files (*.wm" +
    "a,*.wav,*.mp3,*.m4a)|*.wma;*.wav;*.mp3;*.m4a|Video Files(*.m4v,*.mov,*.mp4,*.mpg" +
    ",*.wmv)|*.m4v;*.mov;*.mp4;*.mpg;*.wmv";
            // 
            // FaceDetectingPlayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(565, 368);
            this.Controls.Add(this.gbMediaControls);
            this.Controls.Add(this.mediaPlayer);
            this.MaximizeBox = false;
            this.Name = "FaceDetectingPlayer";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Face Detecting Media Player";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.mediaPlayer)).EndInit();
            this.gbMediaControls.ResumeLayout(false);
            this.gbMediaControls.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private AxWMPLib.AxWindowsMediaPlayer mediaPlayer;
        private System.Windows.Forms.GroupBox gbMediaControls;
        private System.Windows.Forms.TextBox tbMediaPath;
        private System.Windows.Forms.Button btnSelectMedia;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.OpenFileDialog ofdFileSelectDialog;
    }
}

