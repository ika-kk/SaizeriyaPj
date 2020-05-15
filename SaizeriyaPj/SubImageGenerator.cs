using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace SaizeriyaPj
{
    class SubImageGenerator
    {

        const int R = 0;
        const int G = 1;
        const int B = 2;
        const int H = 3;
        const int S = 4;
        const int V = 5;

        // 入力画像
        Bitmap SrcBmp, TargetBmp;

        // 入力画像の各チャンネル(PixelFormat = 8bit)
        public Bitmap[] SrcImages { get; private set; }
        public Bitmap[] TargetImages { get; private set; }

        // 差分画像
        public Bitmap[] SubImages { get; private set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="src"></param>
        /// <param name="target"></param>
        public SubImageGenerator(Bitmap src, Bitmap target)
        {
            SrcImages = new Bitmap[6];
            TargetImages = new Bitmap[6];
            SubImages = new Bitmap[6];

            SrcBmp = new Bitmap(src);
            TargetBmp = new Bitmap(target);

            GetRGBHSV();
            SetSubImages();
        }

        /// <summary>
        /// 各チャンネルの画像を取得する
        /// </summary>
        void GetRGBHSV()
        {
            // RGB取得
            Mat[] srcBgrArray = Cv2.Split(SrcBmp.ToMat());
            Mat[] targetBgrArray = Cv2.Split(TargetBmp.ToMat());

            // HSV取得
            Mat tempSrcHsv = new Mat();
            Mat tempTargetHsv = new Mat();
            Cv2.CvtColor(SrcBmp.ToMat(), tempSrcHsv, ColorConversionCodes.BGR2HSV);
            Cv2.CvtColor(TargetBmp.ToMat(), tempTargetHsv, ColorConversionCodes.BGR2HSV);
            Mat[] srcHsvArray = Cv2.Split(tempSrcHsv);
            Mat[] targetHsvArray = Cv2.Split(tempTargetHsv);

            // 格納
            SrcImages[R] = srcBgrArray[2].ToBitmap();
            SrcImages[G] = srcBgrArray[1].ToBitmap();
            SrcImages[B] = srcBgrArray[0].ToBitmap();
            SrcImages[H] = srcHsvArray[0].ToBitmap();
            SrcImages[S] = srcHsvArray[1].ToBitmap();
            SrcImages[V] = srcHsvArray[2].ToBitmap();
            TargetImages[R] = targetBgrArray[2].ToBitmap();
            TargetImages[G] = targetBgrArray[1].ToBitmap();
            TargetImages[B] = targetBgrArray[0].ToBitmap();
            TargetImages[H] = targetHsvArray[0].ToBitmap();
            TargetImages[S] = targetHsvArray[1].ToBitmap();
            TargetImages[V] = targetHsvArray[2].ToBitmap();
        }

        /// <summary>
        /// 全チャンネルごとの差分画像を作成する
        /// </summary>
        void SetSubImages()
        {
            SubImages[R] = GetAbsSubImage(SrcImages[R], TargetImages[R]);
            SubImages[G] = GetAbsSubImage(SrcImages[G], TargetImages[G]);
            SubImages[B] = GetAbsSubImage(SrcImages[B], TargetImages[B]);
            SubImages[H] = GetAbsSubImage(SrcImages[H], TargetImages[H]);
            SubImages[S] = GetAbsSubImage(SrcImages[S], TargetImages[S]);
            SubImages[V] = GetAbsSubImage(SrcImages[V], TargetImages[V]);
        }

        /// <summary>
        /// bmp0とbmp1の絶対差分画像を作成する
        /// </summary>
        Bitmap GetAbsSubImage(Bitmap bmp0, Bitmap bmp1)
        {
            // 万一入力画像のサイズが違っていたら終了
            if (!bmp0.Size.Equals(bmp1.Size))
            {
                return null;
            }

            int width = bmp0.Width;
            int height = bmp0.Height;
            var ret = new Bitmap(width, height);

            // 書き出し先Bitmap
            var dstBmp = new Bitmap(width, height, PixelFormat.Format8bppIndexed);
            SetPaletteGrayscale(dstBmp);

            // Bitmapをメモリにロックする
            BitmapData data0 = bmp0.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, bmp0.PixelFormat);
            BitmapData data1 = bmp1.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, bmp1.PixelFormat);
            BitmapData dataDest = dstBmp.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, bmp1.PixelFormat);    // 書き出し先

            // バイト列に書き出す
            int stride = data0.Stride;
            var pxs0 = new byte[stride * height];
            var pxs1 = new byte[stride * height];
            var pxsDst = new byte[data1.Stride * bmp1.Height];  // 書き出し先
            System.Runtime.InteropServices.Marshal.Copy(data0.Scan0, pxs0, 0, pxs0.Length);
            System.Runtime.InteropServices.Marshal.Copy(data1.Scan0, pxs1, 0, pxs1.Length);

            // 走査
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int pos = x + stride * y;
                    var val0 = pxs0[pos];
                    var val1 = pxs1[pos];
                    byte valDst = (byte)Math.Abs(val0 - val1);
                    pxsDst[pos] = valDst;
                }
            }
            System.Runtime.InteropServices.Marshal.Copy(pxsDst, 0, dataDest.Scan0, pxsDst.Length);

            // メモリロック解除
            bmp0.UnlockBits(data0);
            bmp1.UnlockBits(data1);
            dstBmp.UnlockBits(dataDest);

            return dstBmp;
        }

        /// <summary>
        /// bmpのカラーパレットを256階調グレースケールに設定する。
        /// </summary>
        /// <param name="bmp"></param>
        void SetPaletteGrayscale(Bitmap bmp)
        {
            var pal = bmp.Palette;
            for (int i = 0; i < 256; i++)
            {
                pal.Entries[i] = Color.FromArgb(i, i, i);
            }
            bmp.Palette = pal;
        }

    }
}