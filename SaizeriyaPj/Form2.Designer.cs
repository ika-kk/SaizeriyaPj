namespace SaizeriyaPj
{
    partial class Form2
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
            this.textBox_Bin = new System.Windows.Forms.TextBox();
            this.label_Bin = new System.Windows.Forms.Label();
            this.label_Median = new System.Windows.Forms.Label();
            this.textBox_Median = new System.Windows.Forms.TextBox();
            this.button_Update = new System.Windows.Forms.Button();
            this.textBox_Blob = new System.Windows.Forms.TextBox();
            this.label_Blob = new System.Windows.Forms.Label();
            this.pictureBox_Result = new System.Windows.Forms.PictureBox();
            this.trackBar_Bin = new System.Windows.Forms.TrackBar();
            this.trackBar_Median = new System.Windows.Forms.TrackBar();
            this.trackBar_Blob = new System.Windows.Forms.TrackBar();
            this.trackBar_Dilate = new System.Windows.Forms.TrackBar();
            this.textBox_Dilate = new System.Windows.Forms.TextBox();
            this.label_Dilate = new System.Windows.Forms.Label();
            this.button_Decide = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Result)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Bin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Median)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Blob)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Dilate)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox_Bin
            // 
            this.textBox_Bin.Location = new System.Drawing.Point(202, 8);
            this.textBox_Bin.Name = "textBox_Bin";
            this.textBox_Bin.Size = new System.Drawing.Size(37, 19);
            this.textBox_Bin.TabIndex = 34;
            this.textBox_Bin.Text = "128";
            this.textBox_Bin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label_Bin
            // 
            this.label_Bin.AutoSize = true;
            this.label_Bin.Font = new System.Drawing.Font("游ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_Bin.Location = new System.Drawing.Point(12, 9);
            this.label_Bin.Name = "label_Bin";
            this.label_Bin.Size = new System.Drawing.Size(80, 17);
            this.label_Bin.TabIndex = 33;
            this.label_Bin.Text = "2値化　閾値";
            // 
            // label_Median
            // 
            this.label_Median.AutoSize = true;
            this.label_Median.Font = new System.Drawing.Font("游ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_Median.Location = new System.Drawing.Point(12, 30);
            this.label_Median.Name = "label_Median";
            this.label_Median.Size = new System.Drawing.Size(177, 17);
            this.label_Median.TabIndex = 33;
            this.label_Median.Text = "ノイズ除去　カーネルサイズ";
            // 
            // textBox_Median
            // 
            this.textBox_Median.Location = new System.Drawing.Point(202, 30);
            this.textBox_Median.Name = "textBox_Median";
            this.textBox_Median.Size = new System.Drawing.Size(37, 19);
            this.textBox_Median.TabIndex = 34;
            this.textBox_Median.Text = "3";
            this.textBox_Median.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // button_Update
            // 
            this.button_Update.Location = new System.Drawing.Point(450, 9);
            this.button_Update.Name = "button_Update";
            this.button_Update.Size = new System.Drawing.Size(75, 82);
            this.button_Update.TabIndex = 38;
            this.button_Update.Text = "更新";
            this.button_Update.UseVisualStyleBackColor = true;
            this.button_Update.Click += new System.EventHandler(this.button_Update_Click);
            // 
            // textBox_Blob
            // 
            this.textBox_Blob.Location = new System.Drawing.Point(202, 52);
            this.textBox_Blob.Name = "textBox_Blob";
            this.textBox_Blob.Size = new System.Drawing.Size(37, 19);
            this.textBox_Blob.TabIndex = 37;
            this.textBox_Blob.Text = "10";
            this.textBox_Blob.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label_Blob
            // 
            this.label_Blob.AutoSize = true;
            this.label_Blob.Font = new System.Drawing.Font("游ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_Blob.Location = new System.Drawing.Point(12, 52);
            this.label_Blob.Name = "label_Blob";
            this.label_Blob.Size = new System.Drawing.Size(125, 17);
            this.label_Blob.TabIndex = 36;
            this.label_Blob.Text = "ブロブ面積　下限値";
            // 
            // pictureBox_Result
            // 
            this.pictureBox_Result.Location = new System.Drawing.Point(12, 169);
            this.pictureBox_Result.Name = "pictureBox_Result";
            this.pictureBox_Result.Size = new System.Drawing.Size(512, 512);
            this.pictureBox_Result.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_Result.TabIndex = 39;
            this.pictureBox_Result.TabStop = false;
            // 
            // trackBar_Bin
            // 
            this.trackBar_Bin.Location = new System.Drawing.Point(246, 8);
            this.trackBar_Bin.Maximum = 255;
            this.trackBar_Bin.Name = "trackBar_Bin";
            this.trackBar_Bin.Size = new System.Drawing.Size(198, 45);
            this.trackBar_Bin.TabIndex = 40;
            this.trackBar_Bin.Value = 128;
            this.trackBar_Bin.Scroll += new System.EventHandler(this.trackBar_Bin_Scroll);
            // 
            // trackBar_Median
            // 
            this.trackBar_Median.Location = new System.Drawing.Point(246, 30);
            this.trackBar_Median.Maximum = 21;
            this.trackBar_Median.Name = "trackBar_Median";
            this.trackBar_Median.Size = new System.Drawing.Size(198, 45);
            this.trackBar_Median.TabIndex = 41;
            this.trackBar_Median.Value = 3;
            this.trackBar_Median.Scroll += new System.EventHandler(this.trackBar_Median_Scroll);
            // 
            // trackBar_Blob
            // 
            this.trackBar_Blob.Location = new System.Drawing.Point(246, 52);
            this.trackBar_Blob.Maximum = 500;
            this.trackBar_Blob.Name = "trackBar_Blob";
            this.trackBar_Blob.Size = new System.Drawing.Size(198, 45);
            this.trackBar_Blob.TabIndex = 42;
            this.trackBar_Blob.Value = 10;
            this.trackBar_Blob.Scroll += new System.EventHandler(this.trackBar_Blob_Scroll);
            // 
            // trackBar_Dilate
            // 
            this.trackBar_Dilate.Location = new System.Drawing.Point(246, 74);
            this.trackBar_Dilate.Maximum = 20;
            this.trackBar_Dilate.Name = "trackBar_Dilate";
            this.trackBar_Dilate.Size = new System.Drawing.Size(198, 45);
            this.trackBar_Dilate.TabIndex = 45;
            this.trackBar_Dilate.TickFrequency = 10;
            this.trackBar_Dilate.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar_Dilate.Value = 5;
            this.trackBar_Dilate.Scroll += new System.EventHandler(this.trackBar_Dilate_Scroll);
            // 
            // textBox_Dilate
            // 
            this.textBox_Dilate.Location = new System.Drawing.Point(202, 74);
            this.textBox_Dilate.Name = "textBox_Dilate";
            this.textBox_Dilate.Size = new System.Drawing.Size(37, 19);
            this.textBox_Dilate.TabIndex = 44;
            this.textBox_Dilate.Text = "5";
            this.textBox_Dilate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label_Dilate
            // 
            this.label_Dilate.AutoSize = true;
            this.label_Dilate.Font = new System.Drawing.Font("游ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_Dilate.Location = new System.Drawing.Point(12, 74);
            this.label_Dilate.Name = "label_Dilate";
            this.label_Dilate.Size = new System.Drawing.Size(47, 17);
            this.label_Dilate.TabIndex = 43;
            this.label_Dilate.Text = "膨張量";
            // 
            // button_Decide
            // 
            this.button_Decide.Location = new System.Drawing.Point(183, 112);
            this.button_Decide.Name = "button_Decide";
            this.button_Decide.Size = new System.Drawing.Size(179, 43);
            this.button_Decide.TabIndex = 46;
            this.button_Decide.Text = "マスク適用";
            this.button_Decide.UseVisualStyleBackColor = true;
            this.button_Decide.Click += new System.EventHandler(this.button_Decide_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(543, 693);
            this.Controls.Add(this.button_Decide);
            this.Controls.Add(this.trackBar_Dilate);
            this.Controls.Add(this.textBox_Dilate);
            this.Controls.Add(this.label_Dilate);
            this.Controls.Add(this.pictureBox_Result);
            this.Controls.Add(this.trackBar_Blob);
            this.Controls.Add(this.trackBar_Median);
            this.Controls.Add(this.trackBar_Bin);
            this.Controls.Add(this.button_Update);
            this.Controls.Add(this.textBox_Blob);
            this.Controls.Add(this.label_Blob);
            this.Controls.Add(this.textBox_Median);
            this.Controls.Add(this.textBox_Bin);
            this.Controls.Add(this.label_Median);
            this.Controls.Add(this.label_Bin);
            this.Name = "Form2";
            this.Text = "Form2";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Result)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Bin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Median)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Blob)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Dilate)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBox_Bin;
        private System.Windows.Forms.Label label_Bin;
        private System.Windows.Forms.Label label_Median;
        private System.Windows.Forms.TextBox textBox_Median;
        private System.Windows.Forms.Button button_Update;
        private System.Windows.Forms.TextBox textBox_Blob;
        private System.Windows.Forms.Label label_Blob;
        private System.Windows.Forms.PictureBox pictureBox_Result;
        private System.Windows.Forms.TrackBar trackBar_Bin;
        private System.Windows.Forms.TrackBar trackBar_Median;
        private System.Windows.Forms.TrackBar trackBar_Blob;
        private System.Windows.Forms.TrackBar trackBar_Dilate;
        private System.Windows.Forms.TextBox textBox_Dilate;
        private System.Windows.Forms.Label label_Dilate;
        private System.Windows.Forms.Button button_Decide;
    }
}