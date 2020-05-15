using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;

namespace SaizeriyaPj
{
    class FeatureMatching
    {
        // 素材画像
        Mat SrcMat;
        Mat TargetMat;

        // 特徴量
        KeyPoint[] KeyPtsSrc;
        KeyPoint[] KeyPtsTarget;

        /// <summary>SrcMatの特徴点の重心</summary>
        public System.Drawing.PointF PtSrc;
        /// <summary>TargetMatの特徴点の重心</summary>
        public System.Drawing.PointF PtTarget;

        /// <summary>マッチングの結果画像</summary>
        public Mat ResultMat;

        public FeatureMatching(Mat src, Mat target)
        {
            // 画像初期化
            SrcMat = src.Clone();
            TargetMat = target.Clone();
            ResultMat = new Mat();

            // 重心初期化
            PtSrc = new System.Drawing.PointF(0.0f, 0.0f);
            PtTarget = new System.Drawing.PointF(0.0f, 0.0f);

            // 特徴点抽出
            var akaze = AKAZE.Create();
            var descriptorSrc = new Mat();
            var descriptorTarget = new Mat();
            akaze.DetectAndCompute(SrcMat, null, out KeyPtsSrc, descriptorSrc);
            akaze.DetectAndCompute(TargetMat, null, out KeyPtsTarget, descriptorTarget);

            // マッチング実行
            var matcher = DescriptorMatcher.Create("BruteForce");
            var matches = matcher.Match(descriptorSrc, descriptorTarget);

            // 結果を昇順にソートし、上位半分の結果を使用する。
            var selectedMatches = matches
                .OrderBy(p => p.Distance)
                //.Take(matches.Length / 2);
                .Take(1);

            // Src - Target 対応画像作成
            Cv2.DrawMatches(SrcMat, KeyPtsSrc, TargetMat, KeyPtsTarget, selectedMatches, ResultMat);

            // 特徴点の重心を求める (Src)
            foreach (var item in selectedMatches)
            {
                int idx = item.QueryIdx;
                PtSrc.X += KeyPtsSrc[idx].Pt.X;
                PtSrc.Y += KeyPtsSrc[idx].Pt.Y;
            }
            PtSrc.X /= (float)selectedMatches.Count();
            PtSrc.Y /= (float)selectedMatches.Count();

            // 特徴点の重心を求める (Target)
            foreach (var item in selectedMatches)
            {
                int idx = item.TrainIdx;
                PtTarget.X += KeyPtsTarget[idx].Pt.X;
                PtTarget.Y += KeyPtsTarget[idx].Pt.Y;
            }
            PtTarget.X /= (float)selectedMatches.Count();
            PtTarget.Y /= (float)selectedMatches.Count();
        }

    }
}
