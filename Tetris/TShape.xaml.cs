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
	/// Interaction logic for TShape.xaml
	/// </summary>
	public partial class TShape : UserControl, Shape
	{
		public TShape()
		{
			InitializeComponent();

			Arrangement = new Rectangle[,] {
				{ null, GridRoot.Children[2] as Rectangle, null },
				{ GridRoot.Children[0] as Rectangle, GridRoot.Children[1] as Rectangle, GridRoot.Children[3] as Rectangle }
			};
		}

		#region Shape Members

		public Rectangle[,] Arrangement { get; set; }

		#endregion
	}
}