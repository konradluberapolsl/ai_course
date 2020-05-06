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
            k_NN n = new k_NN(data.D, "0,0.468119496855346,0.117594936708861,0.226867469879518,0.144620060790274,0.0458465139116203,0.162249134948097,0.152272727272727,0.965475562570983,0.785042394014963,0.207,0.832214767100671,0.656666666666667",4);


            Console.ReadKey();


        }

    }

     
}
