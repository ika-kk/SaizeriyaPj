using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SaizeriyaPj
{
    public partial class Form2 : Form
    {
        Bitmap SrcBmp, TargetBmp;
        Bitmap[] SrcImages = new Bitmap[6];
        Bitmap[] TargetImages = new Bitmap[6];
        Bitmap[] SubImages = new Bitmap[6];
        Bitmap[] BinImages = new Bitmap[6];
        Bitmap[] MedianImages = new Bitmap[6];
        Bitmap UnionImage;
        Bitmap BlobImage;
        Bitmap DilatedImage;

        Bitmap MaskImage;

        Color MaskColor = Color.FromArgb(220, 0, 0, 0);  // 非選択部の色
        Color UnmaskColor = Color.FromArgb(255, 255, 255, 255);       // 選択部の色　この色を透明色とする

        string SaveDir = @"C:\temp\saizeriya\";

        const int R = 0;
        const int G = 1;
        const int B = 2;
        const int H = 3;
        const int S = 4;
        const int V = 5;


        public Form2(Bitmap src, Bitmap target)
        {
            InitializeComponent();

            SrcBmp = new Bitmap(src);
            TargetBmp = new Bitmap(target);

            // 差分画像作成
            var sig = new SubImageGenerator(SrcBmp, TargetBmp);
            for (int i = 0; i < SubImages.Length; i++)
            {
                SrcImages[i] = new Bitmap(sig.SrcImages[i]);
                TargetImages[i] = new Bitmap(sig.TargetImages[i]);
                SubImages[i] = new Bitmap(sig.SubImages[i]);
            }

            UpdateImage();
        }

        private void UpdateImage()
        {
            // 2値化
            UpdateBinImage();

            // メディアン
            UpdateMedianImage();

            // 画像統合
            UnionImage = SubImageOps.UnionImages(MedianImages, new bool[6] { true, true, true, false, true, false });

            // ブロブ
            UpdateBlobImage();

            // 膨張
            UpdateDilateImage();

            // 画像表示
            pictureBox_Result.Image = DilatedImage;

        }

        /// <summary>
        /// 2値化画像BinImageを更新する
        /// </summary>
        void UpdateBinImage()
        {
            bool isByte;
            byte thr;
            isByte = byte.TryParse(textBox_Bin.Text, out thr);

            if (isByte && trackBar_Bin.Minimum <= thr && thr <= trackBar_Bin.Maximum)
            {
                BinImages = SubImageOps.Threshold(SubImages, thr);
            }
            // もし入力値が異常値だったら閾値128で2値化する
            else
            {
                thr = 128;
                textBox_Bin.Text = thr.ToString();
                BinImages = SubImageOps.Threshold(SubImages, thr);
            }
            trackBar_Bin.Value = int.Parse(textBox_Bin.Text);
        }

        /// <summary>
        /// メディアン画像MedianImageを更新する
        /// </summary>
        void UpdateMedianImage()
        {
            bool isInt;
            int ksize;
            isInt = int.TryParse(textBox_Median.Text, out ksize);

            if (isInt && trackBar_Median.Minimum <= ksize && ksize <= trackBar_Median.Maximum)
            {
                // もしカーネルサイズが3より小さければメディアンを行わない
                if (ksize < 3)
                {
                    ksize = 0;
                    textBox_Median.Text = ksize.ToString();
                    for (int i = 0; i < MedianImages.Length; i++)
                    {
                        MedianImages[i] = new Bitmap(BinImages[i]);
                    }
                }
                // もしカーネルサイズが偶数なら奇数に揃える
                else if (ksize % 2 == 0)
                {
                    ksize += 1;
                    textBox_Median.Text = ksize.ToString();
                    MedianImages = SubImageOps.Median(BinImages, ksize);
                }
                // カーネルサイズが3以上かつ奇数ならそのまま実行
                else
                {
                    MedianImages = SubImageOps.Median(BinImages, ksize);
                }
            }
            else
            {
                ksize = 3;
                textBox_Median.Text = ksize.ToString();
                MedianImages = SubImageOps.Median(BinImages, ksize);
            }
            trackBar_Median.Value = int.Parse(textBox_Median.Text);
        }

        /// <summary>
        /// ブロブ処理画像BlobImageを更新する
        /// </summary>
        void UpdateBlobImage()
        {
            bool isInt;
            int minArea;
            isInt = int.TryParse(textBox_Blob.Text, out minArea);
            if (isInt && trackBar_Blob.Minimum <= minArea && minArea <= trackBar_Blob.Maximum)
            {
                BlobImage = SubImageOps.GetImageOmitMinimumNoize(UnionImage, minArea);
            }
            else
            {
                minArea = 10;
                textBox_Blob.Text = minArea.ToString();
                BlobImage = SubImageOps.GetImageOmitMinimumNoize(UnionImage, minArea);
            }
            trackBar_Blob.Value = int.Parse(textBox_Blob.Text);
        }

        /// <summary>
        /// 膨張画像DilatedImageを更新する
        /// </summary>
        void UpdateDilateImage()
        {
            bool isInt;
            int iteration;
            isInt = int.TryParse(textBox_Dilate.Text, out iteration);
            if (isInt && trackBar_Dilate.Minimum <= iteration && iteration <= trackBar_Dilate.Maximum)
            {
                DilatedImage = SubImageOps.DilateImage(BlobImage, iteration);
            }
            else
            {
                iteration = 5;
                textBox_Dilate.Text = iteration.ToString();
                DilatedImage = SubImageOps.DilateImage(BlobImage, iteration);
            }
            trackBar_Dilate.Value = int.Parse(textBox_Dilate.Text);
        }

        // ボタン関連
        private void button_Update_Click(object sender, EventArgs e)
        {
            UpdateImage();
        }

        private void button_Decide_Click(object sender, EventArgs e)
        {
            // マスク生成
            MaskImage = SubImageOps.GetColoredMask(DilatedImage, MaskColor, UnmaskColor);

            // 画像保存
            SaveImages(SaveDir);

            var resultForm = new Form3(SrcBmp, TargetBmp, MaskImage, MaskColor, UnmaskColor);
            resultForm.Show();
        }

        // トラックバー関連
        private void trackBar_Bin_Scroll(object sender, EventArgs e)
        {
            int val = trackBar_Bin.Value;
            textBox_Bin.Text = val.ToString();
            UpdateImage();
        }

        private void trackBar_Median_Scroll(object sender, EventArgs e)
        {
            int val = trackBar_Median.Value;
            textBox_Median.Text = val.ToString();
            UpdateImage();
        }

        private void trackBar_Blob_Scroll(object sender, EventArgs e)
        {
            int val = trackBar_Blob.Value;
            textBox_Blob.Text = val.ToString();
            UpdateImage();
        }

        private void trackBar_Dilate_Scroll(object sender, EventArgs e)
        {
            int val = trackBar_Dilate.Value;
            textBox_Dilate.Text = val.ToString();
            UpdateImage();
        }



        /// <summary>
        /// 片っ端から画像を保存する
        /// </summary>
        public void SaveImages(string dir)
        {
            // フォルダの存在確認
            if (!System.IO.Directory.Exists(dir))
            {
                System.IO.Directory.CreateDirectory(dir);
            }

            SrcBmp.Save(dir + "SrcBmp.bmp", ImageFormat.Bmp);
            TargetBmp.Save(dir + "TargetBmp.bmp", ImageFormat.Bmp);

            SrcImages[R].Save(dir + "_SrcR.bmp", ImageFormat.Bmp);
            SrcImages[G].Save(dir + "_SrcG.bmp", ImageFormat.Bmp);
            SrcImages[B].Save(dir + "_SrcB.bmp", ImageFormat.Bmp);
            SrcImages[H].Save(dir + "_SrcH.bmp", ImageFormat.Bmp);
            SrcImages[S].Save(dir + "_SrcS.bmp", ImageFormat.Bmp);
            SrcImages[V].Save(dir + "_SrcV.bmp", ImageFormat.Bmp);

            TargetImages[R].Save(dir + "_TargetR.bmp", ImageFormat.Bmp);
            TargetImages[G].Save(dir + "_TargetG.bmp", ImageFormat.Bmp);
            TargetImages[B].Save(dir + "_TargetB.bmp", ImageFormat.Bmp);
            TargetImages[H].Save(dir + "_TargetH.bmp", ImageFormat.Bmp);
            TargetImages[S].Save(dir + "_TargetS.bmp", ImageFormat.Bmp);
            TargetImages[V].Save(dir + "_TargetV.bmp", ImageFormat.Bmp);

            SubImages[R].Save(dir + "_SubR.bmp", ImageFormat.Bmp);
            SubImages[G].Save(dir + "_SubG.bmp", ImageFormat.Bmp);
            SubImages[B].Save(dir + "_SubB.bmp", ImageFormat.Bmp);
            SubImages[H].Save(dir + "_SubH.bmp", ImageFormat.Bmp);
            SubImages[S].Save(dir + "_SubS.bmp", ImageFormat.Bmp);
            SubImages[V].Save(dir + "_SubV.bmp", ImageFormat.Bmp);

            BinImages[R].Save(dir + "_BinR.bmp", ImageFormat.Bmp);
            BinImages[G].Save(dir + "_BinG.bmp", ImageFormat.Bmp);
            BinImages[B].Save(dir + "_BinB.bmp", ImageFormat.Bmp);
            BinImages[H].Save(dir + "_BinH.bmp", ImageFormat.Bmp);
            BinImages[S].Save(dir + "_BinS.bmp", ImageFormat.Bmp);
            BinImages[V].Save(dir + "_BinV.bmp", ImageFormat.Bmp);

            MedianImages[R].Save(dir + "_MedianR.bmp", ImageFormat.Bmp);
            MedianImages[G].Save(dir + "_MedianG.bmp", ImageFormat.Bmp);
            MedianImages[B].Save(dir + "_MedianB.bmp", ImageFormat.Bmp);
            MedianImages[H].Save(dir + "_MedianH.bmp", ImageFormat.Bmp);
            MedianImages[S].Save(dir + "_MedianS.bmp", ImageFormat.Bmp);
            MedianImages[V].Save(dir + "_MedianV.bmp", ImageFormat.Bmp);

            DilatedImage.Save(dir + "_Dilated.bmp", ImageFormat.Bmp);
        }

    }
}
