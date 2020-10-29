using Raylib_cs;
using System;
namespace MatrixHierarchies
{
    struct ColorRGB
    {
        public UInt32 colour;

        public byte R
        {
            get => (byte)((colour & 0xFF000000) >> 24);
            set => colour = (colour & 0x00FFFFFF) + (uint)(value << 24);
        }
        public byte G
        {
            get => (byte)((colour & 0x00FF0000) >> 16);
            set => colour = (colour & 0xFF00FFFF) + (uint)(value << 16);
        }
        public byte B
        {
            get => (byte)((colour & 0x0000FF00) >> 8);
            set => colour = (colour & 0xFFFF00FF) + (uint)(value << 8);
        }
        public byte A
        {
            get => (byte)(colour & 0x000000FF);
            set => colour = (colour & 0xFFFFFF00) + value;
        }

        public ColorRGB(byte red, byte green, byte blue, byte alpha)
        {
            colour = (uint)red << 24;
            colour += (uint)green << 16;
            colour += (uint)blue << 8;
            colour += (uint)alpha;
        }
        public ColorRGB(ColorXYZ XYZ)
        {
            Matrix3 transformMatrix = new Matrix3(3.2404542f, -0.9692660f, 0.0556434f,
                                                  -1.5371385f, 1.8760108f, -0.2040259f,
                                                  -0.4985314f, 0.0415560f, 1.0572252f);

            Vector3 rgb = transformMatrix * XYZ.colorV;

            rgb.x = (float)((rgb.x <= 0.0031308) ? 12.92 * rgb.x : Math.Pow(1.055 * rgb.x, 1 / 2.4f) - 0.055);
            rgb.y = (float)((rgb.y <= 0.0031308) ? 12.92 * rgb.y : Math.Pow(1.055 * rgb.y, 1 / 2.4f) - 0.055);
            rgb.z = (float)((rgb.z <= 0.0031308) ? 12.92 * rgb.z : Math.Pow(1.055 * rgb.z, 1 / 2.4f) - 0.055);

            colour = (uint)(rgb.x * 255) << 24;
            colour += (uint)(rgb.y * 255) << 16;
            colour += (uint)(rgb.z * 255) << 8;
            colour += (uint)255;
        }

        public static ColorRGB Lerp(ColorRGB start, ColorRGB end, float increment)
        {
            ColorRGB toReturn = (ColorRGB)ColorLCH.Lerp((ColorLCH)start, (ColorLCH)end, increment);
            toReturn.A = (byte)(start.A + ((end.A - start.A) * increment));

            return toReturn;
        }

        public static ColorRGB operator +(ColorRGB lhs, float rhs)
        {
            lhs.R = (byte)MathF.Min(lhs.R + rhs, 255);
            lhs.G = (byte)MathF.Min(lhs.G + rhs, 255);
            lhs.B = (byte)MathF.Min(lhs.B + rhs, 255);
            lhs.A = (byte)MathF.Min(lhs.A + rhs, 255);
            return lhs;
        }
        public static ColorRGB operator -(ColorRGB lhs, float rhs)
        {
            lhs.R = (byte)MathF.Max(lhs.R - rhs, 0);
            lhs.G = (byte)MathF.Max(lhs.G - rhs, 0);
            lhs.B = (byte)MathF.Max(lhs.B - rhs, 0);
            lhs.A = (byte)MathF.Max(lhs.A - rhs, 0);
            return lhs;
        }
        public static ColorRGB operator +(float lhs, ColorRGB rhs)
        {
            rhs.R = (byte)MathF.Min(rhs.R + lhs, 255);
            rhs.G = (byte)MathF.Min(rhs.G + lhs, 255);
            rhs.B = (byte)MathF.Min(rhs.B + lhs, 255);
            rhs.A = (byte)MathF.Min(rhs.A + lhs, 255);
            return rhs;
        }
        public static implicit operator Color(ColorRGB color)
        {
            return new Color(color.R, color.G, color.B, color.A);
        }
        public static implicit operator ColorRGB(Color color)
        {
            return new ColorRGB(color.r, color.g, color.b, color.a);
        }
    }
}
