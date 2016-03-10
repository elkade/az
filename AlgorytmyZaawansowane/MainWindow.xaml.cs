using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Shapes;

namespace AlgorytmyZaawansowane
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Polygon polygon;
        Point point;
        private Shape _currentShape = Shape.Polygon;
        public Shape CurrentShape {
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
        bool isDrawingStarted = false;
        Point startPoint;
        Point firstPoint;
        Point endPoint;
        Line currentLine;

        public const string InputFileName = "in.txt";
        public const string OutputFileName = "out.txt";

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed)
                return;
            switch (CurrentShape)
            {
                case Shape.Polygon:
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
                        if (e.ClickCount == 1)
                            endPoint = e.GetPosition(this);
                        else
                        {
                            isDrawingStarted = false;
                            startPoint = endPoint;
                            endPoint = firstPoint;
                            Task.Factory.StartNew(() => { MessageBox.Show(polygon.ToString()); });
                        }
                        UpdateCurrentLine();
                    }
                    break;
                case Shape.Point:
                    CreateNewPoint(e.GetPosition(this));

                    break;
            }

        }
        private void CreateNewPoint(Point p)
        {
            point = p;
            double width = 5, height = 5;
            Ellipse e = CreateEllipse(width, height, p.X, p.Y);
            paintSurface.Children.Add(e);
            Canvas.SetLeft(e, p.X - (width / 2));
            Canvas.SetTop(e, p.Y - (height / 2));
        }
        Ellipse CreateEllipse(double width, double height, double desiredCenterX, double desiredCenterY)
        {
            Ellipse ellipse = new Ellipse { Width = width, Height = height };
            double left = desiredCenterX - (width / 2);
            double top = desiredCenterY - (height / 2);

            ellipse.Margin = new Thickness(left, top, 0, 0);
            return ellipse;
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ReadInput();
            //DrawData();
        }

        private void DrawData()
        {
            throw new NotImplementedException();
        }

        private void ReadInput()
        {
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
        private void WriteOutput()
        {
            using (var file = new StreamWriter(OutputFileName))
            {
                try {
                    if (polygon.IsSimple())
                    {
                        string area = polygon.GetArea().ToString();
                        bool isInside = false;// = polygon.IsPointInside(point);
                        file.WriteLine(area + " " + (isInside ? "TAK" : "NIE"));
                    }
                    else file.WriteLine("NOT SIMPLE");
                }
                catch(Exception ex)
                {
                    file.WriteLine("BAD DATA");
                }
            }
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            WriteOutput();
        }
    }
    public enum Shape { Polygon, Point }

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
