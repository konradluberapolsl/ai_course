using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Synapse
    {
        
        private Neuron _fromNeuron;
        private Neuron _toNeuron;
        public Neuron From { get { return _fromNeuron; }  }
        public Neuron To { get { return _toNeuron; }  }
        static Random random = new Random();

        public double Weight { get; set; }
        public double PrevWeight { get; set; }

        public double OutputVal { get; set; }

        public Synapse(Neuron fromNeuron, Neuron toNeuron)
        {
            _fromNeuron = fromNeuron;
            _toNeuron = toNeuron;

            Weight = random.NextDouble() - 0.5;
            PrevWeight = 0;
        }

        public Synapse(Neuron fromNeuron, Neuron toNeuron, double weight)
        {
            _fromNeuron = fromNeuron;
            _toNeuron = toNeuron;

            Weight = weight;
            PrevWeight = 0;

        }

        public Synapse(Neuron toNeuron, double output)
        {
            _toNeuron = toNeuron;
            OutputVal = output;
            Weight = 1;
        }

        public double GetOutput()
        {
            if (From == null)
                return OutputVal;

            return From.OutputVal * Weight;
        }

        public void UpdateWeight(double delta)
        {
            PrevWeight = Weight;
            Weight += delta;
        }
    }
}
