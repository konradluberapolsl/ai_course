using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    static class Functions
    {
        public static double Alpha = 0.5;

        public static double SumInput(List<Synapse> synapses)
        {
            double sum = 0;
            foreach (var syn in synapses)
            {
                sum += syn.GetOutput();
            }
            return sum;
        }
        public static double BipolarLinearFunction(double input) // activation function
         => (1 - Math.Pow(Math.E, -Alpha * input)) / (1 + Math.Pow(Math.E, -Alpha * input));

        public static double BipolarDifferential(double input) // activation function derivative
            => (2 * Alpha * Math.Pow(Math.E, -Alpha * input)) / (Math.Pow(1 + Math.Pow(Math.E, -Alpha * input), 2));

        public static double SigmoidFunction(double input)
        {
            return 1 / (1 + Math.Exp(-input));
        }

        public static double SigmoidFunctionDeriative(double input)
        {
            return SigmoidFunction(input) * (1 - SigmoidFunction(input));
        }

        public static double CalculateError(List<double> outputs, int row , double[][] ExceptedResults)
        {
            double err = 0;
            for (int i = 0; i < outputs.Count; i++)
            {
                err += Math.Pow(outputs[i] - ExceptedResults[row][i], 2);
            }
            return err;
        }
    }
}
