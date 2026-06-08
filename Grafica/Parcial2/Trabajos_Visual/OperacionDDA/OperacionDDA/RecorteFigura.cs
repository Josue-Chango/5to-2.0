using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace OperacionDDA
{
    internal class RecorteFigura
    {

        // algoritmo sutherlandHodgman (poligono, ventana)

        private bool InsideLeft(PointF p, float xmin)
        {
            return p.X >= xmin;
        }

        private bool InsideRight(PointF p, float xmax)
        {
            return p.X <= xmax;
        }

        private bool InsideBottom(PointF p, float ymin)
        {
            return p.Y >= ymin;
        }

        private bool InsideTop(PointF p, float ymax)
        {
            return p.Y <= ymax;
        }

        private PointF IntersectVertical( PointF s, PointF e, float xClip)
        {
            float y = s.Y + (e.Y - s.Y) * (xClip - s.X) / (e.X - s.X);

            return new PointF(xClip, y);
        }

        private PointF IntersectHorizontal( PointF s, PointF e, float yClip)
        {
            float x = s.X + (e.X - s.X) * (yClip - s.Y) / (e.Y - s.Y);

            return new PointF(x, yClip);
        }

        public List<PointF> SutherlandHodgman( List<PointF> polygon, float xmin, float ymin, float xmax, float ymax)
        {
            List<PointF> output = polygon;

            output = ClipLeft(output, xmin);
            output = ClipRight(output, xmax);
            output = ClipBottom(output, ymin);
            output = ClipTop(output, ymax);

            return output;
        }


        //parte izquierda

        private List<PointF> ClipLeft( List<PointF> input, float xmin)
        {
            List<PointF> output = new List<PointF>();

            for (int i = 0; i < input.Count; i++)
            {
                PointF s = input[i];
                PointF e = input[(i + 1) % input.Count];

                bool sInside = InsideLeft(s, xmin);
                bool eInside = InsideLeft(e, xmin);

                if (sInside && eInside)
                {
                    output.Add(e);
                }
                else if (sInside && !eInside)
                {
                    output.Add(
                        IntersectVertical(s, e, xmin));
                }
                else if (!sInside && eInside)
                {
                    output.Add(
                        IntersectVertical(s, e, xmin));

                    output.Add(e);
                }
            }

            return output;
        }

        //Pas=rte derecha

        private List<PointF> ClipRight( List<PointF> input, float xmax)
        {
            List<PointF> output = new List<PointF>();

            for (int i = 0; i < input.Count; i++)
            {
                PointF s = input[i];
                PointF e = input[(i + 1) % input.Count];

                bool sInside = InsideRight(s, xmax);
                bool eInside = InsideRight(e, xmax);

                if (sInside && eInside)
                    output.Add(e);

                else if (sInside && !eInside)
                    output.Add( IntersectVertical(s, e, xmax));

                else if (!sInside && eInside)
                {
                    output.Add( IntersectVertical(s, e, xmax));

                    output.Add(e);
                }
            }

            return output;
        }

        // Parte inferior

        private List<PointF> ClipBottom( List<PointF> input, float ymin)
        {
            List<PointF> output = new List<PointF>();

            for (int i = 0; i < input.Count; i++)
            {
                PointF s = input[i];
                PointF e = input[(i + 1) % input.Count];

                bool sInside = InsideBottom(s, ymin);
                bool eInside = InsideBottom(e, ymin);

                if (sInside && eInside)
                    output.Add(e);

                else if (sInside && !eInside)
                    output.Add( IntersectHorizontal(s, e, ymin));

                else if (!sInside && eInside)
                {
                    output.Add( IntersectHorizontal(s, e, ymin));

                    output.Add(e);
                }
            }

            return output;
        }

        //Parte superior

        private List<PointF> ClipTop( List<PointF> input, float ymax)
        {
            List<PointF> output = new List<PointF>();

            for (int i = 0; i < input.Count; i++)
            {
                PointF s = input[i];
                PointF e = input[(i + 1) % input.Count];

                bool sInside = InsideTop(s, ymax);
                bool eInside = InsideTop(e, ymax);

                if (sInside && eInside)
                    output.Add(e);

                else if (sInside && !eInside)
                    output.Add( IntersectHorizontal(s, e, ymax));

                else if (!sInside && eInside)
                {
                    output.Add( IntersectHorizontal(s, e, ymax));

                    output.Add(e);
                }
            }

            return output;
        }

        public List<PointF> VertexClipping( List<PointF> polygon, float xmin, float ymin, float xmax, float ymax)
        {
            return SutherlandHodgman( polygon, xmin, ymin, xmax, ymax);
        }

        public List<PointF> BoundingBoxClip( List<PointF> polygon, float xmin, float ymin, float xmax, float ymax)
        {
            return SutherlandHodgman( polygon, xmin, ymin, xmax, ymax);
        }
    }
}
