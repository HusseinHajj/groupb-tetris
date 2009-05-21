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
    /// Interaction logic for JShape.xaml
    /// </summary>
    public partial class JShape : UserControl, Shape
    {
        public JShape Model { get; set; }
        public JShape()
        {
            InitializeComponent();
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

        public void BlockCollisionDetection()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
