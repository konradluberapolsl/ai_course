using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class k_NN
    {
        private double[] unknown;
        private int k;
        private double[][] distances;
        public k_NN(double[][] data, string unknown, int k)
        {

            string[] tmp = unknown.Split(',');
            this.unknown = new double[tmp.Length];
            
            for (int i = 0; i < tmp.Length; i++)
            {
                double.TryParse(tmp[i], out this.unknown[i]);
            }

            this.k = k;
            
            distances = Distances(ref data);

            Predict(ref data);
        }

        private double[][] Distances(ref double[][] data)
        {
            double[][] tmp = new double[data.Length][];

            for (int i = 0; i < data.Length; i++)
            {
                tmp[i] = new double[2];
                tmp[i][0] = CalculateDistance(data[i]);
                tmp[i][1] = i;
            }
           
            Sort<double>(tmp, 0);

            double[][] t = new double[k][];
            for (int i = 0; i < t.Length; i++)
            {
                t[i] = new double[2];
                t[i][0] = tmp[i][0];
                t[i][1] = tmp[i][1];
            }

            return t;
        }

        private static void Sort<T>(T[][] data, int col) //wiem mogłem to zrobić na milion lepszych sposobów
        {
            Comparer<T> comparer = Comparer<T>.Default;
            Array.Sort<T[]>(data, (x, y) => comparer.Compare(x[col], y[col]));
        }
         

        private double CalculateDistance(double[] tmp_d)
        {
            double distance = 0;
            for (int  i = 0;  i <unknown.Length;  i++)
            {
                distance += Math.Pow(tmp_d[i] - unknown[i], 2);
            }
            distance = Math.Sqrt(distance);
            return distance;
        }

        void Predict(ref double[][] data)
        {
            Dictionary<double, double> votes = new Dictionary<double, double>();

            for (int i = 0; i < distances.Length; i++)
            {
                double tmp = data[(int)(distances[i][1])][12];
                if (!votes.ContainsKey(tmp))
                {
                    votes.Add(tmp, 1);
                }
                else
                    votes[tmp]++;
            }

            double max = votes.Values.Max();
            int z = 0;
            foreach (KeyValuePair<double ,double> kvp in votes)
            {
                if (kvp.Value == max)
                    z++;

                if (z > 1)
                    Console.WriteLine("Tie! Try other K");

                Console.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value);

            }
            Console.WriteLine("Prediceted quality: " + (int)(votes.FirstOrDefault(x => x.Value == max).Key*10));
        }
    }
}
