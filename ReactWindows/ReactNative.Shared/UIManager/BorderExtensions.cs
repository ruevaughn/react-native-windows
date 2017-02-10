#if WINDOWS_UWP
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
#else
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
#endif

namespace ReactNative.UIManager
{
    static class BorderExtensions
    {
        private static readonly Brush s_defaultBorderBrush = new SolidColorBrush(Colors.Black);

        public static void SetBorderWidth(this Border border, int spacingType, double width)
        {
            var thickness = border.BorderThickness;
            switch (spacingType)
            {
                case EdgeSpacing.Left:
                    thickness.Left = width;
                    break;
                case EdgeSpacing.Top:
                    thickness.Top = width;
                    break;
                case EdgeSpacing.Right:
                    thickness.Right = width;
                    break;
                case EdgeSpacing.Bottom:
                    thickness.Bottom = width;
                    break;
                case EdgeSpacing.All:
                    thickness = new Thickness(width);
                    break;
            }

            border.BorderThickness = thickness;
        }

        public static void SetBorderColor(this Border border, int index, uint? color)
        {
            if (index == 5)
            {
                border.BorderBrush = color.HasValue
                    ? new SolidColorBrush(ColorHelpers.Parse(color.Value))
                    : s_defaultBorderBrush;
            }
            else
            {
#if !WINDOWS_UWP
                DrawingBrush brush;
                if (border.BorderBrush.GetType() == typeof(DrawingBrush))
                {
                    brush = (DrawingBrush)border.BorderBrush;
                }
                else
                {
                    brush = new DrawingBrush();
                }
                brush.TileMode = TileMode.None;
                brush.Stretch = Stretch.Fill;
                brush.Transform = new ScaleTransform(3,3);
                brush.Viewbox = new Rect(new Point(0,0), new Size(3.0, 3.0));
                var drawing = (DrawingGroup)brush.Drawing ?? new DrawingGroup();
                var pen = new Pen(Brushes.Transparent, 0);
                if (drawing.Children.Count <= 4)
                {
                    drawing.Children.Clear();
                    drawing.Children.Add(new GeometryDrawing(Brushes.Transparent, pen, new RectangleGeometry(new Rect(new Point(0, 0), new Size(1, 3))))); // Left
                    drawing.Children.Add(new GeometryDrawing(Brushes.Transparent, pen, new RectangleGeometry(new Rect(new Point(3, 0), new Size(1, 3))))); // Right
                    drawing.Children.Add(new GeometryDrawing(Brushes.Transparent, pen, new RectangleGeometry(new Rect(new Point(0, 0), new Size(3, 1))))); // Top
                    drawing.Children.Add(new GeometryDrawing(Brushes.Transparent, pen, new RectangleGeometry(new Rect(new Point(0, 3), new Size(3, 1))))); // Bottom
                }
                var currentGeometry = (GeometryDrawing)drawing.Children[index];
                currentGeometry.Brush = new SolidColorBrush(ColorHelpers.Parse(color.Value));

                brush.Drawing = drawing;
                border.BorderBrush = brush;
#endif
            }
        }
    }
}
