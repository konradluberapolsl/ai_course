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

        static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");

            Data data = new Data(@"winequalityN.csv");
            data.Normalize();
            data.Shuffle();
            //data.Print();

            double[][] d;
            double[][] data70;
            double[][] data30;
            double[][] expectedValue70;
            double[][] expectedValue30;
            double[][] trainData70;
            double[][] dataToCheck30;

            //Przetasowana i znormalizowana data[][]
            d = data.D;
          

            #region Dzielenie przygotowanie data70, data30 itp
            //Dzielenia data na odpowiednio 70% i 30%
            data70 = new double[(int)(d.Length * 0.7)][];
            data30 = new double[d.Length - data70.Length][];

            for (int i = 0; i < (int)(d.Length * 0.7); i++)
            {
                data70[i] = new double[d[i].Length];
                for (int j = 0; j < d[0].Length; j++)
                {
                    data70[i][j] = d[i][j];
                }
            }

            for (int i = 0; i < data30.Length; i++)
            {
                data30[i] = new double[d[i].Length];
                for (int j = 0; j < d[0].Length; j++)
                {
                    data30[i][j] = d[data30.Length + i][j];
                }
            }

            trainData70 = new double[data70.Length][];
            expectedValue70 = new double[data70.Length][];

            for (int i = 0; i < data70.Length; i++)
            {
                trainData70[i] = new double[13];
                expectedValue70[i] = new double[2];
                for (int j = 0; j < 13; j++)
                {
                    trainData70[i][j] = data70[i][j];
                }
                for (int j = 0; j < 2; j++)
                {
                    expectedValue70[i][j] = data70[i][j + 11];
                }
            }


            dataToCheck30 = new double[data30.Length][];
            expectedValue30 = new double[data30.Length][];

            for (int i = 0; i < data30.Length; i++)
            {
                dataToCheck30[i] = new double[13];
                expectedValue30[i] = new double[2];
                for (int j = 0; j < 13; j++)
                {
                    dataToCheck30[i][j] = data30[i][j];
                }
                for (int j = 0; j < 2; j++)
                {
                    expectedValue30[i][j] = data30[i][j + 11];
                }
            }

            #endregion

            //for (int i = 0; i < data70.Length; i++)
            //{
            //    for (int j = 0; j < data70[i].Length; j++)
            //    {
            //        Console.WriteLine(data70[i][j]);
            //    }

            //}



            Network network = new Network(13, 4, 4, 1);

            network.PushExpectedValue(expectedValue70);
            network.Train(trainData70, 0.1);
            //network.TrainByInterations(trainData70, 3000);
            //network.LoadWeights();
            network.SaveWeightsToFile();



            Console.ReadKey();


        }

    }

     
}
