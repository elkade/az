using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace AlgorytmyZaawansowane
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public const string InputFileName = "in.txt";
        public const string OutputFileName = "out.txt";

        Polygon polygon;
        Point point;

        bool isDrawingStarted = false;
        Point startPoint;
        Point firstPoint;
        Point endPoint;
        Line currentLine;

        private SelectedShape _currentShape = SelectedShape.Polygon;
        public SelectedShape CurrentShape
        {
            get
            {
                return _currentShape;
            }
            set
            {
                isDrawingStarted = false;
                _currentShape = value;
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //ReadInput();
            //DrawData();
        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed)
                return;
            switch (CurrentShape)
            {
                case SelectedShape.Polygon:
                    if (!isDrawingStarted)
                    {
                        if (e.ClickCount == 1)
                        {
                            isDrawingStarted = true;
                            firstPoint = e.GetPosition(this);
                            startPoint = firstPoint;
                            endPoint = firstPoint;
                            polygon = new Polygon { firstPoint };
                            CreateNewLine();
                        }
                    }
                    else
                    {
                        if (e.ClickCount == 1) {
                            endPoint = e.GetPosition(this);
                            UpdateCurrentLine();
                        }
                        else
                        {
                            isDrawingStarted = false;
                            startPoint = endPoint;
                            endPoint = firstPoint;
                            DrawPolygon(polygon);
                        }
                    }
                    break;
                case SelectedShape.Point:
                    point = e.GetPosition(this);
                    DrawPoint(point);
                    break;
            }
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (isDrawingStarted)
                {
                    endPoint = e.GetPosition(this);
                    UpdateCurrentLine();
                }
            }
        }

        private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (isDrawingStarted)
            {
                startPoint = endPoint;
                CreateNewLine();
                if (endPoint != polygon[polygon.Count - 1])
                    polygon.Add(endPoint);
            }
        }

        private void CreateNewLine()
        {
            currentLine = new Line
            {
                X1 = startPoint.X,
                Y1 = startPoint.Y,
                X2 = endPoint.X,
                Y2 = endPoint.Y,
                Stroke = SystemColors.WindowFrameBrush,
                //StrokeThickness = 3
            };
            paintSurface.Children.Add(currentLine);
        }

        private void UpdateCurrentLine()
        {
            currentLine.X2 = endPoint.X;
            currentLine.Y2 = endPoint.Y;
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            paintSurface.Children.Clear();
            isDrawingStarted = false;
        }

        private void DrawPolygon(IEnumerable<Point> vertices)
        {
            var lines = paintSurface.Children.OfType<Line>().ToList();
            foreach (var l in lines) paintSurface.Children.Remove(l);

            var polygons = paintSurface.Children.OfType<System.Windows.Shapes.Polygon>().ToList();
            foreach (var pp in polygons) paintSurface.Children.Remove(pp);

            System.Windows.Shapes.Polygon p = new System.Windows.Shapes.Polygon();
            p.Stroke = Brushes.Black;
            p.Fill = Brushes.LightBlue;
            p.StrokeThickness = 1;
            p.HorizontalAlignment = HorizontalAlignment.Left;
            p.VerticalAlignment = VerticalAlignment.Center;
            p.Points = new PointCollection(vertices);
            Panel.SetZIndex(p, -1);
            paintSurface.Children.Add(p);
        }

        private void DrawPoint(Point point)
        {
            var ellipses = paintSurface.Children.OfType<Ellipse>().ToList();
            foreach (var e in ellipses) paintSurface.Children.Remove(e);
            Ellipse ellipse = new Ellipse();
            ellipse.Height = 10;
            ellipse.Width = 10;
            SolidColorBrush blueBrush = new SolidColorBrush();
            blueBrush.Color = Colors.Red;
            SolidColorBrush blackBrush = new SolidColorBrush();
            blackBrush.Color = Colors.Black;
            ellipse.StrokeThickness = 2;
            ellipse.Stroke = blackBrush;
            ellipse.Fill = blueBrush;

            double left = point.X - (ellipse.Width / 2);
            double top = point.Y - (ellipse.Height / 2);

            ellipse.Margin = new Thickness(left, top, 0, 0);

            paintSurface.Children.Add(ellipse);
        }

        private void DrawData()
        {
            DrawPolygon(polygon);
            DrawPoint(point);
        }

        private void ReadInput()
        {
            try {
                string[] line1, line2;
                using (var file = new System.IO.StreamReader(InputFileName))
                {
                    line1 = file.ReadLine().Split(' ');
                    line2 = file.ReadLine().Split(' ');
                }
                var vertices = new List<Point>();

                for (int i = 0; i < line1.Length; i += 2)
                    vertices.Add(new Point(double.Parse(line1[i]), double.Parse(line1[i + 1])));
                polygon = new Polygon();
                polygon.AddRange(vertices);

                double x = double.Parse(line2[0]);
                double y = double.Parse(line2[1]);
                point = new Point(x, y);
            }
            catch(Exception)
            {
                using (var file = new StreamWriter(OutputFileName))
                {
                    file.WriteLine("BAD DATA");
                }
                MessageBox.Show("Error occured while reading input.");
            }
        }

        private void WriteOutput()
        {
            using (var file = new StreamWriter(OutputFileName))
            {
                try {
                    if (polygon.IsSimple())
                    {
                        string area = polygon.GetArea().ToString();
                        bool isInside = polygon.IsPointInside(point);
                        file.WriteLine(area + " " + (isInside ? "TAK" : "NIE"));
                    }
                    else file.WriteLine("NOT SIMPLE");
                }
                catch(Exception)
                {
                    file.WriteLine("BAD DATA");
                }
            }
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            if (polygon == null)
            {
                MessageBox.Show("No polygon read");
                return;
            }

            Task.Factory.StartNew(() => {
                WriteOutput();
                string pointIn = "";
                if (point != null) {
                    pointIn = ", point " + point.ToString() + (polygon.IsPointInside(point) ? " inside" : " outside");
                }
                MessageBox.Show(polygon.ToString() + pointIn);
            });
        }

        private void ReadButton_Click(object sender, RoutedEventArgs e)
        {
            ReadInput();
            //paintSurface.Children.Clear();
            if(polygon != null)
                DrawData();
        }
    }

    public enum SelectedShape { Polygon, Point }

    public class EnumBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string parameterString = parameter as string;
            if (parameterString == null)
                return DependencyProperty.UnsetValue;

            if (Enum.IsDefined(value.GetType(), value) == false)
                return DependencyProperty.UnsetValue;

            object parameterValue = Enum.Parse(value.GetType(), parameterString);

            return parameterValue.Equals(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value.Equals(true) ? parameter : Binding.DoNothing;
        }
    }
}
