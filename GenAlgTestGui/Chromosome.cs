using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenAlgTest
{
    /// <summary>
    /// Хромосома - представляет собой экземпляр решения
    /// </summary>
    public class Chromosome
    {
        /// <summary>
        /// Гены хромосомы
        /// </summary>
        public double[] Gens { get; set; }
        /// <summary>
        /// Возраст хромосомы
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Создание хромосомы с заданным размером массива генов
        /// </summary>
        /// <param name="size">Длинна массива генов</param>
        public Chromosome(int size)
        {
            Gens = new double[size];
        }

        /// <summary>
        /// Создание зромосомы с заданными значениями генов
        /// </summary>
        /// <param name="gens">Гены</param>
        public Chromosome(params double[] gens)
        {
            Gens = gens.ToArray();
        }

        public override string ToString()
        {
            return '{' + string.Join(";", Gens.Select(g => string.Format("{0,0:N1}", g))) + '}';
        }
    }
}
