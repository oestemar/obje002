using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace obje002
{

    public partial class Form1 : Form
    {
        Bitmap _bitmap;

        Random rand = new Random();

        int[] _circlesizes;
        Rectangle[] _circlerects;
        Point[] _circlexys;
        Pen[] _circlepens;
        SolidBrush[] _circlebrushes;
        vector[] _circlevecs;
        int circlenum;

        List<PointF[]> _xs;
        int[] _xsizes;
        Point[] _xcenters;
        Pen[] _xpens;
        SolidBrush[] _xbrushes;
        vector[] _xvecs;
        int xnum;
        int hexnum;
        int[] _xangles;
        int[] _xinclementangles;

        Boolean ssflag = false;

        List<PointF[]> _polygonsA;
        int[] _polygonsizesA;
        Point[] _polygoncentersA;
        Pen[] _polygonpensA;
        SolidBrush[] _polygonbrushesA;
        vector[] _polygonvecsA;
        int polygonnumA;
        int[] _polygonanglesA;
        int[] _polygoninclementanglesA;
        int vertexA;

        List<PointF[]> _polygonsB;
        int[] _polygonsizesB;
        Point[] _polygoncentersB;
        Pen[] _polygonpensB;
        SolidBrush[] _polygonbrushesB;
        vector[] _polygonvecsB;
        int polygonnumB;
        int[] _polygonanglesB;
        int[] _polygoninclementanglesB;
        int vertexB;

        int motionnumber;

        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            _bitmap = new Bitmap(pic.Width, pic.Height);
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;
            comboBox5.SelectedIndex = 0;
            comboBox6.SelectedIndex = 0;
            comboBox7.SelectedIndex = 0;

            initializeCircles();
            initializeXs();
            initializePolygonsA();
            initializePolygonsB();

        }
        private void initializeCircles()
        {
            _circlexys = new Point[circlenum];
            _circlesizes = new int[circlenum];
            _circlerects = new Rectangle[circlenum];
            _circlepens = new Pen[circlenum];
            _circlebrushes = new SolidBrush[circlenum];
            _circlevecs = new vector[circlenum];

            for (int i = 0; i < circlenum; i++)
            {
                _circlesizes[i] = rand.Next(15, 40);
                _circlexys[i] = new Point(rand.Next(50, 450), rand.Next(50, 250));

                _circlerects[i] = new Rectangle(_circlexys[i].X, _circlexys[i].Y, _circlesizes[i], _circlesizes[i]);

                _circlevecs[i] = velocity(rand);
                _circlepens[i] = pen(rand);
                _circlebrushes[i] = solidbrush(rand);

            }
        }

        private void initializeXs()
        {
            _xcenters = new Point[xnum];
            _xsizes = new int[xnum];
            _xs = new List<PointF[]>();
            _xpens = new Pen[xnum];
            _xbrushes = new SolidBrush[xnum];
            _xvecs = new vector[xnum];
            _xinclementangles = new int[xnum];
            _xangles = new int[xnum];

            for (int i = 0; i < xnum; i++)
            {
                _xsizes[i] = rand.Next(15, 40);
                _xcenters[i] = new Point(rand.Next(50, 450), rand.Next(50, 250));
                PointF[] _x = new PointF[4];
                _x[0] = new PointF(_xcenters[i].X + _xsizes[i] * (float)Math.Cos(45 * Math.PI / 180), _xcenters[i].Y + _xsizes[i] * (float)Math.Sin(45 * Math.PI / 180));
                _x[1] = new PointF(_xcenters[i].X + _xsizes[i] * (float)Math.Cos(135 * Math.PI / 180), _xcenters[i].Y + _xsizes[i] * (float)Math.Sin(135 * Math.PI / 180));
                _x[2] = new PointF(_xcenters[i].X + _xsizes[i] * (float)Math.Cos(225 * Math.PI / 180), _xcenters[i].Y + _xsizes[i] * (float)Math.Sin(225 * Math.PI / 180));
                _x[3] = new PointF(_xcenters[i].X + _xsizes[i] * (float)Math.Cos(315 * Math.PI / 180), _xcenters[i].Y + _xsizes[i] * (float)Math.Sin(315 * Math.PI / 180));

                _xs.Add(_x);

                _xvecs[i] = velocity(rand);
                _xangles[i] = 0;
                _xinclementangles[i] = rand.Next(5, 30);
                _xpens[i] = pen(rand);
                _xbrushes[i] = solidbrush(rand);

            }
        }

        private void initializePolygonsA()
        {
            _polygoncentersA = new Point[polygonnumA];
            _polygonsizesA = new int[polygonnumA];
            _polygonsA = new List<PointF[]>();
            _polygonpensA = new Pen[polygonnumA];
            _polygonbrushesA = new SolidBrush[polygonnumA];
            _polygonvecsA = new vector[polygonnumA];
            _polygonanglesA = new int[polygonnumA];
            _polygoninclementanglesA = new int[polygonnumA];

            for (int i = 0; i < polygonnumA; i++)
            {
                _polygonsizesA[i] = rand.Next(15, 40);
                _polygoncentersA[i] = new Point(rand.Next(50, 450), rand.Next(50, 250));
                PointF[] _p = new PointF[vertexA];
                for (int j = 0; j < vertexA; j++)
                {
                    _p[j] = new PointF(_polygoncentersA[i].X + _polygonsizesA[i] * (float)Math.Cos(360 / vertexA * j * Math.PI / 180), _polygoncentersA[i].Y + _polygonsizesA[i] * (float)Math.Sin(360 / vertexA * j * Math.PI / 180));
                }

                _polygonsA.Add(_p);

                _polygonvecsA[i] = velocity(rand);
                _polygonanglesA[i] = 0;
                _polygoninclementanglesA[i] = rand.Next(5, 30);
                _polygonpensA[i] = pen(rand);
                _polygonbrushesA[i] = solidbrush(rand);
            }
        }

        private void initializePolygonsB()
        {
            _polygoncentersB = new Point[polygonnumB];
            _polygonsizesB = new int[polygonnumB];
            _polygonsB = new List<PointF[]>();
            _polygonpensB = new Pen[polygonnumB];
            _polygonbrushesB = new SolidBrush[polygonnumB];
            _polygonvecsB = new vector[polygonnumB];
            _polygonanglesB = new int[polygonnumB];
            _polygoninclementanglesB = new int[polygonnumB];

            for (int i = 0; i < polygonnumB; i++)
            {
                _polygonsizesB[i] = rand.Next(15, 40);
                _polygoncentersB[i] = new Point(rand.Next(50, 450), rand.Next(50, 250));
                PointF[] _p = new PointF[vertexB];
                for (int j = 0; j < vertexB; j++)
                {
                    _p[j] = new PointF(_polygoncentersB[i].X + _polygonsizesB[i] * (float)Math.Cos(360 / vertexB * j * Math.PI / 180), _polygoncentersB[i].Y + _polygonsizesB[i] * (float)Math.Sin(360 / vertexB * j * Math.PI / 180));
                }

                _polygonsB.Add(_p);

                _polygonvecsB[i] = velocity(rand);
                _polygonanglesB[i] = 0;
                _polygoninclementanglesB[i] = rand.Next(5, 30);
                _polygonpensB[i] = pen(rand);
                _polygonbrushesB[i] = solidbrush(rand);

            }

        }

        private void drawObjects()
        {
            using (Graphics g = Graphics.FromImage(_bitmap))
            {
                g.Clear(Color.Teal);

                for (int i = 0; i < circlenum; i++)
                {
                    Rectangle circlerect = new Rectangle(_circlexys[i].X, _circlexys[i].Y, _circlesizes[i], _circlesizes[i]);

                    g.DrawEllipse(_circlepens[i], circlerect);
                    g.FillEllipse(_circlebrushes[i], circlerect);
                }

                for (int i = 0; i < xnum; i++)
                {
                    Point xcenter = new Point(_xcenters[i].X, _xcenters[i].Y);
                    PointF[] _x = new PointF[4];
                    _x[0] = new PointF(xcenter.X + _xsizes[i] * (float)Math.Cos((45 + _xangles[i]) * Math.PI / 180), xcenter.Y + _xsizes[i] * (float)Math.Sin((45 + _xangles[i]) * Math.PI / 180));
                    _x[1] = new PointF(xcenter.X + _xsizes[i] * (float)Math.Cos((135 + _xangles[i]) * Math.PI / 180), xcenter.Y + _xsizes[i] * (float)Math.Sin((135 + _xangles[i]) * Math.PI / 180));
                    _x[2] = new PointF(xcenter.X + _xsizes[i] * (float)Math.Cos((225 + _xangles[i]) * Math.PI / 180), xcenter.Y + _xsizes[i] * (float)Math.Sin((225 + _xangles[i]) * Math.PI / 180));
                    _x[3] = new PointF(xcenter.X + _xsizes[i] * (float)Math.Cos((315 + _xangles[i]) * Math.PI / 180), xcenter.Y + _xsizes[i] * (float)Math.Sin((315 + _xangles[i]) * Math.PI / 180));

                    for (int j = 0; j < 4; j++)
                    {
                        _xs[i][j] = _x[j];
                        g.DrawLine(_xpens[i], xcenter, _xs[i][j]);
                    }
                }

                for (int i = 0; i < polygonnumA; i++)
                {
                    Point polygoncenterA = new Point(_polygoncentersA[i].X, _polygoncentersA[i].Y);
                    PointF[] _p1 = new PointF[vertexA];
                    for (int j = 0; j < vertexA; j++)
                    {
                        _p1[j] = new PointF(polygoncenterA.X + _polygonsizesA[i] * (float)Math.Cos(((int)360 / vertexA * j + _polygonanglesA[i]) * Math.PI / 180), polygoncenterA.Y + _polygonsizesA[i] * (float)Math.Sin(((int)360 / vertexA * j + _polygonanglesA[i]) * Math.PI / 180));

                    }

                    for (int j = 0; j < vertexA; j++)
                    {
                        _polygonsA[i][j] = _p1[j];
                    }

                    g.DrawPolygon(_polygonpensA[i], _polygonsA[i]);
                    g.FillPolygon(_polygonbrushesA[i], _polygonsA[i]);
                }

                for (int i = 0; i < polygonnumB; i++)
                {
                    Point polygoncenterB = new Point(_polygoncentersB[i].X, _polygoncentersB[i].Y);
                    PointF[] _p2 = new PointF[vertexB];
                    for (int j = 0; j < vertexB; j++)
                    {
                        _p2[j] = new PointF(polygoncenterB.X + _polygonsizesB[i] * (float)Math.Cos((360 / vertexB * j + _polygonanglesB[i]) * Math.PI / 180), polygoncenterB.Y + _polygonsizesB[i] * (float)Math.Sin((360 / vertexB * j + _polygonanglesB[i]) * Math.PI / 180));
                    }


                    for (int j = 0; j < vertexB; j++)
                    {
                        _polygonsB[i][j] = _p2[j];
                    }

                    g.DrawPolygon(_polygonpensB[i], _polygonsB[i]);
                    g.FillPolygon(_polygonbrushesB[i], _polygonsB[i]);
                }
                pic.Image = _bitmap;
            }
        }

        private void moveCircles()
        {
            switch (motionnumber)
            {
                case 0:
                    for (int i = 0; i < circlenum; i++)
                    {
                        _circlexys[i].X += _circlevecs[i].VX;
                        _circlexys[i].Y += _circlevecs[i].VY;

                        if (_circlexys[i].X < 0 || _circlexys[i].X + _circlesizes[i] > pic.Width)
                        {
                            _circlevecs[i].VX *= -1;
                        }

                        if (_circlexys[i].Y < 0 || _circlexys[i].Y + _circlesizes[i] > pic.Height)
                        {
                            _circlevecs[i].VY *= -1;
                        }
                    }
                    break;
                case 1:
                    for (int i = 0; i < circlenum; i++)
                    {
                        if (_circlevecs[i].VX < 0)
                        {
                            _circlevecs[i].VX *= -1;
                        }

                        _circlexys[i].X += _circlevecs[i].VX;
                     

                        if (_circlexys[i].X > pic.Width)
                        {
                            _circlexys[i].X = -10;
                        }
                    }
                    break;
                case 2:
                    for (int i = 0; i < circlenum; i++)
                    {
                        if (_circlevecs[i].VX > 0)
                        {
                            _circlevecs[i].VX *= -1;
                        }

                        _circlexys[i].X += _circlevecs[i].VX;


                        if (_circlexys[i].X < _circlesizes[i] * -1)
                        {
                            _circlexys[i].X = pic.Width + 10;
                        }
                    }
                    break;
                case 3:
                    for (int i = 0; i < circlenum; i++)
                    {
                        if (_circlevecs[i].VY > 0)
                        {
                            _circlevecs[i].VY *= -1;
                        }

                        _circlexys[i].Y += _circlevecs[i].VY;


                        if (_circlexys[i].Y < _circlesizes[i] * -1)
                        {
                            _circlexys[i].Y = pic.Height + 10;
                        }
                    }
                    break;
                case 4:
                    for (int i = 0; i < circlenum; i++)
                    {
                        if (_circlevecs[i].VY < 0)
                        {
                            _circlevecs[i].VY *= -1;
                        }

                        _circlexys[i].Y += _circlevecs[i].VY;


                        if (_circlexys[i].Y > pic.Height)
                        {
                            _circlexys[i].Y = -10;
                        }
                    }
                    break;
                default:
                    break;

            }
        }
    

        private void moveXs()
        {
            switch (motionnumber)
            {
                case 0:
                    for (int i = 0; i < xnum; i++)
                    {
                        _xangles[i] += _xinclementangles[i];

                        if (_xangles[i] > 360)
                        {
                            _xangles[i] -= 360;
                        }

                        _xcenters[i].X += _xvecs[i].VX;
                        _xcenters[i].Y += _xvecs[i].VY;

                        if (_xcenters[i].X < _xsizes[i] || _xcenters[i].X + _xsizes[i] > pic.Width)
                        {
                            _xvecs[i].VX *= -1;
                        }

                        if (_xcenters[i].Y < _xsizes[i] || _xcenters[i].Y + _xsizes[i] > pic.Height)
                        {
                            _xvecs[i].VY *= -1;
                        }
                    }
                    break;
                case 1:
                    for (int i = 0; i < xnum; i++)
                    {
                        _xangles[i] += _xinclementangles[i];

                        if (_xangles[i] > 360)
                        {
                            _xangles[i] -= 360;
                        }

                        if (_xvecs[i].VX < 0)
                        {
                            _xvecs[i].VX *= -1;
                        }

                        _xcenters[i].X += _xvecs[i].VX;
                     
                        if (_xcenters[i].X - _xsizes[i] > pic.Width)
                        {
                            _xcenters[i].X = -10;
                        }
                    }
                    break;
                case 2:
                    for (int i = 0; i < xnum; i++)
                    {
                        _xangles[i] += _xinclementangles[i];

                        if (_xangles[i] > 360)
                        {
                            _xangles[i] -= 360;
                        }

                        if (_xvecs[i].VX > 0)
                        {
                            _xvecs[i].VX *= -1;
                        }

                        _xcenters[i].X += _xvecs[i].VX;

                        if (_xcenters[i].X < _xsizes[i] * -2) 
                        {
                            _xcenters[i].X = pic.Width + 10;
                        }
                    }
                    break;
                case 3:
                    for (int i = 0; i < xnum; i++)
                    {
                        _xangles[i] += _xinclementangles[i];

                        if (_xangles[i] > 360)
                        {
                            _xangles[i] -= 360;
                        }

                        if (_xvecs[i].VY > 0)
                        {
                            _xvecs[i].VY *= -1;
                        }

                        _xcenters[i].Y += _xvecs[i].VY;

                        if (_xcenters[i].Y < _xsizes[i] * -1)
                        {
                            _xcenters[i].Y = pic.Height + 10;
                        }
                    }
                    break;
                case 4:
                    for (int i = 0; i < xnum; i++)
                    {
                        _xangles[i] += _xinclementangles[i];

                        if (_xangles[i] > 360)
                        {
                            _xangles[i] -= 360;
                        }

                        if (_xvecs[i].VY < 0)
                        {
                            _xvecs[i].VY *= -1;
                        }

                        _xcenters[i].Y += _xvecs[i].VY;

                        if (_xcenters[i].Y - _xsizes[i] > pic.Height)
                        {
                            _xcenters[i].Y = _xsizes[i] * -1;
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        private void movePolygonsA()
        {
            switch (motionnumber)
            {
                case 0:
                    for (int i = 0; i < polygonnumA; i++)
                    {
                        _polygonanglesA[i] += _polygoninclementanglesA[i];

                        if (_polygonanglesA[i] > 360)
                        {
                            _polygonanglesA[i] -= 360;
                        }

                        _polygoncentersA[i].X += _polygonvecsA[i].VX;
                        _polygoncentersA[i].Y += _polygonvecsA[i].VY;

                        if (_polygoncentersA[i].X - _polygonsizesA[i] < 0 || _polygoncentersA[i].X + _polygonsizesA[i] > pic.Width)
                        {
                            _polygonvecsA[i].VX *= -1;
                        }

                        if (_polygoncentersA[i].Y - _polygonsizesA[i] < 0 || _polygoncentersA[i].Y + _polygonsizesA[i] > pic.Height)
                        {
                            _polygonvecsA[i].VY *= -1;
                        }
                    }
                    break;
                case 1:
                    for (int i = 0; i < polygonnumA; i++)
                    {
                        _polygonanglesA[i] += _polygoninclementanglesA[i];

                        if (_polygonanglesA[i] > 360)
                        {
                            _polygonanglesA[i] -= 360;
                        }

                        if (_polygonvecsA[i].VX < 0)
                        {
                            _polygonvecsA[i].VX *= -1;
                        }

                        _polygoncentersA[i].X += _polygonvecsA[i].VX;

                        if (_polygoncentersA[i].X - _polygonsizesA[i] / 2 > pic.Width)
                        {
                            _polygoncentersA[i].X = -10;
                        }
                    }
                    break;
                case 2:
                    for (int i = 0; i < polygonnumA; i++)
                    {
                        _polygonanglesA[i] += _polygoninclementanglesA[i];

                        if (_polygonanglesA[i] > 360)
                        {
                            _polygonanglesA[i] -= 360;
                        }

                        if (_polygonvecsA[i].VX > 0)
                        {
                            _polygonvecsA[i].VX *= -1;
                        }

                        _polygoncentersA[i].X += _polygonvecsA[i].VX;

                        if (_polygoncentersA[i].X + _polygonsizesA[i] / 2 < 0)
                        {
                            _polygoncentersA[i].X = pic.Width + 10;
                        }
                    }
                    break;
                case 3:
                    for (int i = 0; i < polygonnumA; i++)
                    {
                        _polygonanglesA[i] += _polygoninclementanglesA[i];

                        if (_polygonanglesA[i] > 360)
                        {
                            _polygonanglesA[i] -= 360;
                        }

                        if (_polygonvecsA[i].VY > 0)
                        {
                            _polygonvecsA[i].VY *= -1;
                        }

                        _polygoncentersA[i].Y += _polygonvecsA[i].VY;

                        if (_polygoncentersA[i].Y + _polygonsizesA[i] < 0)
                        {
                            _polygoncentersA[i].Y = pic.Height + 10;
                        }
                    }
                    break;
                case 4:
                    for (int i = 0; i < polygonnumA; i++)
                    {
                        _polygonanglesA[i] += _polygoninclementanglesA[i];

                        if (_polygonanglesA[i] > 360)
                        {
                            _polygonanglesA[i] -= 360;
                        }

                        if (_polygonvecsA[i].VY < 0)
                        {
                            _polygonvecsA[i].VY *= -1;
                        }

                        _polygoncentersA[i].Y += _polygonvecsA[i].VY;

                        if (_polygoncentersA[i].Y - _polygonsizesA[i] > pic.Height)
                        {
                            _polygoncentersA[i].Y = -10;
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        private void movePolygonsB()
        {
            switch (motionnumber)
            {
                case 0:
                    for (int i = 0; i < polygonnumB; i++)
                    {
                        _polygonanglesB[i] += _polygoninclementanglesB[i];

                        if (_polygonanglesB[i] > 360)
                        {
                            _polygonanglesB[i] -= 360;
                        }

                        _polygoncentersB[i].X += _polygonvecsB[i].VX;
                        _polygoncentersB[i].Y += _polygonvecsB[i].VY;

                        if (_polygoncentersB[i].X - _polygonsizesB[i] < 0 || _polygoncentersB[i].X + _polygonsizesB[i] > pic.Width)
                        {
                            _polygonvecsB[i].VX *= -1;
                        }

                        if (_polygoncentersB[i].Y - _polygonsizesB[i] < 0 || _polygoncentersB[i].Y + _polygonsizesB[i] > pic.Height)
                        {
                            _polygonvecsB[i].VY *= -1;
                        }
                    }
                    break;
                case 1:
                    for (int i = 0; i < polygonnumB; i++)
                    {
                        _polygonanglesB[i] += _polygoninclementanglesB[i];

                        if (_polygonanglesB[i] > 360)
                        {
                            _polygonanglesB[i] -= 360;
                        }

                        if (_polygonvecsB[i].VX < 0)
                        {
                            _polygonvecsB[i].VX *= -1;
                        }

                        _polygoncentersB[i].X += _polygonvecsB[i].VX;

                        if (_polygoncentersB[i].X - _polygonsizesB[i] > pic.Width)
                        {
                            _polygoncentersB[i].X = _polygonsizesB[i] * -1;
                        }
                    }
                    break;
                case 2:
                    for (int i = 0; i < polygonnumB; i++)
                    {
                        _polygonanglesB[i] += _polygoninclementanglesB[i];

                        if (_polygonanglesB[i] > 360)
                        {
                            _polygonanglesB[i] -= 360;
                        }

                        if (_polygonvecsB[i].VX > 0)
                        {
                            _polygonvecsB[i].VX *= -1;
                        }

                        _polygoncentersB[i].X += _polygonvecsB[i].VX;

                        if (_polygoncentersB[i].X + _polygonsizesB[i] < 0)
                        {
                            _polygoncentersB[i].X = pic.Width + _polygonsizesB[i];
                        }
                    }
                    break;
                case 3:
                    for (int i = 0; i < polygonnumB; i++)
                    {
                        _polygonanglesB[i] += _polygoninclementanglesB[i];

                        if (_polygonanglesB[i] > 360)
                        {
                            _polygonanglesB[i] -= 360;
                        }

                        if (_polygonvecsB[i].VY > 0)
                        {
                            _polygonvecsB[i].VY *= -1;
                        }

                        _polygoncentersB[i].Y += _polygonvecsB[i].VY;

                        if (_polygoncentersB[i].Y + _polygonsizesB[i] < 0)
                        {
                            _polygoncentersB[i].Y = pic.Height + _polygonsizesB[i];
                        }
                    }
                    break;
                case 4:
                    for (int i = 0; i < polygonnumB; i++)
                    {
                        _polygonanglesB[i] += _polygoninclementanglesB[i];

                        if (_polygonanglesB[i] > 360)
                        {
                            _polygonanglesB[i] -= 360;
                        }

                        if (_polygonvecsB[i].VY < 0)
                        {
                            _polygonvecsB[i].VY *= -1;
                        }

                        _polygoncentersB[i].Y += _polygonvecsB[i].VY;

                        if (_polygoncentersB[i].Y - _polygonsizesB[i] > pic.Height)
                        {
                            _polygoncentersB[i].Y = -10;
                        }
                    }
                    break;
                default:
                    break;

            }
        }

        class vector
        {
            public int VX;
            public int VY;

            public vector(int vx, int vy)
            {
                this.VX = vx;
                this.VY = vy;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            initializeCircles();
            initializeXs();
            initializePolygonsA();
            initializePolygonsB();
            drawObjects();
        }



        private void timer1_Tick(object sender, EventArgs e)
        {
            moveCircles();
            moveXs();
            movePolygonsA();
            movePolygonsB();
            drawObjects();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (ssflag == false)
            {
                ssflag = true;
                timer1.Start();
            }
            else
            {
                ssflag = false;
                timer1.Stop();
            }
        }

        private vector velocity(Random rand)
        {
            return new vector(rand.Next(2, 10), rand.Next(2, 10));
        }

        private Pen pen(Random rand)
        {
            return new Pen(Color.FromArgb(rand.Next(200, 250), rand.Next(150, 250), rand.Next(150, 250), rand.Next(150, 250)));

        }

        private SolidBrush solidbrush(Random rand)
        {
            return new SolidBrush(Color.FromArgb(rand.Next(150, 250), rand.Next(150, 250), rand.Next(150, 250)));

        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            xnum = Convert.ToInt32(comboBox1.SelectedItem);
            initializeXs();
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            polygonnumA = Convert.ToInt32(comboBox2.SelectedItem);
            initializePolygonsA();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            polygonnumB = Convert.ToInt32(comboBox3.SelectedItem);
            initializePolygonsB();
        }
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            circlenum = Convert.ToInt32(comboBox4.SelectedItem);
            initializeCircles();
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            vertexA = Convert.ToInt32(comboBox5.SelectedItem);
            initializePolygonsA();
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            vertexB = Convert.ToInt32(comboBox6.SelectedItem);
            initializePolygonsB();
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            _bitmap = new Bitmap(pic.Width, pic.Height);
        }

        
        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            motionnumber = comboBox7.SelectedIndex;
        }
    }
}
