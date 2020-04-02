using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Globalization;
using System.Threading.Tasks;
using System.IO;

namespace Testowy_CS
{
    class Program
    {
        private static double[,] array;
        static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
            array = init_data(@"iris.txt");
            k_means(ref array);
            Console.ReadKey();
        }
        static void k_means(ref double[,] array)
        {
            Random rnd = new Random(DateTime.Now.Millisecond);
            int k = 3;
            double[,] cluster_dimentions = new double[k, array.GetLength(1)];
            List<int> centroid_1 = new List<int>();
            List<int> centroid_2 = new List<int>();
            List<int> centroid_3 = new List<int>(); //indexes of points 0-150 
            double[] max = find_max(ref array);
            #region Generating clusters
            for (int j = 0; j < cluster_dimentions.GetLength(0); j++)
            {
                for (int i = 0; i < cluster_dimentions.GetLength(1); i++)
                {
                    cluster_dimentions[j, i] = rnd.NextDouble() * (max[i] + 3);
                    //cluster_dimentions[j, i] = rnd.NextDouble() * 10;
                    Console.WriteLine(j + 1 + ". K dimension " + (i + 1) + ":    " + cluster_dimentions[j, i]);
                }
                Console.WriteLine("------------------------");
            }
            #endregion
            int iteration = 0;
            while (idk(ref centroid_1, ref centroid_2, ref centroid_3, ref cluster_dimentions, ref array))
            {
                Console.WriteLine("Iteration: " + iteration);
                iteration++;
                cluster_dimentions[0, 0] = 0;
                cluster_dimentions[0, 1] = 0;
                cluster_dimentions[0, 2] = 0;
                cluster_dimentions[0, 3] = 0;
                foreach (var item in centroid_1)
                {
                    cluster_dimentions[0, 0] += array[item, 0];
                    cluster_dimentions[0, 1] += array[item, 1];
                    cluster_dimentions[0, 2] += array[item, 2];
                    cluster_dimentions[0, 3] += array[item, 3];
                }
                cluster_dimentions[0, 0] /= centroid_1.Count;
                cluster_dimentions[0, 1] /= centroid_1.Count;
                cluster_dimentions[0, 2] /= centroid_1.Count;
                cluster_dimentions[0, 3] /= centroid_1.Count;

                cluster_dimentions[1, 0] = 0;
                cluster_dimentions[1, 1] = 0;
                cluster_dimentions[1, 2] = 0;
                cluster_dimentions[1, 3] = 0;
                foreach (var item in centroid_2)
                {
                    cluster_dimentions[1, 0] += array[item, 0];
                    cluster_dimentions[1, 1] += array[item, 1];
                    cluster_dimentions[1, 2] += array[item, 2];
                    cluster_dimentions[1, 3] += array[item, 3];
                }
                cluster_dimentions[1, 0] /= centroid_2.Count;
                cluster_dimentions[1, 1] /= centroid_2.Count;
                cluster_dimentions[1, 2] /= centroid_2.Count;
                cluster_dimentions[1, 3] /= centroid_2.Count;

                cluster_dimentions[2, 0] = 0;
                cluster_dimentions[2, 1] = 0;
                cluster_dimentions[2, 2] = 0;
                cluster_dimentions[2, 3] = 0;
                foreach (var item in centroid_3)
                {
                    cluster_dimentions[2, 0] += array[item, 0];
                    cluster_dimentions[2, 1] += array[item, 1];
                    cluster_dimentions[2, 2] += array[item, 2];
                    cluster_dimentions[2, 3] += array[item, 3];
                }
                cluster_dimentions[2, 0] /= centroid_3.Count;
                cluster_dimentions[2, 1] /= centroid_3.Count;
                cluster_dimentions[2, 2] /= centroid_3.Count;
                cluster_dimentions[2, 3] /= centroid_3.Count;
                Console.WriteLine("Centroid 1: ");
                foreach (var item in centroid_1)
                {
                    Console.WriteLine(item);
                }
                Console.WriteLine("------------------------");
                Console.WriteLine("Centroid 2: ");
                foreach (var item in centroid_2)
                {
                    Console.WriteLine(item);
                }
                Console.WriteLine("------------------------");
                Console.WriteLine("Centroid 3: ");
                foreach (var item in centroid_3)
                {
                    Console.WriteLine(item);
                }
            }

            centroid_1.Sort();
            centroid_2.Sort();
            centroid_3.Sort();
            Console.WriteLine("Centroid 1: ");
            foreach (var item in centroid_1)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("------------------------");
            Console.WriteLine("Centroid 2: ");
            foreach (var item in centroid_2)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("------------------------");
            Console.WriteLine("Centroid 3: ");
            foreach (var item in centroid_3)
            {
                Console.WriteLine(item);
            }

        }
        private static double[,] init_data(string path)
        {
            string[] lines = File.ReadAllLines(path);
            double[,] data = new double[lines.Length, 4];

            for (int i = 0; i < lines.Length; i++)
            {
                string[] tmp = lines[i].Split(',');
                for (int j = 0; j < tmp.Length - 1; j++)
                {
                    double.TryParse(tmp[j], out data[i, j]);
                }
            }
            return data;
        }

