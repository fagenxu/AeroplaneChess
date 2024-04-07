using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aeroplane_Chess
{
    class Plane_Line
    {
        int[,] map1 =
        {
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,9,9,9,9,9,9,9,2,0,0,0,0},
            {0,0,1,1,0,9,0,0,6,0,0,9,0,2,2,0,0},
            {0,0,1,1,0,9,0,0,6,0,0,9,0,2,2,0,0},
            {0,1,0,0,0,9,0,0,6,0,0,9,0,0,0,0,0},
            {0,9,9,9,9,0,0,0,6,0,0,0,9,9,9,9,0},
            {0,0,0,0,0,0,0,0,6,0,0,0,0,0,0,9,0},
            {0,0,0,0,0,0,0,0,6,0,0,0,0,0,0,9,0},
            {0,9,9,9,9,9,9,9,0,8,8,8,8,8,8,9,0},
            {0,9,0,0,0,0,0,0,7,0,0,0,0,0,0,9,0},
            {0,9,0,0,0,0,0,0,7,0,0,0,0,0,0,9,0},
            {0,9,9,9,9,0,0,0,7,0,0,0,9,9,9,9,0},
            {0,0,0,0,0,9,0,0,7,0,0,9,0,0,0,4,0},
            {0,0,3,3,0,9,0,0,7,0,0,9,0,4,4,0,0},
            {0,0,3,3,0,9,0,0,7,0,0,9,0,4,4,0,0},
            {0,0,0,0,3,9,9,9,9,9,9,9,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}
        };
        public List<Point> plane_line1 = new List<Point>();
        public Plane_Line()
        {
            int x=4, y=1;
            plane_line1.Add(new Point(4, 1));
            bool flag = true;
            while(flag)
            {
                flag = false;
                if(map1[x-1,y]==9)
                {
                    flag = true;
                    map1[x-1, y] = 0;
                    plane_line1.Add(new Point(x-1, y));
                }
                else if(map1[x, y - 1] == 9)
                {
                    flag = true;
                    map1[x, y-1] = 0;
                    plane_line1.Add(new Point(x, y-1));
                }
                else if (map1[x + 1, y] == 9)
                {
                    flag = true;
                    map1[x + 1, y] = 0;
                    plane_line1.Add(new Point(x + 1, y));
                }
                else if (map1[x, y + 1] == 9)
                {
                    flag = true;
                    map1[x, y + 1] = 0;
                    plane_line1.Add(new Point(x, y+1));
                }
                else
                {
                    for (int i = x - 1; i <= x + 1; i++)
                    {
                        for (int j = y - 1; j <= y + 1; j++)
                        {
                            if (map1[i, j] == 9)
                            {
                                flag = true;
                                map1[i, j] = 0;
                                plane_line1.Add(new Point(i, j));
                            }
                        }
                    }
                }
                x = plane_line1[plane_line1.Count - 1].X;
                y = plane_line1[plane_line1.Count - 1].Y;
            }
        }
    }
}
