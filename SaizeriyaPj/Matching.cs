using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;
using System.Drawing;

namespace SaizeriyaPj
{
    class Matching
    {
        // 素材画像
        Mat SrcMat, TargetMat;

        // マッチングの結果画像
        public Mat MatchingResultMat;

        // 透視変換画像
        public Mat WarpedSrcMat;

        // 特徴量
        KeyPoint[] KeyPtsSrc, KeyPtsTarget;

        // 使用するマッチング結果の割合。
        // 今回は上位0.25に設定し、上位25%の特徴点を使用してマッチングを実施した。。
        double UseRate;

        // マッチング結果
        IEnumerable<DMatch> SelectedMatched;

        /// <summary>
        /// コンストラクタ。Mat類と使用マッチング結果の割合を初期化する。
        /// </summary>
        public Matching(Mat src, Mat target, double useRate)
        {
            SrcMat = src.Clone();
            TargetMat = target.Clone();
            MatchingResultMat = new Mat();
            WarpedSrcMat = new Mat();
            UseRate = useRate;  // useRate = 0.25
        }

        /// <summary>
        /// SrcとTargetのマッチングを行う。
        /// </summary>
        public void RunMutching()
        {
            // Akazeで特徴抽出
            var akaze = AKAZE.Create();
            var descriptorSrc = new Mat();
            var descriptorTarget = new Mat();
            akaze.DetectAndCompute(SrcMat, null, out KeyPtsSrc, descriptorSrc);
            akaze.DetectAndCompute(TargetMat, null, out KeyPtsTarget, descriptorTarget);

            // 総当たりマッチング実行
            var matcher = DescriptorMatcher.Create("BruteForce");
            var matches = matcher.Match(descriptorSrc, descriptorTarget);

            // 結果を昇順にソートし、上位からある割合(UseRate)の結果のみを使用する。
            SelectedMatched = matches
                .OrderBy(p => p.Distance)
                .Take((int)(matches.Length * UseRate));

            // SrcとTargetの対応する特徴点を描画する
            Cv2.DrawMatches(
                SrcMat, KeyPtsSrc,
                TargetMat, KeyPtsTarget,
                SelectedMatched, MatchingResultMat);
        }

        /// <summary>
        /// Srcに対してTargetに合うよう射影変換を適用する
        /// </summary>
        public void FitSrcToTarget()
        {
            // 使用する特徴点の量だけベクトル用意
            int size = SelectedMatched.Count();
            var getPtsSrc = new Vec2f[size];
            var getPtsTarget = new Vec2f[size];

            // SrcとTarget画像の対応する特徴点の座標を取得し、ベクトル配列に格納していく。
            int count = 0;
            foreach (var item in SelectedMatched)
            {
                var ptSrc = KeyPtsSrc[item.QueryIdx].Pt;
                var ptTarget = KeyPtsTarget[item.TrainIdx].Pt;
                getPtsSrc[count][0] = ptSrc.X;
                getPtsSrc[count][1] = ptSrc.Y;
                getPtsTarget[count][0] = ptTarget.X;
                getPtsTarget[count][1] = ptTarget.Y;
                count++;
            }

            // SrcをTargetにあわせこむ変換行列homを取得する。ロバスト推定法はRANZAC。
            var hom = Cv2.FindHomography(
                InputArray.Create(getPtsSrc),
                InputArray.Create(getPtsTarget),
                HomographyMethods.Ransac);

            // 行列homを用いてSrcに射影変換を適用する。
            WarpedSrcMat = new Mat();
            Cv2.WarpPerspective(
                SrcMat, WarpedSrcMat, hom,
                new OpenCvSharp.Size(TargetMat.Width, TargetMat.Height));
        }
    }
}
