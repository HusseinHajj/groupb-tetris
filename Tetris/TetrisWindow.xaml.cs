using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Runtime.CompilerServices;

namespace Tetris
{
    /// <summary>
    /// Interaction logic for TetrisWindow.xaml
    /// </summary>
    public partial class TetrisWindow : Window
    {
        public static readonly DependencyProperty ScoreProperty = DependencyProperty.Register("Score", typeof(int), typeof(TetrisWindow), new UIPropertyMetadata(0));
        public static readonly DependencyProperty LevelProperty = DependencyProperty.Register("Level", typeof(int), typeof(TetrisWindow), new UIPropertyMetadata(1));
        public static readonly DependencyProperty LevelUpProperty = DependencyProperty.Register("LevelUp", typeof(int), typeof(TetrisWindow), new UIPropertyMetadata(2));
        bool activePiece = false;
        public int Score
        {
            get { return (int)GetValue(ScoreProperty); }
            set { SetValue(ScoreProperty, value); }
        }
        public int Level
        {
            get { return (int)GetValue(LevelProperty); }
            set { SetValue(LevelProperty, value); }
        }
        public int LevelUp
        {
            get { return (int)GetValue(LevelUpProperty); }
            set { SetValue(LevelUpProperty, value); }
        }

        Rectangle[,] tetrisBoard;
        DispatcherTimer gameTimer = new DispatcherTimer();

        public TetrisWindow()
        {
            InitializeComponent();

            Canvas.SetLeft(Board, 0);
            Canvas.SetRight(Board, Board.Width);

            tetrisBoard = new Rectangle[(int)(Board.Height / 20d), (int)(Board.Width / 20d)];

            gameTimer.Interval = TimeSpan.FromMilliseconds(50);
            gameTimer.Tick += new EventHandler(gameTimer_Tick);
            gameTimer.Start();
        }

        UIElement nextShape = new UIElement();
        Random generator = new Random();
        bool nextShapeExists = false;


        public UIElement GetCurrentShape()
        {
            return Board.Children.Count > 0 ? Board.Children[Board.Children.Count - 1] : null;
        }

        UIElement shapeToAdd;
        int currentY = 0;

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            if (!activePiece)
            {
                if (!nextShapeExists)
                {
                    GenerateNewShape();
                    nextShapeExists = true;
                }
                Grid1.Children.Remove(nextShape);
                shapeToAdd = nextShape;
                Canvas.SetLeft(shapeToAdd, 100);
                Board.Children.Add(shapeToAdd);
                this.TestRowsDone();
                GenerateNewShape();
                activePiece = true;
            }
            if (activePiece)
            {
                
                if (!HitTest(Direction.Down))
                {
                    int interval = (this.Level) * 5;
                    interval += (keyDownPressed) ? (3 / 2) * interval : 0;
                    for (int i = 0; i < interval / 5; i++)
                    {
                        if(!HitTest(Direction.Down))
                        {
                            currentY += 5;
                            Canvas.SetTop(Board.Children[Board.Children.Count - 1], currentY);
                        }
                    }
                }
                else
                {
                    currentY = 0;
                    activePiece = false;

				AddShapeToBoard(GetCurrentShape() as Shape);

                }
            }
            LevelUp = 10;
            if (keyDownPressed)
                Score += 1;
        }

        private void GenerateNewShape()
        {
            switch (generator.Next() % 7)
            {
                case 0:
                    nextShape = new IShape();
                    break;
                case 1:
                    nextShape = new JShape();
                    break;
                case 2:
                    nextShape = new LShape();
                    break;
                case 3:
                    nextShape = new OShape();
                    break;
                case 4:
                    nextShape = new ZShape();
                    break;
                case 5:
                    nextShape = new S_Shape();
                    break;
                case 6:
                    nextShape = new TShape();
                    break;
            }
            currentTransform = 0;
            Grid.SetRow(nextShape, 1);
            Grid1.Children.Add(nextShape);
        }

        private void Board_KeyDown(object sender, KeyEventArgs e)
        {
            double pieceLeft = Canvas.GetLeft(GetCurrentShape());

            switch (e.Key)
            {
                case Key.Up:
                    if (gameTimer.IsEnabled)
                    {
                        RotateCurrentShape();
                    }
                    break;
                case Key.Right:
                    if (gameTimer.IsEnabled && activePiece && !HitTest(Direction.Right))
                    {
                        Canvas.SetLeft(GetCurrentShape(), pieceLeft + 20);
                    }
                    break;
                case Key.Left:
                    if (gameTimer.IsEnabled && activePiece && !HitTest(Direction.Left))
                    {
                        Canvas.SetLeft(GetCurrentShape(), pieceLeft - 20);
                    }
                    break;
                case Key.Down:
                    if (gameTimer.IsEnabled)
                    {
                        keyDownPressed = true;
                    }
                    break;
                case Key.P:
                    Pause(PauseStatus);
                    break;
            }
        }

