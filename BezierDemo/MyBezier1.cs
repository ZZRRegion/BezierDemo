/***********************************************
* 说    明: 
* CLR版 本：4.0.30319.42000
* 命名空间：BezierDemo
* 类 名 称：MyBezier1
* 创建日期：2019/11/22 14:43:58
* 作    者：ZZR
* 版 本 号：4.0.30319.42000
* 文 件 名：MyBezier1
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
using System.Globalization;

namespace BezierDemo
{
    public class MyBezier1 : UIElement, IDraw
    {
        public Point[] Points { get; set; }
        public double Time { get; set; }
        public MyBezier1()
            :this(new Point[]
            {
                new Point(0, 0),
                new Point(200, 150)
            })
        {

        }
        public MyBezier1(Point[] pts)
        {
            this.Points = pts;
            CompositionTarget.Rendering += CompositionTarget_Rendering;
        }

        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            this.InvalidateVisual();
        }
        protected override void OnRender(DrawingContext drawingContext)
        {
            this.Time += 0.01;
            if (this.Time >= 1)
            {
                this.Time = 0;
            }
            this.DrawBezier(this.Points, drawingContext, Brushes.Black);
            Vector vt = this.Points[1] - this.Points[0];
            Point aa = this.Points[0] + vt * this.Time;//计算A-B的t比例点aa，也就是曲线上的点了
            Point aaa = this.GetBezierPoint();
            drawingContext.DrawEllipse(Brushes.Red, null, aa, 5, 5);
            FormattedText formattedText = new FormattedText("t:" + this.Time.ToString("F2"), CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("宋体"), 20, Brushes.Black);
            drawingContext.DrawText(formattedText, this.Points[1]);
        }
        public void DrawBezier(Point[] pts, DrawingContext dc, Brush brush)
        {
            foreach(Point pt in pts)
            {
                dc.DrawEllipse(brush, null, pt, 5, 5);
            }
            Pen pen = new Pen(brush, 1);
            dc.DrawLine(pen, pts[0], pts[1]);
        }
        /// <summary>
        /// 公式获取曲线点
        /// </summary>
        /// <returns></returns>
        public Point GetBezierPoint()
        {
            Vector[] vectors = new Vector[this.Points.Length];
            for(int i = 0; i < this.Points.Length; i++)
            {
                vectors[i] = (Vector)this.Points[i];
            }
            Vector aa = (1 - this.Time) * vectors[0] + this.Time * vectors[1];
            return (Point)aa;
        }
    }
}
