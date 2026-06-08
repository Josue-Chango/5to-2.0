using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace OperacionDDA
{
    internal class Recorte
    {
        /*int Inside = 0, Left = 1, Right = 2, Top = 4, Bottom = 8;

        funcion ComputeOutCode(x, y, xmin, ymin, xmax, ymax) 
        { 
            Code = Inside;
            si x<xmin entonces Code = Left;
            si x> xmax entonces Code = Right;
            si y<ymin entonces Code = Bottom;
            si y> ymax entonces Code = Top;
        }
        */

        const int INSIDE = 0;
        const int LEFT = 1;
        const int RIGHT = 2;
        const int BOTTOM = 4;
        const int TOP = 8;

        private int ComputeOutCode(
            double x,
            double y,
            double xmin,
            double ymin,
            double xmax,
            double ymax)
        {
            int code = INSIDE;

            if (x < xmin)
                code |= LEFT;
            else if (x > xmax)
                code |= RIGHT;

            if (y < ymin)
                code |= BOTTOM;
            else if (y > ymax)
                code |= TOP;

            return code;
        }

        public bool CohenSutherlandClip(
            ref double x1,
            ref double y1,
            ref double x2,
            ref double y2,
            double xmin,
            double ymin,
            double xmax,
            double ymax)
        {
            int outcode1 = ComputeOutCode(
                x1, y1,
                xmin, ymin,
                xmax, ymax);

            int outcode2 = ComputeOutCode(
                x2, y2,
                xmin, ymin,
                xmax, ymax);

            bool accept = false;

            while (true)
            {
                if ((outcode1 | outcode2) == 0)
                {
                    accept = true;
                    break;
                }

                else if ((outcode1 & outcode2) != 0)
                {
                    break;
                }

                else
                {
                    double x = 0;
                    double y = 0;

                    int outcodeOut =
                        outcode1 != 0
                        ? outcode1
                        : outcode2;

                    if ((outcodeOut & TOP) != 0)
                    {
                        x = x1 + (x2 - x1) *
                            (ymax - y1) /
                            (y2 - y1);

                        y = ymax;
                    }

                    else if ((outcodeOut & BOTTOM) != 0)
                    {
                        x = x1 + (x2 - x1) *
                            (ymin - y1) /
                            (y2 - y1);

                        y = ymin;
                    }

                    else if ((outcodeOut & RIGHT) != 0)
                    {
                        y = y1 + (y2 - y1) *
                            (xmax - x1) /
                            (x2 - x1);

                        x = xmax;
                    }

                    else if ((outcodeOut & LEFT) != 0)
                    {
                        y = y1 + (y2 - y1) *
                            (xmin - x1) /
                            (x2 - x1);

                        x = xmin;
                    }

                    if (outcodeOut == outcode1)
                    {
                        x1 = x;
                        y1 = y;

                        outcode1 = ComputeOutCode(
                            x1, y1,
                            xmin, ymin,
                            xmax, ymax);
                    }
                    else
                    {
                        x2 = x;
                        y2 = y;

                        outcode2 = ComputeOutCode(
                            x2, y2,
                            xmin, ymin,
                            xmax, ymax);
                    }
                }
            }

            return accept;
        }


        public bool LiangBarskyClip( ref double x1, ref double y1, ref double x2, ref double y2, double xmin, double ymin, double xmax, double ymax)
        {
            double dx = x2 - x1;
            double dy = y2 - y1;

            double[] p = { -dx, dx, -dy, dy };

            double[] q = { x1 - xmin, xmax - x1, y1 - ymin, ymax - y1 };

            double u1 = 0.0;
            double u2 = 1.0;

            for (int i = 0; i < 4; i++)
            {
                if (p[i] == 0)
                {
                    if (q[i] < 0)
                        return false;
                }
                else
                {
                    double r = q[i] / p[i];

                    if (p[i] < 0)
                        u1 = Math.Max(u1, r);
                    else
                        u2 = Math.Min(u2, r);
                }
            }

            if (u1 > u2)
                return false;

            double nx1 = x1 + u1 * dx;
            double ny1 = y1 + u1 * dy;

            double nx2 = x1 + u2 * dx;
            double ny2 = y1 + u2 * dy;

            x1 = nx1;
            y1 = ny1;

            x2 = nx2;
            y2 = ny2;

            return true;
        }

        public bool ParametricClip( ref double x1, ref double y1, ref double x2, ref double y2, double xmin, double ymin, double xmax, double ymax)
        {
            double dx = x2 - x1;
            double dy = y2 - y1;

            double tMin = 0;
            double tMax = 1;

            if (dx != 0)
            {
                double tx1 = (xmin - x1) / dx;
                double tx2 = (xmax - x1) / dx;

                tMin = Math.Max(tMin, Math.Min(tx1, tx2));
                tMax = Math.Min(tMax, Math.Max(tx1, tx2));
            }

            if (dy != 0)
            {
                double ty1 = (ymin - y1) / dy;
                double ty2 = (ymax - y1) / dy;

                tMin = Math.Max(tMin, Math.Min(ty1, ty2));
                tMax = Math.Min(tMax, Math.Max(ty1, ty2));
            }

            if (tMin > tMax)
                return false;

            x2 = x1 + tMax * dx;
            y2 = y1 + tMax * dy;

            x1 = x1 + tMin * dx;
            y1 = y1 + tMin * dy;

            return true;
        }


    }
}
