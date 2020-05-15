namespace SaizeriyaPj
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button_ExeMatching = new System.Windows.Forms.Button();
            this.button_InputTarget = new System.Windows.Forms.Button();
            this.button_InputSrc = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox_Target = new System.Windows.Forms.PictureBox();
            this.pictureBox_Src = new System.Windows.Forms.PictureBox();
            this.pictureBox_MatchingResult = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Target)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Src)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_MatchingResult)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button_ExeMatching);
            this.groupBox1.Controls.Add(this.button_InputTarget);
            this.groupBox1.Controls.Add(this.button_InputSrc);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.pictureBox_Target);
            this.groupBox1.Controls.Add(this.pictureBox_Src);
            this.groupBox1.Font = new System.Drawing.Font("游ゴシック", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(658, 356);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "OrigBmp";
            // 
            // button_ExeMatching
            // 
            this.button_ExeMatching.Font = new System.Drawing.Font("游ゴシック", 9F);
            this.button_ExeMatching.Location = new System.Drawing.Point(285, 315);
            this.button_ExeMatching.Name = "button_ExeMatching";
            this.button_ExeMatching.Size = new System.Drawing.Size(90, 35);
            this.button_ExeMatching.TabIndex = 6;
            this.button_ExeMatching.Text = "マッチング";
            this.button_ExeMatching.UseVisualStyleBackColor = true;
            this.button_ExeMatching.Click += new System.EventHandler(this.button_ExeMatching_Click);
            // 
            // button_InputTarget
            // 
            this.button_InputTarget.Font = new System.Drawing.Font("游ゴシック", 9F);
            this.button_InputTarget.Location = new System.Drawing.Point(453, 296);
            this.button_InputTarget.Name = "button_InputTarget";
            this.button_InputTarget.Size = new System.Drawing.Size(75, 23);
            this.button_InputTarget.TabIndex = 5;
            this.button_InputTarget.Text = "読込";
            this.button_InputTarget.UseVisualStyleBackColor = true;
            this.button_InputTarget.Click += new System.EventHandler(this.button_InputTarget_Click);
            // 
            // button_InputSrc
            // 
            this.button_InputSrc.Font = new System.Drawing.Font("游ゴシック", 9F);
            this.button_InputSrc.Location = new System.Drawing.Point(128, 297);
            this.button_InputSrc.Name = "button_InputSrc";
            this.button_InputSrc.Size = new System.Drawing.Size(75, 23);
            this.button_InputSrc.TabIndex = 4;
            this.button_InputSrc.Text = "読込";
            this.button_InputSrc.UseVisualStyleBackColor = true;
            this.button_InputSrc.Click += new System.EventHandler(this.button_InputSrc_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("游ゴシック", 10F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(466, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 18);
            this.label2.TabIndex = 3;
            this.label2.Text = "Target";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("游ゴシック", 10F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(148, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 18);
            this.label1.TabIndex = 2;
            this.label1.Text = "Src";
            // 
            // pictureBox_Target
            // 
            this.pictureBox_Target.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox_Target.Location = new System.Drawing.Point(332, 50);
            this.pictureBox_Target.Name = "pictureBox_Target";
            this.pictureBox_Target.Size = new System.Drawing.Size(320, 240);
            this.pictureBox_Target.TabIndex = 1;
            this.pictureBox_Target.TabStop = false;
            // 
            // pictureBox_Src
            // 
            this.pictureBox_Src.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox_Src.Location = new System.Drawing.Point(6, 50);
            this.pictureBox_Src.Name = "pictureBox_Src";
            this.pictureBox_Src.Size = new System.Drawing.Size(320, 240);
            this.pictureBox_Src.TabIndex = 0;
            this.pictureBox_Src.TabStop = false;
            // 
            // pictureBox_MatchingResult
            // 
            this.pictureBox_MatchingResult.Location = new System.Drawing.Point(13, 376);
            this.pictureBox_MatchingResult.Name = "pictureBox_MatchingResult";
            this.pictureBox_MatchingResult.Size = new System.Drawing.Size(658, 315);
            this.pictureBox_MatchingResult.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_MatchingResult.TabIndex = 1;
            this.pictureBox_MatchingResult.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 703);
            this.Controls.Add(this.pictureBox_MatchingResult);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Target)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Src)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_MatchingResult)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button_ExeMatching;
        private System.Windows.Forms.Button button_InputTarget;
        private System.Windows.Forms.Button button_InputSrc;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox_Target;
        private System.Windows.Forms.PictureBox pictureBox_Src;
        private System.Windows.Forms.PictureBox pictureBox_MatchingResult;
    }
}