using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Shapes;

namespace Tetris
{
    public interface Shape
    {
        List<Point> pointsTop { get; set; }
        List<Point> pointsBottom { get; set; }
        List<Point> pointsLeft { get; set; }
        List<Point> pointsRight { get; set; }
	   Rectangle[,] Arrangement { get; set; }
    }

}
