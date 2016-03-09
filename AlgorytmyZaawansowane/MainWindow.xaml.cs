using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
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

        public MainWindow()
        {
            InitializeComponent();
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

        private void UpdateCurrentLine()
        {
            currentLine.X2 = endPoint.X;
            currentLine.Y2 = endPoint.Y;
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

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            paintSurface.Children.Clear();
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
                file.WriteLine("");
            }
        }
    }
}
