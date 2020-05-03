using System;
using System.IO;

namespace fuzzySystems
{
    class DataFlowers
    {
        public double[][] ReadData(string path)
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

            return data;
        }
    }
}
