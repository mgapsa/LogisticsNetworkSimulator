using DataModel;
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
using System.Windows.Shapes;

namespace LogisticsNetworkSimulator.Graphs
{
    /// <summary>
    /// Interaction logic for GraphWindow.xaml
    /// </summary>
    public partial class GraphWindow : Window
    {
        public GraphWindow(Actor actor)
        {
            InitializeComponent();

            if (actor.GraphData != null)
            {
                double yScale = 1;
                double xScale = 1;
                const double margin = 30;
                const double len = 3;
                double xmin = margin;
                double xmax = graphCanvas.Width - margin;
                double ymin = margin;
                double ymax = graphCanvas.Height - margin;
                double step = 30;
                if (xmax - xmin > actor.GraphData.Length)
                {
                    xScale = ((xmax - xmin) / actor.GraphData.Length);
                }
                yScale = (ymax - ymin) / actor.GraphData.Max();

                // Make the X axis.
                GeometryGroup xaxis_geom = new GeometryGroup();
                xaxis_geom.Children.Add(new LineGeometry(
                    new Point(xmin, ymax), new Point(graphCanvas.Width, ymax)));
                for (double x = xmin + xScale * 6; x <= graphCanvas.Width - xScale; x += xScale * 6)
                {
                    xaxis_geom.Children.Add(new LineGeometry(
                        new Point(x, ymax - len),
                        new Point(x, ymax + len)));
                }

                Path xaxis_path = new Path();
                xaxis_path.StrokeThickness = 1;
                xaxis_path.Stroke = Brushes.Black;
                xaxis_path.Data = xaxis_geom;

                graphCanvas.Children.Add(xaxis_path);
                int d = 0;
                // Make the Y ayis.
                GeometryGroup yaxis_geom = new GeometryGroup();
                yaxis_geom.Children.Add(new LineGeometry(
                    new Point(xmin, ymin), new Point(xmin, graphCanvas.Height - margin)));
                for (double y = margin; y <= graphCanvas.Height - step; y += step)
                {
                    d++;
                    yaxis_geom.Children.Add(new LineGeometry(
                        new Point(xmin - len, y),
                        new Point(xmin + len, y)));
                }

                Path yaxis_path = new Path();
                yaxis_path.StrokeThickness = 1;
                yaxis_path.Stroke = Brushes.Black;
                yaxis_path.Data = yaxis_geom;

                graphCanvas.Children.Add(yaxis_path);

                PointCollection points = new PointCollection();
                double Px;
                double Py;
                for (int i = 0; i < actor.GraphData.Length; i++)
                {
                    Px = xmin + i * xScale;
                    Py = ymax - actor.GraphData[i] * yScale;
                    points.Add(new Point(Px, Py));
                }

                Polyline polyline = new Polyline();
                polyline.StrokeThickness = 1;
                polyline.Stroke = Brushes.Red;
                polyline.Points = points;

                graphCanvas.Children.Add(polyline);
                //tąmetodą pododawać wartości i śmiga!
                //
                //if (link.type == 'a')
                //{
                //    ShopLink shop = link as ShopLink;
                //    if (shop.minS > 0)
                //    {
                //        DrawText(graphCanvas, shop.minS.ToString() + "   " + shop.needForMinS.ToString(), new Point((graphCanvas.Width) / 2, 10), 10, System.Windows.HorizontalAlignment.Center, System.Windows.VerticalAlignment.Center);
                //    }
                //    else
                //    {
                //        DrawText(graphCanvas, shop.S.ToString(), new Point((graphCanvas.Width) / 2, 10), 10, System.Windows.HorizontalAlignment.Center, System.Windows.VerticalAlignment.Center);
                //    }

                //}
                //DrawText(graphCanvas, "Days", new Point((graphCanvas.Width) / 2, graphCanvas.Height - 4), 10, System.Windows.HorizontalAlignment.Center, System.Windows.VerticalAlignment.Center);
                //DrawText(graphCanvas, "Value", new Point(1, 5), 10, System.Windows.HorizontalAlignment.Left, System.Windows.VerticalAlignment.Center);
                int c = 0;
                for (double x = xmin; x <= graphCanvas.Width - xScale; x += xScale * 6)
                {
                    if (c % 30 == 0 || actor.GraphData.Length < 30 || (actor.GraphData.Length < 210 && c%5 ==0))
                    {
                        DrawText(graphCanvas, c.ToString(), new Point(x, graphCanvas.Height - (margin/2)), 16, System.Windows.HorizontalAlignment.Center, System.Windows.VerticalAlignment.Center);
                    }
                    c++;
                }
                double v;
                for (double y = step; y <= graphCanvas.Height - step; y += step)
                {
                    if ((int)((ymax - y) / yScale)%100 == 0)
                    {
                        v = (int)((ymax - y) / yScale);
                        DrawText(graphCanvas, v.ToString(), new Point(8, y), 16, System.Windows.HorizontalAlignment.Center, System.Windows.VerticalAlignment.Center);
                    }
                    d--;
                }
            }
        }

        // Position a label at the indicated point.
        private void DrawText(Canvas can, string text, Point location, double font_size, HorizontalAlignment halign, VerticalAlignment valign)
        {
            // Make the label.
            Label label = new Label();
            label.Content = text;
            label.FontSize = font_size;
            can.Children.Add(label);

            // Position the label.
            label.Measure(new Size(double.MaxValue, double.MaxValue));

            double x = location.X;
            if (halign == HorizontalAlignment.Center)
                x -= label.DesiredSize.Width / 2;
            else if (halign == HorizontalAlignment.Right)
                x -= label.DesiredSize.Width;
            Canvas.SetLeft(label, x);

            double y = location.Y;
            if (valign == VerticalAlignment.Center)
                y -= label.DesiredSize.Height / 2;
            else if (valign == VerticalAlignment.Bottom)
                y -= label.DesiredSize.Height;
            Canvas.SetTop(label, y);
        }
    }
}
