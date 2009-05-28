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
	   Rectangle[,] Arrangement { get; set; }
    }

}
