using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SaizeriyaPj
{
    public partial class Form3 : Form
    {
        PictureBox pictureBox_SrcMask, pictureBox_TargetMask;
        Bitmap SrcBmp, TargetBmp, MaskBmp;
        Color MaskColor, UnmaskColor;

        private void checkBox_TargetMask_CheckedChanged(object sender, EventArgs e)
        {
            pictureBox_TargetMask.Visible = checkBox_TargetMask.Checked;
        }

        private void checkBox_SrcMask_CheckedChanged(object sender, EventArgs e)
        {
            pictureBox_SrcMask.Visible = checkBox_SrcMask.Checked;
        }

        public Form3(Bitmap src, Bitmap target, Bitmap mask, Color maskColor, Color unmaskColor)
        {
            InitializeComponent();

            SrcBmp = new Bitmap(src);
            TargetBmp = new Bitmap(target);
            MaskBmp = new Bitmap(mask);
            MaskColor = maskColor;
            UnmaskColor = unmaskColor;

            pictureBox_SrcMask = new PictureBox();
            pictureBox_SrcMask.Location = new Point(0, 0);
            pictureBox_SrcMask.Size = pictureBox_Src.Size;
            pictureBox_SrcMask.Parent = pictureBox_Src;
            pictureBox_SrcMask.BackColor = Color.Transparent;
            pictureBox_SrcMask.SizeMode = PictureBoxSizeMode.Zoom;

            pictureBox_TargetMask = new PictureBox();
            pictureBox_TargetMask.Location = new Point(0, 0);
            pictureBox_TargetMask.Size = pictureBox_Target.Size;
            pictureBox_TargetMask.Parent = pictureBox_Target;
            pictureBox_TargetMask.BackColor = Color.Transparent;
            pictureBox_TargetMask.SizeMode = PictureBoxSizeMode.Zoom;

            MaskBmp.MakeTransparent(UnmaskColor);

            pictureBox_Src.Image = SrcBmp;
            pictureBox_Target.Image = TargetBmp;
            pictureBox_SrcMask.Image = MaskBmp;
            pictureBox_TargetMask.Image = MaskBmp;
        }

    }
}
