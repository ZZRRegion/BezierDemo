/***********************************************
* 说    明: 
* CLR版 本：4.0.30319.42000
* 命名空间：BezierDemo
* 类 名 称：MyBezier2
* 创建日期：2019/11/22 10:38:09
* 作    者：ZZR
* 版 本 号：4.0.30319.42000
* 文 件 名：MyBezier2
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
    /// 二阶贝塞尔
    /// </summary>
    public class MyBezier2 : UIElement,IDraw
    {
        public Point[] Points { get; set; }
        public double Time { get; set; }
        public MyBezier2()
            :this(new Point[]{
                 new Point(30, 45),
                new Point(100, 100),
                new Point(230, 170),
            })
        {
        }
        public MyBezier2(Point[] pts)
        {
            this.Points = pts;
            CompositionTarget.Rendering += CompositionTarget_Rendering;
        }

        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            this.InvalidateVisual();
        }
        #region 变量
        #endregion
        #region 方法
        #endregion
        protected override void OnRender(DrawingContext drawingContext)
        {
            this.Time += 0.01;
            if(this.Time >= 1)
            {
                this.Time = 0;
            }
            this.DrawBezier(this.Points, drawingContext, Brushes.Black);
            Vector vt1 = this.Points[1] - this.Points[0];
            Point A = this.Points[0] + vt1 * this.Time;//计算p0-p1上t比例的点

            Vector vt2 = this.Points[2] - this.Points[1];
            Point B = this.Points[1] + vt2 * this.Time;//计算p1-p1上t比例的点

            Vector vt3 = B - A;
            Point aa = A + vt3 * this.Time;//计算A-B上t比例的点，也就是贝塞尔曲线上的点
            List<Point> lst = new List<Point>()
            {
                A, B,aa,
            };
            foreach(Point pt in lst)
            {
                drawingContext.DrawEllipse(Brushes.Red, null, pt, 5, 5);
            }
            Pen pen = new Pen(Brushes.Red, 1);
            drawingContext.DrawLine(pen, A, B);
            #region 直接使用公式计算曲线上的点
            Point bb = this.GetBezierPoint();
            drawingContext.DrawEllipse(Brushes.Blue, null, bb, 5, 5);
            #endregion
            ///显示时间值
            FormattedText formattedText = new FormattedText("t:" + this.Time.ToString("F2"), CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("宋体"), 20, Brushes.Black);
            drawingContext.DrawText(formattedText, this.Points[2]);
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
            sgc.QuadraticBezierTo(pts[1], pts[2], true, true);
            sgc.BeginFigure(pts[0], true, false);
            sgc.PolyLineTo(pts, true, true);
            sgc.Close();
            Pen pen = new Pen(brush, 1);
            dc.DrawGeometry(null, pen, sg);
        }
        /// <summary>
        /// 公式法得到曲线上的点
        /// </summary>
        /// <returns></returns>
        public Point GetBezierPoint()
        {
            Vector[] vectors = new Vector[this.Points.Length];
            for (int i = 0; i < this.Points.Length; i++)
            {
                vectors[i] = (Vector)this.Points[i];
            }
            Vector vector = Math.Pow(1 - this.Time, 2) * vectors[0] +
                2 * this.Time * (1 - this.Time) * vectors[1] +
                this.Time * this.Time * vectors[2];///直接公式计算得到曲线上的点
            return (Point)vector;
        }
        #region 事件
        #endregion
    }
}
