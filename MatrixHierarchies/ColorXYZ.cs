using System;
using System.Collections.Generic;
using System.Text;

namespace MatrixHierarchies
{
    struct ColorXYZ
    {
        public Vector3 colorV;

        public float X
        {
            get => colorV.x;
        }
        public float Y
        {
            get => colorV.y;
        }
        public float Z
        {
            get => colorV.z;
        }

        public static ColorXYZ White
        {
            get => new ColorXYZ(1, 1, 1);
        }

        public ColorXYZ(float x, float y, float z)
        {
            colorV = new Vector3(x, y, z);
        }

        public ColorXYZ(ColorRGB RGB)
        {
            Vector3 rgb = new Vector3((float)RGB.R/255, (float)RGB.G/255, (float)RGB.B/255);

            rgb.x = (float)((rgb.x <= 0.04045) ? rgb.x / 12.92 : Math.Pow((rgb.x + 0.16) / 1.16, 2.4f));
            rgb.y = (float)((rgb.y <= 0.04045) ? rgb.y / 12.92 : Math.Pow((rgb.y + 0.16) / 1.16, 2.4f));
            rgb.z = (float)((rgb.z <= 0.04045) ? rgb.z / 12.92 : Math.Pow((rgb.z + 0.16) / 1.16, 2.4f));

            Matrix3 transformMatrix = new Matrix3(0.4124564f, 0.2126729f, 0.0193339f,
                                                  0.3575761f, 0.7151522f, 0.1191920f,
                                                  0.1804375f, 0.0721750f, 0.9503041f);

            colorV = transformMatrix * rgb;
        }
        public ColorXYZ(ColorLUV luv)
        {
            double e = 216.0 / 24389.0;
            float k = 24389.0f / 27.0f;

            colorV.y = (luv.L > k * e) ? MathF.Pow((luv.L + 16) / 116, 3) : luv.L / k;

            float u0 = (4 * ColorXYZ.White.X) / (ColorXYZ.White.X + (15 * ColorXYZ.White.Y) + (3 * ColorXYZ.White.Z));
            float v0 = (9 * ColorXYZ.White.Y) / (ColorXYZ.White.X + (15 * ColorXYZ.White.Y) + (3 * ColorXYZ.White.Z));

            double a = (1.0 / 3.0) * (((52 * luv.L) / (luv.u + (13 * luv.L * u0))) - 1);
            double b = -5 * colorV.y;
            double c = -1.0 / 3.0;
            double d = colorV.y * (((39.0 * luv.L) / (luv.v + (13.0 * luv.L * v0))) - 5);

            colorV.x = (float)((d - b) / (a - c));
            colorV.z = (float)((colorV.x * a) + b);
        }


        public static explicit operator ColorXYZ(ColorRGB color)
        {
            return new ColorXYZ(color);
        }
        public static explicit operator ColorRGB(ColorXYZ color)
        {
            return new ColorRGB(color);
        }
    }
}
