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

        //public List<Shape> GetStackedShapes()
        //{
        //    Shape currentShape = GetCurrentShape();
        //    return Board.Children.OfType<Shape>().Where(shape => shape != currentShape).ToList();
        //}

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
                if (currentY + 80 <= 400)
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



            /*pseudocode for boundry checking. can be moved anywhere
             * On left/right click += LeftRightEvent
             * LeftRightevent()
             * {
             *      onLeftClick
             *      if(leftClickResult.location.X < boardBoarder.Location.leftX)
             *      {
             *          don't allow move
             *      }
             *      onRightClick
             *      if(rightClickResult.Location.X > boardBoarder.Location.rightX)
             *      {
             *          don't allow move
             *      }
             * }
             * On rotation += RotationEvent
             * RotationEvent()
             * {
             *      if(rotationResult.location.X < boardBoarder.Location.leftX)
             *      {
             *          item.location = item.location + (boardBoarder.Location.leftX - rotationResult.location.X)
             *      }
             *      else if(rotationResult.location.X > board.Boarder.Location.rightX)
             *      {
             *          item.location = item.location - (rotationResult.location.X - boardBoarder.Location.rightX)
             *      }
             * }
            */
        }

        private void GenerateNewShape()
        {
            switch (generator.Next() % 7)
            {
                case 0:
                    IShape newShape = new IShape();
                    nextShape = newShape;
                    break;
                case 1:
                    JShape newShape1 = new JShape();
                    nextShape = newShape1;
                    break;
                case 2:
                    LShape newShape2 = new LShape();
                    nextShape = newShape2;
                    break;
                case 3:
                    OShape newShape3 = new OShape();
                    nextShape = newShape3;
                    break;
                case 4:
                    ZShape newShape4 = new ZShape();
                    nextShape = newShape4;
                    break;
                case 5:
                    S_Shape newShape5 = new S_Shape();
                    nextShape = newShape5;
                    break;
                case 6:
                    TShape newShape6 = new TShape();
                    nextShape = newShape6;
                    break;
            }
            Grid.SetRow(nextShape, 1);
            Grid1.Children.Add(nextShape);
        }

        private void Board_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up:
                    RotateCurrentShape();
                    break;
                case Key.Right:
                    //if (pieceRight == right)
                    //{
                    //    //piece will not move right
                    //}
                    //else
                    //{ 
                    //    //add code to move the piece right here
                    //    Canvas.SetRight(Board.Children[child], pieceRight + 20);
                    //}
                    break;
                case Key.Left:
                    //if (pieceLeft == left)
                    //{
                    //    //piece will not move left
                    //}
                    //else
                    //{ 
                    //    //add code to move the piece left here
                    //    Canvas.SetLeft(Board.Children[child], pieceLeft - 20);
                    //}
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
            Board.Children[Board.Children.Count - 1].RenderTransform = new RotateTransform(currentTransform, 30, 20);
        }
	}
}
