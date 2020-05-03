using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyPoints_Bayes
{
    class Program
    {
        static void Main(string[] args)
        {
            var bayes = new Bayes();
            bayes.LoadData(@"GoForAWalk.txt");
            bayes.Learn();
            bayes.Predict("Sunny", "Cool", "Weak");
            bayes.Predict("Rainy", "Hot", "Strong");

            var keyPoints = new KeyPoints();
            keyPoints.AddMask();
            keyPoints.PotencialPoints(5000);

            Console.ReadKey();
        }
    }
}
