using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace SaizeriyaPj
{
    public partial class Form1 : Form
    {
        // 特徴量マッチングインスタンス
        Matching OneMatching;

        // 元画像
        Bitmap SrcBmp, TargetBmp;
        Bitmap ShowingSrcBmp, ShowingTargetBmp;

        // マスクで切り抜いた画像
        Bitmap CroppedSrcBmp, CroppedTargetBmp;

        // マッチング対応画像
        Bitmap MatchingResultBmp;

        // 射影変換画像　これとCroppedTargetBmpを比較することになる
        Bitmap WarpedSrcBmp;

        // 元画像の最大表示サイズ
        const int MaxWidth = 320;
        const int MaxHeight = 240;

        // 特徴量の使用率
        double FeatureRate = 0.1;

        public Form1()
        {
            InitializeComponent();

            // コンポーネントのサイズ指定
            pictureBox_Src.Width = MaxWidth;
            pictureBox_Src.Height = MaxHeight;
            pictureBox_Target.Width = MaxWidth;
            pictureBox_Target.Height = MaxHeight;
        }


        /// <summary>
        /// マッチングボタンイベント
        /// </summary>
        private void button_ExeMatching_Click(object sender, EventArgs e)
        {
            try
            {
                // マスクに基づいて画像のクロップを行う
                int x, y, width, height;
                Rectangle rect;

                // Src
                var maskBmpSrc = new Bitmap(SrcBmp.Width, SrcBmp.Height);
                x = (int)(ShowingMaskSrcRect.X / ScaleSrc);
                y = (int)(ShowingMaskSrcRect.Y / ScaleSrc);
                width = (int)(ShowingMaskSrcRect.Width / ScaleSrc);
                height = (int)(ShowingMaskSrcRect.Height / ScaleSrc);
                rect = new Rectangle(x, y, width, height);
                CroppedSrcBmp = SrcBmp.Clone(rect, SrcBmp.PixelFormat);
                var resizedCroppedSrcBmp = ReduceImage(CroppedSrcBmp, 800, 800);

                // Target
                var maskBmpTarget = new Bitmap(TargetBmp.Width, TargetBmp.Height);
                x = (int)(ShowingMaskTargetRect.X / ScaleTarget);
                y = (int)(ShowingMaskTargetRect.Y / ScaleTarget);
                width = (int)(ShowingMaskTargetRect.Width / ScaleTarget);
                height = (int)(ShowingMaskTargetRect.Height / ScaleTarget);
                rect = new Rectangle(x, y, width, height);
                CroppedTargetBmp = TargetBmp.Clone(rect, TargetBmp.PixelFormat);
                var resizedCroppedTargetBmp = ReduceImage(CroppedTargetBmp, 800, 800);

                // マッチング実行
                OneMatching = new Matching(resizedCroppedSrcBmp.ToMat(), resizedCroppedTargetBmp.ToMat(), FeatureRate);
                OneMatching.RunMutching();

                // 結果表示
                MatchingResultBmp = OneMatching.MatchingResultMat.ToBitmap();
                pictureBox_MatchingResult.Image = MatchingResultBmp;

                // 射影変換
                OneMatching.FitSrcToTarget();
                WarpedSrcBmp = OneMatching.WarpedSrcMat.ToBitmap();

                // 差分画像作成　画像が大きすぎると処理が遅いのである程度縮小する
                var subimage = new Form2(WarpedSrcBmp, resizedCroppedTargetBmp);
                subimage.Show();
            }
            catch (Exception)
            {
                MessageBox.Show("マッチング失敗");
            }
        }


        Bitmap ReduceImage(Bitmap bmp, int width, int height)
        {
            int w = bmp.Width;
            int h = bmp.Height;

            double widthRate = (double)w / (double)width;
            double heightRate = (double)h / (double)height;

            // 縦長
            if (widthRate < heightRate)
            {
                int retWidth = (int)(w / heightRate);
                int retHeight = (int)(h / heightRate);
                var ret = new Bitmap(retWidth, retHeight, bmp.PixelFormat);
                Graphics g = Graphics.FromImage(ret);
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(bmp, 0, 0, retWidth, retHeight);
                g.Dispose();
                return ret;
            }

            // 横長
            if (widthRate > heightRate)
            {
                int retWidth = (int)(w / widthRate);
                int retHeight = (int)(h / widthRate);
                var ret = new Bitmap(retWidth, retHeight, bmp.PixelFormat);
                Graphics g = Graphics.FromImage(ret);
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(bmp, 0, 0, retWidth, retHeight);
                g.Dispose();
                return ret;
            }

            // もともと小さい
            return bmp;
        }
        
        /// <summary>
        /// ソース画像選択
        /// </summary>
        private void button_InputSrc_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.Title = "ソース画像選択";
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                var srcPath = ofd.FileName;
                SrcBmp = new Bitmap(srcPath);
                SetShowingSrcBmp();
                InitSrcMask();
            }
        }

        /// <summary>
        /// ターゲット画像選択
        /// </summary>
        private void button_InputTarget_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.Title = "ターゲット画像選択";
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                var targetPath = ofd.FileName;
                TargetBmp = new Bitmap(targetPath);
                SetShowingTargetBmp();
                InitTargetMask();
            }
        }

        /// <summary>
        /// 表示用のShowingSrcBmpを初期化する
        /// </summary>
        void SetShowingSrcBmp()
        {
            // Src
            if (SrcBmp.Width <= MaxWidth && SrcBmp.Height <= MaxHeight)
            {
                ShowingSrcBmp = new Bitmap(SrcBmp);
            }
            else
            {
                // 表示用画像の圧縮率
                double widthRate = (double)MaxWidth / (double)SrcBmp.Width;
                double heightRate = (double)MaxHeight / (double)SrcBmp.Height;

                if (widthRate < heightRate)
                    ScaleSrc = widthRate;
                else
                    ScaleSrc = heightRate;

                int scaledWidth = (int)(SrcBmp.Width * ScaleSrc);
                int scaledHeight = (int)(SrcBmp.Height * ScaleSrc);

                ShowingSrcBmp = new Bitmap(SrcBmp, scaledWidth, scaledHeight);
            }
            pictureBox_Src.Image = ShowingSrcBmp;
        }

        /// <summary>
        /// 表示用のShowingTargetBmpを初期化する
        /// </summary>
        void SetShowingTargetBmp()
        {
            // Target
            if (TargetBmp.Width <= MaxWidth && TargetBmp.Height <= MaxHeight)
            {
                ShowingTargetBmp = new Bitmap(TargetBmp);
            }
            else
            {
                // 表示用画像の圧縮率
                double widthRate = (double)MaxWidth / (double)TargetBmp.Width;
                double heightRate = (double)MaxHeight / (double)TargetBmp.Height;

                if (widthRate < heightRate)
                    ScaleTarget = widthRate;
                else
                    ScaleTarget = heightRate;

                int scaledWidth = (int)(TargetBmp.Width * ScaleTarget);
                int scaledHeight = (int)(TargetBmp.Height * ScaleTarget);

                ShowingTargetBmp = new Bitmap(TargetBmp, scaledWidth, scaledHeight);
            }
            pictureBox_Target.Image = ShowingTargetBmp;
        }

        // マスク関連

        // マスク表示部
        PictureBox pictureBox_MaskSrc, pictureBox_MaskTarget;

        // ドラッグで囲んだ領域
        Rectangle ShowingMaskSrcRect, ShowingMaskTargetRect;

        // ShowingMaskXxxRectを実際の元画像に合わせて拡大したりした最終的な領域
        Rectangle MaskSrcRect, MaskTargetRect;

        // 画面への表示倍率
        double ScaleSrc, ScaleTarget;

        // 表示用マスク画像
        Bitmap ShowingMaskSrcBmp, ShowingMaskTargetBmp;

        // マスクの色
        Color MaskEdgeColor = Color.FromArgb(255, 255, 0, 255); // 境界の色
        Color MaskFillColor = Color.FromArgb(192, 32, 0, 32);  // 非選択部の色
        Color UnmaskColor = Color.FromArgb(255, 0, 0, 0);       // 選択部の色　この色を透明色とする

        /// <summary>
        /// Srcマスク初期化
        /// </summary>
        void InitSrcMask()
        {
            // マスク表示部設定
            pictureBox_MaskSrc = new PictureBox();
            pictureBox_MaskSrc.Parent = pictureBox_Src;
            pictureBox_MaskSrc.BackColor = Color.Transparent;
            pictureBox_MaskSrc.Size = ShowingSrcBmp.Size;
            pictureBox_MaskSrc.Location = new System.Drawing.Point(0, 0);

            // マスク画像設定
            ShowingMaskSrcBmp = new Bitmap(ShowingSrcBmp.Width, ShowingSrcBmp.Height);
            ShowingMaskSrcRect = new Rectangle(0, 0, ShowingMaskSrcBmp.Width, ShowingMaskSrcBmp.Height);
            MaskSrcRect = new Rectangle(0, 0, SrcBmp.Width, SrcBmp.Height);
            Graphics gSrc = Graphics.FromImage(ShowingMaskSrcBmp);
            gSrc.FillRectangle(new SolidBrush(UnmaskColor), ShowingMaskSrcRect);
            gSrc.Dispose();
            ShowingMaskSrcBmp.MakeTransparent(UnmaskColor);
            pictureBox_MaskSrc.Image = ShowingMaskSrcBmp;

            // イベント追加
            pictureBox_MaskSrc.MouseDown += pictureBox_MaskSrc_MouseDown;
            pictureBox_MaskSrc.MouseMove += pictureBox_MaskSrc_MouseMove;
            pictureBox_MaskSrc.MouseUp += pictureBox_MaskSrc_MouseUp;

            // マスクを強制的に最前面へ
            pictureBox_MaskSrc.BringToFront();
        }

        /// <summary>
        /// Targetマスク初期化
        /// </summary>
        void InitTargetMask()
        {
            // マスク表示部設定
            pictureBox_MaskTarget = new PictureBox();
            pictureBox_MaskTarget.Parent = pictureBox_Target;
            pictureBox_MaskTarget.BackColor = Color.Transparent;
            pictureBox_MaskTarget.Size = ShowingTargetBmp.Size;
            pictureBox_MaskTarget.Location = new System.Drawing.Point(0, 0);

            // マスク画像設定
            ShowingMaskTargetBmp = new Bitmap(ShowingTargetBmp.Width, ShowingTargetBmp.Height);
            ShowingMaskTargetRect = new Rectangle(0, 0, ShowingMaskTargetBmp.Width, ShowingMaskTargetBmp.Height);
            MaskTargetRect = new Rectangle(0, 0, TargetBmp.Width, TargetBmp.Height);
            Graphics gTarget = Graphics.FromImage(ShowingMaskTargetBmp);
            gTarget.FillRectangle(new SolidBrush(UnmaskColor), ShowingMaskTargetRect);
            gTarget.Dispose();
            ShowingMaskTargetBmp.MakeTransparent(UnmaskColor);
            pictureBox_MaskTarget.Image = ShowingMaskTargetBmp;

            // イベント追加
            pictureBox_MaskTarget.MouseDown += pictureBox_MaskTarget_MouseDown;
            pictureBox_MaskTarget.MouseMove += pictureBox_MaskTarget_MouseMove;
            pictureBox_MaskTarget.MouseUp += pictureBox_MaskTarget_MouseUp;

            // マスクを強制的に最前面へ
            pictureBox_MaskTarget.BringToFront();
        }

        // Srcマスクイベント
        System.Drawing.Point BeginPointSrc;
        bool IsMouseDownSrc = false;

        private void pictureBox_MaskSrc_MouseDown(object sender, MouseEventArgs e)
        {
            // ドラッグ開始位置を保持
            BeginPointSrc = e.Location;
            IsMouseDownSrc = true;
        }
        private void pictureBox_MaskSrc_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsMouseDownSrc)
            {
                // 現在の位置を取得してマスク用Rectangleを作成
                System.Drawing.Point EndPoint = e.Location;
                int x, y, width, height;
                x = Math.Min(BeginPointSrc.X, EndPoint.X);
                y = Math.Min(BeginPointSrc.Y, EndPoint.Y);
                width = Math.Abs(BeginPointSrc.X - EndPoint.X);
                height = Math.Abs(BeginPointSrc.Y - EndPoint.Y);
                var rect = new Rectangle(x, y, width, height);

                // 描画
                ShowingMaskSrcBmp = new Bitmap(ShowingMaskSrcBmp.Width, ShowingMaskSrcBmp.Height);
                Graphics g = Graphics.FromImage(ShowingMaskSrcBmp);
                var allRect = new Rectangle(0, 0, pictureBox_MaskSrc.Width, pictureBox_MaskSrc.Height);
                g.FillRectangle(new SolidBrush(MaskFillColor), allRect);       // 外側
                g.FillRectangle(new SolidBrush(UnmaskColor), rect);  // 内側
                g.DrawRectangle(new Pen(MaskEdgeColor, 2.0f), rect); // 枠
                g.Dispose();
                ShowingMaskSrcBmp.MakeTransparent(UnmaskColor);
                pictureBox_MaskSrc.Image = ShowingMaskSrcBmp;
            }
        }
        private void pictureBox_MaskSrc_MouseUp(object sender, MouseEventArgs e)
        {
            if (IsMouseDownSrc)
            {
                // 現在の位置を取得してマスク用Rectangleを作成
                System.Drawing.Point EndPoint = e.Location;
                int x, y, width, height;
                x = Math.Min(BeginPointSrc.X, EndPoint.X);
                y = Math.Min(BeginPointSrc.Y, EndPoint.Y);
                width = Math.Abs(BeginPointSrc.X - EndPoint.X);
                height = Math.Abs(BeginPointSrc.Y - EndPoint.Y);
                ShowingMaskSrcRect = new Rectangle(x, y, width, height);

                IsMouseDownSrc = false;
            }
        }

        // Targetマスクイベント
        System.Drawing.Point BeginPointTarget;
        bool IsMouseDownTarget = false;
        private void pictureBox_MaskTarget_MouseDown(object sender, MouseEventArgs e)
        {
            // ドラッグ開始位置を保持
            BeginPointTarget = e.Location;
            IsMouseDownTarget = true;
        }
        private void pictureBox_MaskTarget_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsMouseDownTarget)
            {
                // 現在の位置を取得してマスク用Rectangleを作成
                System.Drawing.Point EndPoint = e.Location;
                int x, y, width, height;
                x = Math.Min(BeginPointTarget.X, EndPoint.X);
                y = Math.Min(BeginPointTarget.Y, EndPoint.Y);
                width = Math.Abs(BeginPointTarget.X - EndPoint.X);
                height = Math.Abs(BeginPointTarget.Y - EndPoint.Y);
                var rect = new Rectangle(x, y, width, height);

                // 描画
                ShowingMaskTargetBmp = new Bitmap(ShowingMaskTargetBmp.Width, ShowingMaskTargetBmp.Height);
                Graphics g = Graphics.FromImage(ShowingMaskTargetBmp);
                var allRect = new Rectangle(0, 0, pictureBox_MaskSrc.Width, pictureBox_MaskSrc.Height);
                g.FillRectangle(new SolidBrush(MaskFillColor), allRect);       // 外側
                g.FillRectangle(new SolidBrush(UnmaskColor), rect);  // 内側
                g.DrawRectangle(new Pen(MaskEdgeColor, 2.0f), rect); // 枠
                g.Dispose();
                ShowingMaskTargetBmp.MakeTransparent(UnmaskColor);
                pictureBox_MaskTarget.Image = ShowingMaskTargetBmp;
            }
        }
        private void pictureBox_MaskTarget_MouseUp(object sender, MouseEventArgs e)
        {
            if (IsMouseDownTarget)
            {
                // 現在の位置を取得してマスク用Rectangleを作成
                System.Drawing.Point EndPoint = e.Location;
                int x, y, width, height;
                x = Math.Min(BeginPointTarget.X, EndPoint.X);
                y = Math.Min(BeginPointTarget.Y, EndPoint.Y);
                width = Math.Abs(BeginPointTarget.X - EndPoint.X);
                height = Math.Abs(BeginPointTarget.Y - EndPoint.Y);
                ShowingMaskTargetRect = new Rectangle(x, y, width, height);

                IsMouseDownTarget = false;
            }
        }

    }
}
