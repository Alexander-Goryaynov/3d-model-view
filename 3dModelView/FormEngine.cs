using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _3dModelView
{
    public partial class FormEngine : Form
    {
        /// <summary>
        /// Отображаемый бриллиант
        /// </summary>
        private Mesh diamond;
        /// <summary>
        /// Проекционная матрица
        /// </summary>
        private Matrix mProj;

        // Углы поворота вокруг осей X, Y, Z
        private float theta;
        private float fita;
        private float gamma;
        private float distanceToModel = 6f;
        public FormEngine()
        {
            InitializeComponent();
            mProj = new Matrix();
        }

        private void FormEngine_Load(object sender, EventArgs e)
        {
            theta = 0;
            fita = 0;
            gamma = 0;
            diamond = new Mesh();
            diamond.triangles.AddRange(new List<Triangle> {
                // нижняя половина
                new Triangle(0f, 0f, 0f,  -1f, 1.5f, -1f,  1f, 1.5f, -1f),
                new Triangle(0f, 0f, 0f,  -1f, 1.5f, 1f,  1f, 1.5f, 1f),
                new Triangle(0f, 0f, 0f,  -1f, 1.5f, 1f,  -1f, 1.5f, -1f),
                new Triangle(0f, 0f, 0f,  1f, 1.5f, 1f,  1f, 1.5f, -1f),
                // верхняя половина
                new Triangle(1f, 1.5f, -1f,  -1f, 1.5f, -1f,  -.6f, 2f, -.6f),
                new Triangle(1f, 1.5f, -1f,  -.6f, 2f, -.6f,  .6f, 2f, -.6f),
                new Triangle(1f, 1.5f, -1f,  .6f, 2f, -.6f,  1f, 1.5f, 1f),
                new Triangle(.6f, 2f, -.6f,  1f, 1.5f, 1f,  .6f, 2f, .6f),
                new Triangle(.6f, 2f, .6f,  -.6f, 2f, .6f,  1f, 1.5f, 1f),
                new Triangle(-1f, 1.5f, 1f,  1f, 1.5f, 1f,  -.6f, 2f, .6f),
                new Triangle(-1f, 1.5f, 1f,  -.6f, 2f, .6f,  -1f, 1.5f, -1f),
                new Triangle(-1f, 1.5f, -1f,  -.6f, 2f, .6f,  -.6f, 2f, -.6f)

            });
            // расстояние до оси X
            float near = 0.1f;
            // максимальное расстояние видимых объектов
            float far = 1000.0f;
            // угол обзора
            float fieldOfViewAngle = 60.0f;
            // соотношение ширина/высота экрана
            float aspRatio = canvas.Height / (float)canvas.Width;
            float fieldOfViewRadius = 1.0f / (float)Math.Tan(fieldOfViewAngle * 0.5f / 180.0f * 3.1415f);
            mProj.cells[0, 0] = aspRatio * fieldOfViewRadius;
            mProj.cells[1, 1] = fieldOfViewRadius;
            // нормализация относительно Z
            mProj.cells[2, 2] = far / (far - near);
            mProj.cells[3, 2] = (-far * near) / (far - near);
            mProj.cells[2, 3] = 1.0f;
            mProj.cells[3, 3] = 0.0f;
            Timer timer = new Timer { Interval = 10 };
            timer.Tick += new EventHandler(DrawMesh);
            timer.Start();
        }
        /// <summary>
        /// Умножает матрицу на вектор
        /// </summary>
        /// <param name="i">Входной вектор</param>
        /// <param name="o">Ссылка на результирующий вектор</param>
        /// <param name="m">Входная матрица</param>
        private void MultiplyMatrixVector(Vector i, Matrix m, out Vector o)
        {
            o = new Vector
            {
                x = i.x * m.cells[0, 0] + i.y * m.cells[1, 0] + i.z * m.cells[2, 0] + m.cells[3, 0],
                y = i.x * m.cells[0, 1] + i.y * m.cells[1, 1] + i.z * m.cells[2, 1] + m.cells[3, 1],
                z = i.x * m.cells[0, 2] + i.y * m.cells[1, 2] + i.z * m.cells[2, 2] + m.cells[3, 2]
            };
            var w = i.x * m.cells[0, 3] + i.y * m.cells[1, 3] + i.z * m.cells[2, 3] + m.cells[3, 3];
            if (w != 0.0)
            {
                o.x /= w; o.y /= w; o.z /= w;
            }
        }

        private void DrawMesh(object obj, EventArgs args)
        {
            Bitmap bmp = new Bitmap(canvas.Width, canvas.Height);
            Graphics g = Graphics.FromImage(bmp);
            Brush brush = new SolidBrush(Color.Black);
            // фон
            g.FillRectangle(brush, 0, 0, canvas.Width, canvas.Height);
            // матрицы поворота
            Matrix mRotX = new Matrix();
            Matrix mRotY = new Matrix();
            Matrix mRotZ = new Matrix();
            // получаем координаты мыши
            Cursor = new Cursor(Cursor.Current.Handle);
            int xCoordinate = Cursor.Position.X;
            int yCoordinate = Cursor.Position.Y;
            theta = (float)yCoordinate / 50;
            fita = (float)xCoordinate / 50;
            // установка матрицы поворота вокруг оси Z
            mRotZ.cells[0,0] = (float)Math.Cos(gamma);
            mRotZ.cells[0,1] = (float)Math.Sin(gamma);
            mRotZ.cells[1,0] = (float)-Math.Sin(gamma);
            mRotZ.cells[1,1] = (float)Math.Cos(gamma);
            mRotZ.cells[2,2] = 1;
            mRotZ.cells[3,3] = 1;
            // установка матрицы поворота вокруг оси Y
            mRotY.cells[0, 0] = (float)Math.Cos(fita);
            mRotY.cells[0, 2] = (float)Math.Sin(fita);
            mRotY.cells[2, 0] = (float)-Math.Sin(fita);
            mRotY.cells[1, 1] = 1;
            mRotY.cells[2, 2] = (float)Math.Cos(fita);
            mRotY.cells[3, 3] = 1;
            // установка матрицы поворота вокруг оси X
            mRotX.cells[0, 0] = 1;
            mRotX.cells[1, 1] = (float)Math.Cos(theta * 0.5f);
            mRotX.cells[1, 2] = (float)Math.Sin(theta * 0.5f);
            mRotX.cells[2, 1] = (float)-Math.Sin(theta * 0.5f);
            mRotX.cells[2, 2] = (float)Math.Cos(theta * 0.5f);
            mRotX.cells[3, 3] = 1;
            canvas.Image = bmp;
            // отрисовка треугольников
            foreach (var tr in diamond.triangles)
            {
                var projected = new Triangle();
                var moved = new Triangle();
                var rotatedY = new Triangle();
                var rotatedXY = new Triangle();
                var rotatedXYZ = new Triangle();
                // поворот вокруг оси Y
                MultiplyMatrixVector(tr.pnts[0], mRotY, out rotatedY.pnts[0]);
                MultiplyMatrixVector(tr.pnts[1], mRotY, out rotatedY.pnts[1]);
                MultiplyMatrixVector(tr.pnts[2], mRotY, out rotatedY.pnts[2]);
                // поворот вокруг оси X
                MultiplyMatrixVector(rotatedY.pnts[0], mRotX, out rotatedXY.pnts[0]);
                MultiplyMatrixVector(rotatedY.pnts[1], mRotX, out rotatedXY.pnts[1]);
                MultiplyMatrixVector(rotatedY.pnts[2], mRotX, out rotatedXY.pnts[2]);
                // поворот вокруг оси Z
                MultiplyMatrixVector(rotatedXY.pnts[0], mRotZ, out rotatedXYZ.pnts[0]);
                MultiplyMatrixVector(rotatedXY.pnts[1], mRotZ, out rotatedXYZ.pnts[1]);
                MultiplyMatrixVector(rotatedXY.pnts[2], mRotZ, out rotatedXYZ.pnts[2]);
                // сдвиг вдаль
                moved = rotatedXYZ;
                moved.pnts[0].z = rotatedXYZ.pnts[0].z + distanceToModel;
                moved.pnts[1].z = rotatedXYZ.pnts[1].z + distanceToModel;
                moved.pnts[2].z = rotatedXYZ.pnts[2].z + distanceToModel;
                // проекция в 2D
                MultiplyMatrixVector(moved.pnts[0], mProj, out projected.pnts[0]);
                MultiplyMatrixVector(moved.pnts[1], mProj, out projected.pnts[1]);
                MultiplyMatrixVector(moved.pnts[2], mProj, out projected.pnts[2]);
                // масштабирование
                projected.pnts[0].x += 1.0f;
                projected.pnts[0].y += 1.0f;
                projected.pnts[1].x += 1.0f;
                projected.pnts[1].y += 1.0f;
                projected.pnts[2].x += 1.0f;
                projected.pnts[2].y += 1.0f;
                projected.pnts[0].x *= 0.5f * canvas.Width;
                projected.pnts[0].y *= 0.5f * canvas.Height;
                projected.pnts[1].x *= 0.5f * canvas.Width;
                projected.pnts[1].y *= 0.5f * canvas.Height;
                projected.pnts[2].x *= 0.5f * canvas.Width;
                projected.pnts[2].y *= 0.5f * canvas.Height;
                DrawTriangle(g, projected.pnts[0].x, projected.pnts[0].y,
                    projected.pnts[1].x, projected.pnts[1].y,
                    projected.pnts[2].x, projected.pnts[2].y);
            }
        }

        private void DrawTriangle(Graphics g, float x1, float y1,
            float x2, float y2, float x3, float y3)
        {
            Pen pen = new Pen(Color.Cyan, 2.0f);
            g.DrawLine(pen, x1, y1, x2, y2);
            g.DrawLine(pen, x2, y2, x3, y3);
            g.DrawLine(pen, x3, y3, x1, y1);
        }

        private void FormEngine_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch(e.KeyChar)
            {
                case 'w':
                    distanceToModel -= .25f;
                    break;
                case 'ц':
                    distanceToModel -= .25f;
                    break;
                case 's':
                    distanceToModel += .25f;
                    break;
                case 'ы':
                    distanceToModel += .25f;
                    break;
                default:
                    break;
            }
        }

        private void canvas_Click(object sender, EventArgs e)
        {
            gamma += .3f;
        }
    }

    /*
     * Thanks a lot to OneLoneCoder (javidx9) for the 3D Graphics Engine tutorial
    */
}