        private static double[] find_max(ref double[,] array) // pomocnicze do rand
        {
            double[] max = new double[array.GetLength(0)];
            for (int j = 0; j < array.GetLength(1); j++)
            {
                max[j] = 0;
                for (int i = 0; i < array.GetLength(0); i++)
                {
                    if (array[i, j] > max[j])
                        max[j] = array[i, j];
                }
                // Console.WriteLine("Max w " + j + " wymiarze: " + max[j]);

            }

            return max;
        }

        private static bool idk(ref List<int> centroid_1, ref List<int> centroid_2, ref List<int> centroid_3, ref double[,] cluster_dimentions, ref double[,] array)
        {
            bool tmp = false;
            for (int i = 0; i < array.GetLength(0); i++)
            {
                double distance_1 = Math.Sqrt(Math.Pow(array[i, 0] - cluster_dimentions[0, 0], 2) + Math.Pow(array[i, 1] - cluster_dimentions[0, 1], 2) + Math.Pow(array[i, 2] - cluster_dimentions[0, 2], 2) + Math.Pow(array[i, 3] - cluster_dimentions[0, 3], 2));
                double distance_2 = Math.Sqrt(Math.Pow(array[i, 0] - cluster_dimentions[1, 0], 2) + Math.Pow(array[i, 1] - cluster_dimentions[1, 1], 2) + Math.Pow(array[i, 2] - cluster_dimentions[1, 2], 2) + Math.Pow(array[i, 3] - cluster_dimentions[1, 3], 2));
                double distance_3 = Math.Sqrt(Math.Pow(array[i, 0] - cluster_dimentions[2, 0], 2) + Math.Pow(array[i, 1] - cluster_dimentions[2, 1], 2) + Math.Pow(array[i, 2] - cluster_dimentions[2, 2], 2) + Math.Pow(array[i, 3] - cluster_dimentions[2, 3], 2));
                Console.WriteLine(distance_1 + " " + distance_2 + " " + distance_3);
                double min = distance_1;
                if (min > distance_2)
                {
                    min = distance_2;
                }
                if (min > distance_3)
                {
                    min = distance_3;
                }
                if (distance_1 == min)
                {
                    if (!centroid_1.Contains(i))
                    {
                        tmp = true;
                        centroid_1.Add(i);
                        if (centroid_2.Contains(i))
                            centroid_2.Remove(i);
                        else if (centroid_3.Contains(i))
                            centroid_3.Remove(i);
                    }
                    else
                        tmp = false;

                }
                else if (distance_2 == min)
                {
                    if (!centroid_2.Contains(i))
                    {
                        tmp = true;
                        centroid_2.Add(i);
                        if (centroid_1.Contains(i))
                            centroid_1.Remove(i);
                        else if (centroid_3.Contains(i))
                            centroid_3.Remove(i);
                    }
                    else
                        tmp = false;
                }
                else if (distance_3 == min)
                {
                    if (!centroid_3.Contains(i))
                    {
                        tmp = true;
                        centroid_3.Add(i);
                        if (centroid_2.Contains(i))
                            centroid_2.Remove(i);
                        else if (centroid_1.Contains(i))
                            centroid_1.Remove(i);
                    }
                    else
                        tmp = false;
                }
            }

            return tmp;
        }
    }
}

