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

namespace Tetris
{
	/// <summary>
	/// Interaction logic for LShape.xaml
	/// </summary>
	public partial class LShape : UserControl, Shape
	{
		public LShape()
		{
			InitializeComponent();

			Arrangement = new Rectangle[,] {
				{ GridRoot.Children[0] as Rectangle, null },
				{ GridRoot.Children[3] as Rectangle, null },
				{ GridRoot.Children[2] as Rectangle, GridRoot.Children[1] as Rectangle}
			};
		}

		#region Shape Members

		public List<Point> pointsTop
		{
			get
			{
				return pointsTop;
			}
			set
			{
				pointsTop = new List<Point> { new Point(0, 0), new Point(1, 0), new Point(2, 2) };
			}
		}
		public List<Point> pointsRight
		{
			get
			{
				return pointsRight;
			}
			set
			{
				pointsRight = new List<Point> { new Point(1, 0), new Point(1, 1), new Point(2, 2), new Point(2, 3) };
			}
		}
		public List<Point> pointsBottom
		{
			get
			{
				return pointsBottom;
			}
			set
			{
				pointsBottom = new List<Point> { new Point(0, 3), new Point(0, 1), new Point(0, 2) };
			}
		}
		public List<Point> pointsLeft
		{
			get
			{
				return pointsLeft;
			}
			set
			{
				pointsLeft = new List<Point> { new Point(0, 0), new Point(0, 1), new Point(0, 2), new Point(0, 3) };
			}
		}

		public Rectangle[,] Arrangement { get; set; }

		#endregion
	}
}