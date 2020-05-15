using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System.Drawing;
using System.Drawing.Imaging;

namespace SaizeriyaPj
{
    class BasicProc
    {
        public static Mat BitmapToMat(Bitmap bmp)
        {
            return bmp.ToMat();
        }
    }
}
