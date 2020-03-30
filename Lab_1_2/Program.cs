using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Globalization;

namespace Lab_1_2
{
    
    class Program
    {
        private static double[][] data;
        static void Main(string[] args)
        {
            string path = @"nowy.jpg";
            Color[][] matrix = Grafika.Macierz(path);
            Grafika.Sharp(matrix, path);
            Grafika.Gauss(matrix, path);
            Grafika.Blur(matrix, path);
            Grafika.Edge_detection(matrix, path);

            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
            Dane.ReadData(ref data);
            Dane.Normalize(ref data);
            Dane.Shuffle(ref data);
            Dane.printTab(data);
            Console.ReadKey();
        }
        //Miałem wcześniej wszystkie metody w main więc poprstu utorzyłem klasę statyczną żeby nie przerabiać tego jakoś bardzo 
    }
    static class Dane
    {
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

    static class Grafika
    {
        public static Color[][] Macierz(string path)
        {
            Bitmap btm = new Bitmap(path);
            Color[][] matrix = new Color[btm.Width][];
            for (int i = 0; i < btm.Width; i++)
            {
                matrix[i] = new Color[btm.Height];
                for (int j = 0; j < btm.Height; j++)
                {
                    matrix[i][j] = btm.GetPixel(i, j);
                }
            }
            return matrix;
        }
        private static Bitmap Modify(Color[][] matrix, double[][] kernel)
        {
            Bitmap btm = new Bitmap(matrix.Length, matrix[0].Length);
            //for (int i = 0; i < kernel.Length; i++)
            //{
            //    for (int j = 0; j < kernel[i].Length; j++)
            //    {
            //        k += kernel[i][j];
            //    }
            //}
            for (int i = 0; i < matrix.Length - kernel.Length; i++)
            {
                for (int j = 0; j < matrix[0].Length - kernel[0].Length; j++)
                {

                    double pR = 0, pG = 0, pB = 0;
                    for (int x = kernel.Length - 1; x > -1; x--)
                    {
                        for (int y = kernel[0].Length - 1; y > -1; y--)
                        {
                            pR += (matrix[i + kernel.Length - 1 - x][j + kernel[0].Length - 1 - y].R * kernel[x][y]);
                            pG += (matrix[i + kernel.Length - 1 - x][j + kernel[0].Length - 1 - y].G * kernel[x][y]);
                            pB += (matrix[i + kernel.Length - 1 - x][j + kernel[0].Length - 1 - y].B * kernel[x][y]);

                        }
                    }
                    //pR *= k;
                    //pG *= k;
                    //pB *= k;
                    if (pR < 0)
                        pR = 0;
                    if (pG < 0)
                        pG = 0;
                    if (pB < 0)
                        pB = 0;
                    if (pR > 255)
                        pR = 255;
                    if (pG > 255)
                        pG = 255;
                    if (pB > 255)
                        pB = 255;
                    //Console.Write("R: " + pR + " G: " + pG + " B: " + pB + " \n");
                    //Color pxl = btm.GetPixel(i, j);
                    //int newColor = (pxl.R+pxl.G+pxl.B)/3;
                    btm.SetPixel(i, j, Color.FromArgb((int)pR, (int)pG, (int)pB));
                }
            }
            return btm;
        }

        public static void Sharp(Color[][] matrix, string path)
        {
            double[][] kernel = new double[3][];
            kernel[0] = new double[] { -1, -1, -1 };
            kernel[1] = new double[] { -1, 9, -1 };
            kernel[2] = new double[] { -1, -1, -1 };
            Bitmap btm = Modify(matrix, kernel);
            path = path.Replace(".jpg", "");
            btm.Save(path + "_SHARP.jpg");
        }
        public static void Gauss(Color[][] matrix, string path)
        {
            double[][] kernel = new double[3][];
            kernel[0] = new double[] { 0.075, 0.124, 0.075 };
            kernel[1] = new double[] { 0.124, 0.204, 0.124 };
            kernel[2] = new double[] { 0.075, 0.124, 0.075 };
            Bitmap btm = Modify(matrix, kernel);
            path = path.Replace(".jpg", "");
            btm.Save(path + "_GAUSS.jpg");
        }
        public static void Blur(Color[][] matrix, string path)
        {
            double x = 1.0 / 9.0;
            double[][] kernel = new double[3][];
            kernel[0] = new double[] { x, x, x };
            kernel[1] = new double[] { x, x, x };
            kernel[2] = new double[] { x, x, x };
            Bitmap btm = Modify(matrix, kernel);
            path = path.Replace(".jpg", "");
            btm.Save(path + "_BLUR.jpg");
        }
        public static void Edge_detection(Color[][] matrix, string path)
        {
            double[][] kernel = new double[3][];
            kernel[0] = new double[] { 0, 1, 0 };
            kernel[1] = new double[] { 1, -4, 1 };
            kernel[2] = new double[] { 0, 1, 0 };
            Bitmap btm = Modify(matrix, kernel);
            path = path.Replace(".jpg", "");
            btm.Save(path + "_Edge_detection.jpg");
        }
    }
}
