using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris
{
    public class IShapeModel : Shape
    {
        #region Shape Members

        List<System.Windows.Point> Shape.points
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

        void Shape.BlockCollisionDetection()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
