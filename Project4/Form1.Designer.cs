
namespace Project4
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
            this.bitmap = new System.Windows.Forms.PictureBox();
            this.staticCameraButton = new System.Windows.Forms.RadioButton();
            this.trackingCameraButton = new System.Windows.Forms.RadioButton();
            this.attachedCameraButton = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.bitmap)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bitmap
            // 
            this.bitmap.Location = new System.Drawing.Point(-1, -2);
            this.bitmap.Name = "bitmap";
            this.bitmap.Size = new System.Drawing.Size(1400, 800);
            this.bitmap.TabIndex = 0;
            this.bitmap.TabStop = false;
            this.bitmap.Paint += new System.Windows.Forms.PaintEventHandler(this.bitmap_Paint);
            // 
            // staticCameraButton
            // 
            this.staticCameraButton.AutoSize = true;
            this.staticCameraButton.Location = new System.Drawing.Point(6, 26);
            this.staticCameraButton.Name = "staticCameraButton";
            this.staticCameraButton.Size = new System.Drawing.Size(67, 24);
            this.staticCameraButton.TabIndex = 1;
            this.staticCameraButton.TabStop = true;
            this.staticCameraButton.Text = "Static";
            this.staticCameraButton.UseVisualStyleBackColor = true;
            // 
            // trackingCameraButton
            // 
            this.trackingCameraButton.AutoSize = true;
            this.trackingCameraButton.Location = new System.Drawing.Point(6, 56);
            this.trackingCameraButton.Name = "trackingCameraButton";
            this.trackingCameraButton.Size = new System.Drawing.Size(85, 24);
            this.trackingCameraButton.TabIndex = 2;
            this.trackingCameraButton.TabStop = true;
            this.trackingCameraButton.Text = "Tracking";
            this.trackingCameraButton.UseVisualStyleBackColor = true;
            // 
            // attachedCameraButton
            // 
            this.attachedCameraButton.AutoSize = true;
            this.attachedCameraButton.Location = new System.Drawing.Point(6, 86);
            this.attachedCameraButton.Name = "attachedCameraButton";
            this.attachedCameraButton.Size = new System.Drawing.Size(90, 24);
            this.attachedCameraButton.TabIndex = 3;
            this.attachedCameraButton.TabStop = true;
            this.attachedCameraButton.Text = "Attached";
            this.attachedCameraButton.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.staticCameraButton);
            this.groupBox1.Controls.Add(this.attachedCameraButton);
            this.groupBox1.Controls.Add(this.trackingCameraButton);
            this.groupBox1.Location = new System.Drawing.Point(1405, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(267, 134);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Camera mode";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1684, 792);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.bitmap);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bitmap)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox bitmap;
        private System.Windows.Forms.RadioButton staticCameraButton;
        private System.Windows.Forms.RadioButton trackingCameraButton;
        private System.Windows.Forms.RadioButton attachedCameraButton;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}

