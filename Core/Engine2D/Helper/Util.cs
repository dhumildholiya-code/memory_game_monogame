using Microsoft.Xna.Framework;
using System;

namespace Core.Engine2D.Helper
{
    public static class Util
    {
        public static float Remap01(float a, float b, float t)
        {
            return Math.Clamp(((t - a) / (b - a)), 0f, 1f);
        }

        public static float Remap(float a, float b, float c, float d, float t)
        {
            float r = ((t - a) / (b - a)) * (d - c) + c;
            return Math.Clamp(r, c, d);
        }

        public static Point RemapRect(Point p, Rectangle rect)
        {
            float x = Remap(rect.X, rect.X + rect.Width, 0f, Screen.Width, p.X);
            float y = Remap(rect.Y, rect.Y + rect.Height, 0f, Screen.Height, p.Y);
            return new Point((int)x, (int)y);
        }
    }
}
