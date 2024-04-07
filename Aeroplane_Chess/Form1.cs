using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aeroplane_Chess
{
    public partial class Form1 : Form
    {
        Plane_Line p_l1 = new Plane_Line();
        Plane_Line2 p_l2 = new Plane_Line2();
        Plane_Line3 p_l3 = new Plane_Line3();
        Plane_Line4 p_l4 = new Plane_Line4();
        List<Point>[] plane_blank = new List<Point>[4];
        bool is_click = false, is_complete = false;
        int player = 1;
        int num;
        int start=-1;
        Random r = new Random();
        int[,] map =
        {
            {0,0,0,0,9,9,9,9,9,9,9,-2,0,0,0 },
            {0,1,1,0,9,0,0,6,0,0,9,0,2,2,0 },
            {0,1,1,0,9,0,0,6,0,0,9,0,2,2,0 },
           {-1,0,0,0,9,0,0,6,0,0,9,0,0,0,0 },
            {9,9,9,9,0,0,0,6,0,0,0,9,9,9,9 },
            {9,0,0,0,0,0,0,6,0,0,0,0,0,0,9 },
            {9,0,0,0,0,0,0,6,0,0,0,0,0,0,9 },
            {9,5,5,5,5,5,5,0,8,8,8,8,8,8,9 },
            {9,0,0,0,0,0,0,7,0,0,0,0,0,0,9 },
            {9,0,0,0,0,0,0,7,0,0,0,0,0,0,9 },
            {9,9,9,9,0,0,0,7,0,0,0,9,9,9,9 },
            {0,0,0,0,9,0,0,7,0,0,9,0,0,0,-4 },
            {0,3,3,0,9,0,0,7,0,0,9,0,4,4,0 },
            {0,3,3,0,9,0,0,7,0,0,9,0,4,4,0 },
            {0,0,0,-3,9,9,9,9,9,9,9,0,0,0,0 },
        };
        public Form1()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 绘制飞行直飞格
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="x3"></param>
        /// <param name="y3"></param>
        /// <param name="brush"></param>
        public void Draw_Triangle(int x1, int y1, int x2, int y2, int x3, int y3, Brush brush)
        {
            Graphics g = this.CreateGraphics();
            Point[] p = { new Point(x1, y1), new Point(x2, y2), new Point(x3, y3) };
            g.FillPolygon(brush, p);
        }

        public void Plane_GoHome(int plane_num)
        {
            Graphics g = this.CreateGraphics();
            for (int i = 0; i < plane_blank[plane_num - 1].Count; i++)
            //for (int i = 0; i < 1; i++)
            {
                
                switch (plane_num)
                {
                    case 1:
                        map[plane_blank[plane_num - 1][i].X, plane_blank[plane_num - 1][i].Y] = 1;
                        g.FillEllipse(new SolidBrush(Color.Blue), plane_blank[plane_num - 1][i].Y * 30, plane_blank[plane_num - 1][i].X * 30, 29, 29);
                        break;
                    case 2:
                        map[plane_blank[plane_num - 1][i].X, plane_blank[plane_num - 1][i].Y] = 2;
                        g.FillEllipse(new SolidBrush(Color.Yellow), plane_blank[plane_num - 1][i].Y * 30, plane_blank[plane_num - 1][i].X * 30, 29, 29);
                        break;
                    case 3:
                        map[plane_blank[plane_num - 1][i].X, plane_blank[plane_num - 1][i].Y] = 3;
                        g.FillEllipse(new SolidBrush(Color.Red), plane_blank[plane_num - 1][i].Y * 30, plane_blank[plane_num - 1][i].X * 30, 29, 29);
                        break;
                    case 4:
                        map[plane_blank[plane_num - 1][i].X, plane_blank[plane_num - 1][i].Y] = 4;
                        g.FillEllipse(new SolidBrush(Color.Green), plane_blank[plane_num - 1][i].Y * 30, plane_blank[plane_num - 1][i].X * 30, 29, 29);
                        break;

                }
            }
            plane_blank[plane_num - 1] = new List<Point>();
        }
        public void Plane_Crash(int x, int y)
        {
            switch (map[x, y])
            {
                case 1:
                case 11:
                case 111:
                case 1111:
                    Plane_GoHome(1);
                    break;
                case 2:
                case 22:
                case 222:
                case 2222:
                    Plane_GoHome(2);
                    break;
                case 3:
                case 33:
                case 333:
                case 3333:
                    Plane_GoHome(3);
                    break;
                case 4:
                case 44:
                case 444:
                case 4444:
                    Plane_GoHome(4);
                    break;
            }
        }
        public void Plane_Leave(int x, int y)
        {
            Graphics g = this.CreateGraphics();
            switch (map[x, y])
            {
                case 1:
                    if(x==7&&y>0&&y<7)
                        g.FillRectangle(new SolidBrush(Color.Blue), y * 30, x * 30, 29, 29);
                    else
                        g.FillRectangle(new SolidBrush(Color.Gray), y * 30, x * 30, 29, 29);
                    map[x, y] = 9;
                    g.FillEllipse(new SolidBrush(Color.White), y * 30, x * 30, 29, 29);
                    break;
                case 11:
                    map[x, y] = 1;
                    g.FillEllipse(new SolidBrush(Color.Blue), y * 30+5, x * 30+5, 20, 20);
                    break;
                case 111:
                    map[x, y] = 11;
                    g.FillEllipse(new SolidBrush(Color.Blue), y * 30 + 5, x * 30 + 5, 20, 20);
                    g.DrawString("2", new Font("微软雅黑", 10, FontStyle.Bold), new SolidBrush(Color.Black), y * 30 + 5, x * 30 + 5);
                    break;
                case 1111:
                    map[x, y] = 111;
                    g.FillEllipse(new SolidBrush(Color.Blue), y * 30 + 5, x * 30 + 5, 20, 20);
                    g.DrawString("3", new Font("微软雅黑", 10, FontStyle.Bold), new SolidBrush(Color.Black), y * 30 + 5, x * 30 + 5);
                    break;
                case 2:
                    if (y == 7 && x > 0 && x < 7)
                        g.FillRectangle(new SolidBrush(Color.Blue), y * 30, x * 30, 29, 29);
                    else
                        g.FillRectangle(new SolidBrush(Color.Gray), y * 30, x * 30, 29, 29);
                    map[x, y] = 9;
                    g.FillEllipse(new SolidBrush(Color.White), y * 30, x * 30, 29, 29);
                    break;
                case 22:
                    map[x, y] = 2;
                    g.FillEllipse(new SolidBrush(Color.Yellow), y * 30 + 5, x * 30 + 5, 29, 29);
                    break;
                case 222:
                    map[x, y] = 22;
                    g.FillEllipse(new SolidBrush(Color.Yellow), y * 30 + 5, x * 30 + 5, 29, 29);
                    g.DrawString("2", new Font("微软雅黑", 10, FontStyle.Bold), new SolidBrush(Color.Black), y * 30 + 5, x * 30 + 5);
                    break;
                case 2222:
                    map[x, y] = 222;
                    g.FillEllipse(new SolidBrush(Color.Yellow), y * 30 + 5, x * 30 + 5, 29, 29);
                    g.DrawString("3", new Font("微软雅黑", 10, FontStyle.Bold), new SolidBrush(Color.Black), y * 30 + 5, x * 30 + 5);
                    break;
                case 3:
                    if (y == 7 && x > 7 && x < 14)
                        g.FillRectangle(new SolidBrush(Color.Blue), y * 30, x * 30, 29, 29);
                    else
                        g.FillRectangle(new SolidBrush(Color.Gray), y * 30, x * 30, 29, 29);
                    map[x, y] = 9;
                    g.FillEllipse(new SolidBrush(Color.White), y * 30, x * 30, 29, 29);
                    break;
                case 33:
                    map[x, y] = 3;
                    g.FillEllipse(new SolidBrush(Color.Red), y * 30 + 5, x * 30 + 5, 29, 29);
                    break;
                case 333:
                    map[x, y] = 33;
                    g.FillEllipse(new SolidBrush(Color.Red), y * 30 + 5, x * 30 + 5, 29, 29);
                    g.DrawString("2", new Font("微软雅黑", 10, FontStyle.Bold), new SolidBrush(Color.Black), y * 30 + 5, x * 30 + 5);
                    break;
                case 3333:
                    map[x, y] = 333;
                    g.FillEllipse(new SolidBrush(Color.Red), y * 30 + 5, x * 30 + 5, 29, 29);
                    g.DrawString("3", new Font("微软雅黑", 10, FontStyle.Bold), new SolidBrush(Color.Black), y * 30 + 5, x * 30 + 5);
                    break;
                case 4:
                    if (x == 7 && y > 7 && y < 14)
                        g.FillRectangle(new SolidBrush(Color.Blue), y * 30, x * 30, 29, 29);
                    else
                        g.FillRectangle(new SolidBrush(Color.Gray), y * 30, x * 30, 29, 29);
                    map[x, y] = 9;
                    g.FillEllipse(new SolidBrush(Color.White), y * 30, x * 30, 29, 29);
                    break;
                case 44:
                    map[x, y] = 4;
                    g.FillEllipse(new SolidBrush(Color.Green), y * 30 + 5, x * 30 + 5, 29, 29);
                    break;
                case 444:
                    map[x, y] = 44;
                    g.FillEllipse(new SolidBrush(Color.Green), y * 30 + 5, x * 30 + 5, 29, 29);
                    g.DrawString("2", new Font("微软雅黑", 10, FontStyle.Bold), new SolidBrush(Color.Black), y * 30 + 5, x * 30 + 5);
                    break;
                case 4444:
                    map[x, y] = 444;
                    g.FillEllipse(new SolidBrush(Color.Green), y * 30 + 5, x * 30 + 5, 29, 29);
                    g.DrawString("3", new Font("微软雅黑", 10, FontStyle.Bold), new SolidBrush(Color.Black), y * 30 + 5, x * 30 + 5);
                    break;
            }
        }
        public void Plane_TakeOf(int x, int y)
        {
            Graphics g = this.CreateGraphics();
            switch (player)
            {
                case 1:
                    plane_blank[0].Add(new Point(x, y));
                    map[x, y] = 0;
                    g.FillRectangle(new SolidBrush(BackColor), y * 30, x * 30, 29, 29);
                    map[3, 0] = player;
                    g.FillEllipse(new SolidBrush(Color.Blue), 5, 90 + 5, 20, 20);
                    is_complete = true;
                    break;
                case 2:
                    plane_blank[1].Add(new Point(x, y));
                    map[x, y] = 0;
                    g.FillRectangle(new SolidBrush(BackColor), y * 30, x * 30, 29, 29);
                    map[0, 11] = player;
                    g.FillEllipse(new SolidBrush(Color.Yellow), 330 + 5, 5, 20, 20);
                    break;
                case 3:
                    plane_blank[2].Add(new Point(x, y));
                    map[x, y] = 0;
                    g.FillRectangle(new SolidBrush(BackColor), y * 30, x * 30, 29, 29);
                    map[14, 3] = player;
                    g.FillEllipse(new SolidBrush(Color.Red), 90 + 5, 420 + 5, 20, 20);
                    break;
                case 4:
                    plane_blank[3].Add(new Point(x, y));
                    map[x, y] = 0;
                    g.FillRectangle(new SolidBrush(BackColor), y * 30, x * 30, 29, 29);
                    map[11, 14] = player;
                    g.FillEllipse(new SolidBrush(Color.Green), 420 + 5, 330 + 5, 20, 20);
                    break;
            }
        }

        public void Plane_fly(int x, int y)
        {
            Graphics g = this.CreateGraphics();
            switch (player)
            {
                case 1:
                    g.FillEllipse(new SolidBrush(Color.Blue), y * 30 + 5, x * 30 + 5, 20, 20);
                    if (map[x, y] % 10 != 1)
                    {
                        Plane_Crash(x, y);
                        map[x, y] = 1;
                    }
                    else
                    {
                        switch (map[x, y])
                        {
                            case 1:
                                map[x, y] = 11;
                                g.DrawString("2", new Font("微软雅黑", 10, FontStyle.Bold), new SolidBrush(Color.Black), y * 30 + 5, x * 30 + 5);
                                break;
                            case 11:
                                map[x, y] = 111;
                                g.DrawString("3", new Font("微软雅黑", 10, FontStyle.Bold), new SolidBrush(Color.Black), y * 30 + 5, x * 30 + 5);
                                break;
                            case 111:
                                map[x, y] = 1111;
                                g.DrawString("4", new Font("微软雅黑", 10, FontStyle.Bold), new SolidBrush(Color.Black), y * 30 + 5, x * 30 + 5);
                                if(x==7&&y==6)
                                {
                                    MessageBox.Show("蓝色方获胜！！！");
                                    button1.Enabled = false;
                                    button1.Visible = false;
                                    label1.Visible = false;
                                }    
                                break;
                        }
                    }
                    break;
                case 2:
                    g.FillEllipse(new SolidBrush(Color.Yellow), y * 30 + 5, x * 30 + 5, 20, 20);
                    if (map[x, y] % 10 != 2)
                    {
                        Plane_Crash(x, y);
                        map[x, y] = 2;
                    }
                    else
                    {
                        switch (map[x, y])
                        {
                            case 2:
                                map[x, y] = 22;
                                g.DrawString("2", new Font("微软雅黑", 10, FontStyle.Bold), new SolidBrush(Color.Black), y * 30 + 5, x * 30 + 5);
                                break;
                            case 22:
                                map[x, y] = 222;
                                g.DrawString("3", new Font("微软雅黑", 10, FontStyle.Bold), new SolidBrush(Color.Black), y * 30 + 5, x * 30 + 5);
                                break;
                            case 222:
                                map[x, y] = 2222;
                                g.DrawString("4", new Font("微软雅黑", 10, FontStyle.Bold), new SolidBrush(Color.Black), y * 30 + 5, x * 30 + 5);
                                if (x == 6 && y == 7)
                                {
                                    MessageBox.Show("黄色方获胜！！！");
                                    button1.Enabled = false;
                                    button1.Visible = false;
                                    label1.Visible = false;
                                }
                                break;
                        }
                    }
                    break;
                case 3:
                    g.FillEllipse(new SolidBrush(Color.Red), y * 30 + 5, x * 30 + 5, 20, 20);
                    if (map[x, y] % 10 != 3)
                    {
                        Plane_Crash(x, y);
                        map[x, y] = 3;
                    }
                    else
                    {
                        switch (map[x, y])
                        {
                            case 3:
                                map[x, y] = 33;
                                g.DrawString("2", new Font("微软雅黑", 10, FontStyle.Bold), new SolidBrush(Color.Black), y * 30 + 5, x * 30 + 5);
                                break;
                            case 33:
                                map[x, y] = 333;
                                g.DrawString("3", new Font("微软雅黑", 10, FontStyle.Bold), new SolidBrush(Color.Black), y * 30 + 5, x * 30 + 5);
                                break;
                            case 333:
                                map[x, y] = 3333;
                                g.DrawString("4", new Font("微软雅黑", 10, FontStyle.Bold), new SolidBrush(Color.Black), y * 30 + 5, x * 30 + 5);
                                if (x == 8 && y == 7)
                                {
                                    MessageBox.Show("红色方获胜！！！");
                                    button1.Enabled = false;
                                    button1.Visible = false;
                                    label1.Visible = false;
                                }
                                break;
                        }
                    }
                    break;
                case 4:
                    g.FillEllipse(new SolidBrush(Color.Green), y * 30 + 5, x * 30 + 5, 20, 20);
                    if (map[x, y] % 10 != 4)
                    {
                        Plane_Crash(x, y);
                        map[x, y] = 4;
                    }
                    else
                    {
                        switch (map[x, y])
                        {
                            case 4:
                                map[x, y] = 44;
                                g.DrawString("2", new Font("微软雅黑", 10, FontStyle.Bold), new SolidBrush(Color.Black), y * 30 + 5, x * 30 + 5);
                                break;
                            case 44:
                                map[x, y] = 444;
                                g.DrawString("3", new Font("微软雅黑", 10, FontStyle.Bold), new SolidBrush(Color.Black), y * 30 + 5, x * 30 + 5);
                                break;
                            case 444:
                                map[x, y] = 4444;
                                g.DrawString("4", new Font("微软雅黑", 10, FontStyle.Bold), new SolidBrush(Color.Black), y * 30 + 5, x * 30 + 5);
                                if (x == 7 && y == 8)
                                {
                                    MessageBox.Show("绿色方获胜！！！");
                                    button1.Enabled = false;
                                    button1.Visible = false;
                                    label1.Visible = false;
                                }
                                break;
                        }
                    }
                    break;
            }

        }
        public void Plane_Go(int x, int y)//x,y为起点位置
        {
            Graphics g = this.CreateGraphics();
            switch (player)
            {
                case 1:
                    for (int i = 0; i < p_l1.plane_line1.Count; i++)
                    {
                        if (x == p_l1.plane_line1[i].X - 1 && y == p_l1.plane_line1[i].Y - 1)
                        {
                            start = i;
                            break;
                        }
                    }
                    if (start == 0)
                    {
                        map[x, y] = -1;
                        g.FillRectangle(new SolidBrush(Color.Blue), y * 30, x * 30, 29, 29);
                        g.FillEllipse(new SolidBrush(Color.White), y * 30, x * 30, 29, 29);
                    }
                    else
                        Plane_Leave(x, y);
                    if (start + num < p_l1.plane_line1.Count)
                    {
                        if(start+num==18)
                        {
                            Plane_fly(10, 11);
                        }
                        else
                            Plane_fly(p_l1.plane_line1[start + num].X - 1, p_l1.plane_line1[start + num].Y - 1);
                    }
                    else
                        Plane_fly(p_l1.plane_line1[p_l1.plane_line1.Count - 1].X - 1, p_l1.plane_line1[p_l1.plane_line1.Count - 1].Y - 1);
                    break;
                case 2:
                    for (int i = 0; i < p_l2.plane_line2.Count; i++)
                    {
                        if (x == p_l2.plane_line2[i].X - 1 && y == p_l2.plane_line2[i].Y - 1)
                        {
                            start = i;
                            break;
                        }
                    }
                    if (start == 0)
                    {
                        map[x, y] = -2;
                        g.FillRectangle(new SolidBrush(Color.Yellow), y * 30, x * 30, 29, 29);
                        g.FillEllipse(new SolidBrush(Color.White), y * 30, x * 30, 29, 29);
                    }
                    else
                        Plane_Leave(x, y);
                    if (start + num < p_l2.plane_line2.Count)
                    {
                        if (start + num == 18)
                        {
                            Plane_fly(11, 4);
                        }
                        else
                            Plane_fly(p_l2.plane_line2[start + num].X - 1, p_l2.plane_line2[start + num].Y - 1);
                    }
                    else
                        Plane_fly(p_l2.plane_line2[p_l2.plane_line2.Count - 1].X - 1, p_l2.plane_line2[p_l1.plane_line1.Count - 1].Y - 1);
                    break;
                case 3:
                    for (int i = 0; i < p_l3.plane_line3.Count; i++)
                    {
                        if (x == p_l3.plane_line3[i].X - 1 && y == p_l3.plane_line3[i].Y - 1)
                        {
                            start = i;
                            break;
                        }
                    }
                    if (start == 0)
                    {
                        map[x, y] = -3;
                        g.FillRectangle(new SolidBrush(Color.Red), y * 30, x * 30, 29, 29);
                        g.FillEllipse(new SolidBrush(Color.White), y * 30, x * 30, 29, 29);
                    }
                    else
                        Plane_Leave(x, y);
                    if (start + num < p_l3.plane_line3.Count)
                    {
                        if (start + num == 18)
                        {
                            Plane_fly(3, 10);
                        }
                        else
                            Plane_fly(p_l3.plane_line3[start + num].X - 1, p_l3.plane_line3[start + num].Y - 1);
                    }
                    else
                        Plane_fly(p_l3.plane_line3[p_l3.plane_line3.Count - 1].X - 1, p_l3.plane_line3[p_l1.plane_line1.Count - 1].Y - 1);
                    break;
                case 4:
                    for (int i = 0; i < p_l4.plane_line4.Count; i++)
                    {
                        if (x == p_l4.plane_line4[i].X - 1 && y == p_l4.plane_line4[i].Y - 1)
                        {
                            start = i;
                            break;
                        }
                    }
                    if (start == 0)
                    {
                        map[x, y] = -4;
                        g.FillRectangle(new SolidBrush(Color.Green), y * 30, x * 30, 29, 29);
                        g.FillEllipse(new SolidBrush(Color.White), y * 30, x * 30, 29, 29);
                    }
                    else
                        Plane_Leave(x, y);
                    if (start + num < p_l4.plane_line4.Count)
                    {
                        if (start + num == 18)
                        {
                            Plane_fly(4, 3);
                        }
                        else
                            Plane_fly(p_l4.plane_line4[start + num].X - 1, p_l4.plane_line4[start + num].Y - 1);
                    }
                    else
                        Plane_fly(p_l4.plane_line4[p_l4.plane_line4.Count - 1].X - 1, p_l4.plane_line4[p_l1.plane_line1.Count - 1].Y - 1);
                    break;
            }
        }
        public void Change_Player()
        {
            switch (player)
            {
                case 1:
                    player = 2;
                    label4.Text = "黄方回合";
                    label4.ForeColor = Color.Yellow;
                    is_click = false;
                    button1.Enabled = true;
                    label1.Text = "-";
                    break;
                case 2:
                    player = 3;
                    label4.Text = "红方回合";
                    label4.ForeColor = Color.Red;
                    is_click = false;
                    button1.Enabled = true;
                    label1.Text = "-";
                    break;
                case 3:
                    player = 4;
                    label4.Text = "绿方回合";
                    label4.ForeColor = Color.Green;
                    is_click = false;
                    button1.Enabled = true;
                    label1.Text = "-";
                    break;
                case 4:
                    player = 1;
                    label4.Text = "蓝方回合";
                    label4.ForeColor = Color.Blue;
                    is_click = false;
                    button1.Enabled = true;
                    label1.Text = "-";
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text != "STOP")
            {
                button1.Text = "STOP";
                timer1.Start();
            }
            else
            {
                button1.Enabled = false;
                button1.Text = "🎲";
                timer1.Stop();
                is_click = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 4; i++)
            {
                plane_blank[i] = new List<Point>();
            }
            label1.Visible = true;
            button1.Enabled = true;
            button1.Visible = true;
            button2.Enabled = false;
            button2.Visible = false;
            Graphics g = this.CreateGraphics();
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    //飞行正常的格子
                    if (map[i, j] == 9)
                    {
                        g.FillRectangle(new SolidBrush(Color.Gray), j * 30, i * 30, 29, 29);
                        g.FillEllipse(new SolidBrush(Color.White), j * 30, i * 30, 29, 29);
                    }
                    //-4和8为绿色格子
                    if (map[i, j] == 8 || map[i, j] == -4)
                    {
                        g.FillRectangle(new SolidBrush(Color.Green), j * 30, i * 30, 29, 29);
                        g.FillEllipse(new SolidBrush(Color.White), j * 30, i * 30, 29, 29);
                    }
                    if (map[i, j] == 7 || map[i, j] == -3)
                    {
                        g.FillRectangle(new SolidBrush(Color.Red), j * 30, i * 30, 29, 29);
                        g.FillEllipse(new SolidBrush(Color.White), j * 30, i * 30, 29, 29);
                    }
                    if (map[i, j] == 6 || map[i, j] == -2)
                    {
                        g.FillRectangle(new SolidBrush(Color.Yellow), j * 30, i * 30, 29, 29);
                        g.FillEllipse(new SolidBrush(Color.White), j * 30, i * 30, 29, 29);
                    }
                    if (map[i, j] == 5 || map[i, j] == -1)
                    {
                        g.FillRectangle(new SolidBrush(Color.Blue), j * 30, i * 30, 29, 29);
                        g.FillEllipse(new SolidBrush(Color.White), j * 30, i * 30, 29, 29);
                    }
                    if (map[i, j] == 1)
                        g.FillEllipse(new SolidBrush(Color.Blue), j * 30, i * 30, 29, 29);
                    if (map[i, j] == 2)
                        g.FillEllipse(new SolidBrush(Color.Yellow), j * 30, i * 30, 29, 29);
                    if (map[i, j] == 3)
                        g.FillEllipse(new SolidBrush(Color.Red), j * 30, i * 30, 29, 29);
                    if (map[i, j] == 4)
                        g.FillEllipse(new SolidBrush(Color.Green), j * 30, i * 30, 29, 29);
                }
                Draw_Triangle(90, 90, 150, 90, 150, 150, new SolidBrush(Color.Red));
                Draw_Triangle(300, 90, 360, 90, 300, 150, new SolidBrush(Color.Red));
                g.FillRectangle(new SolidBrush(Color.Gray), 120, 90, 30, 30);
                g.FillEllipse(new SolidBrush(Color.White), 120, 90, 30, 30);
                g.FillRectangle(new SolidBrush(Color.Gray), 300, 90, 30, 30);
                g.FillEllipse(new SolidBrush(Color.White), 300, 90, 30, 30);
                Draw_Triangle(90, 90, 90, 150, 150, 150, new SolidBrush(Color.Green));
                Draw_Triangle(90, 300, 90, 360, 150, 300, new SolidBrush(Color.Green));
                g.FillRectangle(new SolidBrush(Color.Gray), 90, 120, 30, 30);
                g.FillEllipse(new SolidBrush(Color.White), 90, 120, 30, 30);
                g.FillRectangle(new SolidBrush(Color.Gray), 90, 300, 30, 30);
                g.FillEllipse(new SolidBrush(Color.White), 90, 300, 30, 30);
                Draw_Triangle(90, 360, 150, 300, 150, 360, new SolidBrush(Color.Yellow));
                Draw_Triangle(300, 300, 300, 360, 360, 360, new SolidBrush(Color.Yellow));
                g.FillRectangle(new SolidBrush(Color.Gray), 120, 330, 30, 30);
                g.FillEllipse(new SolidBrush(Color.White), 120, 330, 30, 30);
                g.FillRectangle(new SolidBrush(Color.Gray), 300, 330, 30, 30);
                g.FillEllipse(new SolidBrush(Color.White), 300, 330, 30, 30);
                Draw_Triangle(300, 300, 360, 300, 360, 360, new SolidBrush(Color.Blue));
                Draw_Triangle(360, 90, 300, 150, 360, 150, new SolidBrush(Color.Blue));
                g.FillRectangle(new SolidBrush(Color.Gray), 330, 300, 30, 30);
                g.FillEllipse(new SolidBrush(Color.White), 330, 300, 30, 30);
                g.FillRectangle(new SolidBrush(Color.Gray), 330, 120, 30, 30);
                g.FillEllipse(new SolidBrush(Color.White),330, 120, 30, 30);

            }


            #region 飞机场
            //g.FillRectangle(new SolidBrush(Color.Blue), 10, 10, 100, 100);
            //g.FillEllipse(new SolidBrush(Color.White), 15, 15, 40, 40);
            //g.FillEllipse(new SolidBrush(Color.White), 15, 65, 40, 40);
            //g.FillEllipse(new SolidBrush(Color.White), 65, 15, 40, 40);
            //g.FillEllipse(new SolidBrush(Color.White), 65, 65, 40, 40);

            //g.FillRectangle(new SolidBrush(Color.Red), 10, 355, 100, 100);
            //g.FillEllipse(new SolidBrush(Color.White), 15, 360, 40, 40);
            //g.FillEllipse(new SolidBrush(Color.White), 15, 410, 40, 40);
            //g.FillEllipse(new SolidBrush(Color.White), 65, 360, 40, 40);
            //g.FillEllipse(new SolidBrush(Color.White), 65, 410, 40, 40);

            //g.FillRectangle(new SolidBrush(Color.Yellow), 355, 10, 100, 100);
            //g.FillEllipse(new SolidBrush(Color.White), 360, 15, 40, 40);
            //g.FillEllipse(new SolidBrush(Color.White), 410, 15, 40, 40);
            //g.FillEllipse(new SolidBrush(Color.White), 360, 65, 40, 40);
            //g.FillEllipse(new SolidBrush(Color.White), 410, 65, 40, 40);

            //g.FillRectangle(new SolidBrush(Color.Green), 355, 355, 100, 100);
            //g.FillEllipse(new SolidBrush(Color.White), 360, 360, 40, 40);
            //g.FillEllipse(new SolidBrush(Color.White), 360, 410, 40, 40);
            //g.FillEllipse(new SolidBrush(Color.White), 410, 360, 40, 40);
            //g.FillEllipse(new SolidBrush(Color.White), 410, 410, 40, 40);
            #endregion
            #region 停机坪
            //g.FillEllipse(new SolidBrush(Color.Blue), 10, 120, 20, 20);
            //g.FillEllipse(new SolidBrush(Color.Red), 120, 435, 20, 20);
            //g.FillEllipse(new SolidBrush(Color.Yellow), 325, 10, 20, 20);
            //g.FillEllipse(new SolidBrush(Color.Green), 435, 325, 20, 20);
            #endregion
            #region 路

            #endregion
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = r.Next(1, 7).ToString();
            //label1.Text = "6";
            num = int.Parse(label1.Text);
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (is_click&&e.X<450&&e.Y<450)
                switch (player)
                {
                    case 1:
                        if (map[e.Y / 30, e.X / 30] % 10 != 1 || (e.X / 30 == 7 && e.Y / 30 == 6))
                            break;
                        if (map[3, 0] == 1 && e.X < 90 && e.Y < 90 && e.X > 30 && e.Y > 30)
                            break;
                        if (int.Parse(label1.Text) > 4 && map[3, 0] == -1 && e.X < 90 && e.Y < 90 && e.X > 30 && e.Y > 30)
                        {
                            Plane_TakeOf(e.Y / 30, e.X / 30);
                        }
                        else if (int.Parse(label1.Text) <= 4 && e.X < 90 && e.Y < 90 && e.X > 30 && e.Y > 30)
                            ;
                        else
                            Plane_Go(e.Y / 30, e.X / 30);
                        Change_Player();
                        break;
                    case 2:
                        if (map[e.Y / 30, e.X / 30] % 10 != 2 || (e.X / 30 == 6 && e.Y / 30 == 7))
                            break;
                        if (map[0, 11] == 2 && e.X < 420 && e.Y < 90 && e.X > 360 && e.Y > 30)
                            break;
                        if (int.Parse(label1.Text) > 4 && map[0, 11] == -2 && e.X < 420 && e.Y < 90 && e.X > 360 && e.Y > 30)
                        {
                            Plane_TakeOf(e.Y / 30, e.X / 30);
                        }
                        else if (int.Parse(label1.Text) <= 4  && e.X < 420 && e.Y < 90 && e.X > 360 && e.Y > 30)
                            ;
                        else
                            Plane_Go(e.Y / 30, e.X / 30);
                        Change_Player();
                        break;
                    case 3:
                        if (map[e.Y / 30, e.X / 30] % 10 != 3 || (e.X / 30 == 7 && e.Y / 30 == 8))
                            break;
                        if (map[14, 3] == 3 && e.X < 90 && e.Y < 420 && e.X > 30 && e.Y > 360)
                            break;
                        if (int.Parse(label1.Text) > 4 && map[14, 3] == -3 && e.X < 90 && e.Y < 420 && e.X > 30 && e.Y > 360)
                        {
                            Plane_TakeOf(e.Y / 30, e.X / 30);
                        }
                        else if (int.Parse(label1.Text) <= 4 && e.X < 90 && e.Y < 420 && e.X > 30 && e.Y > 360)
                            ;
                        else
                            Plane_Go(e.Y / 30, e.X / 30);
                        Change_Player();
                        break;
                    case 4:
                        if (map[e.Y / 30, e.X / 30] % 10 != 4 || (e.X / 30 == 8 && e.Y / 30 == 7))
                            break;
                        if (map[11, 14] == 4 && e.X < 420 && e.Y < 420 && e.X > 360 && e.Y > 360)
                            break;
                        if (int.Parse(label1.Text) > 4 && map[11, 14] == -4 && e.X < 420 && e.Y < 420 && e.X > 360 && e.Y > 360)
                        {
                            Plane_TakeOf(e.Y / 30, e.X / 30);
                        }
                        else if (int.Parse(label1.Text) <= 4 && e.X < 420 && e.Y < 420 && e.X > 360 && e.Y > 360)
                            ;
                        else
                            Plane_Go(e.Y / 30, e.X / 30);
                        Change_Player();
                        break;
                }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            label2.Text = e.X.ToString();
            label3.Text = e.Y.ToString();
        }
    }
}
