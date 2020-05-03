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
            k_means(ref array,3);
            Console.ReadKey();
        }

        static void k_means(ref double[,] array, int k)
        {
            Random rnd = new Random(DateTime.Now.Millisecond);
            double[,] cluster_dimentions = new double[k, array.GetLength(1)];
            List<List<int>> centroids = new List<List<int>>();
            for (int i = 0; i < k; i++)
            {
                centroids.Add(new List<int>()); //indexes of poits 
            }

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
            //cluster_dimentions[0, 0] = 5.006;
            //cluster_dimentions[0, 1] = 3.418;
            //cluster_dimentions[0, 2] = 1.464;
            //cluster_dimentions[0, 3] = 0.244;
            //cluster_dimentions[1, 0] = 5.936;
            //cluster_dimentions[1, 1] = 2.77; //Średnie wartości z bazy pokrywa sie z tym co na wikipedi czyli że dla tej bazy ten algorytm nie jest idealny!
            //cluster_dimentions[1, 2] = 4.26;
            //cluster_dimentions[1, 3] = 1.326;
            //cluster_dimentions[2, 0] = 6.588;
            //cluster_dimentions[2, 1] = 2.974;
            //cluster_dimentions[2, 2] = 5.552;
            //cluster_dimentions[2, 3] = 2.026;
            #endregion
            int iteration = 0;
            while (distances(ref centroids, ref cluster_dimentions, ref array))
            {
                Console.WriteLine("Iteration: " + iteration);
                iteration++;

                if (cluster_dimentions.GetLength(0)==centroids.Count)
                {
                    for (int i = 0; i < centroids.Count; i++)
                    {
                        for (int j = 0; j < cluster_dimentions.GetLength(1); j++)
                        {
                            cluster_dimentions[i, j] = 0;

                            foreach (var item in centroids[i])
                            {
                                cluster_dimentions[i, j] += array[item, j];

                            }
                            cluster_dimentions[i, j] /= centroids[i].Count;

                        }

                    }
                }

                //foreach (var item in centroids)
                //{
                //    Console.WriteLine("Centroid " + (centroids.IndexOf(item)+1)+ " :");
                //    item.Sort();
                //    foreach (var subitem in item)
                //    {
                //        Console.WriteLine(subitem);
                //    }
                //    Console.WriteLine("------------------------");
                //}
            }
            foreach (var item in centroids)
            {
                Console.WriteLine("Centroid " + (centroids.IndexOf(item) + 1) + " :");
                item.Sort();
                foreach (var subitem in item)
                {
                    Console.WriteLine(subitem);
                }
                Console.WriteLine("------------------------");
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

        private static bool distances(ref List<List<int>> centroids, ref double[,] cluster_dimentions, ref double[,] array)
        {
            bool tmp = false;
            for (int i = 0; i < array.GetLength(0); i++)
            {
                double min = 0;
                double[] distance = new double[cluster_dimentions.GetLength(0)];
                for (int j = 0; j < distance.Length; j++)
                {
                    for (int z = 0; z < cluster_dimentions.GetLength(1); z++)
                    {
                        distance[j] += Math.Pow(array[i, z] - cluster_dimentions[j, z], 2);
                    }
                    distance[j] = Math.Sqrt(distance[j]);
                    Console.Write(" " + distance[j] + " ");
                    if (j == 0)
                        min = distance[j];
                    else
                    {
                        if (min > distance[j])
                            min = distance[j];
                    }
                    if (j == distance.Length - 1)
                        Console.Write("\n");
                }

                for (int j = 0; j < distance.Length; j++)
                {
                    if (min == distance[j])
                    {
                        if (!centroids[j].Contains(i))
                        {
                            tmp = true;
                            centroids[j].Add(i);
                            for (int z = 0; z < centroids.Count; z++)
                            {
                                if (z != j)
                                {
                                    if (centroids[z].Contains(i))
                                        centroids[z].Remove(i);

                                }
                            }

                        }
                        else
                            tmp = false;
                    }
                }
            }   
          
            return tmp;
        }
    }
}

