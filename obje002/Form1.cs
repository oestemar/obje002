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

        //円に関する変数
        //variablename about circles

        int[] _circlesizes;
        Rectangle[] _circlerects;
        Point[] _circlexys;
        Pen[] _circlepens;
        SolidBrush[] _circlebrushes;
        vector[] _circlevecs;
        int circlenum;

        //×に関する変数
        //variablename about Xs

        List<PointF[]> _xs;
        int[] _xsizes;
        Point[] _xcenters;
        Pen[] _xpens;
        SolidBrush[] _xbrushes;
        vector[] _xvecs;
        int xnum;
        int[] _xangles;
        int[] _xinclementangles;

        Boolean ssflag = false;

        //多角形に関する変数
        //variable about polygons

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

        //北極星を中心にした回転用の変数
        //for polarrotate 

        Point polarxy;

        float[] _polaranglesC;
        float[] _polaranglesIC;
        float[] _polarsidesC;

        float[] _polaranglesX;
        float[] _polaranglesIX;
        float[] _polarsidesX;

        float[] _polaranglesPA;
        float[] _polaranglesIPA;
        float[] _polarsidesPA;

        float[] _polaranglesPB;
        float[] _polaranglesIPB;
        float[] _polarsidesPB;

        Boolean polarflag = false;
        SolidBrush polarcolor;

        public Form1()
        {
            InitializeComponent();
        }

        //　フォーム1をロードしたときの各種初期値設定
        // initialize values at form1 loading
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

            polarxy = new Point(pic.Width / 2, pic.Height / 2);

            initializeCircles();
            initializeXs();
            initializePolygonsA();
            initializePolygonsB();

        }

        //　円の初期値
        // initialize values about circles
        private void initializeCircles()
        {
            _circlexys = new Point[circlenum];
            _circlesizes = new int[circlenum];
            _circlerects = new Rectangle[circlenum];
            _circlepens = new Pen[circlenum];
            _circlebrushes = new SolidBrush[circlenum];
            _circlevecs = new vector[circlenum];
            
            _polaranglesC = new float[circlenum];
            _polaranglesIC = new float[circlenum];
            _polarsidesC = new float[circlenum];

            for (int i = 0; i < circlenum; i++)
            {
                _circlesizes[i] = rand.Next(15, 40);
                _circlexys[i] = xy(rand);

                _circlerects[i] = new Rectangle(_circlexys[i].X, _circlexys[i].Y, _circlesizes[i], _circlesizes[i]);

                _circlevecs[i] = velocity(rand);
                _circlepens[i] = pen(rand);
                _circlebrushes[i] = solidbrush(rand);

                _polaranglesC[i] = getstartangle(_circlexys[i].X - polarxy.X, _circlexys[i].Y - polarxy.Y);
                _polaranglesIC[i] = (float)rand.Next(10, 50) / 50;
                _polarsidesC[i] = getsides(_circlexys[i]);
            }
        }

        //　Xの初期値
        // initialize values about Xs
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

            _polaranglesX = new float[xnum];
            _polaranglesIX = new float[xnum];
            _polarsidesX = new float[xnum];

            for (int i = 0; i < xnum; i++)
            {
                _xsizes[i] = rand.Next(15, 40);
                _xcenters[i] = xy(rand);
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

                _polaranglesX[i] = getstartangle(_xcenters[i].X - polarxy.X, _xcenters[i].Y - polarxy.Y);
                _polaranglesIX[i] = (float)rand.Next(10, 50) / 50;
                _polarsidesX[i] = getsides(_xcenters[i]); ;
            }
        }

        //　多角形Aの初期値
        // initialize values about polygonsA
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

            _polaranglesPA = new float[polygonnumA];
            _polaranglesIPA = new float[polygonnumA];
            _polarsidesPA = new float[polygonnumA];

            for (int i = 0; i < polygonnumA; i++)
            {
                _polygonsizesA[i] = rand.Next(15, 40);
                _polygoncentersA[i] = xy(rand);
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

                _polaranglesPA[i] = getstartangle(_polygoncentersA[i].X - polarxy.X, _polygoncentersA[i].Y - polarxy.Y);
                _polaranglesIPA[i] = (float)rand.Next(10, 50) / 50;
                _polarsidesPA[i] = getsides(_polygoncentersA[i]);
            }
        }

        //　多角形Bの初期値
        // initialize values about polygonsB

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

            _polaranglesPB = new float[polygonnumB];
            _polaranglesIPB = new float[polygonnumB];
            _polarsidesPB = new float[polygonnumB];

            for (int i = 0; i < polygonnumB; i++)
            {
                _polygonsizesB[i] = rand.Next(15, 40);
                _polygoncentersB[i] = xy(rand);
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

                _polaranglesPB[i] = getstartangle(_polygoncentersB[i].X - polarxy.X, _polygoncentersB[i].Y - polarxy.Y);
                _polaranglesIPB[i] = (float)rand.Next(10, 50) / 50;
                _polarsidesPB[i] = getsides(_polygoncentersB[i]); ;

            }

        }

        //　各種図形の一斉描画
        // drawing on every objects
        private void drawObjects()
        {
            using (Graphics g = Graphics.FromImage(_bitmap))
            {
                g.Clear(Color.Teal);

                if (polarflag)
                {
                    polarcolor = new SolidBrush(Color.FromArgb(250, 250, 200));
                    g.FillEllipse(polarcolor, polarxy.X, polarxy.Y, 10, 10);
                }

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

        //　円を動かす
        // each motions of circles
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
                case 5:
                    for (int i = 0; i < circlenum; i++)
                    {
                        _polaranglesC[i] += _polaranglesIC[i];
                        
                        if (_polaranglesC[i] > 360)
                        {
                            _polaranglesC[i] = 0;
                        }

                        _circlexys[i].X = (int)(polarxy.X + _polarsidesC[i] * (float)Math.Cos(_polaranglesC[i] * Math.PI / 180));
                        _circlexys[i].Y = (int)(polarxy.Y + _polarsidesC[i] * (float)Math.Sin(_polaranglesC[i] * Math.PI / 180));
                    }
                    break;
                default:
                    break;

            }
        }

        //　Xを動かす
        // each motions of Xs

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
                case 5:
                    for (int i = 0; i < xnum; i++)
                    {
                        _xangles[i] += _xinclementangles[i];

                        if (_xangles[i] > 360)
                        {
                            _xangles[i] -= 360;
                        }

                        _polaranglesX[i] += _polaranglesIX[i];

                        if (_polaranglesX[i] > 360)
                        {
                            _polaranglesX[i] = 0;
                        }

                        _xcenters[i].X = (int)(polarxy.X + _polarsidesX[i] * (float)Math.Cos(_polaranglesX[i] * Math.PI / 180));
                        _xcenters[i].Y = (int)(polarxy.Y + _polarsidesX[i] * (float)Math.Sin(_polaranglesX[i] * Math.PI / 180));
                    }
                    break;
                default:
                    break;
            }
        }

        //　多角形Aを動かす
        // each motions of polygonsA

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
                case 5:
                    for (int i = 0; i < polygonnumA; i++)
                    {
                        _polygonanglesA[i] += _polygoninclementanglesA[i];

                        if (_polygonanglesA[i] > 360)
                        {
                            _polygonanglesA[i] -= 360;
                        }

                        _polaranglesPA[i] += _polaranglesIPA[i];

                        if (_polaranglesPA[i] > 360)
                        {
                            _polaranglesPA[i] = 0;
                        }

                        _polygoncentersA[i].X = (int)(polarxy.X + _polarsidesPA[i] * (float)Math.Cos(_polaranglesPA[i] * Math.PI / 180));
                        _polygoncentersA[i].Y = (int)(polarxy.Y + _polarsidesPA[i] * (float)Math.Sin(_polaranglesPA[i] * Math.PI / 180));
                    }
                    break;
                default:
                    break;
            }
        }

        //　多角形Bを動かす
        // each motions of polygonsB

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
                case 5:
                    for (int i = 0; i < polygonnumB; i++)
                    {
                        _polygonanglesB[i] += _polygoninclementanglesB[i];

                        if (_polygonanglesB[i] > 360)
                        {
                            _polygonanglesB[i] -= 360;
                        }

                        _polaranglesPB[i] += _polaranglesIPB[i];

                        if (_polaranglesPB[i] > 360)
                        {
                            _polaranglesPB[i] = 0;
                        }

                        _polygoncentersB[i].X = (int)(polarxy.X + _polarsidesPB[i] * (float)Math.Cos(_polaranglesPB[i] * Math.PI / 180));
                        _polygoncentersB[i].Y = (int)(polarxy.Y + _polarsidesPB[i] * (float)Math.Sin(_polaranglesPB[i] * Math.PI / 180));
                    }
                    break;

                default:
                    break;
            }
        }

        // ベクタークラス設置
        // vector class 

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

        //　Drawボタン
        //　about draw button

        private void button1_Click(object sender, EventArgs e)
        {
            initializeCircles();
            initializeXs();
            initializePolygonsA();
            initializePolygonsB();
            drawObjects();
        }

        //　タイマー設定
        //　about timer1 

        private void timer1_Tick(object sender, EventArgs e)
        {   
                moveCircles();
                moveXs();
                movePolygonsA();
                movePolygonsB();
                drawObjects();
        }

        //　Stsrt/Stopボタン
        //　start/stop button

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < circlenum; i++)
            {
                _polaranglesC[i] = getstartangle(_circlexys[i].X - polarxy.X, _circlexys[i].Y - polarxy.Y);
                _polarsidesC[i] = getsides(_circlexys[i]);
            }
            
            for (int i = 0; i < xnum; i++)
            {
                _polaranglesX[i] = getstartangle(_xcenters[i].X - polarxy.X, _xcenters[i].Y - polarxy.Y);
                _polarsidesX[i] = getsides(_xcenters[i]);
            }
            
            for (int i = 0; i < polygonnumA; i++)
            {
                _polaranglesPA[i] = getstartangle(_polygoncentersA[i].X - polarxy.X, _polygoncentersA[i].Y - polarxy.Y);
                _polarsidesPA[i] = getsides(_polygoncentersA[i]);
            }

            for (int i = 0; i < polygonnumB; i++)
            {
                _polaranglesPB[i] = getstartangle(_polygoncentersB[i].X - polarxy.X, _polygoncentersB[i].Y - polarxy.Y);
                _polarsidesPB[i] = getsides(_polygoncentersB[i]);
            }

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

        //　ランダムな座標を返すファンクション
        //　function which puts coordinates back

        private Point xy(Random rand)
        {
            return new Point(rand.Next(50, pic.Width - 50), rand.Next(50, pic.Height - 50));
        }

        //　ランダムなベクターを返すファンクション
        //　function which puts vectors back

        private vector velocity(Random rand)
        {
            return new vector(rand.Next(2, 10), rand.Next(2, 10));
        }

        //　ランダムなPenカラーを返すファンクション
        //　function which puts color of pen back

        private Pen pen(Random rand)
        {
            return new Pen(Color.FromArgb(rand.Next(200, 250), rand.Next(150, 250), rand.Next(150, 250), rand.Next(150, 250)));

        }

        //　ランダムなSolidbrushを返すファンクション
        //　function which puts Solid brushes back

        private SolidBrush solidbrush(Random rand)
        {
            return new SolidBrush(Color.FromArgb(rand.Next(150, 250), rand.Next(150, 250), rand.Next(150, 250)));
        }

        //　北極星（画面の中心）と各図形の距離を返すファンクション
        //　function which puts a distance between two points back

        private float getsides(Point xy)
        {
            return (float)Math.Sqrt(Math.Pow((xy.X - polarxy.X), 2) + Math.Pow((xy.Y - polarxy.Y), 2));  
        }

        //　北極星（画面中心）を中心に回転する最初の位置角度を返すファンクション
        //　function which puts angle from 0 of each objects back

        private float getstartangle(float x, float y)
        {
            return (float)(Math.Atan2(y, x) * 180 / Math.PI);
        }

        // コンボボックス1はXの数を設定
        // combobox1 is about number of Xs

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            xnum = Convert.ToInt32(comboBox1.SelectedItem);
            initializeXs();
        }

        // コンボボックス2は多角形の数を設定
        // combobox2 is about number of polygonsA
       
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            polygonnumA = Convert.ToInt32(comboBox2.SelectedItem);
            initializePolygonsA();
        }

        // コンボボックス3は多角形の数を設定
        // combobox3 is about number of polygonsB

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            polygonnumB = Convert.ToInt32(comboBox3.SelectedItem);
            initializePolygonsB();
        }

        // コンボボックス4は円の数を設定
        // combobox4 is about number of Circles

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            circlenum = Convert.ToInt32(comboBox4.SelectedItem);
            initializeCircles();
        }

        // コンボボックス5は多角形の頂点の数を設定
        // combobox5 is about number of vertex of PolygonsA

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            vertexA = Convert.ToInt32(comboBox5.SelectedItem);
            initializePolygonsA();
        }

        // コンボボックス6は多角形の頂点の数を設定
        // combobox6 is about number of vertex of PolygonsB

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            vertexB = Convert.ToInt32(comboBox6.SelectedItem);
            initializePolygonsB();
        }

        // form1のサイズが変更された時の設定
        // setting up when form1 size changed

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            _bitmap = new Bitmap(pic.Width, pic.Height);
            polarxy = new Point(pic.Width / 2, pic.Height / 2);
        }

        // コンボボックス7は各種動きの設定用
        // combobox7 is for motion set up

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            motionnumber = comboBox7.SelectedIndex;
            if (motionnumber == 5)
            {
                polarflag = true;
            } else
            {
                polarflag = false;
            }
        }


    }
}
