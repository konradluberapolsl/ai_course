using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{

    class Neuron
    {
        public List<Synapse> Inputs { get; set; }
        public List<Synapse> Outputs { get; set; }
        public double InputVal { get; set; }
        public double OutputVal { get; set; }

        public Neuron()
        {
            Inputs = new List<Synapse>();
            Outputs = new List<Synapse>();
        }

        public void AddOutputNeuron(Neuron neuron)
        {
            Synapse synapse = new Synapse(this, neuron);
            Outputs.Add(synapse);
            neuron.Inputs.Add(synapse);
        }

        public void AddInputSynapse(double input)
        {
            Synapse synapse = new Synapse(this, input);
            Inputs.Add(synapse);
        }

        public void Calculate()
        {
            InputVal = Functions.SumInput(Inputs);
            OutputVal = Functions.BipolarLinearFunction(InputVal);
        }

        public void PushValueOnInput(double input)
        {
            Inputs[0].OutputVal = input;
        }

    }
}
