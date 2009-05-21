﻿using System;
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
    /// Interaction logic for IShape.xaml
    /// </summary>
    public partial class IShape : UserControl, Shape
    {
        public IShapeModel Model { get; set; }
	   public int Rotation { get; set; }
        public IShape()
        {
            InitializeComponent();
		  Rotation = 0;
        }

        #region Shape Members

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

	   #endregion
    }
}