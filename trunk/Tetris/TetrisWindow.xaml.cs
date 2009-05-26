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

            Canvas.SetLeft(Board, 0);
            Canvas.SetRight(Board, Board.Width);

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

        bool needsShift;
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
            needsShift = true;
            Grid.SetRow(nextShape, 1);
            Grid1.Children.Add(nextShape);
        }

        private void Board_KeyDown(object sender, KeyEventArgs e)
        {
            int child = Board.Children.Count - 1;
            double pieceLeft = Canvas.GetLeft(Board.Children[child]);

            switch (e.Key)
            {
                case Key.Up:
                    RotateCurrentShape();
                    if (needsShift == true)
                    {
                        //shift shape to correct rotation offset
                        needsShift = false;
                    }
                    else
                    {
                        needsShift = true;
                    }
                    break;
                case Key.Right:
                    double pieceRight = Canvas.GetLeft(Board.Children[child]) + (this.GetCurrentShape() as UserControl).Width;
                    double right = Canvas.GetRight(Board);
                    if (pieceRight >= right)
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
                    
                    double left = Canvas.GetLeft(Board);
                    if (pieceLeft <= left)
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
				gameTimer_Tick(sender, null);
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

	   private List<Point> GetShapePoints(Shape shape, bool top)
	   {
		   if (Board.Children.OfType<Shape>().Contains(shape))
		   {
			   UserControl shapeUC = (UserControl)shape;
			   int angle = (shapeUC.RenderTransform is RotateTransform) ? (int)shapeUC.RenderTransform.GetValue(RotateTransform.AngleProperty) : 0;
			   int rotation = (top) ? angle % 90 : (angle + 180) % 90;
			   List<Point> points =
				   rotation == 0 ? shape.pointsTop :
				   rotation == 1 ? shape.pointsRight :
				   rotation == 2 ? shape.pointsBottom : shape.pointsLeft;
			   return points.Select(point => new Point(point.X + shapeUC.ActualWidth, point.Y + shapeUC.ActualHeight)).ToList();
		   }
		   return null;
	   }

	   private bool HitTestBottom()
	   {
		   //int rotation = currentTransform % 90;
		   //List<Point> currentPoints = GetShapePoints((Shape)GetCurrentShape(), true);
		   //List<Shape> shapes = GetStackedShapes();
		   //foreach (Shape shape in shapes)
		   //{
		   //     List<Point> comparePoints = GetShapePoints(shape, false);

		   //}
		   return currentY + 80 > 400;
	   }
	}
}
