using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_1_2
{
    static class Dane
    {
        //Miałem wcześniej wszystkie metody w main więc poprstu utorzyłem klasę statyczną żeby nie przerabiać tego jakoś bardzo 
        public static void Normalize(ref double[][] data)
        {
            double minValue = 0;
            double maxValue = 1;
            double min = 0;
            double max = 0;
            for (int i = 0; i < data[0].Length - 3; i++)
            {
                min = findMin(data, i);
                max = findMax(data, i);
                for (int j = 0; j < data.Length; j++)
                {
                    double temp = 0;
                    temp = (data[j][i] - min) / (max - min) * (maxValue - minValue);
                    data[j][i] = temp;
                }

            }
        }

        public static void ReadData(ref double[][] data)
        {
            string[] lines = File.ReadAllLines(@"iris.txt");
            data = new double[lines.Length][];
            IFormatProvider FormatProvider = new System.Globalization.CultureInfo("");
            for (int i = 0; i < lines.Length; i++)
            {
                string[] tmp = lines[i].Split(',');
                data[i] = new double[tmp.Length + 2];
                for (int j = 0; j < tmp.Length; j++)
                {
                    if (j == 4)
                    {
                        if (tmp[j] == "Iris-setosa")
                        {
                            data[i][4] = 0d;
                            data[i][5] = 0d;
                            data[i][6] = 1d;
                        }
                        else if (tmp[j] == "Iris-versicolor")
                        {
                            data[i][4] = 0d;
                            data[i][5] = 1d;
                            data[i][6] = 0d;
                        }
                        else if (tmp[j] == "Iris-virginica")
                        {
                            data[i][4] = 1d;
                            data[i][5] = 0d;
                            data[i][6] = 0d;
                        }
                    }
                    else
                    {
                        double.TryParse(tmp[j], out data[i][j]);
                        //bool a = double.TryParse(tmp[j], System.Globalization.NumberStyles.Float | System.Globalization.NumberStyles.AllowThousands, FormatProvider, out data[i][j]);
                    }
                }
            }
        }

        public static void Shuffle(ref double[][] data)
        {
            Random random = new Random();
            for (int i = data.Length - 1; i >= 0; i--)
            {
                int r = random.Next(0, data.Length);
                for (int j = 0; j < data[i].Length; j++)
                {
                    double tmp = data[i][j];
                    data[i][j] = data[r][j];
                    data[r][j] = tmp;
                }
            }
        }

        private static double findMax(double[][] data, int index)
        {
            double max = 0.0;
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i][index] > max)
                    max = data[i][index];
            }
            return max;
        }

        private static double findMin(double[][] data, int index)
        {
            double min = double.MaxValue;
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i][index] < min)
                    min = data[i][index];
            }
            return min;
        }

        public static void printTab(double[][] data)
        {
            for (int i = 0; i < data.Length; i++)
            {
                Console.Write("Rekord nr: " + (i + 1) + ":");
                for (int j = 0; j < data[i].Length; j++)
                {
                    if (j == 6)
                        Console.Write(" " + data[i][j] + "\n");
                    else
                        Console.Write(" " + data[i][j]);
                }
            }
        }
    }
}
