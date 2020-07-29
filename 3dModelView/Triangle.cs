using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3dModelView
{
    /// <summary>
    /// Треугольник из векторов
    /// </summary>
    public class Triangle
    {
        /// <summary>
        /// 3D координаты вершин треугольника
        /// </summary>
        public Vector[] pnts;
        public Triangle()
        {
            pnts = new Vector[3];
        }
        public Triangle(float x1, float y1, float z1,
            float x2, float y2, float z2,
            float x3, float y3, float z3)
        {
            pnts = new Vector[] {
                new Vector(x1, y1, z1),
                new Vector(x2, y2, z2),
                new Vector(x3, y3, z3)
            };
        }
    }
}
