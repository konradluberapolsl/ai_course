using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace softSets
{
    class Vegetables
    {
        private Dictionary<string, int> count;
        public enum Parameters
        {
            fresh, frozen, 
            spicy, sweet, 
            green, red, 
            local, tropical, 
            leafy, tuberous
        }
        private readonly Dictionary<string, List<Parameters>> types = new Dictionary<string, List<Parameters>>()
        {
            { "Cucumber", new List<Parameters>() { Parameters.fresh, Parameters.green, Parameters.local} },
            { "Hot pepper", new List<Parameters>() { Parameters.fresh, Parameters.spicy, Parameters.red} },
            { "Spinach", new List<Parameters>() { Parameters.frozen, Parameters.green, Parameters.leafy} }
        };

        public Vegetables(params Parameters[] parameters)
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
