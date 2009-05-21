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
    /// Interaction logic for OShape.xaml
    /// </summary>
    public partial class OShape : UserControl, Shape
    {
	    public int Rotation { get; set; }
        public OShape()
        {
            InitializeComponent();
		  Rotation = 0;
        }

        #region Shape Members

        public List<Point> points
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

	   public void Rotate()
	   {
		   Rotation = (Rotation == 270) ? 0 : Rotation + 90;
		   RenderTransform = new RotateTransform(Rotation);
	   }

        public void BlockCollisionDetection()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
