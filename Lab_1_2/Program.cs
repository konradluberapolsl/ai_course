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
      
    }


}
