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

namespace Tetris
{
	/// <summary>
	/// Interaction logic for TetrisWindow.xaml
	/// </summary>
	public partial class TetrisWindow : Window
	{
		public static readonly DependencyProperty ScoreProperty = DependencyProperty.Register("Score", typeof(int), typeof(TetrisWindow), new UIPropertyMetadata(0));
		public static readonly DependencyProperty LevelProperty = DependencyProperty.Register("Level", typeof(int), typeof(TetrisWindow), new UIPropertyMetadata(1));
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

		DispatcherTimer gameTimer = new DispatcherTimer();

		public TetrisWindow()
		{
			InitializeComponent();
			gameTimer.Interval = TimeSpan.FromMilliseconds(50);
			gameTimer.Tick += new EventHandler(gameTimer_Tick);
            gameTimer.Start();
		}

        UIElement nextShape = new UIElement();
        Random generator = new Random();
        bool nextShapeExists = false;


		public UIElement GetCurrentShape()
		{
			return Board.Children[Board.Children.Count - 1];
		}

		public List<Shape> GetStackedShapes()
		{
			Shape currentShape = GetCurrentShape() as Shape;
			return Board.Children.OfType<Shape>().Where(shape => shape != currentShape).ToList();
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
                GenerateNewShape();
                activePiece = true;
            }
            if (activePiece)
            {
                if (!HitTestBottom())
                {
                    currentY += 5;
                    Canvas.SetTop(Board.Children[Board.Children.Count - 1], currentY);
                }
                else
                {
                    currentY = 0;
                    activePiece = false;
                }
            }
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
            Grid.SetRow(nextShape, 1);
            Grid1.Children.Add(nextShape);
        }

        private void Board_KeyDown(object sender, KeyEventArgs e)
        {
            int child = Board.Children.Count - 1;
            double pieceLeft = Canvas.GetLeft(Board.Children[child]);
            double pieceRight = Canvas.GetRight(Board.Children[child]);
            double right = Canvas.GetRight(Board);
            double left = Canvas.GetLeft(Board);

            switch (e.Key)
            {
                case Key.Up:
                    RotateCurrentShape();
                    break;
                case Key.Right:
                    if (pieceRight == right)
                    {
                        //piece will not move right
                    }
                    else
                    { 
                        //add code to move the piece right here
                        Canvas.SetLeft(Board.Children[child], pieceLeft + 20);
                    }
                    break;
                case Key.Left:
                    if (pieceLeft == left)
                    {
                        //piece will not move left
                    }
                    else
                    { 
                        //add code to move the piece left here
                        Canvas.SetLeft(Board.Children[child], pieceLeft - 20);
                    }
                    break;
                case Key.Down:
                    break;
                case Key.P:
                    break;
            }
        }

        int currentTransform = 0;

        private void RotateCurrentShape()
        {
            currentTransform += 90;
		  GetCurrentShape().RenderTransform = new RotateTransform(currentTransform, ((UserControl)GetCurrentShape()).ActualWidth / 2d, ((UserControl)GetCurrentShape()).ActualHeight / 2d);
        }

	   private bool HitTestBottom()
	   {
		   return currentY + 80 > 400;
	   }
	}
}
