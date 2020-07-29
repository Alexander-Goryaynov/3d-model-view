using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3dModelView
{
    /// <summary>
    /// Класс является основой для матриц проекции и поворота
    /// </summary>
    public class Matrix
    {
        public float[,] cells;
        public Matrix()
        {
            cells = new float[4, 4];
        }
    }
}
