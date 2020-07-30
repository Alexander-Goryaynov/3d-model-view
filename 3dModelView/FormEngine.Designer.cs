namespace _3dModelView
{
    partial class FormEngine
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
            this.canvas = new System.Windows.Forms.PictureBox();
            this.label = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).BeginInit();
            this.SuspendLayout();
            // 
            // canvas
            // 
            this.canvas.Location = new System.Drawing.Point(13, 33);
            this.canvas.Name = "canvas";
            this.canvas.Size = new System.Drawing.Size(989, 573);
            this.canvas.TabIndex = 0;
            this.canvas.TabStop = false;
            this.canvas.Click += new System.EventHandler(this.canvas_Click);
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Location = new System.Drawing.Point(13, 14);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(834, 13);
            this.label.TabIndex = 1;
            this.label.Text = "Горизонтальное движение мыши вращает модель вокруг оси X,   вертикальное - вокруг" +
    " Y,   клик мыши - вокруг оси Z,   клавиша W - приблизить,   S - отодвинуть\r\n";
            // 
            // FormEngine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1014, 618);
            this.Controls.Add(this.label);
            this.Controls.Add(this.canvas);
            this.Name = "FormEngine";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "3D модель";
            this.Load += new System.EventHandler(this.FormEngine_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FormEngine_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox canvas;
        private System.Windows.Forms.Label label;
    }
}