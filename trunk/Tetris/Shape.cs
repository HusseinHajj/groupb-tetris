using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Tetris
{
    public interface Shape
    {
        List<Point> points { get; set; }
        void BlockCollisionDetection();
    }

}
