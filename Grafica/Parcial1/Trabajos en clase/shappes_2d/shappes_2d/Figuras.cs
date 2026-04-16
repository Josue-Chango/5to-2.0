using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shappes_2d
{
    /*internal class Figuras
    {
    }*/
    public class Figuras
    {
        public float Weight { get; set; }
        public float Height { get; set; }
        public Figuras(float weight, float height)
        {
            Weight = weight;
            Height = height;
        }
        public Figuras()
        {
        }

        public void DibujarRectangulo(Graphics g, float weight, float height) {
            if (weight < 6 && height < 6)
            {
                float redim_weight = weight * 10;
                float redim_height = height * 10;
                Pen pen = new Pen(Color.Blue, 2);
                g.DrawRectangle(Pens.Red, 300, 100, redim_weight, redim_height);
            }
            else
            {
                Pen pen = new Pen(Color.Blue, 2);
                g.DrawRectangle(Pens.Red, 300, 100, weight, height);
            }
        }

    }

}
