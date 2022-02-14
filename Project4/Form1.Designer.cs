
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.constantShading = new System.Windows.Forms.RadioButton();
            this.phongShading = new System.Windows.Forms.RadioButton();
            this.gouraudShading = new System.Windows.Forms.RadioButton();
            this.stopCheckbox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.bitmap)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // bitmap
            // 
            this.bitmap.Location = new System.Drawing.Point(-1, -2);
            this.bitmap.Name = "bitmap";
            this.bitmap.Size = new System.Drawing.Size(1400, 800);
            this.bitmap.TabIndex = 0;
            this.bitmap.TabStop = false;
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
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.constantShading);
            this.groupBox2.Controls.Add(this.phongShading);
            this.groupBox2.Controls.Add(this.gouraudShading);
            this.groupBox2.Location = new System.Drawing.Point(1405, 152);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(267, 134);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Shading model";
            // 
            // constantShading
            // 
            this.constantShading.AutoSize = true;
            this.constantShading.Checked = true;
            this.constantShading.Location = new System.Drawing.Point(6, 26);
            this.constantShading.Name = "constantShading";
            this.constantShading.Size = new System.Drawing.Size(88, 24);
            this.constantShading.TabIndex = 1;
            this.constantShading.TabStop = true;
            this.constantShading.Text = "Constant";
            this.constantShading.UseVisualStyleBackColor = true;
            this.constantShading.CheckedChanged += new System.EventHandler(this.constantShading_CheckedChanged);
            // 
            // phongShading
            // 
            this.phongShading.AutoSize = true;
            this.phongShading.Location = new System.Drawing.Point(6, 86);
            this.phongShading.Name = "phongShading";
            this.phongShading.Size = new System.Drawing.Size(72, 24);
            this.phongShading.TabIndex = 3;
            this.phongShading.Text = "Phong";
            this.phongShading.UseVisualStyleBackColor = true;
            this.phongShading.CheckedChanged += new System.EventHandler(this.phongShading_CheckedChanged);
            // 
            // gouraudShading
            // 
            this.gouraudShading.AutoSize = true;
            this.gouraudShading.Location = new System.Drawing.Point(6, 56);
            this.gouraudShading.Name = "gouraudShading";
            this.gouraudShading.Size = new System.Drawing.Size(87, 24);
            this.gouraudShading.TabIndex = 2;
            this.gouraudShading.Text = "Gouraud";
            this.gouraudShading.UseVisualStyleBackColor = true;
            this.gouraudShading.CheckedChanged += new System.EventHandler(this.gouraudShading_CheckedChanged);
            // 
            // stopCheckbox
            // 
            this.stopCheckbox.AutoSize = true;
            this.stopCheckbox.Location = new System.Drawing.Point(1411, 293);
            this.stopCheckbox.Name = "stopCheckbox";
            this.stopCheckbox.Size = new System.Drawing.Size(133, 24);
            this.stopCheckbox.TabIndex = 6;
            this.stopCheckbox.Text = "Stop animation";
            this.stopCheckbox.UseVisualStyleBackColor = true;
            this.stopCheckbox.CheckedChanged += new System.EventHandler(this.stopCheckbox_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1684, 792);
            this.Controls.Add(this.stopCheckbox);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.bitmap);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bitmap)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox bitmap;
        private System.Windows.Forms.RadioButton staticCameraButton;
        private System.Windows.Forms.RadioButton trackingCameraButton;
        private System.Windows.Forms.RadioButton attachedCameraButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton constantShading;
        private System.Windows.Forms.RadioButton phongShading;
        private System.Windows.Forms.RadioButton gouraudShading;
        private System.Windows.Forms.CheckBox stopCheckbox;
    }
}

