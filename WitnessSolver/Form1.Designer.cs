namespace WitnessSolver
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ImageBox = new System.Windows.Forms.PictureBox();
            this.btnGetImage = new System.Windows.Forms.Button();
            this.btnSwap = new System.Windows.Forms.Button();
            this.btnNextSolution = new System.Windows.Forms.Button();
            this.btnPrevSolution = new System.Windows.Forms.Button();
            this.ButtonContour = new System.Windows.Forms.Button();
            this.ImageBoxEmgu = new Emgu.CV.UI.ImageBox();
            this.TrackBarThreshold = new System.Windows.Forms.TrackBar();
            this.TrackBarLinking = new System.Windows.Forms.TrackBar();
            this.TextBoxThreshold = new System.Windows.Forms.TextBox();
            this.TextBoxLinking = new System.Windows.Forms.TextBox();
            this.ButtonTetris = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ImageBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImageBoxEmgu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBarThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBarLinking)).BeginInit();
            this.SuspendLayout();
            // 
            // ImageBox
            // 
            this.ImageBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ImageBox.Location = new System.Drawing.Point(12, 12);
            this.ImageBox.Name = "ImageBox";
            this.ImageBox.Size = new System.Drawing.Size(1074, 415);
            this.ImageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ImageBox.TabIndex = 0;
            this.ImageBox.TabStop = false;
            // 
            // btnGetImage
            // 
            this.btnGetImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnGetImage.Location = new System.Drawing.Point(12, 433);
            this.btnGetImage.Name = "btnGetImage";
            this.btnGetImage.Size = new System.Drawing.Size(75, 23);
            this.btnGetImage.TabIndex = 1;
            this.btnGetImage.Text = "Get Image";
            this.btnGetImage.UseVisualStyleBackColor = true;
            this.btnGetImage.Click += new System.EventHandler(this.GetImageButtonClicked);
            // 
            // btnSwap
            // 
            this.btnSwap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSwap.Location = new System.Drawing.Point(1011, 433);
            this.btnSwap.Name = "btnSwap";
            this.btnSwap.Size = new System.Drawing.Size(75, 23);
            this.btnSwap.TabIndex = 2;
            this.btnSwap.Text = "Swap";
            this.btnSwap.UseVisualStyleBackColor = true;
            this.btnSwap.Click += new System.EventHandler(this.SwappedClicked);
            // 
            // btnNextSolution
            // 
            this.btnNextSolution.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNextSolution.Location = new System.Drawing.Point(707, 433);
            this.btnNextSolution.Name = "btnNextSolution";
            this.btnNextSolution.Size = new System.Drawing.Size(75, 23);
            this.btnNextSolution.TabIndex = 3;
            this.btnNextSolution.Text = "Next";
            this.btnNextSolution.UseVisualStyleBackColor = true;
            this.btnNextSolution.Click += new System.EventHandler(this.NextSolutionClicked);
            // 
            // btnPrevSolution
            // 
            this.btnPrevSolution.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrevSolution.Location = new System.Drawing.Point(626, 433);
            this.btnPrevSolution.Name = "btnPrevSolution";
            this.btnPrevSolution.Size = new System.Drawing.Size(75, 23);
            this.btnPrevSolution.TabIndex = 4;
            this.btnPrevSolution.Text = "Prev";
            this.btnPrevSolution.UseVisualStyleBackColor = true;
            this.btnPrevSolution.Click += new System.EventHandler(this.PreviousSolutionClicked);
            // 
            // ButtonContour
            // 
            this.ButtonContour.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ButtonContour.Location = new System.Drawing.Point(93, 433);
            this.ButtonContour.Name = "ButtonContour";
            this.ButtonContour.Size = new System.Drawing.Size(75, 23);
            this.ButtonContour.TabIndex = 5;
            this.ButtonContour.Text = "Contour";
            this.ButtonContour.UseVisualStyleBackColor = true;
            this.ButtonContour.Click += new System.EventHandler(this.ButtonContour_Click);
            // 
            // ImageBoxEmgu
            // 
            this.ImageBoxEmgu.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ImageBoxEmgu.Location = new System.Drawing.Point(12, 12);
            this.ImageBoxEmgu.Name = "ImageBoxEmgu";
            this.ImageBoxEmgu.Size = new System.Drawing.Size(1074, 415);
            this.ImageBoxEmgu.TabIndex = 2;
            this.ImageBoxEmgu.TabStop = false;
            this.ImageBoxEmgu.Visible = false;
            // 
            // TrackBarThreshold
            // 
            this.TrackBarThreshold.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TrackBarThreshold.Location = new System.Drawing.Point(174, 433);
            this.TrackBarThreshold.Maximum = 255;
            this.TrackBarThreshold.Name = "TrackBarThreshold";
            this.TrackBarThreshold.Size = new System.Drawing.Size(104, 45);
            this.TrackBarThreshold.TabIndex = 6;
            this.TrackBarThreshold.TickFrequency = 10;
            this.TrackBarThreshold.Value = 255;
            this.TrackBarThreshold.Scroll += new System.EventHandler(this.TrackBarThreshold_Scroll);
            // 
            // TrackBarLinking
            // 
            this.TrackBarLinking.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TrackBarLinking.Location = new System.Drawing.Point(350, 433);
            this.TrackBarLinking.Maximum = 250;
            this.TrackBarLinking.Name = "TrackBarLinking";
            this.TrackBarLinking.Size = new System.Drawing.Size(104, 45);
            this.TrackBarLinking.TabIndex = 7;
            this.TrackBarLinking.TickFrequency = 10;
            this.TrackBarLinking.Value = 100;
            this.TrackBarLinking.Scroll += new System.EventHandler(this.TrackBarLinking_Scroll);
            // 
            // TextBoxThreshold
            // 
            this.TextBoxThreshold.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TextBoxThreshold.Enabled = false;
            this.TextBoxThreshold.Location = new System.Drawing.Point(284, 433);
            this.TextBoxThreshold.Name = "TextBoxThreshold";
            this.TextBoxThreshold.Size = new System.Drawing.Size(60, 23);
            this.TextBoxThreshold.TabIndex = 8;
            // 
            // TextBoxLinking
            // 
            this.TextBoxLinking.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TextBoxLinking.Enabled = false;
            this.TextBoxLinking.Location = new System.Drawing.Point(460, 434);
            this.TextBoxLinking.Name = "TextBoxLinking";
            this.TextBoxLinking.Size = new System.Drawing.Size(60, 23);
            this.TextBoxLinking.TabIndex = 9;
            // 
            // ButtonTetris
            // 
            this.ButtonTetris.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonTetris.Location = new System.Drawing.Point(930, 434);
            this.ButtonTetris.Name = "ButtonTetris";
            this.ButtonTetris.Size = new System.Drawing.Size(75, 23);
            this.ButtonTetris.TabIndex = 10;
            this.ButtonTetris.Text = "Tetris";
            this.ButtonTetris.UseVisualStyleBackColor = true;
            this.ButtonTetris.Click += new System.EventHandler(this.ButtonTetris_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1098, 468);
            this.Controls.Add(this.ButtonTetris);
            this.Controls.Add(this.TextBoxLinking);
            this.Controls.Add(this.TextBoxThreshold);
            this.Controls.Add(this.TrackBarLinking);
            this.Controls.Add(this.TrackBarThreshold);
            this.Controls.Add(this.ImageBoxEmgu);
            this.Controls.Add(this.ButtonContour);
            this.Controls.Add(this.btnPrevSolution);
            this.Controls.Add(this.btnNextSolution);
            this.Controls.Add(this.btnSwap);
            this.Controls.Add(this.btnGetImage);
            this.Controls.Add(this.ImageBox);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.ImageBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImageBoxEmgu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBarThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBarLinking)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PictureBox ImageBox;
        private Button btnGetImage;
        private Button btnSwap;
        private Button btnNextSolution;
        private Button btnPrevSolution;
        private Button ButtonContour;
        private Emgu.CV.UI.ImageBox ImageBoxEmgu;
        private TrackBar TrackBarThreshold;
        private TrackBar TrackBarLinking;
        private TextBox TextBoxThreshold;
        private TextBox TextBoxLinking;
        private Button ButtonTetris;
    }
}