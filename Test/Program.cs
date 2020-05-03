using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Threading;
using System.IO;

namespace Test
{
    class Program
    {
        private static double[][] data;

        static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
            
            ReadData(ref data);
            Normalize(ref data);
            Shuffle(ref data);
            printTab(data);
            

            Console.ReadKey();
        }

        public static void ReadData(ref double[][] data)
        {
            string[] lines = File.ReadAllLines(@"winequalityN.csv");
            data = new double[lines.Length][];

            for (int i = 0; i < lines.Length; i++)
            {
                string[] tmp = lines[i].Split(',');
                data[i] = new double[tmp.Length];
                for (int j = 0; j < tmp.Length; j++)
                {
                    if (j == 0)
                    {
                        if (tmp[j] == "white")
                        {
                            data[i][0] = 0d;
                        }
                        else if (tmp[j] == "red")
                        {
                            data[i][0] = 1d;
                        }
                    }
                    else
                    {
                        double.TryParse(tmp[j], out data[i][j]);
                    }
                }
            }
        }

        public static void printTab(double[][] data)
        {
            for (int i = 0; i < data.Length; i++)
            {
                Console.Write("Rekord nr: " + (i + 1) + ":");
                for (int j = 0; j < data[i].Length; j++)
                {
                    if (j == 12)
                        Console.Write(" " + data[i][j] + "\n");
                    else
                        Console.Write(" " + data[i][j]);
                }
            }
        }

        public static void Normalize(ref double[][] data)
        {
            double minValue = 0;
            double maxValue = 1;
            double min = 0;
            double max = 0;
            for (int i = 1; i < data[0].Length; i++)
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
    }
}
