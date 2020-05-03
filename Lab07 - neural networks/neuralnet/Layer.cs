using System.Collections.Generic;

namespace neutralNetwork
{
    class Layer
    {
        public List<Neuron> neurons { get; set; }

        public Layer()
        {
            neurons = new List<Neuron>();
        }

        public Layer(int numOfNeurons)
        {
            neurons = new List<Neuron>();
            for (int i = 0; i < numOfNeurons; i++)
            {
                neurons.Add(new Neuron());
            }
        }

        public void ConnectLayersWithOtherLayers(Layer other)
        {
            foreach (var thisNeuron in neurons)
            {
                foreach (var otherNeuron in other.neurons)
                {
                    thisNeuron.AddOutputNeuron(otherNeuron);
                }
            }
        }

        public void CalculateLayer()
        {
            foreach (var neuron in neurons)
            {
                neuron.Calculate();
            }
        }
    }
}