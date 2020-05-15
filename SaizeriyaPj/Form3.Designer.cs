namespace SaizeriyaPj
{
    partial class Form3
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
            this.pictureBox_Src = new System.Windows.Forms.PictureBox();
            this.pictureBox_Target = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBox_SrcMask = new System.Windows.Forms.CheckBox();
            this.checkBox_TargetMask = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Src)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Target)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox_Src
            // 
            this.pictureBox_Src.Location = new System.Drawing.Point(12, 41);
            this.pictureBox_Src.Name = "pictureBox_Src";
            this.pictureBox_Src.Size = new System.Drawing.Size(480, 480);
            this.pictureBox_Src.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_Src.TabIndex = 1;
            this.pictureBox_Src.TabStop = false;
            // 
            // pictureBox_Target
            // 
            this.pictureBox_Target.Location = new System.Drawing.Point(498, 41);
            this.pictureBox_Target.Name = "pictureBox_Target";
            this.pictureBox_Target.Size = new System.Drawing.Size(480, 480);
            this.pictureBox_Target.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_Target.TabIndex = 2;
            this.pictureBox_Target.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("游ゴシック", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(227, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 31);
            this.label2.TabIndex = 4;
            this.label2.Text = "Src";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("游ゴシック", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(696, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 31);
            this.label3.TabIndex = 5;
            this.label3.Text = "Target";
            // 
            // checkBox_SrcMask
            // 
            this.checkBox_SrcMask.AutoSize = true;
            this.checkBox_SrcMask.Checked = true;
            this.checkBox_SrcMask.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_SrcMask.Font = new System.Drawing.Font("游ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.checkBox_SrcMask.Location = new System.Drawing.Point(285, 15);
            this.checkBox_SrcMask.Name = "checkBox_SrcMask";
            this.checkBox_SrcMask.Size = new System.Drawing.Size(99, 20);
            this.checkBox_SrcMask.TabIndex = 6;
            this.checkBox_SrcMask.Text = "マスク有効化";
            this.checkBox_SrcMask.UseVisualStyleBackColor = true;
            this.checkBox_SrcMask.CheckedChanged += new System.EventHandler(this.checkBox_SrcMask_CheckedChanged);
            // 
            // checkBox_TargetMask
            // 
            this.checkBox_TargetMask.AutoSize = true;
            this.checkBox_TargetMask.Checked = true;
            this.checkBox_TargetMask.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_TargetMask.Font = new System.Drawing.Font("游ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.checkBox_TargetMask.Location = new System.Drawing.Point(789, 15);
            this.checkBox_TargetMask.Name = "checkBox_TargetMask";
            this.checkBox_TargetMask.Size = new System.Drawing.Size(99, 20);
            this.checkBox_TargetMask.TabIndex = 7;
            this.checkBox_TargetMask.Text = "マスク有効化";
            this.checkBox_TargetMask.UseVisualStyleBackColor = true;
            this.checkBox_TargetMask.CheckedChanged += new System.EventHandler(this.checkBox_TargetMask_CheckedChanged);
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(991, 647);
            this.Controls.Add(this.checkBox_TargetMask);
            this.Controls.Add(this.checkBox_SrcMask);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBox_Target);
            this.Controls.Add(this.pictureBox_Src);
            this.Name = "Form3";
            this.Text = "Form3";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Src)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Target)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox_Src;
        private System.Windows.Forms.PictureBox pictureBox_Target;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBox_SrcMask;
        private System.Windows.Forms.CheckBox checkBox_TargetMask;
    }
}