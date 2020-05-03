using System;
using System.Collections.Generic;
using System.Linq;

namespace neutralNetwork
{
    class Network
    {
        static double learnigRate = 0.5;
        internal List<Layer> Layers;
        internal double[][] ExpectedResult;
        double[][] difResults;

        public Network(int inputNeurons, int hiddenLayers, int hiddenNeurons, int outputNeurons)
        {
            Layers = new List<Layer>();
            AddInputLayer(inputNeurons);


            for (int i = 0; i < hiddenLayers; i++)
            {
                AddLayers(new Layer(hiddenNeurons));
            }
            AddLayers(new Layer(outputNeurons));

            difResults = new double[Layers.Count][];

            for (int i = 0; i < Layers.Count; i++)
            {
                difResults[i] = new double[Layers[i].neurons.Count];
            }

        }

        public void AddInputLayer(int inputNeurons)
        {
            Layer inputLayer = new Layer(inputNeurons);
            foreach (var neuron in inputLayer.neurons)
            {
                neuron.AddInputSynapse(0);
            }
            Layers.Add(inputLayer);
        }

        public void AddLayers(Layer toAddLayer)
        {
            Layer lastLayer = Layers.Last();
            lastLayer.ConnectLayersWithOtherLayers(toAddLayer);
            Layers.Add(toAddLayer);
        }

        public void PushExpectedValue(double[][] data)
        {
            ExpectedResult = data;
        }

        public void PushInputValues(double[] inputs)
        {
            for (int i = 0; i < inputs.Length; i++)
            {
                Layers[0].neurons[i].PushValueOnInput(inputs[i]);
            }
        }

        public List<double> GetOutputs()
        {
            List<double> output = new List<double>();

            for (int i = 0; i < Layers.Count; i++)
            {
                Layers[i].CalculateLayer();
            }
            foreach (var neuron in Layers.Last().neurons)
            {
                output.Add(neuron.OutputVal);
            }

            return output;
        }


        public void CalculateDiffrences(List<double> outputs, int row)
        {
            for (int i = 0; i < Layers.Last().neurons.Count; i++)
            {
                difResults[Layers.Count - 1][i] = (ExpectedResult[row][i] - outputs[i]) * Functions.SigmoidFunctionDeriative(Layers.Last().neurons[i].InputVal);
            }

            for (int i = Layers.Count - 2; i > 0; i--)
            {
                for (int j = 0; j < Layers[i].neurons.Count; j++)
                {
                    difResults[i][j] = 0;

                    for (int k = 0; k < Layers[i + 1].neurons.Count; k++)
                    {
                        difResults[i][j] += difResults[i + 1][k] * Layers[i + 1].neurons[k].Inputs[j].Weight;
                    }

                    difResults[i][j] *= Functions.SigmoidFunctionDeriative(Layers[i].neurons[j].InputVal);
                }
            }
        }


        public void ChangeWeights(List<double> outputs, int row)
        {
            CalculateDiffrences(outputs, row);

            for (int i = Layers.Count - 1; i > 0; i--)
            {
                for (int j = 0; j < Layers[i].neurons.Count; j++)
                {
                    for (int k = 0; k < Layers[i - 1].neurons.Count; k++)
                    {
                        Layers[i].neurons[j].Inputs[k].UpdateWeight(learnigRate * 2 * difResults[i][j] * Layers[i - 1].neurons[k].OutputVal);
                    }
                }
            }
        }

        public void Train(double[][] inputs, double maxErr)
        {
            Console.WriteLine("Training ...");
            double error = double.MaxValue;
            while (error / inputs.Length > maxErr)
            {
                error = 0;
                List<double> outputs = new List<double>();

                for (int i = 0; i < inputs.Length; i++)
                {
                    PushInputValues(inputs[i]);
                    outputs = GetOutputs();
                    ChangeWeights(outputs, i);
                    error += Functions.CalculateError(outputs, i, ExpectedResult);
                }
            }
            Console.WriteLine("End");
        }
    }
}