        bool keyDownPressed = false;

        int currentTransform = 0;

        private void RotateCurrentShape()
        {
            RotateArrangement(GetCurrentShape() as Shape);
            if (!HitTest(Direction.Down) && !HitTest(Direction.Left) && !HitTest(Direction.Right))
            {
                currentTransform += 90;
                ((UserControl)GetCurrentShape()).LayoutTransform = new RotateTransform(currentTransform);
            }
            else
            {
                RotateArrangement(GetCurrentShape() as Shape);
                RotateArrangement(GetCurrentShape() as Shape);
                RotateArrangement(GetCurrentShape() as Shape);
            }
        }

        private void RotateArrangement(Shape shape)
        {
            if (shape != null)
            {
                Rectangle[,] temp = new Rectangle[shape.Arrangement.GetLength(1), shape.Arrangement.GetLength(0)];
                for (int i = 0; i < shape.Arrangement.GetLength(0); i++)
                {
                    for (int j = 0; j < shape.Arrangement.GetLength(1); j++)
                    {
                        temp[j, shape.Arrangement.GetLength(0) - i - 1] = shape.Arrangement[i, j];
                    }
                }
                shape.Arrangement = temp;
            }
        }

        private int ShapeLeftIndex(Shape shape)
        {
            double left = Canvas.GetLeft(shape as UIElement);
            left = (Double.IsNaN(left)) ? 0 : left;
            return (int)(left / 20d);
        }

        private int ShapeTopIndex(Shape shape)
        {
            double top = Canvas.GetTop(shape as UIElement);
            top = (Double.IsNaN(top)) ? 0 : top;
            return (int)(top / 20d);
        }

        private bool ShapeExists(Rectangle[,] array, int col, int row)
        {
            if (row >= 0 && col >= 0 && row < tetrisBoard.GetLength(0) && col < tetrisBoard.GetLength(1))
                return array[row, col] != null && array[row, col].Visibility == Visibility.Visible;
            return true;
        }

        private void AddShapeToBoard(Shape shape)
        {
            int top = ShapeTopIndex(shape);
            int left = ShapeLeftIndex(shape);
		  Canvas.SetLeft(shape as UIElement, left * 20d);
		  Canvas.SetTop(shape as UIElement, top * 20d);
            for (int i = 0; i < shape.Arrangement.GetLength(0); i++)
            {
                for (int j = 0; j < shape.Arrangement.GetLength(1); j++)
                {
                    tetrisBoard[top + i, left + j] = shape.Arrangement[i, j];
                }
            }
        }

        private bool ShapeInBoundaries(Shape shape)
        {
            int top = ShapeTopIndex(shape);
            int left = ShapeLeftIndex(shape);
            return top >= 0 && left >= 0 && top + shape.Arrangement.GetLength(0) <= tetrisBoard.GetLength(0) && left + shape.Arrangement.GetLength(1) <= tetrisBoard.GetLength(1);
        }

        private bool HitTest(Direction direction)
        {
            Shape shape = GetCurrentShape() as Shape;
            int left = ShapeLeftIndex(shape);
            int top = ShapeTopIndex(shape);
            if (ShapeInBoundaries(shape))
            {
                for (int i = 0; i < shape.Arrangement.GetLength(1); i++)
                {
                    for (int j = 0; j < shape.Arrangement.GetLength(0); j++)
                    {
                        if (ShapeExists(shape.Arrangement, i, j))
                        {
                            if ((direction == Direction.Down && ShapeExists(tetrisBoard, left + i, top + j + 1)) ||
                               (direction == Direction.Left && ShapeExists(tetrisBoard, left - 1, top + j + 1)) ||
                               (direction == Direction.Right && ShapeExists(tetrisBoard, left + i + 1, top + j + 1)))
                                return true;
                        }
                    }
                }
                return false;
            }
            return true;
        }
        private void TestRowsDone()
        {
            int row = 0;
            while (row != 20)
            {
                bool rowDone = false;
                int column = 0;
                while (column != 10)
                {
                    if (ShapeExists(tetrisBoard, column, row))
                    {
                        rowDone = true;
                    }
                    else
                    {
                        rowDone = false;
                        break;
                    }
                    column++;
                }
                if (rowDone == true)
                {
                    this.Close();
                }
                row++;
            }
        }

        public void Start()
        {
            gameTimer.Start();
        }
        public void Stop()
        {
            gameTimer.Stop();
        }
        public void Pause(TextBlock pauseStatus)
        {
            if (gameTimer.IsEnabled)
            {
                Stop();
                pauseStatus.Visibility = Visibility.Visible;
            }
            else // - This will be used when we have a game over status - if (!gameOverStatus)
            {
                Start();
                pauseStatus.Visibility = Visibility.Hidden;
            }
        }


        private void Board_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down)
                keyDownPressed = false;
        }
    }
}
