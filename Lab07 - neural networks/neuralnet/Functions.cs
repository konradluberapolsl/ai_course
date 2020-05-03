using System;
using System.Collections.Generic;

namespace neutralNetwork
{
    static class Functions
    {
        public static double SumInput(List<Synapse> synapses)
        {
            double sum = 0;
            foreach (var syn in synapses)
            {
                sum += syn.GetOutput();
            }
            return sum;
        }

        public static double SigmoidFunction(double input)
        {
            return 1 / (1 + Math.Exp(-input));
        }

        public static double SigmoidFunctionDeriative(double input)
        {
            return SigmoidFunction(input) * (1 - SigmoidFunction(input));
        }

        public static double CalculateError(List<double> outputs, int row, double[][] ExceptedResults)
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
