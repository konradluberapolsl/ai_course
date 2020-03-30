using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_1_2
{
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
