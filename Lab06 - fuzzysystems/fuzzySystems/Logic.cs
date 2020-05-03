using System;
using System.Collections.Generic;

namespace fuzzySystems
{
    class Logic
    {
        List<FuzzyRule> rules;
        List<double> results;

        string[] decisions;
        int datalen;

        public Logic()
        {
            rules = new List<FuzzyRule>();
            results = new List<double>();
            datalen = 0;
        }

        public void FuzzyLogic(double[][] data)
        {
            datalen = data.Length;
            rules.Add(new FuzzyRule(0.0, 0.1, 0.2, 0.3));
            rules.Add(new FuzzyRule(0.3, 0.2, 0.8, 0.8));
            rules.Add(new FuzzyRule(0.9, 0.4, 1, 1));

            DataToRule(data);
            Calculate();
            Decision();

        }
        public void DataToRule(double[][] data)
        {
            for (int i = 0; i < datalen; i++)
            {
                rules[0].AddElement(data[i][2]);
                rules[1].AddElement(data[i][1]);
                rules[2].AddElement(data[i][0]);
            }
        }

        void Calculate()
        {
            for (int i = 0; i < datalen; i++)
            {
                double a = 1d;
                for (int j = 0; j < rules.Count; j++)
                {
                    a += rules[j].GeElement(i);
                }
                results.Add(a);
            }
        }

        void Decision()
        {
            decisions = new string[datalen];

            for (int i = 0; i < datalen; i++)
            {
                decisions[i] = Judge(results[i]);
                Console.WriteLine("{0}   {1:0.00}    {2}", i, results[i], decisions[i]);
            }
        }


        string Judge(double a)
        {
            if (a > 2) return "3. Virginica";
            else if (a > 1) return "2. Versicolor";
            else return "1. Setosa";
        }
    }
}
