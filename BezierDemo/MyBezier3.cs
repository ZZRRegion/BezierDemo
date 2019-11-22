/***********************************************
* 说    明: 
* CLR版 本：4.0.30319.42000
* 命名空间：BezierDemo
* 类 名 称：MyBezier3
* 创建日期：2019/11/22 13:32:35
* 作    者：ZZR
* 版 本 号：4.0.30319.42000
* 文 件 名：MyBezier3
* 修改记录：
*  R1：
*	  修改作者：
*     修改日期：
*     修改理由：
***********************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Globalization;

namespace BezierDemo
{
    /// <summary>
    /// 三阶贝塞尔曲线
    /// </summary>
    public class MyBezier3 : UIElement,IDraw
    {
        public Point[] Points { get; set; }
        public double Time { get; set; }
        public MyBezier3()
            :this(new Point[]
            {
                new Point(20, 40),
                new Point(100, 200),
                new Point(140, 50),
                new Point(230, 140),
            })
        {

        }
        public MyBezier3(Point[] points)
        {
            this.Points = points;
            CompositionTarget.Rendering += CompositionTarget_Rendering;
        }

        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            this.InvalidateVisual();
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            this.Time += 0.01;
            if(this.Time >= 1)
            {
                this.Time = 0;
            }
            this.DrawBezier(this.Points, drawingContext, Brushes.Black);
            Vector vt1 = this.Points[1] - this.Points[0];
            Point A = this.Points[0] + vt1 * this.Time;//p0-p1的t比例的点A

            Vector vt2 = this.Points[2] - this.Points[1];
            Point B = this.Points[1] + vt2 * this.Time;//p2-p1的t比例点B

            Vector vt3 = this.Points[3] - this.Points[2];
            Point C = this.Points[2] + vt3 * this.Time;//p3-p2的t比例点C
            List<Point> ptlst = new List<Point>()
            {
                A, B, C,
            };
            foreach(Point pt in ptlst)
            {
                drawingContext.DrawEllipse(Brushes.Red, null, pt, 5, 5);
            }
            Pen pen = new Pen(Brushes.Red, 1);
            drawingContext.DrawLine(pen, A, B);
            drawingContext.DrawLine(pen, B, C);

            Vector vvt1 = B - A;
            Point a = A + vvt1 * this.Time;//计算A-B得t比例点a

            Vector vvt2 = C - B;
            Point b = B + vvt2 * this.Time;//计算B-C的t比例点b
            drawingContext.DrawEllipse(Brushes.Blue, null, a, 5, 5);
            drawingContext.DrawEllipse(Brushes.Blue, null, b, 5, 5);
            Pen pen2 = new Pen(Brushes.Blue, 1);
            drawingContext.DrawLine(pen2, a, b);

            Vector vvvt1 = b - a;
            Point cc = a + vvvt1 * this.Time;//计算b-a的t比例点cc,cc就是曲线上的点了
            //drawingContext.DrawEllipse(Brushes.Black, null, cc, 5, 5);

            Point ccc = this.GetBezierPoint();
            drawingContext.DrawEllipse(Brushes.Orange, null, ccc, 5, 5);
            FormattedText formattedText = new FormattedText("t:" + this.Time.ToString("F2"), CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("宋体"), 20, Brushes.Black);
            drawingContext.DrawText(formattedText, this.Points.Last());
        }
        public void DrawBezier(Point[] pts, DrawingContext dc, Brush brush)
        {
            foreach(Point pt in pts)//绘制点
            {
                dc.DrawEllipse(brush, null, pt, 5, 5);
            }
            StreamGeometry sg = new StreamGeometry();
            StreamGeometryContext sgc = sg.Open();
            sgc.BeginFigure(pts[0], true, false);
            sgc.BezierTo(pts[1], pts[2], pts[3], true, true);
            sgc.BeginFigure(pts[0], true, false);
            sgc.PolyLineTo(pts, true, true);
            sgc.Close();
            Pen pen = new Pen(brush, 1);
            dc.DrawGeometry(null, pen, sg);
        }
        /// <summary>
        /// 公式获取贝塞尔曲线点
        /// </summary>
        /// <returns></returns>
        public Point GetBezierPoint()
        {
            Vector[] vectors = new Vector[this.Points.Length];
            for(int i = 0; i < this.Points.Length; i++)
            {
                vectors[i] = (Vector)this.Points[i];
            }
            Vector cc = Math.Pow(1 - this.Time, 3) * vectors[0] +
                3 * this.Time * Math.Pow(1 - this.Time, 2) * vectors[1] +
                3 * Math.Pow(this.Time, 2) * (1 - this.Time) * vectors[2] +
                Math.Pow(this.Time, 3) * vectors[3];
            return (Point)cc;
        }
        #region 变量
        #endregion
        #region 方法
        #endregion
        #region 事件
        #endregion
    }
}
