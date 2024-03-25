namespace ChessExperiment
{
    partial class GameScreen
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.GameTimer = new System.Windows.Forms.Timer(this.components);
            this.displayChecksLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // GameTimer
            // 
            this.GameTimer.Enabled = true;
            this.GameTimer.Interval = 15;
            this.GameTimer.Tick += new System.EventHandler(this.GameTimer_Tick);
            // 
            // displayChecksLabel
            // 
            this.displayChecksLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.displayChecksLabel.BackColor = System.Drawing.Color.Transparent;
            this.displayChecksLabel.Enabled = false;
            this.displayChecksLabel.Font = new System.Drawing.Font("Modern No. 20", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.displayChecksLabel.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.displayChecksLabel.Location = new System.Drawing.Point(3, 0);
            this.displayChecksLabel.Name = "displayChecksLabel";
            this.displayChecksLabel.Size = new System.Drawing.Size(794, 707);
            this.displayChecksLabel.TabIndex = 1;
            this.displayChecksLabel.Text = "Red Moves First";
            this.displayChecksLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GameScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.displayChecksLabel);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "GameScreen";
            this.Size = new System.Drawing.Size(800, 800);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.GameScreen_Paint);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GameScreen_KeyPress);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.GameScreen_MouseClick);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.GameScreen_MouseDoubleClick);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer GameTimer;
        private System.Windows.Forms.Label displayChecksLabel;
    }
}
