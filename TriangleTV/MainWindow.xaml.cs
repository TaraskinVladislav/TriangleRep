using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace TriangleDrawingWPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void DrawTriangle_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(txtSideA.Text, out double a) &&
                double.TryParse(txtSideB.Text, out double b) &&
                double.TryParse(txtSideC.Text, out double c) &&
                IsValidTriangle(a, b, c))
            {
                DrawTriangle(a, b, c);
                string triangleType = GetTriangleType(a, b, c);
                txtTriangleType.Text = $"Тип треугольника: {triangleType}";
            }
            else
            {
                MessageBox.Show("Введите корректные значения сторон треугольника.");
            }
        }

        private bool IsValidTriangle(double a, double b, double c)
        {
            return a + b > c && a + c > b && b + c > a;
        }

        private void DrawTriangle(double a, double b, double c)
        {
            canvas.Children.Clear();

            Point p1 = new Point(0, 0);

            Point p2 = new Point(a, 0);

            double angleC = Math.Acos((a * a + b * b - c * c) / (2 * a * b));

            Point p3 = new Point(b * Math.Cos(angleC), b * Math.Sin(angleC));

            Line sideA = new Line
            {
                X1 = p1.X,
                Y1 = p1.Y,
                X2 = p2.X,
                Y2 = p2.Y,
                Stroke = Brushes.Black,
                StrokeThickness = 2
            };

            Line sideB = new Line
            {
                X1 = p2.X,
                Y1 = p2.Y,
                X2 = p3.X,
                Y2 = p3.Y,
                Stroke = Brushes.Black,
                StrokeThickness = 2
            };

            Line sideC = new Line
            {
                X1 = p3.X,
                Y1 = p3.Y,
                X2 = p1.X,
                Y2 = p1.Y,
                Stroke = Brushes.Black,
                StrokeThickness = 2
            };

            canvas.Children.Add(sideA);
            canvas.Children.Add(sideB);
            canvas.Children.Add(sideC);

            CenterTriangle(p1, p2, p3);
        }

        private void CenterTriangle(Point p1, Point p2, Point p3)
        {
            double canvasWidth = canvas.ActualWidth;
            double canvasHeight = canvas.ActualHeight;

            double centerX = canvasWidth / 2;
            double centerY = canvasHeight / 2;

            double triangleCenterX = (p1.X + p2.X + p3.X) / 3;
            double triangleCenterY = (p1.Y + p2.Y + p3.Y) / 3;

            double offsetX = centerX - triangleCenterX;
            double offsetY = centerY - triangleCenterY;

            foreach (UIElement element in canvas.Children)
            {
                if (element is Line line)
                {
                    line.X1 += offsetX;
                    line.Y1 += offsetY;
                    line.X2 += offsetX;
                    line.Y2 += offsetY;
                }
            }
        }

        private string GetTriangleType(double a, double b, double c)
        {
            if (a == b && b == c)
            {
                return "Равносторонний";
            }
            else if (a == b || b == c || a == c)
            {
                return "Равнобедренный";
            }
            else
            {
                return "Разносторонний";
            }
        }
    }
}
