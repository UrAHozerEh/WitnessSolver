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
            this.ImageBox = new System.Windows.Forms.PictureBox();
            this.btnGetImage = new System.Windows.Forms.Button();
            this.btnSwap = new System.Windows.Forms.Button();
            this.btnNextSolution = new System.Windows.Forms.Button();
            this.btnPrevSolution = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ImageBox)).BeginInit();
            this.SuspendLayout();
            // 
            // ImageBox
            // 
            this.ImageBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ImageBox.Location = new System.Drawing.Point(12, 12);
            this.ImageBox.Name = "ImageBox";
            this.ImageBox.Size = new System.Drawing.Size(776, 397);
            this.ImageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ImageBox.TabIndex = 0;
            this.ImageBox.TabStop = false;
            // 
            // btnGetImage
            // 
            this.btnGetImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnGetImage.Location = new System.Drawing.Point(12, 415);
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
            this.btnSwap.Location = new System.Drawing.Point(713, 415);
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
            this.btnNextSolution.Location = new System.Drawing.Point(409, 415);
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
            this.btnPrevSolution.Location = new System.Drawing.Point(328, 415);
            this.btnPrevSolution.Name = "btnPrevSolution";
            this.btnPrevSolution.Size = new System.Drawing.Size(75, 23);
            this.btnPrevSolution.TabIndex = 4;
            this.btnPrevSolution.Text = "Prev";
            this.btnPrevSolution.UseVisualStyleBackColor = true;
            this.btnPrevSolution.Click += new System.EventHandler(this.PreviousSolutionClicked);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnPrevSolution);
            this.Controls.Add(this.btnNextSolution);
            this.Controls.Add(this.btnSwap);
            this.Controls.Add(this.btnGetImage);
            this.Controls.Add(this.ImageBox);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.ImageBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private PictureBox ImageBox;
        private Button btnGetImage;
        private Button btnSwap;
        private Button btnNextSolution;
        private Button btnPrevSolution;
    }
}