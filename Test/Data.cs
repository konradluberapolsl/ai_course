using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Data
    {
        private double[][] data;
        public double[][] D { get { return data; } set { data = value; } }

        public Data(string path)
        {
            string[] lines = File.ReadAllLines(path);
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
                        double.TryParse(tmp[j], out data[i][j]); // w puste miejsca wstawia 0
                    }
                }
            }
            //this.data = data;
        }
        public void Print()
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

        public void Normalize()
        {
            double minValue = 0;
            double maxValue = 1;
            double min = 0;
            double max = 0;
            for (int i = 1; i < data[0].Length; i++)
            {
                min = findMin( i);
                max = findMax( i);
                for (int j = 0; j < data.Length; j++)
                {
                    double temp = 0;
                    temp = (data[j][i] - min) / (max - min) * (maxValue - minValue);
                    data[j][i] = temp;
                }

            }
        }

        public void Shuffle()
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


        private double findMax( int index)
        {
            double max = 0.0;
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i][index] > max)
                    max = data[i][index];
            }
            return max;
        }

        private double findMin( int index)
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



