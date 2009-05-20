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
        DispatcherTimer gameTimer = new DispatcherTimer();
        bool activePiece = false;
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
        }
	}
}
