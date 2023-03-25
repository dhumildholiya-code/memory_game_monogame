using System;

namespace Core.Engine2D.Helper
{
    public static class Util
    {
        public static float Remap01(float a, float b, float t)
        {
            return Math.Clamp(((t - a) / (b - a)), 0f, 1f);
        }
    }
}
