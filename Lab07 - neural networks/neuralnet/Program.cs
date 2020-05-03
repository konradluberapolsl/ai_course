using System;
using System.Collections.Generic;
using System.Linq;

namespace neutralNetwork
{
    class Program
    {
        static void Main(string[] args)
        {
            Data d = new Data();
            double[][] data = d.ReadIrisDataBase(@"iris_database.txt");

            double[][] expectedValue = new double[data.Length][];
            double[][] trainData = new double[data.Length][];
            for (int i = 0; i < data.Length; i++)
            {
                trainData[i] = new double[4];
                expectedValue[i] = new double[3];
                for (int j = 0; j < 4; j++)
                {
                    trainData[i][j] = data[i][j];
                }
                for (int j = 0; j < 3; j++)
                {
                    expectedValue[i][j] = data[i][j + 4];
                }
            }

            Network network = new Network(4, 2, 4, 3);
            network.PushExpectedValue(expectedValue);

            network.Train(trainData, 0.15);

            double[] whatFlower = { 5.9, 2.8, 5.1, 2.0 };
            network.PushInputValues(whatFlower);

            List<double> output = network.GetOutputs();
            int index = output.IndexOf(output.Max());

            if (index == 0) Console.Write("Iris-setosa");
            else if (index == 1) Console.Write("Iris-versicolor");
            else if (index == 2) Console.Write("Iris-virginica");
            
            Console.ReadKey();
        }
    }
}
