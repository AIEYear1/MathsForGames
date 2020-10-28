using System;

namespace MatrixHierarchies
{
    struct ColorLUV
    {
        public float L;
        public float u;
        public float v;

        public ColorLUV(float l, float u, float v)
        {
            this.L = l;
            this.u = u;
            this.v = v;
        }
        public ColorLUV(ColorXYZ xyz)
        {
            float yr = xyz.Y / ColorXYZ.White.Y;
            float U = (4 * xyz.X) / (xyz.X + (15 * xyz.Y) + (3 * xyz.Z));
            float V = (9 * xyz.Y) / (xyz.X + (15 * xyz.Y) + (3 * xyz.Z));
            float Ur = (4 * ColorXYZ.White.X) / (ColorXYZ.White.X + (15 * ColorXYZ.White.Y) + (3 * ColorXYZ.White.Z));
            float Vr = (9 * ColorXYZ.White.Y) / (ColorXYZ.White.X + (15 * ColorXYZ.White.Y) + (3 * ColorXYZ.White.Z));
            double e = 216.0 / 24389.0;
            float k = 24389.0f / 27.0f;

            L = (yr > e) ? (116 * MathF.Pow(yr, 1.0f / 3.0f)) - 16 : k * yr;
            u = (13 * L) * (U - Ur);
            v = (13 * L) * (V - Vr);
        }

        public ColorLUV(ColorLCH lch)
        {
            L = lch.L;
            u = (float)(lch.C * Math.Cos(lch.H * (Math.PI / 180)));
            v = (float)(lch.C * Math.Sin(lch.H * (Math.PI / 180)));
        }


        public static explicit operator ColorXYZ(ColorLUV color)
        {
            return new ColorXYZ(color);
        }
        public static explicit operator ColorLUV(ColorXYZ color)
        {
            return new ColorLUV(color);
        }
        public static explicit operator ColorRGB(ColorLUV color)
        {
            return new ColorRGB((ColorXYZ)color);
        }
        public static explicit operator ColorLUV(ColorRGB color)
        {
            return new ColorLUV((ColorXYZ)color);
        }
    }
}
