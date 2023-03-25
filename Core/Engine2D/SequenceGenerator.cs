using System;

namespace Core.Engine2D
{
    public class SequenceGenerator
    {
        public int[] CreateSequence(int n, int maxValue)
        {
            Random rand = new Random();
            int[] seq = new int[n];
            for (int i = 0; i < n; i++)
            {
                seq[i] = rand.Next(0, maxValue + 1);
            }
            return seq;
        }
    }
}
