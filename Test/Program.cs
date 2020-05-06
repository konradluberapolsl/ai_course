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
            data.Print();

            Console.ReadKey();
        }

    }

     
}
