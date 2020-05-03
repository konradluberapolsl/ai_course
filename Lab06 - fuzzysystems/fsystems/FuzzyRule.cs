using System;
using System.Collections.Generic;

namespace fuzzySystems
{
    class FuzzyRule
    {
        double a, b, c;
        double? d;

        private List<double> elements;

        public FuzzyRule(double a, double b, double c) : this(a, b, c, null)
        {

        }

        public FuzzyRule(double a, double b, double c, double? d)
        {
            this.a = a;
            this.b = b;
            this.c = c;
            this.d = d;
            elements = new List<double>();
        }

        double TriangleRule(double x)
        {
            if ((a < x) && (x <= b)) return (x - a) / (b - a);
            if ((b < x) && (x < c)) return (c - x) / (c - b);
            return 0;
        }

        double TrapezeRule(double x)
        {
            double dd = (double)d;
            if (x <= a) return 0;
            if (x <= b) return (x - a) / (b - a);
            if (x < c) return 1;
            if (x <= dd) return (dd - x) / (dd - c);
            return 0;
        }

        public void AddElement(double x)
        {
            if (d == null)
            {
                elements.Add(TriangleRule(x));
            }
            else
            {
                elements.Add(TrapezeRule(x));
            }
        }

        public double GeElement(int index)
        {
            if (index < elements.Count && index >= 0)
            {
                return elements[index];
            }
            else throw new IndexOutOfRangeException("Of of range");
        }
    }
}
