using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenAlgTest
{
   /* public abstract class AChromosome
    {
        public double[] Gens;
    }*/
    /*
    public class PointChromosome : AChromosome
    {
        public PointChromosome(double x, double y)
        {
            Gens = new double[2] { x, y };
        }

        public override string ToString()
        {
            return String.Format("({0,0:N2}, {1,0:N2})", Gens[0], Gens[1]);
        }
    }

    public class FuncAproxChromosomeFixed : AChromosome
    {
        public FuncAproxChromosomeFixed(double A, double B, double C, double D, double E)
        {
            Gens = new double[5] { A, B, C, D, E };
        }

        public override string ToString()
        {
            return String.Format("({0,0:N1}x^2 + {1,0:N1}x + {2,0:N1}*sin({3,0:N1}*x) + {4,0:N1}", Gens[0], Gens[1], Gens[2], Gens[3], Gens[4]);
        }
    }
    */

    public class FuncAproxChromosome// : AChromosome
    {
        public double[] Gens { get; set; }
        public int Age { get; set; }

        public FuncAproxChromosome(int size)
        {
            Gens = new double[size];
        }

        public FuncAproxChromosome(params double[] gens)
        {
            Gens = gens.ToArray();
        }

        public override string ToString()
        {
            return '{' + string.Join(";", Gens.Select(g => string.Format("{0,0:N1}", g))) + '}';
        }
    }
}
