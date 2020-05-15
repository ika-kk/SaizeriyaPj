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
    class SubImageOps
    {

        public Bitmap[] MedianImages { get; private set; }

        static int R = 0;
        static int G = 1;
        static int B = 2;
        static int H = 3;
        static int S = 4;
        static int V = 5;

        /// <summary>
        /// 配列subImages内の画像を閾値thrで2値化する
        /// </summary>
        public static Bitmap[] Threshold(Bitmap[] subImages, byte thr)
        {
            var r = new Mat();
            var g = new Mat();
            var b = new Mat();
            var h = new Mat();
            var s = new Mat();
            var v = new Mat();
            Cv2.Threshold(subImages[R].ToMat(), r, thr, 255, ThresholdTypes.Binary);
            Cv2.Threshold(subImages[G].ToMat(), g, thr, 255, ThresholdTypes.Binary);
            Cv2.Threshold(subImages[B].ToMat(), b, thr, 255, ThresholdTypes.Binary);
            Cv2.Threshold(subImages[H].ToMat(), h, thr, 255, ThresholdTypes.Binary);
            Cv2.Threshold(subImages[S].ToMat(), s, thr, 255, ThresholdTypes.Binary);
            Cv2.Threshold(subImages[V].ToMat(), v, thr, 255, ThresholdTypes.Binary);

            var binImages = new Bitmap[6];

            binImages[R] = r.ToBitmap();
            binImages[G] = g.ToBitmap();
            binImages[B] = b.ToBitmap();
            binImages[H] = h.ToBitmap();
            binImages[S] = s.ToBitmap();
            binImages[V] = v.ToBitmap();

            return binImages;
        }

        /// <summary>
        /// 配列subImages内の画像を閾値thrで2値化する
        /// </summary>
        public static Bitmap[] Median(Bitmap[] binImages, int ksize)
        {
            var r = new Mat();
            var g = new Mat();
            var b = new Mat();
            var h = new Mat();
            var s = new Mat();
            var v = new Mat();

            Cv2.MedianBlur(binImages[R].ToMat(), r, ksize);
            Cv2.MedianBlur(binImages[G].ToMat(), g, ksize);
            Cv2.MedianBlur(binImages[B].ToMat(), b, ksize);
            Cv2.MedianBlur(binImages[H].ToMat(), h, ksize);
            Cv2.MedianBlur(binImages[S].ToMat(), s, ksize);
            Cv2.MedianBlur(binImages[V].ToMat(), v, ksize);

            var medianImages = new Bitmap[6];

            medianImages[R] = r.ToBitmap();
            medianImages[G] = g.ToBitmap();
            medianImages[B] = b.ToBitmap();
            medianImages[H] = h.ToBitmap();
            medianImages[S] = s.ToBitmap();
            medianImages[V] = v.ToBitmap();

            return medianImages;
        }

        /// <summary>
        /// images内の画像の白部分をORで統合した画像を得る。
        /// useOrNotのimagesに対応するインデックスの要素をtrueにすると使用し、falseなら無視する。
        /// 例 : useOrNot = {true, true, true, false, false, true} => R, G, B, V を使用する。
        /// </summary>
        public static Bitmap UnionImages(Bitmap[] images, bool[] useOrNot)
        {
            // Matに変換
            var mat = new Mat[images.Length];
            for (int i = 0; i < images.Length; i++)
            {
                mat[i] = images[i].ToMat();
            }

            var unionMat = new Mat(mat[0].Height, mat[0].Width, mat[0].Type());
            for (int i = 0; i < mat.Length; i++)
            {
                if (useOrNot[i])
                    Cv2.BitwiseOr(unionMat, mat[i], unionMat);
            }
            return unionMat.ToBitmap();
        }

        /// <summary>
        /// imageの白部分の面積がminArea以上のものだけ反映した画像を返す
        /// </summary>
        public static Bitmap GetImageOmitMinimumNoize(Bitmap image, int minArea)
        {
            var mat = Cv2.Split(image.ToMat())[0];

            ConnectedComponents cc = Cv2.ConnectedComponentsEx(mat);
            //var maxBlob = cc.GetLargestBlob();
            //var filtered = new Mat();
            //cc.FilterByBlob(mat, filtered, maxBlob);
            //Cv2.ImShow("max", filtered);

            List<ConnectedComponents.Blob> blobs = new List<ConnectedComponents.Blob>();
            for (int i = 0; i < cc.LabelCount; i++)
            {
                int area = cc.Blobs[i].Area;
                if (area >= minArea)
                {
                    blobs.Add(cc.Blobs[i]);
                }
            }

            var filtered = new Mat();
            cc.FilterByBlobs(mat, filtered, blobs);
            return new Bitmap(filtered.ToBitmap());
        }

        /// <summary>
        /// imageにiteration回膨張処理を施す
        /// </summary>
        public static Bitmap DilateImage(Bitmap image, int iteration)
        {
            var mat = new Mat();
            var neiborhood8 = new Mat(new OpenCvSharp.Size(3, 3), MatType.CV_8U);
            Cv2.Dilate(image.ToMat(), mat, null, null, iteration);
            return mat.ToBitmap();
        }

        /// <summary>
        /// imageの白部分をunmaskedColor、黒部分をmaskedColorに変換した画像を得る。
        /// </summary>
        public static Bitmap GetColoredMask(Bitmap image, Color maskColor, Color unmaskColor)
        {
            int width = image.Width;
            int height = image.Height;
            Bitmap ret = new Bitmap(width, height);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    var col = image.GetPixel(x, y);
                    if (col.R > 128)
                        ret.SetPixel(x, y, unmaskColor);
                    else
                        ret.SetPixel(x, y, maskColor);
                }
            }

            return ret;
        }

    }
}
