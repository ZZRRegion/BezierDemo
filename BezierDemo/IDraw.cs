/***********************************************
* 说    明: 
* CLR版 本：4.0.30319.42000
* 命名空间：BezierDemo
* 类 名 称：IDraw
* 创建日期：2019/11/22 10:46:03
* 作    者：ZZR
* 版 本 号：4.0.30319.42000
* 文 件 名：IDraw
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

namespace BezierDemo
{
    public interface IDraw
    {
        void DrawBezier(Point[] pts, DrawingContext dc, Brush brush);
        Point[] Points { get; set; }
        double Time { get; set; }
        Point GetBezierPoint();
        #region 变量
        #endregion
        #region 方法
        #endregion
        #region 事件
        #endregion
    }
}
