using System;
using System.IO;

namespace neutralNetwork
{
    class Data
    {
        static double Normalize(double value, double min, double max, double nmax, double nmin)
        {
            double x = ((value - min) / (max - min)) * (nmax - nmin);
            return x;
        }
        public double[][] Normalize2D(double[][] tab, int column, double nmax, double nmin)
        {
            double max = FindMax(tab, column);
            double min = FindMin(tab, column);
            double[][] temp = tab;

            for (int i = 0; i < tab.Length; i++)
            {
                temp[i][column] = Normalize(temp[i][column], min, max, nmax, nmin);
            }
            return tab;
        }
        private static double FindMax(double[][] data, int index)
        {
            double max = 0.0;
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i][index] > max)
                    max = data[i][index];
            }
            return max;
        }
        private static double FindMin(double[][] data, int index)
        {
            double min = double.MaxValue;
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i][index] < min)
                    min = data[i][index];
            }
            return min;
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
        public double[][] ReadIrisDataBase(string path)
        {
            string[] lines = File.ReadAllLines(path);
            double[][] data = new double[lines.Length][];

            for (int i = 0; i < lines.Length; i++)
            {
                string[] temp = lines[i].Split(',');

                data[i] = new double[temp.Length + 2];

                for (int j = 0; j < temp.Length - 1; j++)
                {
                    data[i][j] = Convert.ToDouble(temp[j].Replace('.', ','));
                }

                for (int k = 0; k < 3; k++)
                {
                    if (temp[4] == "Iris-setosa")
                    {
                        data[i][4] = 0;
                        data[i][5] = 0;
                        data[i][6] = 1;
                    }
                    else if (temp[4] == "Iris-versicolor")
                    {
                        data[i][4] = 0;
                        data[i][5] = 1;
                        data[i][6] = 0;
                    }
                    else if (temp[4] == "Iris-virginica")
                    {
                        data[i][4] = 1;
                        data[i][5] = 0;
                        data[i][6] = 0;
                    }
                }

            }
            for (int i = 0; i < 3; i++)
            {
                data = Normalize2D(data, i, 1, 0);
            }
            data = Shuffle(data);
            return data;
        }

    }
}
