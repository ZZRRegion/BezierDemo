using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BezierDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public DrawType DrawType { get; set; } = DrawType.B2;
        public List<Point> Points { get; set; } = new List<Point>();
        private List<Ellipse> lst = new List<Ellipse>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void StackPanel_Click(object sender, RoutedEventArgs e)
        {
            if(e.Source is Button btn && btn.Tag is string tag)
            {
                switch (tag)
                {
                    case "b1":
                        this.DrawType = DrawType.B1;
                        break;
                    case "b2":
                        this.DrawType = DrawType.B2;
                        break;
                    case "b3":
                        this.DrawType = DrawType.B3;
                        break;
                    case "Move":
                        this.DrawType = DrawType.Move;
                        this.ClearEllip();
                        break;
                    case "Clear":
                        this.grid.Children.Clear();
                        break;
                }
                this.Points.Clear();
            }
        }
        private Point bgpt = new Point();
        
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Point pt = e.GetPosition(this.grid);
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                this.bgpt = pt;
                if (this.DrawType == DrawType.B2 || this.DrawType == DrawType.B3 || this.DrawType == DrawType.B1)
                {
                    this.Points.Add(pt);
                    Ellipse ellipse = new Ellipse()
                    {
                        Width = 10,
                        Height = 10,
                        Fill = Brushes.Red,
                    };
                    this.lst.Add(ellipse);
                    Canvas.SetLeft(ellipse, pt.X - 5);
                    Canvas.SetTop(ellipse, pt.Y - 5);
                    this.grid.Children.Add(ellipse);
                    if(this.Points.Count == 2 && this.DrawType == DrawType.B1)
                    {
                        this.ClearEllip();
                        this.grid.Children.Add(new MyBezier1(this.Points.ToArray()));
                        this.Points.Clear();
                    }
                    else if (this.Points.Count == 3 && this.DrawType == DrawType.B2)
                    {
                        this.ClearEllip();
                        this.grid.Children.Add(new MyBezier2(this.Points.ToArray()));
                        this.Points.Clear();
                    }
                    else if(this.Points.Count == 4 && this.DrawType == DrawType.B3)
                    {
                        this.ClearEllip();
                        this.grid.Children.Add(new MyBezier3(this.Points.ToArray()));
                        this.Points.Clear();
                    }
                }
            }
        }
        private void ClearEllip()
        {
            foreach(Ellipse ellipse in this.lst)
            {
                this.grid.Children.Remove(ellipse);
            }
        }
        private IDraw moveDraw;
        private int index;
        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            if(this.DrawType == DrawType.Move)
            {
                Point pt = e.GetPosition(this.grid);
                this.grid.Cursor = Cursors.Arrow;
                foreach(IDraw draw in this.grid.Children)
                {
                    for(int i = 0; i < draw.Points.Length; i++)
                    {
                        Point pt3 = draw.Points[i];
                        pt3.Offset(-5, -5);
                        Rect rect = new Rect(pt3, new Size(10, 10));
                        if (rect.Contains(pt))
                        {
                            this.grid.Cursor = Cursors.Hand;
                            this.moveDraw = draw;
                            this.index = i;
                            break;
                        }
                    }
                }
                if(this.moveDraw != null && e.LeftButton == MouseButtonState.Pressed)
                {
                    Vector vector = pt - this.bgpt;
                    this.moveDraw.Points[this.index] = this.moveDraw.Points[this.index] + vector;
                    this.bgpt = pt;
                }
            }
        }
    }
    public enum DrawType
    {
        None,
        B1,
        B2,
        B3,
        Move,
        Clear,
    }
}
