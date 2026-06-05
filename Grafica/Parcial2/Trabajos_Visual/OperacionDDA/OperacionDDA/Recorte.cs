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

    }
}
