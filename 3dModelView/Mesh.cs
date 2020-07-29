using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3dModelView
{
    /// <summary>
    /// Объёмная фигура
    /// </summary>
    public class Mesh
    {
        /// <summary>
        /// Треугольники, из которых состоит фигуры
        /// </summary>
        public List<Triangle> triangles;
        public Mesh()
        {
            triangles = new List<Triangle>();
        }
    }
}
