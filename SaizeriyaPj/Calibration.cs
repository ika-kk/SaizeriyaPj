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
    class Calibration
    {
        public Bitmap CalibratedLeftbmp { get; private set; }
        public Bitmap CalibratedRightBmp { get; private set; }

        List<System.Drawing.Point> PtsLeft, PtsRight;
        Mat OrigLeft, OrigRight;
        List<Mat> RegionLeftList, RegionRightList;

        public Calibration(
            Bitmap left, List<System.Drawing.Point> ptsLeft, 
            Bitmap right, List<System.Drawing.Point> ptsRight)
        {
            OrigLeft = left.ToMat();
            OrigRight = right.ToMat();

            PtsLeft = new List<System.Drawing.Point>(ptsLeft);
            PtsRight = new List<System.Drawing.Point>(ptsRight);

        }

        /// <summary>
        /// ポイントのリストをソートする。
        /// 現状は 0:左上 1:右上 2:右下 3:左下
        /// </summary>
        /// <param name="pts"></param>
        void SortPts(List<System.Drawing.Point> pts)
        {
            List<System.Drawing.Point> tempPts = new List<System.Drawing.Point>(pts);
            System.Drawing.Point lt, rt, rb, lb;

            // 左上のポイントを求める
            int ltIdx = -1;
            int min = int.MaxValue;
            for (int i = 0; i < pts.Count; i++)
            {
                int xy = pts[i].X + pts[i].Y;
                if (xy < min)
                {
                    min = xy;
                    ltIdx = i;
                }
            }
            lt = pts[ltIdx];

            // 左上以外のポイントのみ残す
            tempPts.Remove(lt);

            // 左上のポイントから他のポイントへの角度を見ていく
            var tanList = new List<double>();
            foreach (var item in tempPts)
            {
                double rad = Math.Atan2(item.Y - lt.Y, item.X - lt.X);
                tanList.Add(rad);
            }

            //




        }

        void CropRegion()
        {

        }



    }
}
