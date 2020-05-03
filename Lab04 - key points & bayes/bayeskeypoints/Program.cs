using System;
using System.Drawing;

using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace bayeskeypoints
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