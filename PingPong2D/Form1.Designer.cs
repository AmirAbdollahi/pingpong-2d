namespace PingPong2D
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.picMain = new System.Windows.Forms.PictureBox();
            this.tmrBall = new System.Windows.Forms.Timer(this.components);
            this.tmrRightPadUp = new System.Windows.Forms.Timer(this.components);
            this.tmrRightPadDown = new System.Windows.Forms.Timer(this.components);
            this.tmrLeftPadUp = new System.Windows.Forms.Timer(this.components);
            this.tmrLeftPadDown = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.picMain)).BeginInit();
            this.SuspendLayout();
            // 
            // picMain
            // 
            this.picMain.BackColor = System.Drawing.Color.Black;
            this.picMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picMain.Location = new System.Drawing.Point(0, 0);
            this.picMain.Name = "picMain";
            this.picMain.Size = new System.Drawing.Size(982, 553);
            this.picMain.TabIndex = 0;
            this.picMain.TabStop = false;
            this.picMain.Paint += new System.Windows.Forms.PaintEventHandler(this.picMain_Paint);
            // 
            // tmrBall
            // 
            this.tmrBall.Interval = 1;
            this.tmrBall.Tick += new System.EventHandler(this.tmrBall_Tick);
            // 
            // tmrRightPadUp
            // 
            this.tmrRightPadUp.Interval = 1;
            this.tmrRightPadUp.Tick += new System.EventHandler(this.tmrRightPadUp_Tick);
            // 
            // tmrRightPadDown
            // 
            this.tmrRightPadDown.Interval = 1;
            this.tmrRightPadDown.Tick += new System.EventHandler(this.tmrRightPadDown_Tick);
            // 
            // tmrLeftPadUp
            // 
            this.tmrLeftPadUp.Interval = 1;
            this.tmrLeftPadUp.Tick += new System.EventHandler(this.tmrLeftPadUp_Tick);
            // 
            // tmrLeftPadDown
            // 
            this.tmrLeftPadDown.Interval = 1;
            this.tmrLeftPadDown.Tick += new System.EventHandler(this.tmrLeftPadDown_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 553);
            this.Controls.Add(this.picMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "Ping Pong 2D";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.picMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picMain;
        private System.Windows.Forms.Timer tmrBall;
        private System.Windows.Forms.Timer tmrRightPadUp;
        private System.Windows.Forms.Timer tmrRightPadDown;
        private System.Windows.Forms.Timer tmrLeftPadUp;
        private System.Windows.Forms.Timer tmrLeftPadDown;
    }
}

