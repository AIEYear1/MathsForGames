using System;
using System.Collections.Generic;
using System.Text;

namespace MatrixHierarchies
{
    struct ColorLCH
    {
        float l;
        float c;
        float h;

        public float L
        {
            get => l;
            set => l = value;
        }
        public float C
        {
            get => c;
            set => c = value;
        }
        public float H
        {
            get => h;
            set => h = value;
        }

        public ColorLCH(float l, float c, float h)
        {
            this.l = l;
            this.c = c;
            this.h = h;
        }

        public ColorLCH(ColorLUV luv)
        {
            l = luv.L;
            c = (float)Math.Sqrt((luv.u * luv.u) + (luv.v * luv.v));

            double angle = MathF.Atan2(luv.v, luv.u) * (180 / MathF.PI);
            h = (float)((angle < 0) ? angle + 360 : angle);
        }

        public static ColorLCH Lerp(ColorLCH start, ColorLCH end, float increment)
        {
            ColorLCH toReturn = start;
            toReturn.L += (end.L - start.L) * increment;
            toReturn.C += (end.C - start.C) * increment;
            toReturn.H += (end.H - start.H) * increment;

            return toReturn;
        }


        public static explicit operator ColorLCH(ColorLUV color)
        {
            return new ColorLCH(color);
        }
        public static explicit operator ColorLUV(ColorLCH color)
        {
            return new ColorLUV(color);
        }
        public static explicit operator ColorRGB(ColorLCH color)
        {
            return new ColorRGB((ColorXYZ)((ColorLUV)color));
        }
        public static explicit operator ColorLCH(ColorRGB color)
        {
            return new ColorLCH((ColorLUV)((ColorXYZ)color));
        }
    }
}
