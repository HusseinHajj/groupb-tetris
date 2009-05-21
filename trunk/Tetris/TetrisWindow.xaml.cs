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
            OShape o = new OShape();
            Board.Children.Add(o);
		}

		public Shape GetCurrentShape()
		{
			return Board.Children.OfType<Shape>().ElementAt(Board.Children.Count - 1);
		}

		public List<Shape> GetStackedShapes()
		{
			Shape currentShape = GetCurrentShape();
			return Board.Children.OfType<Shape>().Where(shape => shape != currentShape).ToList();
		}

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            if (!activePiece)
            {

            }
        }

        private void Board_KeyDown(object sender, KeyEventArgs e)
        {
            double left = Canvas.GetLeft(Board);
            double right = Canvas.GetRight(Board);

            int child = Board.Children.Count - 1;
            double pieceLeft = Canvas.GetLeft(Board.Children[child]);
            double pieceRight = Canvas.GetRight(Board.Children[child]);

            switch (e.Key)
            {//Board.Children[Board.Children.Count - 1]
                case Key.Up:
                    //add rotation before the check

                    pieceLeft = Canvas.GetLeft(Board.Children[child]);
                    pieceRight = Canvas.GetRight(Board.Children[child]);
                    if (pieceLeft < left)
                    { 
                        Canvas.SetLeft(Board.Children[child], left);
                    }
                    else if (pieceRight > right)
                    {
                        Canvas.SetRight(Board.Children[child], right);
                    }
                    break;
                case Key.Right:
                    if (pieceRight == right)
                    {
                        //piece will not move right
                    }
                    else
                    { 
                        //add code to move the piece right here
                        Canvas.SetRight(Board.Children[child], pieceRight + 20);
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
	}
}
