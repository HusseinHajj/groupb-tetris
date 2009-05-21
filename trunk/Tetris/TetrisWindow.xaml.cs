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
		}

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            if (!activePiece)
            {

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

        private void Board_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up:
                    break;
                case Key.Right:
                    break;
                case Key.Left:
                    break;
                case Key.Down:
                    break;
                case Key.P:
                    break;
            }
        }
	}
}
