using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenAlgTest
{
    interface IPopulation
    {
        IChromosome Mutate(IChromosome chromosome);
        IChromosome Crossover(IChromosome a, IChromosome b);
        void NextGeneration();
    }

    /*public class PointPopulation : List<PointChromosome>//, IPopulation
    {
        Random rnd = new Random();
        public Point[] points = new Point[3];
        int len;
        public int age = 0;
        public bool _debug = false;

        public event EventHandler<LogEvent> Event;

        public void Log(string str)
        {
            Event?.Invoke(this, new LogEvent(str));
            if (_debug)
                Console.WriteLine(str);
        }

        public PointPopulation(int n, bool _debug = false)
        {
            len = n;
            this._debug = _debug;

            Log($"Создание популяции из {n} хромосом: ");

            for (int i = 0; i < n; i++)
            {
                var chr = new PointChromosome(rnd.NextDouble() * 10, rnd.NextDouble() * 10);
                Add(chr);
                Log($"- ({chr})");
            }

            Log($"Добавление точек, куда должны прийти эти хуйни: ");

            for (int i = 0; i < 3; i++)
            {
                points[i].x = rnd.Next(1, 9);
                points[i].y = rnd.Next(1, 9);
                Log($"- ({points[i].x}, {points[i].y})");
            }
        }

        public PointChromosome Mutate(PointChromosome chromosome)
        {
            var chr = new PointChromosome(0, 0);
            chr.Gens[0] = Math.Max(0, Math.Min(10, chromosome.Gens[0] + rnd.NextDouble() - 0.5));
            chr.Gens[1] = Math.Max(0, Math.Min(10, chromosome.Gens[1] + rnd.NextDouble() - 0.5));

            Log($"{chromosome} мутировал в {chr}");

            return chr;
        }

        public PointChromosome Crossover(PointChromosome a, PointChromosome b)
        {
            if (a.Gens[0] == b.Gens[0] && a.Gens[1] == b.Gens[1])
            {
                Log($"Особь {a} попыталась трахнуть сама себя");
                return null;
            }
            var chr = new PointChromosome(0, 0);
            var p = rnd.NextDouble();
            chr.Gens[0] = p * a.Gens[0] + (1 - p) * b.Gens[0];
            chr.Gens[1] = p * a.Gens[1] + (1 - p) * b.Gens[1];

            Log($"{a} и {b} поебались и родили {chr}");

            return chr;
        }

        public double Fitness(PointChromosome chromosome)
        {
            var fit = double.PositiveInfinity;

            for (int i = 0; i < 3; i++)
            {
                var temp = Math.Sqrt(Math.Pow(points[i].x - chromosome.Gens[0], 2) + Math.Pow(points[i].y - chromosome.Gens[1], 2));

                if (fit > temp)
                    fit = temp;
            }
            
            return 10 * Math.Sqrt(2) - fit;
        }

        public void NextGeneration()
        {
            Log("Создание следующего поколения:");

            // Add mutated
            var prevGen = this.ToArray();
            foreach (var chr in prevGen)
            {
                Add(Mutate(chr));
            }

            // Add crossovers

            for (int i = 0; i < prevGen.Length; i++)
            {
                for (int j = 0; j < prevGen.Length; j++)
                {
                    if (i != j)
                    {
                        var child = Crossover(prevGen[i], prevGen[j]);
                        if (child != null)
                            Add(child);
                    }
                }
            }

            // Sort by Fitness
            Sort();

            for (int i = 0; i < len; i++)
            {
                Log($"- ({this[i]}), приспособленность = {Fitness(this[i])}");
            }
            Log("Не приспособившиеся: ");
            for (int i = len; i < this.Count; i++)
            {
                Log($"- ({this[i]}), приспособленность = {Fitness(this[i])}");
            }

            // Remove fitless
            this.RemoveRange(len, this.Count - len);

            age++;

            Log("Возраст популяции: " + age);
        }

        public new void Sort()
        {
            // Тут быдлокод, сюда не смотреть!
            var sorted = this.OrderByDescending(c => Fitness(c)).ToArray();
            this.Clear();
            this.AddRange(sorted);
        }
    }

    public class FuncAproxPopulationFixed : List<FuncAproxChromosomeFixed>//, IPopulation
    {
        Random rnd = new Random();
        public int len = 5;
        public int age = 0;
        Point[] points;

        

        public FuncAproxPopulationFixed(Point[] points)
        {
            this.points = points;
            for (int i = 0; i < len; i++)
            {
                var a = rnd.NextDouble() * 10 - 5;
                var b = rnd.NextDouble() * 10 - 5;
                var c = rnd.NextDouble() * 10 - 5;
                var d = rnd.NextDouble() * 10 - 5;
                var e = rnd.NextDouble() * 10 - 5;
                Add(new FuncAproxChromosomeFixed(a, b, c, d, e));
            }
        }

        public FuncAproxChromosomeFixed Crossover(FuncAproxChromosomeFixed a, FuncAproxChromosomeFixed b)
        {
            var kA = rnd.NextDouble();
            var kB = rnd.NextDouble();
            var kC = rnd.NextDouble();
            var kD = rnd.NextDouble();
            var kE = rnd.NextDouble();

            var A = kA * a.Gens[0] + (1 - kA) * b.Gens[0];
            var B = kB * a.Gens[1] + (1 - kB) * b.Gens[1];
            var C = kC * a.Gens[2] + (1 - kC) * b.Gens[2];
            var D = kD * a.Gens[3] + (1 - kD) * b.Gens[3];
            var E = kE * a.Gens[4] + (1 - kE) * b.Gens[4];

            return new FuncAproxChromosomeFixed(A,B,C,D,E);
        }

        public FuncAproxChromosomeFixed Mutate(FuncAproxChromosomeFixed chromosome)
        {
            var n = new FuncAproxChromosomeFixed(0, 0, 0, 0, 0);
            for (int i = 0; i < 5; i++)
            {
                n.Gens[i] = chromosome.Gens[i] + rnd.NextDouble() * 5 - 2.5;
            }
            return n;
        }

        public void NextGeneration()
        {
            // Add mutated
            var prevGen = this.ToArray();
            
            foreach (var chr in prevGen)
            {
                Add(Mutate(chr));
            }

            // Add 1 randomed

            var a = rnd.NextDouble() * 10 - 5;
            var b = rnd.NextDouble() * 10 - 5;
            var c = rnd.NextDouble() * 10 - 5;
            var d = rnd.NextDouble() * 10 - 5;
            var e = rnd.NextDouble() * 10 - 5;
            Add(new FuncAproxChromosomeFixed(a, b, c, d, e));

            // Add crossovers

            for (int i = 0; i < prevGen.Length; i++)
            {
                for (int j = 0; j < prevGen.Length; j++)
                {
                    if (i != j)
                    {
                        var child = Crossover(prevGen[i], prevGen[j]);
                        if (child != null)
                            Add(child);
                    }
                }
            }

            //var first = this.First();

            // Sort by Fitness
            Sort();

            /*if (first == this.First())
                genDontMove++;
            else
                genDontMove = 0;

            if (genDontMove > 500)
                for (int i = 0; i < len * len-2; i++)
                {
                    var A = rnd.NextDouble() * 10 - 5;
                    var B = rnd.NextDouble() * 10 - 5;
                    var C = rnd.NextDouble() * 10 - 5;
                    var D = rnd.NextDouble() * 10 - 5;
                    var E = rnd.NextDouble() * 10 - 5;
                    Add(new FuncAproxChromosome(A, B, C, D, E));
                    this.RemoveAt(0);
                    genDontMove = 0;
                }
                
            // Remove fitless
            this.RemoveRange(len, this.Count - len);

            age++;
        }

        public new void Sort()
        {
            var sorted = this.OrderByDescending(c => Fitness(c)).ToArray();
            this.Clear();
            this.AddRange(sorted);
        }

        public double Fitness(FuncAproxChromosomeFixed chromosome)
        {
            var fit = 0;
            for (int i = 0; i < 5; i++)
            {
                var x = points[i].x;
                var y1 = chromosome.Gens[0] * x * x + chromosome.Gens[1] * x + chromosome.Gens[2] * Math.Sin(chromosome.Gens[3] * x) + chromosome.Gens[4];
                var y2 = points[i].y;
                fit -= (int)(100 * Math.Abs(y1 - y2));
            }

            return fit;
        }
    }

    public struct Point
    {
        public double x;
        public double y;
    }*/

    public class FuncAproxPopulation : List<FuncAproxChromosome>
    {
        private Random Rnd;
        public int Lenght { get; private set; }
        private int GensCount;
        public Func<double[], double> Fitness { get; private set; }
        private double MutateRange = 0.2;
        private Tuple<double,double> Range;
        public long Age { get; private set; }
        private int? MaxAgeToBank;
        public List<FuncAproxChromosome> Bank = new List<FuncAproxChromosome>();

        public FuncAproxPopulation(int gensCount, Func<double[],double> fitnessFunction, int count = 5, double mutateRange = 0.2, int? maxAgeToBank = 30000, Tuple<double, double> range = null)
        {
            Rnd = new Random();
            GensCount = gensCount;
            Fitness = fitnessFunction;
            Lenght = count;
            MutateRange = mutateRange;
            MaxAgeToBank = maxAgeToBank;
            Range = range ?? new Tuple<double, double>(0,1);
            CreateRandomChromosomes(Lenght);
        }

        private void CreateRandomChromosomes(int count)
        {
            for (int i = 0; i < count; i++)
            {
                double[] gens = new double[GensCount];
                for (int j = 0; j < gens.Length; j++)
                    gens[j] = Rnd.NextDouble() * (Range.Item2 - Range.Item1) + Range.Item1;
                Add(new FuncAproxChromosome(gens));
            }
        }

        public new void Sort()
        {
            var sorted = this.OrderByDescending(c => Fitness(c.Gens)).ToArray();
            this.Clear();
            this.AddRange(sorted);
        }

        private FuncAproxChromosome Mutate(FuncAproxChromosome a)
        {
            var n = new FuncAproxChromosome(GensCount);
            for (int i = 0; i < GensCount; i++)
                n.Gens[i] = a.Gens[i] + Rnd.NextDouble() * MutateRange * 2 - MutateRange;
            return n;
        }

        private FuncAproxChromosome CrossOver(FuncAproxChromosome a, FuncAproxChromosome b)
        {
            var percent = new double[GensCount];
            for (int i = 0; i < GensCount; i++)
                percent[i] = Rnd.NextDouble();

            var n = new FuncAproxChromosome(GensCount);
            for (int i = 0; i < GensCount; i++)
                n.Gens[i] = percent[i] * a.Gens[i] + (1 - percent[i]) * b.Gens[i];
            return n;
        }

        public void NextGeneration()
        {
            // Add mutated
            var prevGen = this.ToArray();

            foreach (var chr in prevGen)
                Add(Mutate(chr));

            // Add crossovers
            for (int i = 0; i < prevGen.Length; i++)
            {
                for (int j = 0; j < prevGen.Length; j++)
                {
                    if (i != j)
                    {
                        var child = CrossOver(prevGen[i], prevGen[j]);
                        if (child != null)
                            Add(child);
                    }
                }
            }
            
            
            this.ForEach(c => c.Age++);

            CreateRandomChromosomes(Lenght / 2);

            //this.RemoveAll(c => c.Age > 1);

            if (MaxAgeToBank.HasValue && this.First().Age > MaxAgeToBank.Value)
            {
                Bank.Add(this.First());
                this.RemoveAll(c => true);
                CreateRandomChromosomes(Lenght);
            }

            Sort();

            this.RemoveRange(Lenght, this.Count - Lenght);

            Age++;
        }
    }
}
