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

namespace MazeGui.UserControls
{
    /// <summary>
    /// Interaction logic for MazeBoard.xaml
    /// </summary>
    /// <seealso cref="System.Windows.Controls.UserControl" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class MazeBoard : UserControl
    {
       

        //members.
        private int rows;
        private int cols;
        private Rectangle[,] mazeGrid;
        private Brush barrierBrush;
        private Brush freeRecBrush;
        private Brush playerPosBrush;
        private Brush goalPosBrush;
        private Rectangle playerPositionRectangle;
        private double rectangleHeight;
        private double rectanglWidth;



        /// <summary>
        /// Gets or sets the height of the rectangle.
        /// </summary>
        /// <value>
        /// The height of the rectangle.
        /// </value>
        public double RectangleHeight
        {
            get
            {
                return this.rectangleHeight;
            }
            set
            {
                this.rectangleHeight = value;
            }
        }

        /// <summary>
        /// Gets or sets the width of the rectangl.
        /// </summary>
        /// <value>
        /// The width of the rectangl.
        /// </value>
        public double RectanglWidth
        {
            get
            {
                return this.rectanglWidth;
            }
            set
            {
                this.rectanglWidth = value;
            }
        }


        /// <summary>
        /// Gets or sets the maze.
        /// </summary>
        /// <value>
        /// The maze.
        /// </value>
        public string Maze
        {
            get
            {
                return (string)GetValue(MazeProperty);
            }
            set
            {
                SetValue(MazeProperty, value);
            }
        }



        /// <summary>
        /// Gets or sets the player position.
        /// </summary>
        /// <value>
        /// The player position.
        /// </value>
        public string PlayerPosition
        {
            get
            {
                return (string)GetValue(PlayerPositionProperty);
            }
            set
            {
                SetValue(PlayerPositionProperty, value);
            }
        }
        /// <summary>
        /// Gets or sets the connection error.
        /// </summary>
        /// <value>
        /// The connection error.
        /// </value>
        public string ConnectionError
        {
            get
            {
                return (string)GetValue(ConnectionErrorProperty);
            }
            set
            {
                SetValue(ConnectionErrorProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the initial position.
        /// </summary>
        /// <value>
        /// The initial position.
        /// </value>
        public string InitialPosition
        {
            get
            {
                return (string)GetValue(InitialPositionProperty);
            }
            set
            {
                SetValue(InitialPositionProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the goal position.
        /// </summary>
        /// <value>
        /// The goal position.
        /// </value>
        public string GoalPosition
        {
            get
            {
                return (string)GetValue(GoalPositionProperty);
            }
            set
            {
                SetValue(GoalPositionProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the rows.
        /// </summary>
        /// <value>
        /// The rows.
        /// </value>
        public string Rows
        {
            get
            {
                return (string)GetValue(RowsProperty);
            }
            set
            {
                SetValue(RowsProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the cols.
        /// </summary>
        /// <value>
        /// The cols.
        /// </value>
        public string Cols
        {
            get
            {
                return (string)GetValue(ColsProperty);
            }
            set
            {
                SetValue(ColsProperty, value);
            }
        }

        /// <summary>
        /// Gets the x.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <returns></returns>
        public static int GetX(string position)
        {
            string[] arr = position.Split(',');
            string xPostion = arr[0];
            arr = xPostion.Split('(');
            int x = int.Parse(arr[1]);
            return x;
        }


        /// <summary>
        /// Gets the y.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <returns></returns>
        public static int GetY(string position)
        {
            string[] arr = position.Split(',');
            string yPosition = arr[1];
            arr = yPosition.Split(')');
            int y = int.Parse(arr[0]);
            return y;
        }





        /// <summary>
        /// The connection error property
        /// </summary>
        public static readonly DependencyProperty ConnectionErrorProperty = DependencyProperty.Register(//added
                  "ConnectionError",
                  typeof(string),
                  typeof(MazeBoard),
                 null);

        /// <summary>
        /// The maze property
        /// </summary>
        public static readonly DependencyProperty MazeProperty = DependencyProperty.Register(
                  "Maze",
                  typeof(string),
                  typeof(MazeBoard),
               new PropertyMetadata(""));

        /// <summary>
        /// The rows property
        /// </summary>
        public static readonly DependencyProperty RowsProperty = DependencyProperty.Register(
         "Rows",
         typeof(string),
         typeof(MazeBoard),
         new PropertyMetadata(""));

        /// <summary>
        /// The cols property
        /// </summary>
        public static readonly DependencyProperty ColsProperty = DependencyProperty.Register(
       "Cols",
       typeof(string),
       typeof(MazeBoard),
       new PropertyMetadata(""));


        /// <summary>
        /// The initial position property
        /// </summary>
        public static readonly DependencyProperty InitialPositionProperty = DependencyProperty.Register(
            "InitialPosition",
            typeof(string),
            typeof(MazeBoard),
             new PropertyMetadata(""));


        /// <summary>
        /// The goal position property
        /// </summary>
        public static readonly DependencyProperty GoalPositionProperty = DependencyProperty.Register(
        "GoalPosition",
        typeof(string),
        typeof(MazeBoard),
         new PropertyMetadata(""));




        /// <summary>
        /// The player position property
        /// </summary>
        public static readonly DependencyProperty PlayerPositionProperty = DependencyProperty.Register(
       "PlayerPosition",
       typeof(string),
       typeof(MazeBoard),
       new PropertyMetadata(MovePlayerPositionRectangle));


        /// <summary>
        /// Raises the <see cref="E:System.Windows.FrameworkElement.Initialized" />
        /// event. This method is invoked whenever <see cref="P:System.Windows.FrameworkElement.IsInitialized" /> 
        /// is set to true internally.
        /// </summary>
        /// <param name="e">The <see cref="T:System.Windows.RoutedEventArgs" /> that contains the event data.</param>
        protected override void OnInitialized(EventArgs e)
        {

            InitializeComponent();

            if ((int.TryParse(Rows, out rows)) &&
             (int.TryParse(Cols, out cols)))
            {
                mazeGrid = new Rectangle[rows, cols];
                rectangleHeight = MazeCanvas.Height / rows;
                RectanglWidth = MazeCanvas.Width / cols;

                //sers the brushed.
                barrierBrush = new SolidColorBrush(Colors.Black);
                freeRecBrush = new SolidColorBrush(Colors.White);
                playerPosBrush = new ImageBrush(new BitmapImage
                    (new Uri(@"pack://application:,,,/MazeGui;component/Resources/minionphoto.png")));
                goalPosBrush = new ImageBrush(new BitmapImage
                    (new Uri(@"pack://application:,,,/MazeGui;component/Resources/destinationimage.png")));

                //initializes the rectangles.
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        Rectangle rectangle = new Rectangle();
                        rectangle.Height = RectangleHeight;
                        rectangle.Width = RectanglWidth;
                        if (Maze[(i * cols) + j] == '1')
                        {
                            rectangle.Fill = barrierBrush;
                        }
                        else
                        {
                            rectangle.Fill = freeRecBrush;
                        }

                        MazeCanvas.Children.Add(rectangle);
                        Canvas.SetTop(rectangle, i * RectangleHeight);
                        Canvas.SetLeft(rectangle, j * RectanglWidth);
                        mazeGrid[i, j] = rectangle;
                    }
                }

                playerPositionRectangle = new Rectangle();
                playerPositionRectangle.Height = RectangleHeight;
                playerPositionRectangle.Width = RectanglWidth;
                playerPositionRectangle.Fill = playerPosBrush;
                MazeCanvas.Children.Add(playerPositionRectangle);


                Rectangle goalPosRect = new Rectangle();
                goalPosRect.Height = RectangleHeight;
                goalPosRect.Width = RectanglWidth;
                goalPosRect.Fill = goalPosBrush;
                MazeCanvas.Children.Add(goalPosRect);




                int x = GetX(InitialPosition);
                int y = GetY(InitialPosition);

                Canvas.SetTop(playerPositionRectangle, x * RectangleHeight);
                Canvas.SetLeft(playerPositionRectangle, y * RectanglWidth);



                x = GetX(GoalPosition);
                y = GetY(GoalPosition);

                Canvas.SetTop(goalPosRect, x * RectangleHeight);
                Canvas.SetLeft(goalPosRect, y * RectanglWidth);
                mazeGrid[x, y] = goalPosRect;
            }
            base.OnInitialized(e);
        }



        /// <summary>
        /// Moves the player rectangle.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public void MovePlayerRec(int x, int y)
        {
            if (playerPositionRectangle != null)
            {
                Canvas.SetTop(playerPositionRectangle, x * RectangleHeight);
                Canvas.SetLeft(playerPositionRectangle, y * RectanglWidth);
            }
        }

        /// <summary>
        /// Moves the player position rectangle.
        /// </summary>
        /// <param name="d">The d.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void MovePlayerPositionRectangle(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            string newPos = e.NewValue as string;
            MazeBoard mazeBoard = d as MazeBoard;
            int x = GetX(newPos);
            int y = GetY(newPos);
            string goalPos = mazeBoard.GoalPosition;
            int xGoal = GetX(goalPos);
            int yGoal = GetY(goalPos);

            mazeBoard.MovePlayerRec(x, y);

      

        }

        /// <summary>
        /// Restarts the maze.
        /// </summary>
        public void RestartMaze()
        {
            PlayerPosition = InitialPosition;

        }


    }
}
