using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;
using System.Drawing;
using System.Drawing.Imaging;
using OpenCvSharp.Extensions;

namespace SaizeriyaPj
{
    class RegionSetter
    {
        string SaveDir = @"C:\Users\Kyouta.K\source\repos\SaizeriyaPj\SaizeriyaPj\image\";

        System.Drawing.Point LeftUp, RightUp, RightDown, LeftDown;
        Bitmap OrigBmp;
        Mat CvMat;
        Mat GrayMat;
        Mat HSVMat;
        Mat HMat, SMat, VMat;
        Mat BinMat;

        public RegionSetter(Bitmap bmp)
        {
            OrigBmp = new Bitmap(bmp);
            CvMat = OrigBmp.ToMat();

            GetStdMats();

            var sobelMat = new Mat();
            Cv2.Sobel(CvMat, sobelMat, GrayMat.Type(), 1, 1);

            sobelMat.SaveImage(@"C:\Users\Kyouta.K\source\repos\SaizeriyaPj\SaizeriyaPj\image\sobel.bmp");

        }

        void GetStdMats()
        {
            // グレースケール画像
            GrayMat = new Mat();
            Cv2.CvtColor(CvMat, GrayMat, ColorConversionCodes.BGR2GRAY);
            GrayMat.SaveImage(SaveDir + "gray.bmp");

            // HSV画像
            HSVMat = new Mat();
            Cv2.CvtColor(CvMat, HSVMat, ColorConversionCodes.BGR2HSV);
            HSVMat.SaveImage(SaveDir + "hue.bmp");

            var mats = Cv2.Split(HSVMat);
            HMat = mats[0];
            SMat = mats[1];
            VMat = mats[2];
            HMat.SaveImage(SaveDir + "h.bmp");
            SMat.SaveImage(SaveDir + "s.bmp");
            VMat.SaveImage(SaveDir + "v.bmp");
        }


    }
}
