using System;

namespace fuzzySystems
{
    class Normalization
    {
        static double FindMaxValue(double[][] lista, int k)
        {
            double max = lista[0][k];
            for (int i = 0; i < lista.Length; i++)
            {
                if (max < lista[i][k])
                {
                    max = lista[i][k];
                }
            }
            return max;
        }

        static double FindMinValue(double[][] lista, int k)
        {
            double min = lista[0][k];
            for (int i = 0; i < lista.Length; i++)
            {
                if (min > lista[i][k])
                {
                    min = lista[i][k];
                }
            }
            return min;
        }

        static double Normalize(double value, double min, double max, double nmax, double nmin)
        {
            double x = ((value - min) / (max - min)) * (nmax - nmin);
            return x;
        }

        public double[][] DoNormalize(double[][] tab, int column, double nmax, double nmin)
        {
            double max = FindMaxValue(tab, column);
            double min = FindMinValue(tab, column);
            double[][] temp = tab;

            for (int i = 0; i < tab.Length; i++)
            {
                temp[i][column] = Normalize(temp[i][column], min, max, nmax, nmin);
            }
            return tab;
        }

        public double[][] Shuffle(double[][] tab)
        {
            Random random = new Random();
            for (int i = tab.Length - 1; i > 0; i--)
            {
                int swapIndex = random.Next(i + 1);
                double[] temp = tab[i];
                tab[i] = tab[swapIndex];
                tab[swapIndex] = temp;
            }
            return tab;
        }
    }
}
