using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace softSets
{
    class Trousers
    {
        Dictionary<string, int> count;
        public enum Parameters
        {
            expensive, cheap,
            jeans, sweatpants,
            classic, modern, fit,
            navy, black,
            withButton, withZipper
        }
        private readonly Dictionary<string, List<Parameters>> types = new Dictionary<string, List<Parameters>>()
        {
            { "Oldschool", new List<Parameters>() { Parameters.cheap, Parameters.jeans, Parameters.classic, Parameters.withButton} },
            { "Newschool", new List<Parameters>() { Parameters.expensive, Parameters.sweatpants, Parameters.modern} },
            { "Student trousers", new List<Parameters>() { Parameters.jeans,Parameters.navy, Parameters.withButton} },
            { "Trousers for beauty girl", new List<Parameters>() { Parameters.jeans, Parameters.black, Parameters.fit, Parameters.withZipper } }
        };
        public Trousers(params Parameters[] parameters)
        {
            count = InitializeScores();

            foreach (var param in parameters)
            {
                foreach (var item in types)
                {
                    if (item.Value.Contains(param))
                    {
                        count[item.Key]++;
                    }
                }
            }
        }
        private Dictionary<string, int> InitializeScores()
        {
            var scores = new Dictionary<string, int>();
            foreach (var item in types)
            {
                scores.Add(item.Key, 0);
            }

            return scores;
        }
        private Dictionary<string, int> PickBest()
        {
            int bestScore = 0;
            var best = new Dictionary<string, int>();
            foreach (var item in count)
            {
                if (item.Value > bestScore)
                {
                    bestScore = item.Value;
                    best.Clear();
                    best.Add(item.Key, item.Value);
                }
                else if (item.Value == bestScore)
                {
                    best.Add(item.Key, item.Value);
                }
            }

            return best;
        }
        public override string ToString()
        {
            string val = "";
            var best = PickBest();
            foreach (var item in best)
            {
                val += $"{item.Key} ({item.Value})\n";
            }

            return val;
        }
    }
}
