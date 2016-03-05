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
        bool isStarted = false;
        public MainWindow()
        {
            InitializeComponent();
        }
        Polygon polygon;
        Point startPoint;
        Point firstPoint;
        Point endPoint;
        Line currentLine;
        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed)
                return;
            if (!isStarted)
            {
                if (e.ClickCount == 1)
                {
                    isStarted = true;
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
                    isStarted = false;
                    startPoint = endPoint;
                    endPoint = firstPoint;
                    Task.Factory.StartNew(()=> { MessageBox.Show(polygon.ToString()); });
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
                if (isStarted)
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
            if (isStarted)
            {
                startPoint = endPoint;
                CreateNewLine();
                if(endPoint!=polygon[polygon.Count-1])
                    polygon.Add(endPoint);
            }
        }
    }
}
