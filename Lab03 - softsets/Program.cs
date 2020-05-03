using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace softSets
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--------------\nTrousers\n--------------");
            var tA = new Trousers(Trousers.Parameters.jeans, Trousers.Parameters.modern, Trousers.Parameters.withZipper);
            var tB = new Trousers(Trousers.Parameters.jeans, Trousers.Parameters.classic, Trousers.Parameters.navy, Trousers.Parameters.withButton);
            Console.WriteLine("A: \n"+tA.ToString());
            Console.WriteLine("B: \n"+tB.ToString());

            Console.WriteLine("--------------\nVegetables\n--------------");
            var vA = new Vegetables(Vegetables.Parameters.fresh, Vegetables.Parameters.spicy, Vegetables.Parameters.red);
            var vB = new Vegetables(Vegetables.Parameters.frozen,Vegetables.Parameters.green,Vegetables.Parameters.sweet,Vegetables.Parameters.leafy);
            var vC = new Vegetables(Vegetables.Parameters.fresh, Vegetables.Parameters.green, Vegetables.Parameters.red, Vegetables.Parameters.sweet);
            Console.WriteLine("A: \n" + vA.ToString());
            Console.WriteLine("B: \n" + vB.ToString());
            Console.WriteLine("C: \n" + vC.ToString());

            Console.ReadKey();
        }
    }
}
